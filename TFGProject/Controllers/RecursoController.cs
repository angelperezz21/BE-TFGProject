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
        private readonly IWebHostEnvironment _environment;

        public RecursoController(IMapper mapper, IRecursoRepository recursoRepository, IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _recursoRepository = recursoRepository;
            _environment = environment;
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

        [HttpGet("listaRecursosNuevos")]
        public async Task<IActionResult> GetListRecursosNuevos()
        {
            try
            {
                var listrecursos = await _recursoRepository.GetListRecursosPublicados();

                var listrecursosDto = _mapper.Map<IEnumerable<RecursoDto>>(listrecursos);

                DateTime fechaActual = DateTime.Now;

                var primeros10Objetos = listrecursosDto.OrderBy(o => Math.Abs((fechaActual.Ticks - ((DateTime)o.FechaCreacionRecurso).Ticks))).Take(10);

                return Ok(primeros10Objetos);

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

        [Authorize(Roles = "Beneficiario")]
        [HttpGet("solicitudesRecursos/{id}")]
        public async Task<IActionResult> GetMisSolicitudesRecurso(int id)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (id.ToString() != userId)
                {
                    return Unauthorized();
                }

                var listRecursos = await _recursoRepository.GetSolicitudesRecursos(id);

                var listrecursosDto = _mapper.Map<IEnumerable<RecursoDto>>(listRecursos);

                return Ok(listrecursosDto);

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

                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (recurso.IdEmpresa.ToString() != userId)
                {
                    return Unauthorized();
                }

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
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] RecursoDto recursoDto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                if (recursoDto.IdEmpresa.ToString() != userId)
                {
                    return Unauthorized();
                }

                var recursoItem = await _recursoRepository.GetRecurso((int)recursoDto.Id);

                if (recursoItem == null)
                {
                    return NotFound();
                }

                await _recursoRepository.UpdateRecurso(recursoDto, (int)recursoDto.Id);

                return Ok();
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
        public async Task<IActionResult> CambiarEstado([FromBody] EstadoRecursoDto estadoRecurso)
        {
            try
            {
                var recurso = await _recursoRepository.SolicitarRecurso(estadoRecurso.idRecurso, estadoRecurso.idBeneficiario);
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
        public async Task<IActionResult> AceptarRecurso([FromBody] EstadoRecursoDto estadoRecurso)
        {
            try
            {
                var recurso = await _recursoRepository.AceptarRecurso(estadoRecurso.idRecurso, estadoRecurso.idBeneficiario);
                if (recurso == null) return StatusCode(409, "No se puede actualizar el recurso");
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Empresa")]
        [HttpGet("GetNotificaciones/{id}")]
        public async Task<IActionResult> GetRecursoNotificaciones(int id)
        {
            try
            {
                var recurso = await _recursoRepository.GetRecurso(id);

                if (recurso == null)
                {
                    return NotFound();
                }

                var recursoDto = _mapper.Map<RecursoDto>(recurso);

                var listBeneficiarios = await _recursoRepository.GetNotificaciones(recursoDto);

                return Ok(listBeneficiarios);

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
