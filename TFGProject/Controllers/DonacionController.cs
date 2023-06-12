//< Aplicación destinada a facilitar la colaboraciñón entre Empresas y ONGs>
//  Copyright (C) < 2023 >  < Ángel Pérez Martín>

//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.

//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.

//  You should have received a copy of the GNU General Public License
//  in the LICENSE.txt file  If not, see <https://www.gnu.org/licenses/>.
using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        private readonly IWebHostEnvironment _environment;


        public DonacionController(IMapper mapper, IDonacionRepository donacionRepository, IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _donacionRepository = donacionRepository;
            _environment = environment;
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

        [HttpGet("donacionesTotales")]
        public async Task<IActionResult> GetTotal()
        {
            try
            {
                var listdonacions = await _donacionRepository.GetListDonacions();

                var listdonacionsDto = _mapper.Map<IEnumerable<DonacionDto>>(listdonacions);

                return Ok(listdonacionsDto.Count());

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles ="Empresa")]
        [HttpGet("certificado/{id}")]
        public async Task<IActionResult> GetCertificado(int id)
        {
            
            var donacion = await _donacionRepository.GetDonacion(id);

            var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            if (donacion.IdEmpresa.ToString() != userId)
            {
                return Unauthorized();
            }

            var certificadoPath = _donacionRepository.GenerarPDFCertificado(donacion, _environment.WebRootPath);

            return Ok(new {certificadoPath = certificadoPath.Result });


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

        [Authorize(Roles ="Empresa")]
        [HttpPut("envioDonacion/{id}")]
        public async Task<IActionResult> envioDonacion(int id)
        {
            try
            {
                var donacion = await _donacionRepository.GetDonacion(id);

                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (donacion.IdEmpresa.ToString() != userId)
                {
                    return Unauthorized();
                }

                donacion = await _donacionRepository.UpdateEnvioDonacion(id);

                if (donacion == null)
                {
                    return BadRequest("Ya esta enviada la donación.");
                }

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Beneficiario")]
        [HttpPut("recibirDonacion/{id}")]
        public async Task<IActionResult> recibirDonacion(int id)
        {
            try
            {
                var donacion = await _donacionRepository.GetDonacion(id);

                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (donacion.IdBeneficiario.ToString() != userId)
                {
                    return Unauthorized();
                }

                donacion = await _donacionRepository.UpdateRecibirDonacion(id);

                if (donacion == null)
                {
                    return BadRequest("Donación ya recibida o no enviada.");
                }
                

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
