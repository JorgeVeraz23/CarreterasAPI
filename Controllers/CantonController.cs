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

namespace APICarreteras.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CantonController : ControllerBase
    {
        private readonly ILogger<CantonController> _logger;
        private readonly ICantonRepositorio _cantonRepo;
        private readonly IMapper _mapper;
        protected Response _response;
        public CantonController(ILogger<CantonController> logger, ICantonRepositorio cantonRepo, IMapper mapper)
        {
            _logger = logger;
            _cantonRepo = cantonRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> GetCantones()
        {
            try
            {
                _logger.LogInformation("Obtener los Cantones");
                IEnumerable<Canton> cantonList = await _cantonRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<CantonDto>>(cantonList);
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

        [HttpGet("{id:int}", Name = "GetCanton")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetCanton(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Villa con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                var canton = await _cantonRepo.Obtener(c => c.IdCanton == id);
                if (canton == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<CantonDto>(canton);
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
        public async Task<ActionResult<APIResponse>> CrearCanton([FromBody] CantonCreateDto createDto)
        {
            try
            {
                if (createDto == null)
                {
                    return BadRequest("El objeto createDto es nulo.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingCanton = await _cantonRepo.Obtener(v => v.Nombre.ToLower() == createDto.Nombre.ToLower());
                if (existingCanton != null)
                {
                    ModelState.AddModelError("NombreExiste", "El cantón con ese nombre ya existe.");
                    return BadRequest(ModelState);
                }

                Canton modelo = _mapper.Map<Canton>(createDto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;

                await _cantonRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetCanton", new { id = modelo.IdCanton }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }





        }

    }
}