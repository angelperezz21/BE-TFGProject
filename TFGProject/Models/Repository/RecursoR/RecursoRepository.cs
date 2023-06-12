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
using Microsoft.EntityFrameworkCore;
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
            beneficiario.Notificacion++;

            sendAceptarEmail(empresa, beneficiario);

            _context.Add(new Donacion
            {
                FechaDonacion = DateTime.Now,
                IdBeneficiario = idBeneficiario,
                IdEmpresa = recurso.IdEmpresa,
                Cantidad = recurso.Cantidad,
                valorTotal = recurso.Cantidad * recurso.Precio,
                MetodoEntrega = recurso.MetodoEntrega,
                NombreRecurso = recurso.Nombre
            });

            await _context.SaveChangesAsync();

            sendComunicarEB(empresa, beneficiario);

            return recurso;
        }

        public void sendSolicitarEmail(Empresa empresa, Beneficiario beneficiario)
        {
            string fromMail = "easyDonationORG@gmail.com";
            string fromPassword = "axvqbrdsmpctynkw";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Solicitar recurso";
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
            string fromMail = "easyDonationORG@gmail.com";
            string fromPassword = "axvqbrdsmpctynkw";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Aceptar recurso";
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

        public void sendComunicarEB(Empresa empresa, Beneficiario beneficiario)
        {
            string fromMail = "easyDonationORG@gmail.com";
            string fromPassword = "axvqbrdsmpctynkw";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Donación";
            message.To.Add(new MailAddress(empresa.Email));
            message.To.Add(new MailAddress(beneficiario.Email));
            message.Body = "<html><body><h1>Notifiación por creación de la doanción</h1><p>Hola "
                + empresa.Nombre + " y " + beneficiario.Nombre +
                "<p>se ha creado la donación, para comunicaros con entre vosotros se os adjuntan vuestros correos y teléfonos.</p>" +
                "<p>Correo: " + beneficiario.Email + " Teléfono: " + beneficiario.Contacto + ".</p>" +
                "<p>Correo: " + empresa.Email + " Teléfono: " + empresa.Contacto + ".</p>" +
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
            var listSolicitantes = recurso.Solicitantes?.Split(',', StringSplitOptions.RemoveEmptyEntries)
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

        public async Task<List<Recurso>> GetSolicitudesRecursos(int id)
        {
            var listRecursos = new List<Recurso>();

            var listAllRecursos = await _context.Recursos.ToListAsync();

            foreach(var rec in listAllRecursos)
            {
                if (rec.Solicitantes != null) { 
                    var listSolicitantes = rec.Solicitantes.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                                .Select(x => int.Parse(x))
                                                .ToList();
                    if (listSolicitantes.Contains(id))
                    {
                        listRecursos.Add(rec);
                    }
                }
            }
            return listRecursos;
        }

        public async Task UpdateRecurso(RecursoDto recursoDto, int id)
        {
            var recursoItem = await _context.Recursos.FirstOrDefaultAsync(x => x.Id == id);
            
            if (recursoItem != null)
            {
                if (recursoDto.Precio != recursoItem.Precio) recursoItem.Precio = recursoDto.Precio;
                if (recursoDto.Descripcion != recursoItem.Descripcion) recursoItem.Descripcion = recursoDto.Descripcion;
                if (recursoDto.Nombre != recursoItem.Nombre) recursoItem.Nombre = recursoDto.Nombre;
                if (recursoDto.Cantidad != recursoItem.Cantidad) recursoItem.Cantidad = recursoDto.Cantidad;
                if (recursoDto.MetodoEntrega != recursoItem.MetodoEntrega) recursoItem.MetodoEntrega = recursoDto.MetodoEntrega;
                if (recursoDto.imgUrl != recursoItem.imgUrl) recursoItem.imgUrl = recursoDto.imgUrl;

                await _context.SaveChangesAsync();
            }
        }
    }
}
