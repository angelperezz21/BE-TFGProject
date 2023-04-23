using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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


        [HttpGet("listaRecursos")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listrecursos = await _recursoRepository.GetListRecursos();

                var listrecursosDto = _mapper.Map<IEnumerable<RecursoDto>>(listrecursos);

                return Ok(listrecursosDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "Recurso")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
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

        [Authorize(Roles = "Recurso")]
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

        [Authorize(Roles = "Recurso")]
        [HttpPost]
        public async Task<IActionResult> Post(RecursoDto recursoDto)
        {
            try
            {
                var recurso = _mapper.Map<Recurso>(recursoDto);

                recurso = await _recursoRepository.AddRecurso(recurso);

                var recursoItemDto = _mapper.Map<RecursoDto>(recurso);

                return CreatedAtAction("Get", new { id = recursoItemDto.Id }, recursoItemDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
