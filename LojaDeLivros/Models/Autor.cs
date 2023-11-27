﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaDeLivros.Models
{
    public class Autor
    {
        public int CodAu { get; set; }

        [Required(ErrorMessage = "O campo nome do Autor é obrigatório.")]
        [StringLength(40, ErrorMessage = "O campo Nome deve ter no máximo 40 caracteres.")]
        [Column(TypeName = "varchar(40)")]
        public string Nome { get; set; }

        public ICollection<Livro_Autor>? Livros { get; set; }
    }

}
