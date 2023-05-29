using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using TFGProject.Models.DTO;

namespace TFGProject.Models.Repository.EmpresaR
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly ApplicationDbContext _context;

        public EmpresaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void sendEmail(Empresa empresa)
        {
            string fromMail = "easyDonationORG@gmail.com";
            string fromPassword = "axvqbrdsmpctynkw";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Bienvenido a EasyDonation";
            message.To.Add(new MailAddress(empresa.Email));
            message.Body = "<html><body><h1>Bienvenido a nuestro sitio web</h1><p>Hola " 
                + empresa.Nombre + 
                ",</p><p>Gracias por registrarte en nuestro sitio web. Esperamos que disfrutes de nuestros servicios y te sientas como en casa.</p>" + 
                "<p>Tus credenciales de inicio de sesión son:</p><ul><li><strong>Email:</strong> " 
                + empresa.Email +
                "</li><li><strong>Contraseña:</strong> " + empresa.Contrasenya + 
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

        public void sendEmailRecuperación(Empresa empresa)
        {
            string fromMail = "easyDonationORG@gmail.com";
            string fromPassword = "axvqbrdsmpctynkw";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Email de recuperación";
            message.To.Add(new MailAddress(empresa.Email));
            message.Body = "<html><body><h1>Email para recuperar tu contraseña</h1><p>Hola "
                + empresa.Nombre +
                ",</p><p>Has solcitado la recuperación de tu contraseña</p>" +
                "<p>Tus credenciales de inicio de sesión son:</p><ul><li><strong>Email:</strong> "
                + empresa.Email +
                "</li><li><strong>Contraseña:</strong> " + empresa.PasswordSinHash +
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


        public async Task<Empresa> AddEmpresa(Empresa empresa)
        {

            var existEmpresa = _context.Empresas.FirstOrDefault(b => b.Email == empresa.Email);
            if (existEmpresa != null) return null;
            var existBeneficiario = _context.Beneficiarios.FirstOrDefault(b => b.Email == empresa.Email);
            if (existBeneficiario != null) return null;

            empresa.PasswordSinHash = empresa.Contrasenya;
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(empresa.Contrasenya));
                empresa.Contrasenya = Convert.ToBase64String(hash);
            }

            _context.Add(empresa);
            sendEmail(empresa);
            await _context.SaveChangesAsync();
            return empresa;
        }

        public bool CheckCIF(string CIF)
        {
            var existEmpresa = _context.Empresas.FirstOrDefault(b => b.CIF == CIF);
            if (existEmpresa != null) return true;
            return false;
        }


        public async Task NuevoSeguido(int idBeneficiario,int idEmpresa)
        {
            var empresa = await _context.Empresas.FindAsync(idEmpresa);
            var beneficiario = await _context.Beneficiarios.FindAsync(idBeneficiario);

            empresa.BeneficiariosQueSigo.Add(
                new EmpresasSiguenBeneficiarios { Beneficiario = beneficiario, Empresa = empresa});

            await _context.SaveChangesAsync();
        }

        public async Task UnfollowSeguido(int idBeneficiario, int idEmpresa)
        {
            var empresa = await _context.Empresas.Include(x=>x.BeneficiariosQueSigo).FirstOrDefaultAsync(x => x.Id == idEmpresa);
            var beneficiario = await _context.Beneficiarios.FindAsync(idBeneficiario);

            var seguida = empresa.BeneficiariosQueSigo.FirstOrDefault(e => e.IdEmpresa == idEmpresa && e.IdBeneficiario == idBeneficiario);

            empresa.BeneficiariosQueSigo.Remove(seguida);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmpresa(Empresa empresa)
        {
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Empresa>> GetListEmpresas()
        {           
            return await _context.Empresas.ToListAsync();
        }

        public async Task<List<Beneficiario>> GetBeneficiariosSeguidos(int id)
        {
            var empresa = await _context.Empresas.Include(x => x.BeneficiariosQueSigo).FirstOrDefaultAsync(x => x.Id == id);
            var beneficiarios = new List<Beneficiario>();
            foreach (var beneSigo in empresa.BeneficiariosQueSigo) {
                beneficiarios.Add(await _context.Beneficiarios.FindAsync(beneSigo.IdBeneficiario));
            }
            return beneficiarios;
        }

        public async Task<List<Donacion>> GetListDonaciones(int id)
        {
            var empresa = await _context.Empresas.Include(x=>x.Donaciones).FirstOrDefaultAsync(x=>x.Id==id);
            List<Donacion> donaciones = new List<Donacion>();
            foreach(var donacion in empresa.Donaciones.ToList())
            {
                if (donacion.Enviada == true && donacion.Recibida == true)
                {
                    donacion.Beneficiario = await _context.Beneficiarios.FindAsync(donacion.IdBeneficiario);
                    donaciones.Add(donacion);
                }
            }
            return donaciones;
        }

        public async Task<List<Donacion>> GetListDonacionesPendientes(int id)
        {
            var empresa = await _context.Empresas.Include(x => x.Donaciones).FirstOrDefaultAsync(x => x.Id == id);
            List<Donacion> donaciones = new List<Donacion>();
            foreach (var donacion in empresa.Donaciones.ToList())
            {
                if (donacion.Enviada == false || donacion.Recibida == false)
                {
                    donacion.Beneficiario = await _context.Beneficiarios.FindAsync(donacion.IdBeneficiario);
                    donaciones.Add(donacion);
                }
            }
            return donaciones;
        }

        public async Task<Empresa> GetEmpresa(int id)
        {
            return await _context.Empresas.FindAsync(id);
        }

        public async Task UpdateEmpresa(EmpresaDto empresa, int id)
        {
            var empresaItem = await _context.Empresas.FirstOrDefaultAsync(x => x.Id == id);
            using (var sha256 = SHA256.Create())
            {
                if (empresaItem != null && empresaItem.Contrasenya!=empresa.Contrasenya)
                {
                    empresaItem.PasswordSinHash = empresa.Contrasenya;
                    var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(empresa.Contrasenya));
                    empresa.Contrasenya = Convert.ToBase64String(hash);
                }              
            }
            if (empresaItem != null)
            {
                if (empresa.Contrasenya != empresaItem.Contrasenya) empresaItem.Contrasenya = empresa.Contrasenya;
                if (empresa.Descripcion != empresaItem.Descripcion) empresaItem.Descripcion = empresa.Descripcion;
                if (empresa.Nombre != empresaItem.Nombre) empresaItem.Nombre = empresa.Nombre;
                if (empresa.Telefono != empresaItem.Telefono) empresaItem.Telefono = (int) empresa.Telefono;
                if (empresa.Direccion != empresaItem.Direccion) empresaItem.Direccion = empresa.Direccion;
                if (empresa.Web != empresaItem.Web) empresaItem.Web = empresa.Web;
                if (empresa.Contacto != empresaItem.Contacto) empresaItem.Contacto = empresa.Contacto;
                if (empresa.Categoria != empresaItem.Categoria) empresaItem.Categoria = empresa.Categoria;
                if (empresa.Notificacion != empresaItem.Notificacion) empresaItem.Notificacion = empresa.Notificacion;
                if (empresa.imgUrl != empresaItem.imgUrl) empresaItem.imgUrl = empresa.imgUrl;

                await _context.SaveChangesAsync();
            }

        }

        public async Task GetContrasenya(string email)
        {
            var existEmpresa = await _context.Empresas.FirstOrDefaultAsync(b => b.Email == email);
            if(existEmpresa!=null) sendEmailRecuperación(existEmpresa);

        }

        public bool ExisteEmpresa(string CIF)
        {

            EdgeOptions options = new EdgeOptions();

            options.AddArgument("--headless");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--start-maximized");
            options.AddArgument("--no-sandbox");


            var driverPath = Path.Combine(Directory.GetCurrentDirectory(), "ejecutable/", "msedge.exe");
          
            EdgeDriver driver = new EdgeDriver(driverPath,options);


            driver.Navigate().GoToUrl("https://sede.registradores.org/site/mercantil");

            IWebElement cookies = driver.FindElement(By.Id("closeCookiesInfo"));

            cookies.Click();

            IWebElement botonBuscar = driver.FindElement(By.LinkText("Buscar por sociedad"));

            botonBuscar.Click();


            IWebElement divEliminar = driver.FindElement(By.Id("captcha-0"));

            driver.ExecuteScript("arguments[0].remove();", divEliminar);


            divEliminar = driver.FindElement(By.Id("ccode"));
            driver.ExecuteScript("arguments[0].remove();", divEliminar);

            IWebElement radioButton = driver.FindElement(By.Id("tipoIdMerc1"));
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].checked = true;", radioButton);

            IWebElement boton = driver.FindElement(By.CssSelector("button[data-qa='buscarMercSubmit']"));

            IWebElement element = driver.FindElement(By.Id("terminoBusquedaMerc"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            js.ExecuteScript("arguments[0].dataset.validationLength='4-15';", element);
            js.ExecuteScript("arguments[0].classList.remove('disable');", element);
            js.ExecuteScript("arguments[0].className = arguments[0].className.replace('error', 'valid');", element);
            js.ExecuteScript("arguments[0].maxlength= '15';", element);
            js.ExecuteScript("arguments[0].dataset.validationHasKeyupEvent = true;", element);
            js.ExecuteScript("arguments[0].dataset.validationErrorLength = 'NIF debe tener entre 4 y 15 caracteres'", element);
            js.ExecuteScript("arguments[0].dataset.validationHasKeyupEvent= true;", element);
            js.ExecuteScript("arguments[0].value =  '" + CIF + "';", element);
            js.ExecuteScript("arguments[0].textContent =  'Valencia Club De Futbol';", element);

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].removeAttribute('disabled')", boton);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].classList.remove('btn--disabled')", boton);

            System.Threading.Thread.Sleep(5000);

            boton.Click();

            IWebElement tdElement = driver.FindElement(By.CssSelector("td[data-qa='resultNif1']"));

            string spanText = tdElement.FindElement(By.TagName("span")).Text;

            driver.Close();

            if (spanText != CIF) return false;

            return true;
            
        }

 
    }
}
