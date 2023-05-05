﻿using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using TFGProject.Models.DTO;

namespace TFGProject.Models.Repository.RecursoR
{
    public class RecursoRepository : IRecursoRepository
    {
        private readonly ApplicationDbContext _context;

        public RecursoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Recurso> AddRecurso(Recurso recurso)
        {
            recurso.FechaCreacionRecurso = DateTime.Now;
            _context.Add(recurso);
            await _context.SaveChangesAsync();
            return recurso;
        }

        public async Task DeleteRecurso(Recurso recurso)
        {
            _context.Recursos.Remove(recurso);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Recurso>> GetListRecursosPublicados()
        {
            var empresas = await _context.Empresas.Include(x => x.Recursos).ToListAsync();
            List<Recurso> listRecursos = new List<Recurso>();
            foreach (var empresa in empresas)
            {
                foreach (var rec in empresa.Recursos)
                {
                    if (rec.Estado == 1)
                    {
                        listRecursos.Add(rec);
                    }
                }
            }
                
            return listRecursos;
        }

        public async Task<List<Recurso>> GetListRecursosEmpresa(int id)
        {
            var empresa = await _context.Empresas.Include(x=>x.Recursos).FirstOrDefaultAsync(x => x.Id == id);

            return empresa.Recursos.ToList();
        }

        public async Task<Recurso> GetRecurso(int id)
        {
            return await _context.Recursos.FindAsync(id);
        }

        public async Task<Recurso> SolicitarRecurso(int idRecurso, int idBeneficiario)
        {
            var recurso =  await _context.Recursos.FindAsync(idRecurso);
            var listSolicitantes = recurso.Solicitantes.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                            .Select(x => int.Parse(x))
                                            .ToList();
            recurso.Solicitantes = recurso.Solicitantes + idBeneficiario.ToString() + ",";

            var empresa = await _context.Empresas.FindAsync(recurso.IdEmpresa);
            empresa.Notificacion++;

            var beneficiario = await _context.Beneficiarios.FindAsync(idBeneficiario);

            sendSolicitarEmail(empresa, beneficiario);

            await _context.SaveChangesAsync();
            return recurso;
        }
        public async Task<Recurso> AceptarRecurso(int idRecurso, int idBeneficiario)
        {
            var recurso = await _context.Recursos.FindAsync(idRecurso);
            if (recurso.Estado == 1) recurso.Estado++;
            else return null;
            recurso.Solicitantes = "";

            var empresa = await _context.Empresas.FindAsync(recurso.IdEmpresa);            

            var beneficiario = await _context.Beneficiarios.FindAsync(idBeneficiario);

            sendAceptarEmail(empresa, beneficiario);
            await _context.SaveChangesAsync();
            return recurso;
        }

        public void sendSolicitarEmail(Empresa empresa, Beneficiario beneficiario)
        {
            string fromMail = "easyDonatioORG@gmail.com";
            string fromPassword = "zcybiotsmdqhoxds";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Bienvenido a EasyDonation";
            message.To.Add(new MailAddress(empresa.Email));
            message.Body = "<html><body><h1>Notifiación por solicitud de recurso</h1><p>Hola "
                + empresa.Nombre +
                ",</p><p>El beneficiario </p>" + beneficiario.Nombre + "<p> ha solicitado tu recurso.</p> " + 
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

        public void sendAceptarEmail(Empresa empresa, Beneficiario beneficiario)
        {
            string fromMail = "easyDonatioORG@gmail.com";
            string fromPassword = "zcybiotsmdqhoxds";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Bienvenido a EasyDonation";
            message.To.Add(new MailAddress(beneficiario.Email));
            message.Body = "<html><body><h1>Notifiación por aceptación de recurso</h1><p>Hola "
                + beneficiario.Nombre +
                ",</p><p>La empresa </p>" + empresa.Nombre + "<p> ha aceptado la solicitud del recurso solicitado.</p> " +
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

        public async Task<List<SolicitanteDto>> GetNotificaciones(RecursoDto recurso)
        {
            var listSolicitantes = recurso.Solicitantes.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                          .Select(x => int.Parse(x))
                                          .ToList();

            var listBeneficiarios = new List<SolicitanteDto>();
            foreach (var soli in listSolicitantes)
            {
                var beneficiario = await _context.Beneficiarios.FindAsync(soli);
                listBeneficiarios.Add(new SolicitanteDto { Id = soli, Nombre = beneficiario.Nombre });
            }

            return listBeneficiarios;

        }
    }
}
