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
    [Migration("20230420101306_newPropertiesDonacion")]
    partial class newPropertiesDonacion
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
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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
                        .IsRequired()
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

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.Property<string>("Web")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Beneficiarios");
                });

            modelBuilder.Entity("TFGProject.Models.Certificado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ruta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Certificados");
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

                    b.Property<int?>("IdCertificado")
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

                    b.HasIndex("IdCertificado")
                        .IsUnique()
                        .HasFilter("[IdCertificado] IS NOT NULL");

                    b.HasIndex("IdEmpresa");

                    b.ToTable("Donaciones");
                });

            modelBuilder.Entity("TFGProject.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contacto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contrasenya")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
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

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Web")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("idEmpresa")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("TFGProject.Models.Necesita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("IdBeneficiario")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Precio")
                        .HasColumnType("float");

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

                    b.HasKey("Id");

                    b.HasIndex("IdEmpresa");

                    b.ToTable("Recursos");
                });

            modelBuilder.Entity("TFGProject.Models.Donacion", b =>
                {
                    b.HasOne("TFGProject.Models.Beneficiario", "Beneficiario")
                        .WithMany("Donaciones")
                        .HasForeignKey("IdBeneficiario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TFGProject.Models.Certificado", "Certificado")
                        .WithOne("Donacion")
                        .HasForeignKey("TFGProject.Models.Donacion", "IdCertificado");

                    b.HasOne("TFGProject.Models.Empresa", "Empresa")
                        .WithMany("Donaciones")
                        .HasForeignKey("IdEmpresa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beneficiario");

                    b.Navigation("Certificado");

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

                    b.Navigation("Necesidades");
                });

            modelBuilder.Entity("TFGProject.Models.Certificado", b =>
                {
                    b.Navigation("Donacion")
                        .IsRequired();
                });

            modelBuilder.Entity("TFGProject.Models.Empresa", b =>
                {
                    b.Navigation("Donaciones");

                    b.Navigation("Recursos");
                });
#pragma warning restore 612, 618
        }
    }
}
