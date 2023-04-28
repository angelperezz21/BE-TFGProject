﻿using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
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
            string fromMail = "easyDonatioORG@gmail.com";
            string fromPassword = "zcybiotsmdqhoxds";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Bienvenido a EasyDonation";
            message.To.Add(new MailAddress(beneficiario.Email));
            message.Body = "<html><body><h1>Bienvenido a nuestro sitio web</h1><p>Hola "
                + beneficiario.Nombre +
                ",</p><p>Gracias por registrarte en nuestro sitio web. Esperamos que disfrutes de nuestros servicios y te sientas como en casa.</p>" +
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
            var existEmpresa = _context.Empresas.FirstOrDefault(b => b.Email == beneficiario.Email);
            if (existEmpresa != null) return null;
            var existBeneficiario = _context.Beneficiarios.FirstOrDefault(b => b.Email == beneficiario.Email);
            if (existBeneficiario != null) return null;
            _context.Add(beneficiario);
            await _context.SaveChangesAsync();
            return beneficiario;
        }

        public async Task NuevoSeguido(int idBeneficiario, int idEmpresa)
        {
            var empresa = await _context.Empresas.FindAsync(idEmpresa);
            var beneficiario = await _context.Beneficiarios.FindAsync(idBeneficiario);

            beneficiario.EmpresasQueSigo.Add(
                new BeneficiariosSiguenEmpresa { Beneficiario = beneficiario, Empresa = empresa });

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
            var empresa = await _context.Beneficiarios.Include(x => x.Donaciones).FirstOrDefaultAsync(x => x.Id == id);
            return empresa.Donaciones.ToList();
        }

        public async Task<Beneficiario> GetBeneficiario(int id)
        {
            return await _context.Beneficiarios.FindAsync(id);
        }

        public async Task UpdateBeneficiario(BeneficiarioDto beneficiario, int id)
        {
            var beneficiarioItem = await _context.Beneficiarios.FirstOrDefaultAsync(x => x.Id == id);

            if (beneficiarioItem != null)
            {
                if (beneficiario.Contrasenya != beneficiarioItem.Email) beneficiarioItem.Email = beneficiario.Contrasenya;
                if (beneficiario.Descripcion != beneficiarioItem.Descripcion) beneficiarioItem.Descripcion = beneficiario.Descripcion;
                if (beneficiario.Nombre != beneficiarioItem.Nombre) beneficiarioItem.Nombre = beneficiario.Nombre;
                if (beneficiario.Telefono != beneficiarioItem.Telefono) beneficiarioItem.Telefono = (int)beneficiario.Telefono;
                if (beneficiario.Direccion != beneficiarioItem.Direccion) beneficiarioItem.Direccion = beneficiario.Direccion;
                if (beneficiario.Web != beneficiarioItem.Web) beneficiarioItem.Web = beneficiario.Web;
                if (beneficiario.Contacto != beneficiarioItem.Contacto) beneficiarioItem.Contacto = beneficiario.Contacto;
                if (beneficiario.Categoria != beneficiarioItem.Categoria) beneficiarioItem.Categoria = beneficiario.Categoria;

                await _context.SaveChangesAsync();
            }

        }
    }
}
