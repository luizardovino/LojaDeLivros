using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using LojaDeLivros.Models;

namespace LojaDeLivros.Pages
{
    public class RelatorioModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public RelatorioModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();

            //var livros = ObterDadosLivros();
            //return new JsonResult(new { data = livros });
            //return new JsonResult(livros);

        }

        public IActionResult OnGetLivrosData()
        {
            var livros = ObterDadosLivros();
            //return new JsonResult(livros);
            return new JsonResult(new { data = livros });
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
                            Codl = reader.GetOrdinal("Codl").ToString(),
                            Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                            Editora = reader.GetString(reader.GetOrdinal("Editora")),
                            Edicao = reader.GetString(reader.GetOrdinal("Edicao")),
                            AnoPublicacao = reader.GetOrdinal("AnoPublicacao").ToString(),
                            Valor = reader.GetOrdinal("Valor").ToString(),
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
