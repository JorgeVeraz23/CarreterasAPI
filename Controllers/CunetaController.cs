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
    public class CunetaController : ControllerBase
    {
        private readonly ILogger<CunetaController> _logger;
        private readonly ICunetaRepositorio _cunetaRepo;
        private readonly IMapper _mapper;
        private readonly ITramoRepositorio _tramoRepositorio;
        protected Response _response;

        public CunetaController(ILogger<CunetaController> logger, ICunetaRepositorio cunetaRepo, ITramoRepositorio tramoRepositorio, IMapper mapper)
        {
            _logger = logger;
            _cunetaRepo = cunetaRepo;
            _tramoRepositorio = tramoRepositorio;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> GetCuneta()
        {
            try
            {
                _logger.LogInformation("Obtener las cunetas");
                IEnumerable<Cunetum> cunetaList = await _cunetaRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<CunetaDto>>(cunetaList);
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

        [HttpGet("{id:int}", Name = "GetCuneta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> GetCuneta(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Cuneta con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                var cuneta = await _cunetaRepo.Obtener(c => c.IdCuneta == id);
                if (cuneta == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<CunetaDto>(cuneta);
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
        public async Task<ActionResult<Response>> CrearCuneta([FromBody] CunetaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                if (await _tramoRepositorio.Obtener(v => v.IdTramo == createDto.IdTramo) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de Tramo no existe");
                    return BadRequest(ModelState);
                }


                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                Cunetum modelo = _mapper.Map<Cunetum>(createDto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;


                await _cunetaRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetCuneta", new { id = modelo.IdCuneta }, _response);
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

        public async Task<IActionResult> DeleteCuneta(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var cuneta = await _cunetaRepo.Obtener(v => v.IdCuneta == id);
                if (cuneta == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _cunetaRepo.Remover(cuneta);
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
        public async Task<IActionResult> UpdateCuneta(int id, [FromBody] CunetaUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IdCuneta)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }


            if (await _tramoRepositorio.Obtener(v => v.IdTramo == updateDto.IdTramo) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id de Tramo no existe");
                return BadRequest(ModelState);
            }


            Cunetum modelo = _mapper.Map<Cunetum>(updateDto);

            await _cunetaRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }



    }
}
