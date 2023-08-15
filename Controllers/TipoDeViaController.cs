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
    public class TipoDeViaController : ControllerBase
    {
        private readonly ILogger<TipoDeViaController> _logger;
        private readonly ITipoDeViaRepositorio _tipodeviaRepo;
        private readonly IMapper _mapper;
        protected Response _response;
        public TipoDeViaController(ILogger<TipoDeViaController> logger, ITipoDeViaRepositorio tipodeviaRepo, IMapper mapper)
        {
            _logger = logger;
            _tipodeviaRepo = tipodeviaRepo;
            _mapper = mapper;
            _response = new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Models.Response>> GetCantones()
        {
            try
            {
                _logger.LogInformation("Obtener los TiposDeVia");
                IEnumerable<TipoDeVium> tipoDeViaList = await _tipodeviaRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<TipoDeVium>>(tipoDeViaList);
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

        [HttpGet("{id:int}", Name = "GetTipoDeVia")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> GetTipoDeVia(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Tipo de via con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                var tipodevia = await _tipodeviaRepo.Obtener(c => c.IdTipoVia == id);
                if (tipodevia == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<TipoDeViaDto>(tipodevia);
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
        public async Task<ActionResult<Response>> CrearTipoDeVia([FromBody] TipoDeViaCreateDto createDto)
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

                TipoDeVium modelo = _mapper.Map<TipoDeVium>(createDto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;

                await _tipodeviaRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetTipoDeVia", new { id = modelo.IdTipoVia }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }


        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeleteTipoDeVia(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var tipodevia = await _tipodeviaRepo.Obtener(v => v.IdTipoVia == id);
                if (tipodevia == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _tipodeviaRepo.Remover(tipodevia);
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
        public async Task<IActionResult> UpdateTipoDeVia(int id, [FromBody] TipoDeViaUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IdTipoVia)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            TipoDeVium modelo = _mapper.Map<TipoDeVium>(updateDto);
            await _tipodeviaRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialTipoDeVia(int id, JsonPatchDocument<TipoDeViaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var tipodevia = await _tipodeviaRepo.Obtener(v => v.IdTipoVia == id, tracked: false);
            if (tipodevia == null) return BadRequest();
            TipoDeViaUpdateDto TipoDeviaDto = _mapper.Map<TipoDeViaUpdateDto>(tipodevia);

            patchDto.ApplyTo(TipoDeviaDto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoDeVium modelo = _mapper.Map<TipoDeVium>(TipoDeviaDto);
            await _tipodeviaRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }



    }





}
