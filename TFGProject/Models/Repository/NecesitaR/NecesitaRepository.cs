using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using TFGProject.Models.DTO;

namespace TFGProject.Models.Repository.NecesitaR
{
    public class NecesitaRepository : INecesitaRepository
    {
        private readonly ApplicationDbContext _context;

        public NecesitaRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Necesita> AddNecesita(Necesita necesita)
        {
            necesita.FechaCreacionNecesita = DateTime.Now;
            _context.Add(necesita);
            await _context.SaveChangesAsync();
            return necesita;
        }

        public async Task DeleteNecesita(Necesita necesita)
        {
            _context.Necesidades.Remove(necesita);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Necesita>> GetListNecesidadesBeneficiario(int id)
        {

            var beneficiario = await _context.Beneficiarios.Include(x => x.Necesidades).FirstOrDefaultAsync(x => x.Id == id);

            return beneficiario.Necesidades.ToList();
        }

        public async Task<List<Necesita>> GetListNecesidadesPublicadas()
        {
            var beneficiarios = await _context.Beneficiarios.Include(x => x.Necesidades).ToListAsync();
            List<Necesita> listNecesidades = new List<Necesita>();
            foreach (var beneficiario in beneficiarios)
            {
                foreach (var ben in beneficiario.Necesidades)
                {
                    if (ben.Estado == 0 || ben.Estado == 1)
                    {
                        listNecesidades.Add(ben);
                    }
                }
            }

            return listNecesidades;
        }

        public async Task<Necesita> GetNecesita(int id)
        {
            return await _context.Necesidades.FindAsync(id);
        }

        public async Task<Necesita> SolicitarNecesidad(int idNecesidad, int idEmpresa)
        {
            var necesita = await _context.Necesidades.FindAsync(idNecesidad);

            necesita.Solicitantes = necesita.Solicitantes + idEmpresa.ToString() + ",";

            var beneficiario = await _context.Beneficiarios.FindAsync(necesita.IdBeneficiario);
            beneficiario.Notificacion++;

            var empresa = await _context.Empresas.FindAsync(idEmpresa);

            sendSolicitarEmail(empresa, beneficiario);

            await _context.SaveChangesAsync();
            return necesita;
        }
        public async Task<Necesita> AceptarNecesidad(int idNecesidad, int idEmpresa)
        {
            var necesita = await _context.Necesidades.FindAsync(idNecesidad);
            if (necesita.Estado == 1) necesita.Estado++;
            else return null;
            necesita.Solicitantes = "";

            var beneficiario = await _context.Beneficiarios.FindAsync(necesita.IdBeneficiario);

            var empresa = await _context.Empresas.FindAsync(idEmpresa);
            empresa.Notificacion++;

            sendAceptarEmail(empresa, beneficiario);

            _context.Add(new Donacion
            {
                FechaDonacion = DateTime.Now,
                IdBeneficiario = necesita.IdBeneficiario,
                IdEmpresa = idEmpresa,
                Cantidad = necesita.Cantidad,
                valorTotal = necesita.Cantidad * necesita.Precio,
                MetodoEntrega = necesita.MetodoEntrega,
                NombreRecurso = necesita.Nombre
            });

            await _context.SaveChangesAsync();

            sendComunicarEB(empresa,beneficiario);

            return necesita;
        }


        public void sendSolicitarEmail(Empresa empresa, Beneficiario beneficiario)
        {
            string fromMail = "easyDonatioORG@gmail.com";
            string fromPassword = "zcybiotsmdqhoxds";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Solicitud Necesidad";
            message.To.Add(new MailAddress(beneficiario.Email));
            message.Body = "<html><body><h1>Notifiación por solicitud de necesidad</h1><p>Hola "
                + beneficiario.Nombre +
                ",</p><p>La empresa </p>" + empresa.Nombre + "<p> ha solicitado tu necesidad.</p> " +
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
            message.Subject = "Aceptar Necesidad";
            message.To.Add(new MailAddress(empresa.Email));
            message.Body = "<html><body><h1>Notifiación por aceptación de necesidad</h1><p>Hola "
                + empresa.Nombre +
                ",</p><p>El beneficiario </p>" + beneficiario.Nombre + "<p> ha aceptado la solicitud de la necesidad solicitado y se va a proceder a realizar la donación.</p> " +
                "<p>A continuación, deberás confirmar el envió de la donación desde el apartado en tu perfil y una vez el " +
                "beneficiario la reciba podrás acceder a su información.</p>" + 
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
            string fromMail = "easyDonatioORG@gmail.com";
            string fromPassword = "zcybiotsmdqhoxds";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Donación";
            message.To.Add(new MailAddress(empresa.Email));
            message.To.Add(new MailAddress(beneficiario.Email));
            message.Body = "<html><body><h1>Notifiación por creación de la doanción</h1><p>Hola "
                + empresa.Nombre + " y "+ beneficiario.Nombre +
                "<p>se ha creado la donación, para comunicaros con entre vosotros se os adjuntan vuestros correos y teléfonos.</p>" +
                "<p>Correo: " +beneficiario.Email + " Teléfono: " + beneficiario.Contacto + ".</p>" +
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

        public async Task<List<SolicitanteDto>> GetNotificaciones(NecesitaDto necesita)
        {
            var listSolicitantes = necesita.Solicitantes?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                          .Select(x => int.Parse(x))
                                          .ToList();

            var listEmpresas = new List<SolicitanteDto>();
            foreach (var soli in listSolicitantes)
            {
                var empresa = (await _context.Empresas.FindAsync(soli));
                listEmpresas.Add(new SolicitanteDto { Id = soli, Nombre = empresa.Nombre });
            }

            return listEmpresas;
        }

        public async Task<List<Necesita>> GetSolicitudesNecesita(int id)
        {
            var listNecesitas = new List<Necesita>();

            var listAllNecesitas = await _context.Necesidades.ToListAsync();

            foreach (var nec in listAllNecesitas)
            {
                if (nec.Solicitantes != null)
                {
                    var listSolicitantes = nec.Solicitantes.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                                .Select(x => int.Parse(x))
                                                .ToList();
                    if (listSolicitantes.Contains(id))
                    {
                        listNecesitas.Add(nec);
                    }
                }
            }
            return listNecesitas;
        }

        public async Task UpdateNecesita(NecesitaDto necesitaDto, int id)
        {
            var necesitaItem = await _context.Necesidades.FirstOrDefaultAsync(x => x.Id == id);

            if (necesitaItem != null)
            {
                if (necesitaDto.Precio != necesitaItem.Precio) necesitaItem.Precio = necesitaDto.Precio;
                if (necesitaDto.Descripcion != necesitaItem.Descripcion) necesitaItem.Descripcion = necesitaDto.Descripcion;
                if (necesitaDto.Nombre != necesitaItem.Nombre) necesitaItem.Nombre = necesitaDto.Nombre;
                if (necesitaDto.Cantidad != necesitaItem.Cantidad) necesitaItem.Cantidad = necesitaDto.Cantidad;
                if (necesitaDto.MetodoEntrega != necesitaItem.MetodoEntrega) necesitaItem.MetodoEntrega = necesitaDto.MetodoEntrega;
                if (necesitaDto.imgUrl != necesitaItem.imgUrl) necesitaItem.imgUrl = necesitaDto.imgUrl;

                await _context.SaveChangesAsync();
            }
        }
    }
}
