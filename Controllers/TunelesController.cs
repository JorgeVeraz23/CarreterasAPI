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
    public class TunelesController : ControllerBase
    {
        private readonly ILogger<TunelesController> _logger;
        private readonly ITuneleRepositorio _tuneleRepo;
        private readonly ITramoRepositorio _tramoRepo;
        private readonly IMapper _mapper;
        protected Response _response;

        public TunelesController(ILogger<TunelesController> logger, ITuneleRepositorio tuneleRepo, ITramoRepositorio tramoRepo, IMapper mapper)
        {
            _logger = logger;
            _tuneleRepo = tuneleRepo;
            _tramoRepo = tramoRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> GetTunele()
        {
            try
            {
                _logger.LogInformation("Obtener los tuneles");
                IEnumerable<Tunele> tunelesList = await _tuneleRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<TunelesDto>>(tunelesList);
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


        [HttpGet("{id:int}", Name = "GetTuneles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> GetTuneles(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer el Tuneles con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                var tuneles = await _tuneleRepo.Obtener(c => c.IdTunel == id);
                if (tuneles == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<TunelesDto>(tuneles);
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
        public async Task<ActionResult<Response>> CrearTuneles([FromBody] TunelesCreateDto createDto)
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
                Tunele modelo = _mapper.Map<Tunele>(createDto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;


                await _tuneleRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetTuneles", new { id = modelo.IdTunel }, _response);
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

        public async Task<IActionResult> DeleteTuneles(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var tuneles = await _tuneleRepo.Obtener(v => v.IdTunel == id);
                if (tuneles == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _tuneleRepo.Remover(tuneles);
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
        public async Task<IActionResult> UpdateTunele(int id, [FromBody] TunelesUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IdTunel)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }


            if (await _tuneleRepo.Obtener(v => v.IdTunel == updateDto.IdTunel) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id de Tramo no existe");
                return BadRequest(ModelState);
            }


            Tunele modelo = _mapper.Map<Tunele>(updateDto);

            await _tuneleRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }






    }
}
