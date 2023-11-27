using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaDeLivros.Models
{
    public class Livro_Assunto
    {
        public int Livro_Codl { get; set; }
        public Livro Livro { get; set; }

        public int Assunto_CodAs { get; set; }
        public Assunto Assunto { get; set; }


    }
}
