using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace LojaDeLivros.Models
{
    public class LojaDbContext : DbContext
    {
        public LojaDbContext(DbContextOptions<LojaDbContext> options) : base(options)
        {
        }

        public DbSet<Livro> Livros { get; set; }

        public DbSet<Autor> Autores { get; set; }

        public DbSet<Assunto> Assuntos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Modelo Livro
            modelBuilder.Entity<Livro>()
            .HasKey(l => l.Codl);

            modelBuilder.Entity<Livro>()
                .Property(l => l.Titulo)
                .IsRequired();

            modelBuilder.Entity<Livro>()
                .Property(l => l.Valor)
                .HasColumnType("decimal(18,2)");

            //Modelo Autor
            modelBuilder.Entity<Autor>()
           .HasKey(l => l.CodAu);

            modelBuilder.Entity<Autor>()
                .Property(l => l.Nome)
                .IsRequired();

            //Modelo Assunto
            modelBuilder.Entity<Assunto>()
            .HasKey(l => l.CodAs);

            modelBuilder.Entity<Assunto>()
                .Property(l => l.Descricao)
                .IsRequired();


            //Livro_Autor
            modelBuilder.Entity<Livro_Autor>()
           .HasKey(la => new { la.Livro_Codl, la.Autor_CodAu });


            // Configuração do relacionamento muitos para muitos entre Livro e Autor
            modelBuilder.Entity<Livro_Autor>()
           .HasKey(la => new { la.Livro_Codl, la.Autor_CodAu });


            modelBuilder.Entity<Livro_Autor>()
                .HasOne(la => la.Livro)
                .WithMany(l => l.Autores)
                .HasForeignKey(la => la.Livro_Codl);

            modelBuilder.Entity<Livro_Autor>()
                .HasOne(la => la.Autor)
                .WithMany(a => a.Livros)
                .HasForeignKey(la => la.Autor_CodAu);


            // Configuração do relacionamento muitos para muitos entre Livro e Assunto
            modelBuilder.Entity<Livro_Assunto>()
         .HasKey(la => new { la.Livro_Codl, la.Assunto_CodAs });

            modelBuilder.Entity<Livro_Assunto>()
                .HasOne(la => la.Livro)
                .WithMany(l => l.Assuntos)
                .HasForeignKey(la => la.Livro_Codl);

            modelBuilder.Entity<Livro_Assunto>()
                .HasOne(la => la.Assunto)
                .WithMany(a => a.Livros)
                .HasForeignKey(la => la.Assunto_CodAs);
        }


    }
}
