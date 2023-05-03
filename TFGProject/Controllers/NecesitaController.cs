using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TFGProject.Models;
using TFGProject.Models.DTO;
using TFGProject.Models.Repository.NecesitaR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TFGProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NecesitaController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly INecesitaRepository _necesitaRepository;

        public NecesitaController(IMapper mapper, INecesitaRepository necesitaRepository)
        {
            _mapper = mapper;
            _necesitaRepository = necesitaRepository;
        }


        [HttpGet("listaNecesidadesPublicadas")]
        public async Task<IActionResult> GetListNecesidadesBeneficiario()
        {
            try
            {
                var listnecesitas = await _necesitaRepository.GetListNecesidadesPublicadas();

                var listnecesitasDto = _mapper.Map<IEnumerable<NecesitaDto>>(listnecesitas);

                return Ok(listnecesitasDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("listaNecesidadesBeneficiario/{id}")]
        public async Task<IActionResult> GetListaNecesidadesBeneficiario(int id)
        {
            try
            {
                var listnecesidades = await _necesitaRepository.GetListNecesidadesBeneficiario(id);

                var listnecesidadesDto = _mapper.Map<IEnumerable<NecesitaDto>>(listnecesidades);

                return Ok(listnecesidadesDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "Beneficiario")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNecesidad(int id)
        {
            try
            {
                var necesita = await _necesitaRepository.GetNecesita(id);

                if (necesita == null)
                {
                    return NotFound();
                }

                var necesitaDto = _mapper.Map<NecesitaDto>(necesita);

                return Ok(necesitaDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Beneficiario")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNecesidad(int id)
        {
            try
            {
                var necesita = await _necesitaRepository.GetNecesita(id);

                if (necesita == null)
                {
                    return NotFound();
                }

                await _necesitaRepository.DeleteNecesita(necesita);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Beneficiario")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]  NecesitaDto necesitaDto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (necesitaDto.IdBeneficiario.ToString() != userId)
                {
                    return Unauthorized();
                }


                var necesita = _mapper.Map<Necesita>(necesitaDto);

                necesita = await _necesitaRepository.AddNecesita(necesita);

                var necesitaItemDto = _mapper.Map<NecesitaDto>(necesita);

                return CreatedAtAction("Get", new { id = necesitaItemDto.Id }, necesitaItemDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Empresa")]
        [HttpPut("solicitarNecesidad/{id}")]
        public async Task<IActionResult> CambiarEstado(int idNecesita, int idEmpresa)
        {
            try
            {
                var recurso = await _necesitaRepository.SolicitarNecesidad(idNecesita,idEmpresa);
                if (recurso == null) return StatusCode(409, "No se puede actualizar la necesidad");
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Beneficiario")]
        [HttpPut("aceptarNecesidad/{id}")]
        public async Task<IActionResult> AceptarRecurso(int id)
        {
            try
            {
                var recurso = await _necesitaRepository.AceptarNecesidad(id);
                if (recurso == null) return StatusCode(409, "No se puede actualizar la necesidad");
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Beneficiario")]
        [HttpPut("publicarNecesidad/{id}")]
        public async Task<IActionResult> PublicarRecurso(int id)
        {
            try
            {
                var recurso = await _necesitaRepository.PublicarNecesidad(id);
                if (recurso == null) return StatusCode(409, "No se puede actualizar la necesidad");
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
