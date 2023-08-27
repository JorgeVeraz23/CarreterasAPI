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
    public class CalendarioDeActuacioneController : ControllerBase
    {
        private readonly ILogger<CalendarioDeActuacioneController> _logger;
        private readonly ICalendarioDeActuacionesRepositorio _calendariodeactuacioneRepo;
        private readonly IMapper _mapper;
        private readonly ITramoRepositorio _tramoRepo;
        private readonly ICostoReparacionRepositorio _costoreparacionRepo;
        protected Response _response;

        public CalendarioDeActuacioneController(ILogger<CalendarioDeActuacioneController> logger, ICalendarioDeActuacionesRepositorio calendariodeactuacioneRepo, ITramoRepositorio tramoRepo, ICostoReparacionRepositorio costoreparacionRepo,IMapper mapper)
        {
            _logger = logger;
            _calendariodeactuacioneRepo = calendariodeactuacioneRepo;
            _tramoRepo = tramoRepo;
            _costoreparacionRepo = costoreparacionRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> GetCalendarioDeActuaciones()
        {
            try
            {
                _logger.LogInformation("Obtener los calendarios de actuaciones");
                IEnumerable<CalendarioDeActuacione> calendariodeactuacioneList = await _calendariodeactuacioneRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<CalendarioDeActuacionesDto>>(calendariodeactuacioneList);
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

        [HttpGet("{id:int}", Name = "GetCalendarioDeActuaciones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> GetCalendarioDeActuaciones(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer el calendario de actuaciones con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                var calendariodeactuacione = await _calendariodeactuacioneRepo.Obtener(c => c.IdCalendario == id);
                if (calendariodeactuacione == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<CalendarioDeActuacionesDto>(calendariodeactuacione);
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
        public async Task<ActionResult<Response>> CrearCalendarioDeActuaciones([FromBody] CalendarioDeActuacionesCreateDto createDto)
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

                if (await _costoreparacionRepo.Obtener(v => v.IdCostoReparacion == createDto.IdCostoReparacion) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de costo de reparacion no existe");
                    return BadRequest(ModelState);
                }


                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                CalendarioDeActuacione modelo = _mapper.Map<CalendarioDeActuacione>(createDto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;


                await _calendariodeactuacioneRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetCalendarioDeActuaciones", new { id = modelo.IdCalendario }, _response);
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
        public async Task<IActionResult> DeleteCalendarioDeActuacione(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var calendariodeactuacione = await _calendariodeactuacioneRepo.Obtener(v => v.IdCalendario == id);
                if (calendariodeactuacione == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _calendariodeactuacioneRepo.Remover(calendariodeactuacione);
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
        public async Task<IActionResult> UpdateCalendarioDeActuacione(int id, [FromBody] CalendarioDeActuacionesUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IdCalendario)
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

            if (await _costoreparacionRepo.Obtener(v => v.IdCostoReparacion == updateDto.IdCostoReparacion) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id de costo de reparacion no existe");
                return BadRequest(ModelState);
            }


            CalendarioDeActuacione modelo = _mapper.Map<CalendarioDeActuacione>(updateDto);

            await _calendariodeactuacioneRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }


    }


}
