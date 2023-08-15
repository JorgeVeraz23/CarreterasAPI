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
    public class AlcantarilladoController : ControllerBase
    {
        private readonly ILogger<AlcantarilladoController> _logger;
        private readonly IAlcantarilladoRepositorio _alcantarilladoRepo;
        private readonly IMapper _mapper;
        private readonly ITramoRepositorio _tramoRepo;
        protected Response _response;
        public AlcantarilladoController(ILogger<AlcantarilladoController> logger, IAlcantarilladoRepositorio alcantarilladoRepo, ITramoRepositorio tramoRepo, IMapper mapper)
        {
            _logger = logger;
            _alcantarilladoRepo = alcantarilladoRepo;
            _tramoRepo = tramoRepo;
            _mapper = mapper;
            _response = new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> GetAlcantarillados()
        {
            try
            {
                _logger.LogInformation("Obtener los alcantarillados");
                IEnumerable<Alcantarillado> alcantarilladoList = await _alcantarilladoRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<AlcantarilladoDto>>(alcantarilladoList);
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

        [HttpGet("{id:int}", Name = "GetAlcantarillado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> GetAlcantarillado(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Alcantarillado con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                var alcantarillado = await _alcantarilladoRepo.Obtener(c => c.IdAlcantarillado == id);
                if (alcantarillado == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<AlcantarilladoDto>(alcantarillado);
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
        public async Task<ActionResult<Response>> CrearAlcantarillado([FromBody] AlcantarilladoCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
               /* if (await _alcantarilladoRepo.Obtener(v => v. == createDto.IdAlcantarillado) != null)
                {
                    ModelState.AddModelError("NombreExiste", "El numero de alcantarillado con ese Nombre ya existe!");
                    return BadRequest(ModelState);
                }*/

                if (await _tramoRepo.Obtener(v => v.IdTramo == createDto.IdTramo) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de Tramo no existe");
                    return BadRequest(ModelState);
                }

                
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                Alcantarillado modelo = _mapper.Map<Alcantarillado>(createDto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;


                await _alcantarilladoRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetAlcantarillado", new { id = modelo.IdAlcantarillado }, _response);
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

        public async Task<IActionResult> DeleteAlcantarillado(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var alcantarillado = await _alcantarilladoRepo.Obtener(v => v.IdAlcantarillado == id);
                if (alcantarillado == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _alcantarilladoRepo.Remover(alcantarillado);
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
        public async Task<IActionResult> UpdateAlcantarillado(int id, [FromBody] AlcantarilladoUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IdAlcantarillado)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            /*if (await _alcantarilladoRepo.Obtener(v => v.Nombre == updateDto.Nombre) == null)
            {
                ModelState.AddModelError("Nombre Existe", "La carretera con ese nombre existe");
                return BadRequest(ModelState);
            }*/
            if (await _tramoRepo.Obtener(v => v.IdTramo == updateDto.IdTramo) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id de Tramo no existe");
                return BadRequest(ModelState);
            }
            

            Alcantarillado modelo = _mapper.Map<Alcantarillado>(updateDto);

            await _alcantarilladoRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }





    }
}
