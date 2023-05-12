using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TFGProject.Models;
using TFGProject.Models.DTO;
using TFGProject.Models.Repository.EmpresaR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TFGProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IWebHostEnvironment _environment;

        public EmpresaController(IMapper mapper, IEmpresaRepository empresaRepository, IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _empresaRepository = empresaRepository;
            _environment = environment;
        }


        [HttpGet("listaEmpresas")]
        public async Task<IActionResult> GetListaEmpresas()
        {
            try
            {
                var listempresas = await _empresaRepository.GetListEmpresas();

                var listempresasDto = _mapper.Map<IEnumerable<EmpresaPerfilDto>>(listempresas);

                return Ok(listempresasDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("empresasTotales")]
        public async Task<IActionResult> GetTotalEmpresas()
        {
            try
            {
                var listempresas = await _empresaRepository.GetListEmpresas();

                var listempresasDto = _mapper.Map<IEnumerable<EmpresaPerfilDto>>(listempresas);

                return Ok(listempresasDto.Count());

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

       
        [HttpGet("empresaPerfil/{id}")]
        public async Task<IActionResult> GetPerfilEmpresa(int id)
        {
            try
            {
                var empresa = await _empresaRepository.GetEmpresa(id);

                if (empresa == null)
                {
                    return NotFound();
                }

                var empresaDto = _mapper.Map<EmpresaPerfilDto>(empresa);

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
                

                return Ok(empresaDto);

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
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put([FromBody] EmpresaDto empresaDto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (empresaDto.Id.ToString() != userId)
                {
                    return Unauthorized();
                }

                var empresaItem = await _empresaRepository.GetEmpresa((int)empresaDto.Id);

                if (empresaItem == null)
                {
                    return NotFound();
                }

                await _empresaRepository.UpdateEmpresa(empresaDto, (int)empresaDto.Id);

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
                await _empresaRepository.GetContrasenya(email);
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Empresa")]
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

                var empresa = await _empresaRepository.GetEmpresa(id);

                if (empresa == null)
                {
                    return NotFound();
                }

                var empresaDto = _mapper.Map<EmpresaDto>(empresa);

                return Ok(empresaDto.Notificacion);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // en tu controlador de C#

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile image)
        {
                // generar un nombre único para la imagen
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

                // guardar la imagen en el sistema de archivos del servidor
                var filePath = Path.Combine(_environment.WebRootPath,"uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // devolver la ruta de acceso de la imagen guardada
                var imagePath = $"/uploads/{fileName}";
                return Ok(new { imagePath = imagePath });
        }

    }
}
