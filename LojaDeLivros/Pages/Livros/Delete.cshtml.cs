using LojaDeLivros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LojaDeLivros.Pages.Livros
{
    public class DeleteModel : PageModel
    {
        private readonly LojaDbContext _context;

        [BindProperty]
        public Livro Livro { get; set; }

        public DeleteModel(LojaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Livro = await _context.Livros.Include(l => l.Autores).ThenInclude(al => al.Autor)
            .Include(l => l.Assuntos)
            .ThenInclude(sl => sl.Assunto).FirstOrDefaultAsync(m => m.Codl == id);


            if (Livro == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var livro = await _context.Livros
                .Include(l => l.Autores)
                .FirstOrDefaultAsync(m => m.Codl == Livro.Codl);

            if (livro != null)
            {
                try
                {   
                    _context.Livros.Remove(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException is SqlException sqlException && (sqlException.Number == 547 || sqlException.Number == 2601))
                    {
                        ModelState.AddModelError(string.Empty, "N�o � poss�vel excluir o Livro devido a restri��es de chave estrangeira.");
                        return Page();
                    }

                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}