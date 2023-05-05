
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using System.Net;
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

        public async Task<Donacion> AddDonacion(Donacion donacion, bool cert)
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


        public HttpResponseMessage GenerarPDFCertificado(Donacion donacion)
        {
            //Document doc = new Document();

            //// Crear un MemoryStream para escribir los bytes del archivo PDF
            //MemoryStream memStream = new MemoryStream();

            //// Crear un escritor de PDF que escribirá en el MemoryStream
            //PdfWriter writer = PdfWriter.GetInstance(doc, memStream);

            //// Abrir el documento para escribir contenido
            //doc.Open();

            //// Agregar contenido al documento (por ejemplo, un párrafo de texto)
            //Paragraph paragraph = new Paragraph("Este es el contenido del archivo PDF que estamos generando.");
            //doc.Add(paragraph);
            //doc.Add(paragraph);
            //doc.Add(paragraph);


            //doc.Add(paragraph);
            //doc.Add(paragraph);

            //// Cerrar el documento
            //doc.Close();

            //// Obtener los bytes del MemoryStream y devolverlos
            //byte[] fileBytes = doc.ToArray();
            //// Crea una respuesta HTTP con el certificado de donación como archivo adjunto
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            //response.Content = new ByteArrayContent(fileBytes);
            //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            //response.Content.Headers.ContentDisposition.FileName = "CertificadoDonacion.pdf";
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            return response;

        }


    }
}
