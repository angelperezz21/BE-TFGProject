
//using iTextSharp.text;
//using iTextSharp.text.pdf;


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


            //MemoryStream memoryStream = new MemoryStream();
            //Document doc = new Document(new PdfDocument(new PdfWriter("C:/Users/angel/Desktop/TFG/TFGProject/TFGProject/")));            

            //// Agregar el contenido del certificado de donación
            //Paragraph paragraph = new Paragraph();
            //paragraph.Add("Certificado de Donación\n\n");
            //paragraph.Add($"Donante: {donacion.Empresa.Nombre}\n");
            //paragraph.Add($"Precio: {donacion.valorTotal}\n");
            //paragraph.Add($"Fecha: {donacion.FechaDonacion}\n");
            //paragraph.Add($"Organización beneficiaria: {donacion.Beneficiario.Nombre}\n\n");
            //paragraph.Add("Gracias por su donación.");

            //doc.Add(paragraph);
            //doc.Close();

            // Crear una respuesta HTTP con el archivo PDF como contenido
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            //response.Content = new ByteArrayContent(memoryStream.ToArray());
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            //response.Content.Headers.ContentDisposition.FileName = "CertificadoDonacion.pdf";

            return response;

        }


    }
}
