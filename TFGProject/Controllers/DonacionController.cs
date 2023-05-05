using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Security.Claims;
using TFGProject.Models;
using TFGProject.Models.DTO;
using TFGProject.Models.Repository.DonacionR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TFGProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonacionController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IDonacionRepository _donacionRepository;

        public DonacionController(IMapper mapper, IDonacionRepository donacionRepository)
        {
            _mapper = mapper;
            _donacionRepository = donacionRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listdonacions = await _donacionRepository.GetListDonacions();

                var listdonacionsDto = _mapper.Map<IEnumerable<DonacionDto>>(listdonacions);

                return Ok(listdonacionsDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("certificado/{id}")]
        public async Task<HttpResponseMessage> GetCertificado(int id)
        {
            
                var donacion = await _donacionRepository.GetDonacion(id);

                //if (donacion == null)
                //{
                //    return NotFound();
                //}
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        
                //if (donacion.IdEmpresa.ToString() != userId && donacion.IdBeneficiario.ToString() != userId)
                //{
                //    return Unauthorized();
                //}

                return _donacionRepository.GenerarPDFCertificado(donacion);                
                

            
            
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {

                var donacion = await _donacionRepository.GetDonacion(id);

                if (donacion == null)
                {
                    return NotFound();
                }
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (donacion.IdEmpresa.ToString() != userId || donacion.IdBeneficiario.ToString() != userId)
                {
                    return Unauthorized();
                }


                var donacionDto = _mapper.Map<DonacionDto>(donacion);

                return Ok(donacionDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(NewDonacionDto newDonacionDto, bool certificado)
        {
            try
            {
                var donacion = _mapper.Map<Donacion>(newDonacionDto);

                donacion = await _donacionRepository.AddDonacion(donacion, certificado);

                var donacionItemDto = _mapper.Map<DonacionDto>(donacion);

                return CreatedAtAction("Get", new { id = donacionItemDto.Id }, donacionItemDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
