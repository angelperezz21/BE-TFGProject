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
        private readonly IWebHostEnvironment _environment;

        public NecesitaController(IMapper mapper, INecesitaRepository necesitaRepository, IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _necesitaRepository = necesitaRepository;
            _environment = environment;
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

        [HttpGet("listaNecesidadesNuevas")]
        public async Task<IActionResult> GetListNecesidadesNuevasBeneficiario()
        {
            try
            {
                var listnecesitas = await _necesitaRepository.GetListNecesidadesPublicadas();

                var listnecesitasDto = _mapper.Map<IEnumerable<NecesitaDto>>(listnecesitas);
                
                DateTime fechaActual = DateTime.Now;

                var primeros10Objetos = listnecesitasDto.OrderBy(o => Math.Abs((fechaActual.Ticks - ((DateTime)o.FechaCreacionNecesita).Ticks))).Take(10);

                return Ok(primeros10Objetos);

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

        [Authorize(Roles = "Empresa")]
        [HttpGet("solicitudesNecesitas/{id}")]
        public async Task<IActionResult> GetMisSolicitudesNecesitas(int id)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (id.ToString() != userId)
                {
                    return Unauthorized();
                }

                var listNecesitas = await _necesitaRepository.GetSolicitudesNecesita(id);

                var listnecesitasDto = _mapper.Map<IEnumerable<NecesitaDto>>(listNecesitas);

                return Ok(listnecesitasDto);

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


                return Ok(necesitaItemDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Empresa")]
        [HttpPut("solicitarNecesidad/{id}")]
        public async Task<IActionResult> CambiarEstado([FromBody] EstadoNecesitaDto estadoNecesita)
        {
            try
            {
                var recurso = await _necesitaRepository.SolicitarNecesidad(estadoNecesita.idNecesidad, estadoNecesita.idEmpresa);
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
        public async Task<IActionResult> AceptarRecurso(int idNecesita, int idEmpresa)
        {
            try
            {
                var recurso = await _necesitaRepository.AceptarNecesidad(idNecesita, idEmpresa);
                if (recurso == null) return StatusCode(409, "No se puede actualizar la necesidad");
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Beneficiario")]
        [HttpGet("GetNotificaciones/{id}")]
        public async Task<IActionResult> GetNecesidadNotificaciones(int id)
        {
            try
            {
                var necesita = await _necesitaRepository.GetNecesita(id);

                if (necesita == null)
                {
                    return NotFound();
                }

                var necesitaDto = _mapper.Map<NecesitaDto>(necesita);

                var listSolicitantes = await _necesitaRepository.GetNotificaciones(necesitaDto);

                return Ok(listSolicitantes);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile image)
        {
            // generar un nombre único para la imagen
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

            // guardar la imagen en el sistema de archivos del servidor
            var filePath = Path.Combine(_environment.WebRootPath, "publicaciones", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // devolver la ruta de acceso de la imagen guardada
            var imagePath = $"/publicaciones/{fileName}";
            return Ok(new { imagePath = imagePath });
        }


    }
}
