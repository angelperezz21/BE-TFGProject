﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TFGProject;

#nullable disable

namespace TFGProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230505084332_certificadoDonacion")]
    partial class certificadoDonacion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TFGProject.Models.Beneficiario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1000);

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contacto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contrasenya")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Notificacion")
                        .HasColumnType("int");

                    b.Property<string>("PasswordSinHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.Property<string>("Web")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Beneficiarios");
                });

            modelBuilder.Entity("TFGProject.Models.BeneficiariosSiguenEmpresa", b =>
                {
                    b.Property<int>("IdBeneficiario")
                        .HasColumnType("int");

                    b.Property<int>("IdEmpresa")
                        .HasColumnType("int");

                    b.HasKey("IdBeneficiario", "IdEmpresa");

                    b.HasIndex("IdEmpresa");

                    b.ToTable("BeneficiariosSiguenEmpresa");
                });

            modelBuilder.Entity("TFGProject.Models.Donacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaDonacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdBeneficiario")
                        .HasColumnType("int");

                    b.Property<int>("IdEmpresa")
                        .HasColumnType("int");

                    b.Property<string>("MetodoEntrega")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreRecurso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("valorTotal")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("IdBeneficiario");

                    b.HasIndex("IdEmpresa");

                    b.ToTable("Donaciones");
                });

            modelBuilder.Entity("TFGProject.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contacto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contrasenya")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Notificacion")
                        .HasColumnType("int");

                    b.Property<string>("PasswordSinHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.Property<string>("Web")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("TFGProject.Models.EmpresasSiguenBeneficiarios", b =>
                {
                    b.Property<int>("IdBeneficiario")
                        .HasColumnType("int");

                    b.Property<int>("IdEmpresa")
                        .HasColumnType("int");

                    b.HasKey("IdBeneficiario", "IdEmpresa");

                    b.HasIndex("IdEmpresa");

                    b.ToTable("EmpresasSiguenBeneficiarios");
                });

            modelBuilder.Entity("TFGProject.Models.Necesita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<bool>("Certificado")
                        .HasColumnType("bit");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacionRecurso")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdBeneficiario")
                        .HasColumnType("int");

                    b.Property<int?>("IdSolicitante")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Precio")
                        .HasColumnType("float");

                    b.Property<string>("Solicitantes")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdBeneficiario");

                    b.ToTable("Necesidades");
                });

            modelBuilder.Entity("TFGProject.Models.Recurso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<bool>("Certificado")
                        .HasColumnType("bit");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacionRecurso")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdEmpresa")
                        .HasColumnType("int");

                    b.Property<string>("MetodoEntrega")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Precio")
                        .HasColumnType("float");

                    b.Property<string>("Solicitantes")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdEmpresa");

                    b.ToTable("Recursos");
                });

            modelBuilder.Entity("TFGProject.Models.BeneficiariosSiguenEmpresa", b =>
                {
                    b.HasOne("TFGProject.Models.Beneficiario", "Beneficiario")
                        .WithMany("EmpresasQueSigo")
                        .HasForeignKey("IdBeneficiario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TFGProject.Models.Empresa", "Empresa")
                        .WithMany("BeneficiariosQueMeSiguen")
                        .HasForeignKey("IdEmpresa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beneficiario");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("TFGProject.Models.Donacion", b =>
                {
                    b.HasOne("TFGProject.Models.Beneficiario", "Beneficiario")
                        .WithMany("Donaciones")
                        .HasForeignKey("IdBeneficiario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TFGProject.Models.Empresa", "Empresa")
                        .WithMany("Donaciones")
                        .HasForeignKey("IdEmpresa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beneficiario");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("TFGProject.Models.EmpresasSiguenBeneficiarios", b =>
                {
                    b.HasOne("TFGProject.Models.Beneficiario", "Beneficiario")
                        .WithMany("EmpresasQueMeSiguen")
                        .HasForeignKey("IdBeneficiario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TFGProject.Models.Empresa", "Empresa")
                        .WithMany("BeneficiariosQueSigo")
                        .HasForeignKey("IdEmpresa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beneficiario");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("TFGProject.Models.Necesita", b =>
                {
                    b.HasOne("TFGProject.Models.Beneficiario", "Beneficiario")
                        .WithMany("Necesidades")
                        .HasForeignKey("IdBeneficiario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beneficiario");
                });

            modelBuilder.Entity("TFGProject.Models.Recurso", b =>
                {
                    b.HasOne("TFGProject.Models.Empresa", "Empresa")
                        .WithMany("Recursos")
                        .HasForeignKey("IdEmpresa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("TFGProject.Models.Beneficiario", b =>
                {
                    b.Navigation("Donaciones");

                    b.Navigation("EmpresasQueMeSiguen");

                    b.Navigation("EmpresasQueSigo");

                    b.Navigation("Necesidades");
                });

            modelBuilder.Entity("TFGProject.Models.Empresa", b =>
                {
                    b.Navigation("BeneficiariosQueMeSiguen");

                    b.Navigation("BeneficiariosQueSigo");

                    b.Navigation("Donaciones");

                    b.Navigation("Recursos");
                });
#pragma warning restore 612, 618
        }
    }
}
