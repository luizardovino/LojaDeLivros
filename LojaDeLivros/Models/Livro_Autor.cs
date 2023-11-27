using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaDeLivros.Models
{
    public class Livro_Autor
    {
        public int Livro_Codl { get; set; }
        public Livro Livro { get; set; }

        public int Autor_CodAu { get; set; }
        public Autor Autor { get; set; }


    }
}
