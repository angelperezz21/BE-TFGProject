﻿//< Aplicación destinada a facilitar la colaboraciñón entre Empresas y ONGs>
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
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using TFGProject.Models.DTO;

namespace TFGProject.Models.Repository.BeneficiarioR
{
    public class BeneficiarioRepository : IBeneficiarioRepository
    {
        private readonly ApplicationDbContext _context;

        public BeneficiarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void sendEmail(Beneficiario beneficiario)
        {
            string fromMail = "easyDonationORG@gmail.com";
            string fromPassword = "axvqbrdsmpctynkw";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Bienvenido a EasyDonation";
            message.To.Add(new MailAddress(beneficiario.Email));
            message.Body = "<html><body><h1>Bienvenido a nuestro sitio web</h1><p>Hola "
                + beneficiario.Nombre +
                ",</p><p>Gracias por registrarte en nuestro sitio web. Esperamos que disfrutes de nuestros servicios y te sientas como en casa.</p>" +
                "<p>Tus credenciales de inicio de sesión son:</p><ul><li><strong>Email:</strong> "
                + beneficiario.Email +
                "</li><li><strong>Contraseña:</strong> " + beneficiario.PasswordSinHash +
                "</li></ul><p>Gracias,</p><p>El equipo de EasyDonation</p></body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }

        public void sendEmailRecuperación(Beneficiario beneficiario)
        {
            string fromMail = "easyDonationORG@gmail.com";
            string fromPassword = "axvqbrdsmpctynkw";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Email de recuperación";
            message.To.Add(new MailAddress(beneficiario.Email));
            message.Body = "<html><body><h1>Email para recuperar tu contraseña</h1><p>Hola "
                + beneficiario.Nombre +
                ",</p><p>Has solcitado la recuperación de tu contraseña</p>" +
                "<p>Tus credenciales de inicio de sesión son:</p><ul><li><strong>Email:</strong> "
                + beneficiario.Email +
                "</li><li><strong>Contraseña:</strong> " + beneficiario.Contrasenya +
                "</li></ul><p>Gracias,</p><p>El equipo de EasyDonation</p></body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }

        public async Task<Beneficiario> AddBeneficiario(Beneficiario beneficiario)
        {
            var existEmpresa = await _context.Empresas.FirstOrDefaultAsync(b => b.Email == beneficiario.Email);
            if (existEmpresa != null) return null;
            var existBeneficiario = await _context.Beneficiarios.FirstOrDefaultAsync(b => b.Email == beneficiario.Email);
            if (existBeneficiario != null) return null;

            beneficiario.PasswordSinHash = beneficiario.Contrasenya;
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(beneficiario.Contrasenya));
                beneficiario.Contrasenya = Convert.ToBase64String(hash);
            }

            _context.Add(beneficiario);
            sendEmail(beneficiario);
            await _context.SaveChangesAsync();
            return beneficiario;
        }

        public bool CheckCIF(string CIF)
        {
            var existBeneficiario = _context.Beneficiarios.FirstOrDefault(b => b.CIF == CIF);
            if (existBeneficiario != null) return true;
            return false;
        }

        public async Task NuevoSeguido(int idBeneficiario, int idEmpresa)
        {
            var empresa = await _context.Empresas.FindAsync(idEmpresa);
            var beneficiario = await _context.Beneficiarios.FindAsync(idBeneficiario);

            beneficiario.EmpresasQueSigo.Add(
                new BeneficiariosSiguenEmpresa { Beneficiario = beneficiario, Empresa = empresa });

            await _context.SaveChangesAsync();
        }

        public async Task UnfollowSeguido(int idBeneficiario, int idEmpresa)
        {
            var empresa = await _context.Empresas.FindAsync(idEmpresa);
            var beneficiario = await _context.Beneficiarios.Include(x => x.EmpresasQueSigo).FirstOrDefaultAsync(x => x.Id == idBeneficiario);

            var seguida =  beneficiario.EmpresasQueSigo.FirstOrDefault(e => e.IdEmpresa == idEmpresa && e.IdBeneficiario == idBeneficiario);

            beneficiario.EmpresasQueSigo.Remove(seguida);

            await _context.SaveChangesAsync();
        }


