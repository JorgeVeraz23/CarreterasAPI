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
    public class TaludController : ControllerBase
    {
        private readonly ILogger<TaludController> _logger;
        private readonly ITaludRepositorio _taludRepo;
        private readonly IMapper _mapper;
        private readonly ICantonRepositorio _cantonRepo;
        private readonly ITramoRepositorio _tramoRepo;
        protected Response _response;
        public TaludController(ILogger<TaludController> logger, ITaludRepositorio taludRepo, ITramoRepositorio tramoRepo, ICantonRepositorio cantonRepo, IMapper mapper)
        {
            _logger = logger;
            _taludRepo = taludRepo;
            _tramoRepo = tramoRepo;
            _cantonRepo = cantonRepo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> GetTalud()
        {
            try
            {
                _logger.LogInformation("Obtener los tramos");
                IEnumerable<Talud> taludList= await _taludRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<TaludDto>>(taludList);
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

        [HttpGet("{id:int}", Name = "GetTalud")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> GetTalud(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer el Talud con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                var talud = await _taludRepo.Obtener(c => c.IdTalud == id);
                if (talud == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<TaludDto>(talud);
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
        public async Task<ActionResult<Response>> CrearTalud([FromBody] TaludCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                

                if (await _cantonRepo.Obtener(v => v.IdCanton == createDto.Canton) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de Canton no existe");
                    return BadRequest(ModelState);
                }

                if (await _tramoRepo.Obtener(v => v.IdTramo == createDto.IdTramo) == null)
                {
                    ModelState.AddModelError("ClaveFornea", "El Id de Tramo de via no existe");
                    return BadRequest(ModelState);
                }
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                Talud modelo = _mapper.Map<Talud>(createDto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;


                await _taludRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetTalud", new { id = modelo.IdTalud }, _response);
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

        public async Task<IActionResult> DeleteTalud(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var talud = await _taludRepo.Obtener(v => v.IdTalud == id);
                if (talud == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _taludRepo.Remover(talud);
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
        public async Task<IActionResult> UpdateTalud(int id, [FromBody] TaludUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IdTalud)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }



            if (await _cantonRepo.Obtener(v => v.IdCanton == updateDto.Canton) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id de Canton no existe");
                return BadRequest(ModelState);
            }

            if (await _tramoRepo.Obtener(v => v.IdTramo == updateDto.IdTramo) == null)
            {
                ModelState.AddModelError("ClaveFornea", "El Id de Tramo de via no existe");
                return BadRequest(ModelState);
            }

            Talud modelo = _mapper.Map<Talud>(updateDto);

            await _taludRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }

    }
}
