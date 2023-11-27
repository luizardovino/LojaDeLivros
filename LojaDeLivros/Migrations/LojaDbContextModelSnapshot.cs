﻿// <auto-generated />
using LojaDeLivros.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LojaDeLivros.Migrations
{
    [DbContext(typeof(LojaDbContext))]
    partial class LojaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LojaDeLivros.Models.Assunto", b =>
                {
                    b.Property<int>("CodAs")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodAs"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(40)");

                    b.HasKey("CodAs");

                    b.ToTable("Assuntos");
                });

            modelBuilder.Entity("LojaDeLivros.Models.Autor", b =>
                {
                    b.Property<int>("CodAu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodAu"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(40)");

                    b.HasKey("CodAu");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("LojaDeLivros.Models.Livro", b =>
                {
                    b.Property<int>("Codl")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Codl"));

                    b.Property<string>("AnoPublicacao")
                        .IsRequired()
                        .HasColumnType("varchar(4)");

                    b.Property<string>("Edicao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Editora")
                        .IsRequired()
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(40)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Codl");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("LojaDeLivros.Models.Livro_Assunto", b =>
                {
                    b.Property<int>("Livro_Codl")
                        .HasColumnType("int");

                    b.Property<int>("Assunto_CodAs")
                        .HasColumnType("int");

                    b.HasKey("Livro_Codl", "Assunto_CodAs");

                    b.HasIndex("Assunto_CodAs");

                    b.ToTable("Livro_Assunto");
                });

            modelBuilder.Entity("LojaDeLivros.Models.Livro_Autor", b =>
                {
                    b.Property<int>("Livro_Codl")
                        .HasColumnType("int");

                    b.Property<int>("Autor_CodAu")
                        .HasColumnType("int");

                    b.HasKey("Livro_Codl", "Autor_CodAu");

                    b.HasIndex("Autor_CodAu");

                    b.ToTable("Livro_Autor");
                });

            modelBuilder.Entity("LojaDeLivros.Models.Marca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Marcas");
                });

            modelBuilder.Entity("LojaDeLivros.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MarcaId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Preco")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MarcaId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("LojaDeLivros.Models.Livro_Assunto", b =>
                {
                    b.HasOne("LojaDeLivros.Models.Assunto", "Assunto")
                        .WithMany("Livros")
                        .HasForeignKey("Assunto_CodAs")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LojaDeLivros.Models.Livro", "Livro")
                        .WithMany("Assuntos")
                        .HasForeignKey("Livro_Codl")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assunto");

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("LojaDeLivros.Models.Livro_Autor", b =>
                {
                    b.HasOne("LojaDeLivros.Models.Autor", "Autor")
                        .WithMany("Livros")
                        .HasForeignKey("Autor_CodAu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LojaDeLivros.Models.Livro", "Livro")
                        .WithMany("Autores")
                        .HasForeignKey("Livro_Codl")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autor");

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("LojaDeLivros.Models.Produto", b =>
                {
                    b.HasOne("LojaDeLivros.Models.Marca", "Marca")
                        .WithMany()
                        .HasForeignKey("MarcaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marca");
                });

            modelBuilder.Entity("LojaDeLivros.Models.Assunto", b =>
                {
                    b.Navigation("Livros");
                });

            modelBuilder.Entity("LojaDeLivros.Models.Autor", b =>
                {
                    b.Navigation("Livros");
                });

            modelBuilder.Entity("LojaDeLivros.Models.Livro", b =>
                {
                    b.Navigation("Assuntos");

                    b.Navigation("Autores");
                });
#pragma warning restore 612, 618
        }
    }
}
