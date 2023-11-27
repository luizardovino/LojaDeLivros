using System;
using System.Linq;

namespace LojaDeLivros.Models
{
    public class DbInitializer
    {
        public static void Initialize(LojaDbContext context)
        {
            //context.Database.EnsureCreated(); // Garante que o banco de dados foi criado

            //// Verifica se já existem Livros cadastrados
            //if (context.Livros.Any())
            //{
            //    return; // O banco de dados já foi inicializado
            //}

        }
    }
}
