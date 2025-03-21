﻿// <auto-generated />
using System;
using LivrariaSouza.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LivrariaSouza.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241209183848_mudarValorUnitParaDecimalTabelaDetalhesVenda")]
    partial class mudarValorUnitParaDecimalTabelaDetalhesVenda
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LivrariaSouza.Models.Models.Carrinho", b =>
                {
                    b.Property<int>("IdCarrinho")
                        .HasColumnType("int");

                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorUnit")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdCarrinho", "LivroId", "IdUsuario");

                    b.HasIndex("IdUsuario");

                    b.HasIndex("LivroId");

                    b.ToTable("Carrinhos");
                });

            modelBuilder.Entity("LivrariaSouza.Models.Models.DetalhesVenda", b =>
                {
                    b.Property<int>("IdDetalhe")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDetalhe"));

                    b.Property<string>("CapaLivro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdRegistroVenda")
                        .HasColumnType("int");

                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ValorUnit")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdDetalhe");

                    b.HasIndex("IdRegistroVenda");

                    b.HasIndex("LivroId");

                    b.ToTable("DetalhesVendas");
                });

            modelBuilder.Entity("LivrariaSouza.Models.Models.Livro", b =>
                {
                    b.Property<int>("LivroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LivroId"));

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CapaLivro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataLancamento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("NumeroPag")
                        .HasColumnType("int");

                    b.Property<int>("QntdEstoque")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ValorCompra")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorVenda")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("LivroId");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("LivrariaSouza.Models.Models.RegistroDeVendas", b =>
                {
                    b.Property<int>("IdCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCompra"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("NomeUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdCompra");

                    b.HasIndex("IdUsuario");

                    b.ToTable("RegistroDeVendas");
                });

            modelBuilder.Entity("LivrariaSouza.Models.Models.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUsuario");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            IdUsuario = 1,
                            Email = "Eduardo.Azoia@valtech.com",
                            Nome = "Eduardo Azoia",
                            Senha = "123456",
                            Telefone = "(48) 91234-8754"
                        });
                });

            modelBuilder.Entity("LivrariaSouza.Models.Models.Carrinho", b =>
                {
                    b.HasOne("LivrariaSouza.Models.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LivrariaSouza.Models.Models.Livro", "Livro")
                        .WithMany()
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Livro");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("LivrariaSouza.Models.Models.DetalhesVenda", b =>
                {
                    b.HasOne("LivrariaSouza.Models.Models.RegistroDeVendas", "RegistroDeVendas")
                        .WithMany()
                        .HasForeignKey("IdRegistroVenda")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LivrariaSouza.Models.Models.Livro", "Livro")
                        .WithMany()
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Livro");

                    b.Navigation("RegistroDeVendas");
                });

            modelBuilder.Entity("LivrariaSouza.Models.Models.RegistroDeVendas", b =>
                {
                    b.HasOne("LivrariaSouza.Models.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