        public async Task DeleteBeneficiario(Beneficiario beneficiario)
        {
            _context.Beneficiarios.Remove(beneficiario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Empresa>> GetEmpresasSeguidos(int id)
        {
            var beneficiario = await _context.Beneficiarios.Include(x => x.EmpresasQueSigo).FirstOrDefaultAsync(x => x.Id == id);
            var empresas = new List<Empresa>();
            foreach (var empSigo in beneficiario.EmpresasQueSigo)
            {
                empresas.Add(await _context.Empresas.FindAsync(empSigo.IdEmpresa));
            }
            return empresas;
        }

        public async Task<List<Beneficiario>> GetListBeneficiarios()
        {
            return await _context.Beneficiarios.ToListAsync();
        }

        public async Task<List<Donacion>> GetListDonaciones(int id)
        {
            var beneficiario = await _context.Beneficiarios.Include(x => x.Donaciones).FirstOrDefaultAsync(x => x.Id == id);
            List<Donacion> donaciones = new List<Donacion>();
            foreach (var donacion in beneficiario.Donaciones.ToList())
            {
                if (donacion.Enviada == true && donacion.Recibida == true)
                {
                    donacion.Empresa = await _context.Empresas.FindAsync(donacion.IdEmpresa);
                    donaciones.Add(donacion);
                }
            }
            return donaciones;
        }

        public async Task<List<Donacion>> GetListDonacionesPendientes(int id)
        {
            var beneficiario = await _context.Beneficiarios.Include(x => x.Donaciones).FirstOrDefaultAsync(x => x.Id == id);
            List<Donacion> donaciones = new List<Donacion>();
            foreach (var donacion in beneficiario.Donaciones.ToList())
            {
                if (donacion.Enviada == false || donacion.Recibida == false)
                {
                    donacion.Empresa = await _context.Empresas.FindAsync(donacion.IdEmpresa);
                    donaciones.Add(donacion);
                }
            }
            return donaciones;
        }

        public async Task<Beneficiario> GetBeneficiario(int id)
        {
            return await _context.Beneficiarios.FindAsync(id);
        }

        public async Task UpdateBeneficiario(BeneficiarioDto beneficiario, int id)
        {
            var beneficiarioItem = await _context.Beneficiarios.FirstOrDefaultAsync(x => x.Id == id);
            using (var sha256 = SHA256.Create())
            {
                if (beneficiarioItem != null && beneficiarioItem.Contrasenya != beneficiario.Contrasenya)
                {
                    beneficiarioItem.PasswordSinHash = beneficiario.Contrasenya;
                    var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(beneficiario.Contrasenya));
                    beneficiario.Contrasenya = Convert.ToBase64String(hash);
                }
            }
            if (beneficiarioItem != null)
            {
                if (beneficiario.Contrasenya != beneficiarioItem.Contrasenya) beneficiarioItem.Contrasenya = beneficiario.Contrasenya;
                if (beneficiario.Descripcion != beneficiarioItem.Descripcion) beneficiarioItem.Descripcion = beneficiario.Descripcion;
                if (beneficiario.Nombre != beneficiarioItem.Nombre) beneficiarioItem.Nombre = beneficiario.Nombre;
                if (beneficiario.Telefono != beneficiarioItem.Telefono) beneficiarioItem.Telefono = (int)beneficiario.Telefono;
                if (beneficiario.Direccion != beneficiarioItem.Direccion) beneficiarioItem.Direccion = beneficiario.Direccion;
                if (beneficiario.Web != beneficiarioItem.Web) beneficiarioItem.Web = beneficiario.Web;
                if (beneficiario.Contacto != beneficiarioItem.Contacto) beneficiarioItem.Contacto = beneficiario.Contacto;
                if (beneficiario.Categoria != beneficiarioItem.Categoria) beneficiarioItem.Categoria = beneficiario.Categoria;
                if (beneficiario.Notificacion != beneficiarioItem.Notificacion) beneficiarioItem.Notificacion = beneficiario.Notificacion;
                if (beneficiario.imgUrl != beneficiarioItem.imgUrl) beneficiarioItem.imgUrl = beneficiario.imgUrl;

                await _context.SaveChangesAsync();
            }

        }

        public async Task GetContrasenya(string email)
        {
            var existBeneficiario = await _context.Beneficiarios.FirstOrDefaultAsync(b => b.Email == email);
            if(existBeneficiario != null) sendEmailRecuperación(existBeneficiario);

        }


        public bool ExisteBeneficiario(Beneficiario beneficiario)
        {
            EdgeOptions options = new EdgeOptions();

            options.AddArgument("--headless");
            options.AddArgument("--start-maximized");

            EdgeDriver driver = new EdgeDriver(options);


            driver.Navigate().GoToUrl("https://www.dateas.com/es/explore/search?entity_name=asociaciones-organizaciones-espana");

            IWebElement campoEntrada = driver.FindElement(By.Id("mainfield"));            
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            js.ExecuteScript("arguments[0].value = 'Caritas';", campoEntrada);
            IWebElement boton = driver.FindElement(By.CssSelector(".button-red-large.button-centered"));
            boton.Click();

            IWebElement tabla = driver.FindElement(By.CssSelector("tbody"));
            IWebElement primeraFila = tabla.FindElement(By.CssSelector("tr"));
            IWebElement enlaceOrganizacion = primeraFila.FindElement(By.CssSelector("td[data-label='Organización'] a"));
            
            driver.Navigate().GoToUrl(enlaceOrganizacion.GetAttribute("href"));

            IWebElement filaCif = driver.FindElement(By.XPath("//tr[th='Cif']"));

            string cif = filaCif.FindElement(By.XPath("td")).Text;

            driver.Close();

            if (cif != beneficiario.CIF) return false;

            return true;



        }
    }
}
