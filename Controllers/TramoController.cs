using AutoMapper;
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
using APICarreteras.Controller;

namespace APICarreteras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TramoController : ControllerBase
    {
        private readonly ILogger<TramoController> _logger;
        private readonly ICarreteraRepositorio _carreteraRepo;
        private readonly ITramoRepositorio _tramoRepositorio;
        private readonly IMapper _mapper;
        protected Response _response;
        public TramoController(ILogger<TramoController> logger, ICarreteraRepositorio carreteraRepo, ITramoRepositorio tramoRepositorio, IMapper mapper)
        {
            _logger = logger;
            _carreteraRepo = carreteraRepo;
            _tramoRepositorio = tramoRepositorio;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> GetTramos()
        {
            try
            {
                _logger.LogInformation("Obtener los tramos");
                IEnumerable<Tramo> tramoList = await _tramoRepositorio.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<TramoDto>>(tramoList);
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


        [HttpGet("{id:int}", Name = "GetTramo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> GetTramo(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Tramo con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                var tramo = await _tramoRepositorio.Obtener(c => c.IdTramo == id);
                if (tramo == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<TramoDto>(tramo);
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
        public async Task<ActionResult<Response>> CrearTramo([FromBody] TramoCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var existingTramo = await _tramoRepositorio.Obtener(v => v.Nombre.ToLower() == createDto.Nombre.ToLower());
                if (existingTramo != null)
                {
                    ModelState.AddModelError("NombreExiste", "El tramo con ese nombre ya existe.");
                    return BadRequest(ModelState);
                }

                if (await _carreteraRepo.Obtener(v => v.IdCarretera == createDto.IdCarretera) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de Carretera no existe");
                    return BadRequest(ModelState);
                }

                
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                Tramo modelo = _mapper.Map<Tramo>(createDto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;


                await _tramoRepositorio.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetTramo", new { id = modelo.IdTramo }, _response);
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

        public async Task<IActionResult> DeleteTramo(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var tramo = await _tramoRepositorio.Obtener(v => v.IdTramo == id);
                if (tramo == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _tramoRepositorio.Remover(tramo);
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
        public async Task<IActionResult> UpdateTramo(int id, [FromBody] TramoUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IdTramo)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            
            if (await _carreteraRepo.Obtener(v => v.IdCarretera == updateDto.IdCarretera) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El id de la carretera no existe");
                return BadRequest(ModelState);
            }

            Tramo modelo = _mapper.Map<Tramo>(updateDto);

            await _tramoRepositorio.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }



    }
}
