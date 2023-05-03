using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TFGProject.Models;
using TFGProject.Models.DTO;
using TFGProject.Models.Repository.RecursoR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TFGProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecursoController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IRecursoRepository _recursoRepository;

        public RecursoController(IMapper mapper, IRecursoRepository recursoRepository)
        {
            _mapper = mapper;
            _recursoRepository = recursoRepository;
        }


        [HttpGet("listaRecursosPublicados")]
        public async Task<IActionResult> GetListRecursosPublicados()
        {
            try
            {
                var listrecursos = await _recursoRepository.GetListRecursosPublicados();

                var listrecursosDto = _mapper.Map<IEnumerable<RecursoDto>>(listrecursos);

                return Ok(listrecursosDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpGet("listaRecursosEmpresa/{id}")]
        public async Task<IActionResult> GetListaRecursosEmpresa(int id)
        {
            try
            {
                var listrecursos = await _recursoRepository.GetListRecursosEmpresa(id);

                var listrecursosDto = _mapper.Map<IEnumerable<RecursoDto>>(listrecursos);

                return Ok(listrecursosDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [Authorize(Roles = "Empresa")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecurso(int id)
        {
            try
            {
                var recurso = await _recursoRepository.GetRecurso(id);

                if (recurso == null)
                {
                    return NotFound();
                }

                var recursoDto = _mapper.Map<RecursoDto>(recurso);

                return Ok(recursoDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Empresa")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var recurso = await _recursoRepository.GetRecurso(id);

                if (recurso == null)
                {
                    return NotFound();
                }

                await _recursoRepository.DeleteRecurso(recurso);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Empresa")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RecursoDto recursoDto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (recursoDto.IdEmpresa.ToString() != userId)
                {
                    return Unauthorized();
                }

                var recurso = _mapper.Map<Recurso>(recursoDto);

                recurso = await _recursoRepository.AddRecurso(recurso);

                var recursoItemDto = _mapper.Map<RecursoDto>(recurso);

                return Ok(recursoItemDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Beneficiario")]
        [HttpPut("solicitarRecurso/{id}")]
        public async Task<IActionResult> CambiarEstado(int idRecurso, int idBeneficiario)
        {
            try
            {
                var recurso = await _recursoRepository.SolicitarRecurso(idRecurso, idBeneficiario);
                if(recurso==null) return StatusCode(409, "No se puede actualizar el recurso");
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Empresa")]
        [HttpPut("aceptarRecurso/{id}")]
        public async Task<IActionResult> AceptarRecurso(int id)
        {
            try
            {
                var recurso = await _recursoRepository.AceptarRecurso(id);
                if (recurso == null) return StatusCode(409, "No se puede actualizar el recurso");
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Empresa")]
        [HttpPut("publicarRecurso/{id}")]
        public async Task<IActionResult> PublicarRecurso(int id)
        {
            try
            {
                var recurso = await _recursoRepository.PublicarRecurso(id);
                if (recurso == null) return StatusCode(409, "No se puede actualizar el recurso");
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
