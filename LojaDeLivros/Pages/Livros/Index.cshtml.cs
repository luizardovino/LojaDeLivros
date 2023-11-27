using LojaDeLivros.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaDeLivros.Pages.Livros
{
    public class IndexModel : PageModel
    {
        private readonly LojaDbContext _context;

        public IndexModel(LojaDbContext context)
        {
            _context = context;
        }

        public IList<Livro> Livros { get; set; }

        public async Task OnGetAsync()
        {

            Livros = await _context.Livros.Include(l => l.Autores).ThenInclude(al => al.Autor)
                    .Include(l => l.Assuntos)
                    .ThenInclude(sl => sl.Assunto).ToListAsync();

        }
    }
}