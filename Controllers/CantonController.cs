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
        public async Task<ActionResult<Response>> GetVillas()
        {
            try
            {
                _logger.LogInformation("Obtener las Villas");
                IEnumerable<Canton> villaList = await _cantonRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<CantonDto>>(villaList);
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

    }
}
