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
    public class CarreteraDetalleController : ControllerBase
    {
        private readonly ILogger<CarreteraDetalleController> _logger;
        private readonly ICarreteraDetalleRepositorio _carreteradetalleRepo;
        private readonly IMapper _mapper;
        private readonly ICarreteraRepositorio _carreteraRepo;
        private readonly ITramoRepositorio _tramoRepo;
        private readonly ITipoRodaduraRepositorio _tiporodaduraRepo;
        protected Response _response;

        public CarreteraDetalleController(ILogger<CarreteraDetalleController> logger, ICarreteraDetalleRepositorio carreteraDetalleRepo,ICarreteraRepositorio carreteraRepo, ITramoRepositorio tramoRepo, ITipoRodaduraRepositorio tiporodaduraRepo, IMapper mapper)
        {
            _logger = logger;
            _carreteraRepo = carreteraRepo;
            _carreteradetalleRepo = carreteraDetalleRepo;
            _tramoRepo = tramoRepo;
            _mapper = mapper;
            _tiporodaduraRepo = tiporodaduraRepo;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> GetCarreteraDetalle()
        {
            try
            {
                _logger.LogInformation("Obtener los detalles de carretera");
                IEnumerable<CarreteraDetalle> carreteradetalleList = await _carreteradetalleRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<CarreteraDetalleDto>>(carreteradetalleList);
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

        [HttpGet("{id:int}", Name = "GetCarreteraDetalle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> GetCarreteraDetalle(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer detalles de la carretera con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                var carreteradetalle = await _carreteradetalleRepo.Obtener(c => c.IdCarreteraDetalle == id);
                if (carreteradetalle == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<CarreteraDetalleDto>(carreteradetalle);
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
        public async Task<ActionResult<Response>> CrearCarreteraDetalle([FromBody] CarreteraDetalleCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                

                if (await _carreteraRepo.Obtener(v => v.IdCarretera == createDto.IdCarretera) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de Carretera no existe");
                    return BadRequest(ModelState);
                }

                if (await _tramoRepo.Obtener(v => v.IdTramo == createDto.IdTramo) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de tramo no existe");
                    return BadRequest(ModelState);
                }

                if (await _tiporodaduraRepo.Obtener(v => v.IdTipoRodadura == createDto.IdTipoRodadura) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de tipo de rodadura no existe");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                CarreteraDetalle modelo = _mapper.Map<CarreteraDetalle>(createDto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;


                await _carreteradetalleRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetCarreteraDetalle", new { id = modelo.IdTipoRodadura }, _response);
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

        public async Task<IActionResult> DeleteCarreteraDetalle(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var carreteradetalle = await _carreteradetalleRepo.Obtener(v => v.IdCarreteraDetalle == id);
                if (carreteradetalle == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _carreteradetalleRepo.Remover(carreteradetalle);
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
        public async Task<IActionResult> UpdateCarreteraDetalle(int id, [FromBody] CarreteraDetalleUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IdCarreteraDetalle)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }


            if (await _carreteraRepo.Obtener(v => v.IdCarretera == updateDto.IdCarretera) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id de Carretera no existe");
                return BadRequest(ModelState);
            }

            if (await _tramoRepo.Obtener(v => v.IdTramo == updateDto.IdTramo) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id de tramo no existe");
                return BadRequest(ModelState);
            }

            if (await _tiporodaduraRepo.Obtener(v => v.IdTipoRodadura == updateDto.IdTipoRodadura) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id de tipo de rodadura no existe");
                return BadRequest(ModelState);
            }



            CarreteraDetalle modelo = _mapper.Map<CarreteraDetalle>(updateDto);

            await _carreteradetalleRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }




    }
}
