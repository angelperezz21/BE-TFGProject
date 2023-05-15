using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Headers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TFGProject.Models.Repository.DonacionR
{
    public class DonacionRepository : IDonacionRepository
    {
        private readonly ApplicationDbContext _context;        
        public DonacionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Donacion> AddDonacion(Donacion donacion)
        {
            _context.Add(donacion);
            await _context.SaveChangesAsync();
            return donacion;
        }

        public async Task<List<Donacion>> GetListDonacions()
        {
            return await _context.Donaciones.ToListAsync();
        }

        public async Task<Donacion> GetDonacion(int id)
        {
            return await _context.Donaciones.Include(x=>x.Beneficiario).Include(x=>x.Empresa).FirstOrDefaultAsync(x=>x.Id==id);
        }


        public async Task<string> GenerarPDFCertificado(Donacion donacion, String ruta)
        {


            var fileName = "CertificadoDonacion" + donacion.IdEmpresa.ToString() + ".pdf";
            var filePath = Path.Combine(ruta, "certificados", "CertificadoDonacion.pdf");
            Document doc = new Document();
            MemoryStream memStream = new MemoryStream();
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            PdfWriter writer = PdfWriter.GetInstance(doc, memStream);

            doc.Open();
            Paragraph encabezado = new Paragraph("CERTIFICADO DE DONACIÓN");
            encabezado.Alignment = Element.ALIGN_CENTER;
            encabezado.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16f);
            encabezado.SpacingAfter = 20f;
            doc.Add(encabezado);

            // Agregar información de la empresa
            Paragraph infoEmpresa = new Paragraph();
            infoEmpresa.Add(new Phrase("EMPRESA DONANTE"));
            infoEmpresa.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f);
            infoEmpresa.SpacingAfter = 10f;
            doc.Add(infoEmpresa);

            // Agregar nombre de la empresa
            Paragraph nombreEmpresa = new Paragraph("Nombre de la Empresa: " + donacion.Empresa.Nombre);
            nombreEmpresa.SpacingAfter = 5f;
            doc.Add(nombreEmpresa);

            // Agregar dirección de la empresa
            Paragraph direccionEmpresa = new Paragraph("Dirección de la Empresa: " + donacion.Empresa.Direccion);
            direccionEmpresa.SpacingAfter = 5f;
            doc.Add(direccionEmpresa);

            Paragraph infoOng = new Paragraph();
            infoOng.Add(new Phrase("ONG BENEFICIARIA"));
            infoOng.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f);
            infoOng.SpacingBefore = 20f;
            infoOng.SpacingAfter = 10f;
            doc.Add(infoOng);

            // Agregar nombre de la ONG
            Paragraph nombreOng = new Paragraph("Nombre de la ONG: " + donacion.Beneficiario.Nombre);
            nombreOng.SpacingAfter = 5f;
            doc.Add(nombreOng);

            // Agregar dirección de la ONG
            Paragraph direccionOng = new Paragraph("Dirección de la ONG" + donacion.Beneficiario.Direccion);
            direccionOng.SpacingAfter = 5f;
            doc.Add(direccionOng);

            // Agregar información de la donación
            Paragraph infoDonacion = new Paragraph();
            infoDonacion.Add(new Phrase("DATOS DE LA DONACIÓN"));
            infoDonacion.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f);
            infoDonacion.SpacingBefore = 20f;
            infoDonacion.SpacingAfter = 10f;
            doc.Add(infoDonacion);

            // Agregar fecha de la donación
            Paragraph fechaDonacion = new Paragraph("Que la entidad " + donacion.Empresa.Nombre + " con CIF " + donacion.Empresa.CIF + 
                " y " +
                "domicilio en " + donacion.Empresa.Direccion+ " ha realizado, con fecha " + donacion.FechaDonacion.ToShortDateString() + 
                " una aportación económica a esta Entidad, " + donacion.Beneficiario.Nombre +
                ", CIF " + donacion.Beneficiario.CIF + " y domicilio en " + donacion.Beneficiario.Direccion + 
                " que figura como entidad beneficiaria del mecenazgo en el artº 16.c de la Ley 49/2002, de 23 de diciembre, "
                +
                "de régimen fiscal de las entidades sin fines lucrativos y de los incentivos fiscales al mecenazgo," + 
                " por importe de " + donacion.valorTotal + " euros usados para " + donacion.NombreRecurso +
                ", teniendo dicha donación la consideración de irrevocable, sin perjuicio de lo establecido en las normas imperativas civiles que regulan la revocación de donaciones.");
            fechaDonacion.Alignment = Element.ALIGN_JUSTIFIED;
            fechaDonacion.SpacingAfter = 5f;
            doc.Add(fechaDonacion);

            // Agregar recurso donado
            Paragraph recursoDonado = new Paragraph("Y para que conste, a los efectos previstos en la Ley 49/2002, de 23 de diciembre,"+
                " de régimen fiscal de las entidades sin fines lucrativos y de los incentivos fiscales al mecenazgo, " + 
                "se expide el presente certificado en fecha " + donacion.FechaDonacion.ToShortDateString());
            recursoDonado.SpacingAfter = 5f;
            recursoDonado.Alignment = Element.ALIGN_JUSTIFIED;
            doc.Add(recursoDonado);

            doc.Close();

            var empresaPassword = await _context.Empresas.FindAsync(donacion.IdEmpresa);

            string WorkingFolder = ruta + "/certificados/";
            string InputFile = Path.Combine(WorkingFolder, "CertificadoDonacion.pdf");
            string OutputFile = Path.Combine(WorkingFolder, "CertificadoDonacion13.pdf");

            using (Stream input = new FileStream(InputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (Stream output = new FileStream(OutputFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfReader reader = new PdfReader(input);
                    PdfEncryptor.Encrypt(reader, output, true, empresaPassword.PasswordSinHash, "secret", PdfWriter.ALLOW_SCREENREADERS);
                }
            }
            var certificadoPath = $"certificados/{fileName}";

            return certificadoPath;

        }

        public async Task<Donacion> UpdateEnvioDonacion(int id)
        {
            var donacion = await _context.Donaciones.FindAsync(id);
            if (donacion.Enviada) return null;
            donacion.Enviada = true;
            await _context.SaveChangesAsync();
            return donacion;
        }

        public async Task<Donacion> UpdateRecibirDonacion(int id)
        {
            var donacion = await _context.Donaciones.FindAsync(id);
            if (!donacion.Enviada || donacion.Recibida) return null;
            donacion.Recibida = true;
            await _context.SaveChangesAsync();
            return donacion;
        }
    }
}
