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
    public class CostoReparacionController : ControllerBase
    {

        private readonly ILogger<CostoReparacionController> _logger;
        private readonly ICostoReparacionRepositorio _costoreparacionRepo;
        private readonly IMapper _mapper;
        private readonly IDanosRepositorio _danoRepo;
        protected Response _response;

        public CostoReparacionController(ILogger<CostoReparacionController> logger, ICostoReparacionRepositorio costoreparacionRepo, IDanosRepositorio danoRepositorio, IMapper mapper)
        {
            _logger = logger;
            _costoreparacionRepo = costoreparacionRepo;
            _danoRepo = danoRepositorio;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> GetCostoReparaciom()
        {
            try
            {
                _logger.LogInformation("Obtener los costos de reparaciom");
                IEnumerable<CostoReparacion> costoreparacionList = await _costoreparacionRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<CostoReparacionDto>>(costoreparacionList);
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

        [HttpGet("{id:int}", Name = "GetCostoReparacion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> GetCostoReparacion(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer costo de reparacion con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                var costoreparacion = await _costoreparacionRepo.Obtener(c => c.IdCostoReparacion == id);
                if (costoreparacion == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<CostoReparacionDto>(costoreparacion);
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
        public async Task<ActionResult<Response>> CrearCostoReparacion([FromBody] CostoReparacionCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                if (await _danoRepo.Obtener(v => v.IdDanos == createDto.IdDanos) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de daño no existe");
                    return BadRequest(ModelState);
                }


                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                CostoReparacion modelo = _mapper.Map<CostoReparacion>(createDto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;


                await _costoreparacionRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetCostoReparacion", new { id = modelo.IdCostoReparacion }, _response);
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

        public async Task<IActionResult> DeleteCostoReparacion(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var costoreparacion = await _costoreparacionRepo.Obtener(v => v.IdCostoReparacion == id);
                if (costoreparacion == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _costoreparacionRepo.Remover(costoreparacion);
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
        public async Task<IActionResult> UpdateCostoReparacion(int id, [FromBody] CostoReparacionUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IdCostoReparacion)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }


            if (await _danoRepo.Obtener(v => v.IdDanos == updateDto.IdDanos) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id de daño no existe");
                return BadRequest(ModelState);
            }


            CostoReparacion modelo = _mapper.Map<CostoReparacion>(updateDto);

            await _costoreparacionRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }



    }
}
