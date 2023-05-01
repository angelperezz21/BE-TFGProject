using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TFGProject.Models;
using TFGProject.Models.DTO;
using TFGProject.Models.Repository.EmpresaR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TFGProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaController(IMapper mapper, IEmpresaRepository empresaRepository)
        {
            _mapper = mapper;
            _empresaRepository = empresaRepository;
        }


        [HttpGet("listaEmpresas")]
        public async Task<IActionResult> GetListaEmpresas()
        {
            try
            {
                var listempresas = await _empresaRepository.GetListEmpresas();

                var listempresasDto = _mapper.Map<IEnumerable<EmpresaDto>>(listempresas);

                return Ok(listempresasDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("listaBeneficiariosSeguidos/{id}")]
        public async Task<IActionResult> GetListaSeguidos(int id)
        {
            try
            {
                var empresa = await _empresaRepository.GetEmpresa(id);

                if (empresa == null)
                {
                    return NotFound();
                }

                var beneficiarios = await _empresaRepository.GetBeneficiariosSeguidos(id);

                var listaBeneficiariosDto = _mapper.Map<IEnumerable<BeneficiarioPerfilDto>>(beneficiarios);

                return Ok(listaBeneficiariosDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Empresa")]
        [HttpGet("listaDonaciones/{id}")]
        public async Task<IActionResult> GetListaDonaciones(int id)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (id.ToString() != userId)
                {
                    return Unauthorized();
                }

                var empresa = await _empresaRepository.GetEmpresa(id);

                if (empresa == null)
                {
                    return NotFound();
                }

                var donaciones = await _empresaRepository.GetListDonaciones(id);

                var listaDonacionesDto = _mapper.Map<IEnumerable<DonacionDto>>(donaciones);

                return Ok(listaDonacionesDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Empresa")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpresa(int id)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (id.ToString() != userId)
                {
                    return Unauthorized();
                }

                var empresa = await _empresaRepository.GetEmpresa(id);

                if (empresa == null)
                {
                    return NotFound();
                }

                var empresaDto = _mapper.Map<EmpresaDto>(empresa);

                return Ok(empresaDto);

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
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (id.ToString() != userId)
                {
                    return Unauthorized();
                }
                var empresa = await _empresaRepository.GetEmpresa(id);

                if (empresa == null)
                {
                    return NotFound();
                }

                await _empresaRepository.DeleteEmpresa(empresa);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] EmpresaDto empresaDto)
        {
            try
            {
                var empresa = _mapper.Map<Empresa>(empresaDto);

                empresa = await _empresaRepository.AddEmpresa(empresa);

                var empresaItemDto = _mapper.Map<EmpresaDto>(empresa);

                return Ok(empresaItemDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Empresa")]
        [HttpPost("seguirBeneficiario")]
        public async Task<IActionResult> Post([FromBody] SeguirUsuario model)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (model.idEmpresa.ToString() != userId)
                {
                    return Unauthorized();
                }
                await _empresaRepository.NuevoSeguido(model.idBeneficiario, model.idEmpresa);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Empresa")]
        [HttpPost("dejarSeguirBeneficiario")]
        public async Task<IActionResult> PostUnfollowEmpresa([FromBody] SeguirUsuario model)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (model.idEmpresa.ToString() != userId)
                {
                    return Unauthorized();
                }

                await _empresaRepository.UnfollowSeguido(model.idBeneficiario, model.idEmpresa);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Empresa")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EmpresaDto empresaDto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (id.ToString() != userId)
                {
                    return Unauthorized();
                }

                var empresaItem = await _empresaRepository.GetEmpresa(id);

                if (empresaItem == null)
                {
                    return NotFound();
                }

                await _empresaRepository.UpdateEmpresa(empresaDto, id);

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("recuperarContrasenya/{email}")]
        public async Task<IActionResult> GetContraseña(string email)
        {
            try
            {
                await _empresaRepository.GetContrasenya(email);
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
