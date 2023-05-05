using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TFGProject.Models;
using TFGProject.Models.DTO;
using TFGProject.Models.Repository.BeneficiarioR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TFGProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiarioController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IBeneficiarioRepository _beneficiarioRepository;

        public BeneficiarioController(IMapper mapper, IBeneficiarioRepository beneficiarioRepository)
        {
            _mapper = mapper;
            _beneficiarioRepository = beneficiarioRepository;
        }


        [HttpGet("listaBeneficiarios")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listbeneficiarios = await _beneficiarioRepository.GetListBeneficiarios();

                var listbeneficiariosDto = _mapper.Map<IEnumerable<BeneficiarioDto>>(listbeneficiarios);

                return Ok(listbeneficiariosDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        
        [HttpGet("listaEmpresasSeguidos/{id}")]
        public async Task<IActionResult> GetListaSeguidos(int id)
        {
            try
            {
                var beneficiario = await _beneficiarioRepository.GetBeneficiario(id);

                if (beneficiario == null)
                {
                    return NotFound();
                }

                var empresas = await _beneficiarioRepository.GetEmpresasSeguidos(id);

                var listaEmrpesasDto = _mapper.Map<IEnumerable<EmpresaPerfilDto>>(empresas);

                return Ok(listaEmrpesasDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "Beneficiario")]
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

                var beneficiario = await _beneficiarioRepository.GetBeneficiario(id);

                if (beneficiario == null)
                {
                    return NotFound();
                }

                var donaciones = await _beneficiarioRepository.GetListDonaciones(id);

                var listaDonacionesDto = _mapper.Map<IEnumerable<DonacionDto>>(donaciones);

                return Ok(listaDonacionesDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Beneficiario")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBeneficiario(int id)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (id.ToString() != userId)
                {
                    return Unauthorized();
                }

                var beneficiario = await _beneficiarioRepository.GetBeneficiario(id);

                if (beneficiario == null)
                {
                    return NotFound();
                }

                var beneficiarioDto = _mapper.Map<BeneficiarioDto>(beneficiario);

                return Ok(beneficiarioDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Beneficiario")]
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

                var beneficiario = await _beneficiarioRepository.GetBeneficiario(id);

                if (beneficiario == null)
                {
                    return NotFound();
                }

                await _beneficiarioRepository.DeleteBeneficiario(beneficiario);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

     
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] BeneficiarioDto beneficiarioDto)
        {
            try
            {
                var beneficiario = _mapper.Map<Beneficiario>(beneficiarioDto);

                beneficiario = await _beneficiarioRepository.AddBeneficiario(beneficiario);

               
                return Ok(beneficiarioDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Beneficiario")]
        [HttpPost("seguirEmpresa")]
        public async Task<IActionResult> Post([FromBody] SeguirUsuario model)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (model.idBeneficiario.ToString() != userId)
                {
                    return Unauthorized();
                }

                await _beneficiarioRepository.NuevoSeguido(model.idBeneficiario, model.idEmpresa);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Beneficiario")]
        [HttpPost("dejarSeguirEmpresa")]
        public async Task<IActionResult> PostUnfollowEmpresa([FromBody] SeguirUsuario model)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (model.idBeneficiario.ToString() != userId)
                {
                    return Unauthorized();
                }

                await _beneficiarioRepository.UnfollowSeguido(model.idBeneficiario, model.idEmpresa);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Beneficiario")]
        [HttpPut("update")]
        public async Task<IActionResult> Put([FromBody] BeneficiarioDto beneficiarioDto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (beneficiarioDto.Id.ToString() != userId)
                {
                    return Unauthorized();
                }

                var beneficiarioItem = await _beneficiarioRepository.GetBeneficiario((int)beneficiarioDto.Id);

                if (beneficiarioItem == null)
                {
                    return NotFound();
                }

                await _beneficiarioRepository.UpdateBeneficiario(beneficiarioDto, (int)beneficiarioDto.Id);

                return Ok();

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
                await _beneficiarioRepository.GetContrasenya(email);                
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Beneficiario")]
        [HttpGet("Notificaciones/{id}")]
        public async Task<IActionResult> GetNotificaciones(int id)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (id.ToString() != userId)
                {
                    return Unauthorized();
                }

                var beneficiario = await _beneficiarioRepository.GetBeneficiario(id);

                if (beneficiario == null)
                {
                    return NotFound();
                }

                var beneficiarioDto = _mapper.Map<BeneficiarioDto>(beneficiario);

                return Ok(beneficiarioDto.Notificacion);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
