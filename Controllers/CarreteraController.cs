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
    public class CarreteraController : ControllerBase
    {
        private readonly ILogger<CarreteraController> _logger;
        private readonly ICarreteraRepositorio _carreteraRepo;
        private readonly IMapper _mapper;
        private readonly ICantonRepositorio _cantonRepositorio;
        private readonly ITipoDeViaRepositorio _tipodeviaRepositorio;
        protected Response _response;
        public CarreteraController(ILogger<CarreteraController> logger, ICarreteraRepositorio carreteraRepo,ITipoDeViaRepositorio tipoDeViaRepositorio, ICantonRepositorio cantonRepositorio, IMapper mapper)
        {
            _logger = logger;
            _carreteraRepo = carreteraRepo;
            _tipodeviaRepositorio = tipoDeViaRepositorio;
            _cantonRepositorio = cantonRepositorio;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> GetCarreteras()
        {
            try
            {
                _logger.LogInformation("Obtener las carreteras");
                IEnumerable<Carretera> carreteraList = await _carreteraRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<CarreteraDto>>(carreteraList);
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

        [HttpGet("{id:int}", Name = "GetCarretera")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> GetCarretera(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Carretera con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                var carretera = await _carreteraRepo.Obtener(c => c.IdCarretera == id);
                if (carretera == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<CarreteraDto>(carretera);
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
        public async Task<ActionResult<Response>> CrearCarretera([FromBody] CarreteraCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _carreteraRepo.Obtener(v => v.IdCarretera == createDto.IdCarretera) != null)
                {
                    ModelState.AddModelError("NombreExiste", "El numero de Villa con ese Nombre ya existe!");
                    return BadRequest(ModelState);
                }

                if (await _cantonRepositorio.Obtener(v => v.IdCanton == createDto.IdCanton) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de Canton no existe");
                    return BadRequest(ModelState);
                }

                if (await _tipodeviaRepositorio.Obtener(v => v.IdTipoVia == createDto.IdTipoVia) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de Tipo de via no existe");
                    return BadRequest(ModelState);
                }
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                Carretera modelo = _mapper.Map<Carretera>(createDto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;


                await _carreteraRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetCarretera", new { id = modelo.IdTipoVia }, _response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };


            }
            return _response;
        }



    }
}
