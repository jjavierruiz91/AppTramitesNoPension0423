﻿// <auto-generated />
using System;
using Aplicativo.net.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aplicativo.net.Migrations
{
    [DbContext(typeof(AplicativoContext))]
    partial class AplicativoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Aplicativo.net.Models.Certificado", b =>
                {
                    b.Property<int>("Codcertificado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Codstramite")
                        .HasColumnType("int");

                    b.Property<string>("Fechacreacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombrecer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codcertificado");

                    b.ToTable("Certificados");
                });

            modelBuilder.Entity("Aplicativo.net.Models.Documento", b =>
                {
                    b.Property<int>("Codocumento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Codstramite")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fechaactualizacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fechacreacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombredoc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Plantilla")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Tamanio")
                        .HasColumnType("float");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codocumento");

                    b.ToTable("Documentos");
                });

            modelBuilder.Entity("Aplicativo.net.Models.Stramite", b =>
                {
                    b.Property<int>("Codstramite")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodFuncionario")
                        .HasColumnType("int");

                    b.Property<int>("Codtramite")
                        .HasColumnType("int");

                    b.Property<int>("Codusuario")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fecha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sectorial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoTramite")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codstramite");

                    b.ToTable("Stramites");
                });

            modelBuilder.Entity("Aplicativo.net.Models.Tramite", b =>
                {
                    b.Property<int>("Codtramite")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Costo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Duracion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modalidad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sectorial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codtramite");

                    b.ToTable("Tramites");
                });

            modelBuilder.Entity("Aplicativo.net.Models.Usuario", b =>
                {
                    b.Property<int>("Codusuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellidos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Celular")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<string>("FechaNacimiento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FechaRegistro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GrupoEtnico")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Municipio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sexo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codusuario");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Aplicativo.net.Models.nopension", b =>
                {
                    b.Property<int>("Codsolicitante")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Identificacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombrecompleto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("estadoCertificado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fechaVencimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("qrPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("totalDescargas")
                        .HasColumnType("int");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Codsolicitante");

                    b.ToTable("Nopension");
                });
#pragma warning restore 612, 618
        }
    }
}
