using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APICarreteras.Models;
using APICarreteras.Models;
using APICarreteras.Models;
using APICarreteras.Repository;
using APICarreteras.Repository.IRepositorio;
using System.Net;
using APICarreteras.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace APICarreteras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuenteController : ControllerBase
    {
        private readonly ILogger<PuenteController> _logger;
        private readonly IPuenteRepositorio _puenteRepo;
        private readonly IMapper _mapper;
        private readonly ITramoRepositorio _tramoRepositorio;
        protected Response _response;
        public PuenteController(ILogger<PuenteController> logger, IPuenteRepositorio puenteRepo, ITramoRepositorio tramoRepositorio, IMapper mapper)
        {
            _logger = logger;
            _puenteRepo = puenteRepo;
            _tramoRepositorio = tramoRepositorio;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> GetPuentes()
        {
            try
            {
                _logger.LogInformation("Obtener los puentes");
                IEnumerable<Puente> puenteList = await _puenteRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<PuenteDto>>(puenteList);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }

        [HttpGet("{id:int}", Name = "GetPuente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> GetPuente(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Puente con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                var puente = await _puenteRepo.Obtener(c => c.IdPuente == id);
                if (puente == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<PuenteDto>(puente);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response>> CrearPuente([FromBody] PuenteCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var existingPuente = await _puenteRepo.Obtener(v => v.Nombre.ToLower() == createDto.Nombre.ToLower());
                if (existingPuente != null)
                {
                    ModelState.AddModelError("NombreExiste", "El puente con ese nombre ya existe.");
                    return BadRequest(ModelState);
                }

                if (await _tramoRepositorio.Obtener(v => v.IdTramo == createDto.IdTramo) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de Tramo no existe");
                    return BadRequest(ModelState);
                }

                
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                Puente modelo = _mapper.Map<Puente>(createDto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;


                await _puenteRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetPuente", new { id = modelo.IdPuente }, _response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };


            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeletePuente(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var puente = await _puenteRepo.Obtener(v => v.IdPuente == id);
                if (puente == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _puenteRepo.Remover(puente);
                _response.statusCode = HttpStatusCode.NoContent;
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

                throw;
            }
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePuente(int id, [FromBody] PuenteUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IdPuente)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

           
            if (await _tramoRepositorio.Obtener(v => v.IdTramo == updateDto.IdTramo) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id de Tramo no existe");
                return BadRequest(ModelState);
            }
           

            Puente modelo = _mapper.Map<Puente>(updateDto);

            await _puenteRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }

    }
}
