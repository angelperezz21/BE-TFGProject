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
            doc.Add(new Paragraph("Hola " + "juan" + ", bienvenido al mundo de iTextSharp."));

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


    }
}
