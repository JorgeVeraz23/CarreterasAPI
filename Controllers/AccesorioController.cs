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

namespace APICarreteras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesorioController : ControllerBase
    {
        private readonly ILogger<AccesorioController> _logger;
        private readonly IAccesorioRepositorio _accesorioRepo;
        private readonly IMapper _mapper;
        private readonly ITramoRepositorio _tramoRepo;
        protected Response _response;
        public AccesorioController(ILogger<AccesorioController> logger, IAccesorioRepositorio accesorioRepo, ITramoRepositorio tramoRepo, IMapper mapper)
        {
            _logger = logger;
            _accesorioRepo = accesorioRepo;
            _tramoRepo = tramoRepo;
            _mapper = mapper;
            _response = new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> GetAccesorio()
        {
            try
            {
                _logger.LogInformation("Obtener los accesorios");
                IEnumerable<Accesorio> accesorioList = await _accesorioRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<AccesorioDto>>(accesorioList);
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


        [HttpGet("{id:int}", Name = "GetAccesorio")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> GetAccesorio(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Accesorio con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                var accesorio = await _accesorioRepo.Obtener(c => c.IdAccesorios == id);
                if (accesorio == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<AccesorioDto>(accesorio);
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
        public async Task<ActionResult<Response>> CrearAccesorio([FromBody] AccesorioCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                

                if (await _tramoRepo.Obtener(v => v.IdTramo == createDto.IdTramo) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de Tramo no existe");
                    return BadRequest(ModelState);
                }


                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                Accesorio modelo = _mapper.Map<Accesorio>(createDto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;


                await _accesorioRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetAccesorio", new { id = modelo.IdAccesorios }, _response);
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

        public async Task<IActionResult> DeleteAccesorio(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var accesorio = await _accesorioRepo.Obtener(v => v.IdAccesorios == id);
                if (accesorio == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _accesorioRepo.Remover(accesorio);
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
        public async Task<IActionResult> UpdateAccesorio(int id, [FromBody] AccesorioUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IdAccesorios)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

           
            if (await _tramoRepo.Obtener(v => v.IdTramo == updateDto.IdTramo) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id de Tramo no existe");
                return BadRequest(ModelState);
            }


            Accesorio modelo = _mapper.Map<Accesorio>(updateDto);

            await _accesorioRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }




    }
}
