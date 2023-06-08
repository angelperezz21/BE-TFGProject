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
using Microsoft.EntityFrameworkCore;
using TFGProject.Models.DTO;
using TFGProject.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;

namespace TFGProject.Models.Repository.LoginR
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationDbContext _context;
        private string secretKey;

        public LoginRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            secretKey = configuration.GetValue<string>("Jwt:Key");
        }

        public async Task<EmpresaLogin> LoginEmpresa(LoginUsuario loginUsuario)
        {
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(loginUsuario.Contrasenya));
                loginUsuario.Contrasenya = Convert.ToBase64String(hash);
            }

            var user = await _context.Empresas.FirstOrDefaultAsync(user => user.Email.ToLower().Equals(loginUsuario.Email.ToLower())
            && user.Contrasenya.Equals(loginUsuario.Contrasenya));

            if (user == null) return null;

            user.Contrasenya = "";

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.GetType().Name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            EmpresaLogin empresaLogin = new EmpresaLogin()
            {
                Token = tokenHandler.WriteToken(token),
                Empresa = user
            };

            return empresaLogin;
        }

        public async Task<BeneficiarioLogin> LoginBeneficiario(LoginUsuario loginUsuario)
        {
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(loginUsuario.Contrasenya));
                loginUsuario.Contrasenya = Convert.ToBase64String(hash);
            }

            var user = await _context.Beneficiarios.FirstOrDefaultAsync(user => user.Email.ToLower().Equals(loginUsuario.Email.ToLower())
            && user.Contrasenya.Equals(loginUsuario.Contrasenya));
            
            if (user == null) return null;

            user.Contrasenya = "";

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.GetType().Name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            BeneficiarioLogin beneficiarioLogin = new BeneficiarioLogin()
            {
                Token = tokenHandler.WriteToken(token),
                Beneficiario = user
            };

            return beneficiarioLogin;
        }


    }
}
