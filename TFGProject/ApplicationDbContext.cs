using TFGProject.Models;
using Microsoft.EntityFrameworkCore;

namespace TFGProject
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Beneficiario> Beneficiarios { get; set; }
        public DbSet<Necesita> Necesidades { get; set; }
        public DbSet<Donacion> Donaciones { get; set; }
        public DbSet<Recurso> Recursos { get; set; }
        public DbSet<Certificado> Certificados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>().HasKey(b => b.Id);

            modelBuilder.Entity<Beneficiario>().HasKey(b => b.Id);

            modelBuilder.Entity<Necesita>().HasKey(b => b.Id);
            modelBuilder.Entity<Necesita>()
                .HasOne(z => z.Beneficiario).WithMany(z => z.Necesidades).HasForeignKey(b => b.IdBeneficiario);

            modelBuilder.Entity<Recurso>().HasKey(b => b.Id);
            modelBuilder.Entity<Recurso>()
                .HasOne(z => z.Empresa).WithMany(z => z.Recursos).HasForeignKey(b => b.IdEmpresa);

            modelBuilder.Entity<Certificado>().HasKey(b => b.Id);

            modelBuilder.Entity<Donacion>().HasKey(b => b.Id);
            modelBuilder.Entity<Donacion>()
                .HasOne(z => z.Certificado).WithOne(z => z.Donacion).HasForeignKey<Donacion>(z => z.IdCertificado);
            modelBuilder.Entity<Donacion>()
                .HasOne(z => z.Empresa).WithMany(z => z.Donaciones).HasForeignKey(z => z.IdEmpresa);
            modelBuilder.Entity<Donacion>()
                .HasOne(z => z.Beneficiario).WithMany(z => z.Donaciones).HasForeignKey(z => z.IdBeneficiario);

            modelBuilder.Entity<EmpresasSiguenBeneficiarios>().HasKey(b=> new {b.IdBeneficiario,b.IdEmpresa});
            modelBuilder.Entity<EmpresasSiguenBeneficiarios>().
                HasOne(x=>x.Beneficiario).WithMany(x=>x.EmpresasQueMeSiguen).HasForeignKey(x=>x.IdBeneficiario);
            modelBuilder.Entity<EmpresasSiguenBeneficiarios>().
                HasOne(x=>x.Empresa).WithMany(x=>x.BeneficiariosQueSigo).HasForeignKey(x=>x.IdEmpresa);

            modelBuilder.Entity<BeneficiariosSiguenEmpresa>().HasKey(b => new { b.IdBeneficiario, b.IdEmpresa });
            modelBuilder.Entity<BeneficiariosSiguenEmpresa>().
                HasOne(x=>x.Beneficiario).WithMany(x=>x.EmpresasQueSigo).HasForeignKey(x=>x.IdBeneficiario);
            modelBuilder.Entity<BeneficiariosSiguenEmpresa>().
                HasOne(x=>x.Empresa).WithMany(x=>x.BeneficiariosQueMeSiguen).HasForeignKey(x=>x.IdEmpresa);
        }
    }
}
