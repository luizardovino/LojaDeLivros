using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using LojaDeLivros.Models;

namespace LojaDeLivros.Pages.Relatorios
{
    public class RelatorioLivrosModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public RelatorioLivrosModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnGetLivrosData()
        {
            var livros = ObterDadosLivros();
            return new JsonResult(livros);
        }

        private List<LivroView> ObterDadosLivros()
        {
            var livros = new List<LivroView>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM LivrosView", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var livro = new LivroView
                        {
                            Codl = reader.GetString(reader.GetOrdinal("Codl")),
                            Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                            Editora = reader.GetString(reader.GetOrdinal("Editora")),
                            Edicao = reader.GetString(reader.GetOrdinal("Edicao")),
                            AnoPublicacao = reader.GetString(reader.GetOrdinal("AnoPublicacao")),
                            Valor = reader.GetString(reader.GetOrdinal("Valor")),
                            Descricao = reader.GetString(reader.GetOrdinal("Descricao")),
                            Nome = reader.GetString(reader.GetOrdinal("Nome"))  
                        };

                        livros.Add(livro);
                    }
                }
            }

            return livros;
        }
    }

   
}
