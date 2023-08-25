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
    public class CamaraDeSeguridadController : ControllerBase
    {
        private readonly ILogger<CamaraDeSeguridadController> _logger;
        private readonly ICamaraDeSeguridadRepositorio _camaradeseguridadRepo;
        private readonly IMapper _mapper;
        private readonly ITramoRepositorio _tramoRepo;
        protected Response _response;

        public CamaraDeSeguridadController(ILogger<CamaraDeSeguridadController> logger, ICamaraDeSeguridadRepositorio camaradeseguridadRepo, ITramoRepositorio tramoRepo, IMapper mapper)
        {
            _logger = logger;
            _camaradeseguridadRepo = camaradeseguridadRepo;
            _tramoRepo = tramoRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> GetCamaraDeSeguridad()
        {
            try
            {
                _logger.LogInformation("Obtener las camaras de seguridad");
                IEnumerable<CamarasDeSeguridad> camaradeseguridadList = await _camaradeseguridadRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<CamarasDeSeguridadDto>>(camaradeseguridadList);
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

        [HttpGet("{id:int}", Name = "GetCamarasDeSeguridad")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> GetCamaraDeSeguridad(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer las camaras de seguridad con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                var camaradeseguridad = await _camaradeseguridadRepo.Obtener(c => c.IdCamara == id);
                if (camaradeseguridad == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<CamarasDeSeguridadDto>(camaradeseguridad);
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
        public async Task<ActionResult<Response>> CrearCamaraDeSeguridad([FromBody] CamarasDeSeguridadCreateDto createDto)
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
                CamarasDeSeguridad modelo = _mapper.Map<CamarasDeSeguridad>(createDto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;


                await _camaradeseguridadRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetCamarasDeSeguridad", new { id = modelo.IdCamara }, _response);
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

        public async Task<IActionResult> DeleteCamaraDeSeguridad(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var camaradeseguridad = await _camaradeseguridadRepo.Obtener(v => v.IdCamara == id);
                if (camaradeseguridad == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _camaradeseguridadRepo.Remover(camaradeseguridad);
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
        public async Task<IActionResult> UpdateCamaraDeSeguridad(int id, [FromBody] CamarasDeSeguridadUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IdCamara)
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


            CamarasDeSeguridad modelo = _mapper.Map<CamarasDeSeguridad>(updateDto);

            await _camaradeseguridadRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }

    }


}
