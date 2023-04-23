using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TFGProject.Models;
using TFGProject.Models.DTO;
using TFGProject.Models.Repository.LoginR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TFGProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ILoginRepository _loginRepository;

        public LoginController(IMapper mapper, ILoginRepository loginRepository)
        {
            _mapper = mapper;
            _loginRepository = loginRepository;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuario loginUsuario, int userType)
        {
            try
            {
                if(userType == 1)
                {
                    var user = await _loginRepository.LoginEmpresa(loginUsuario);
                    if (user == null) return NotFound();
                    return Ok(user);
                }

                else
                {
                    var user = await _loginRepository.LoginBeneficiario(loginUsuario);
                    if (user == null) return NotFound();
                    return Ok(user);
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
