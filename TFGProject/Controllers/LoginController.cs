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
//  along with this program.  If not, see <https://www.gnu.org/licenses/>.
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
        public async Task<IActionResult> Login([FromBody] LoginUsuario loginUsuario)
        {
            try
            {
                if(loginUsuario.userType == 1)
                {

                    var user = await _loginRepository.LoginEmpresa(loginUsuario);
                    if (user == null) return StatusCode(404, "Error contraseña");
                    return Ok(user);
                }

                else
                {
                    var user = await _loginRepository.LoginBeneficiario(loginUsuario);
                    if (user == null) return StatusCode(404, "Error contraseña");
                    return Ok(user);;
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
