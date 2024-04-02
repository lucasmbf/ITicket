﻿// <auto-generated />
using ITicket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ITicket.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20240402123641_criacaoBDSQlite")]
    partial class criacaoBDSQlite
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("ITicket.Models.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Departamento")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Gestor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdm")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAtendente")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSolicitante")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Ramal")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdUsuario");

                    b.ToTable("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
