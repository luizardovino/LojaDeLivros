using LojaDeLivros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaDeLivros.Pages.Livros
{
    public class EditModel : PageModel
    {
        private readonly LojaDbContext _context;

        [BindProperty]
        public Livro Livro { get; set; }

        public List<SelectListItem> Autores { get; set; }

        public List<SelectListItem> Assuntos { get; set; }


        [BindProperty]
        public List<int> AutoresSelecionados { get; set; }


        [BindProperty]
        public List<int> AssuntosSelecionados { get; set; }

        public IList<Livro> Livros { get; set; }

        public EditModel(LojaDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            Livro = await _context.Livros
               .Include(l => l.Autores)
               .ThenInclude(al => al.Autor)
               .Include(l => l.Assuntos)
               .ThenInclude(at => at.Assunto)
               .FirstOrDefaultAsync(m => m.Codl == id);



            if (Livro == null)
            {
                return NotFound();
            }

            AutoresSelecionados = Livro.Autores.Select(al => al.Autor_CodAu).ToList();
            AssuntosSelecionados = Livro.Assuntos.Select(al => al.Assunto_CodAs).ToList();


            Autores = await _context.Autores
            .Select(a => new SelectListItem
            {
                Value = a.CodAu.ToString(),
                Text = a.Nome,
                Selected = AutoresSelecionados.Contains(a.CodAu)
            })
            .ToListAsync();


            Assuntos = await _context.Assuntos
            .Select(a => new SelectListItem
            {
                Value = a.CodAs.ToString(),
                Text = a.Descricao,
                Selected = AssuntosSelecionados.Contains(a.CodAs)
            })
            .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Autores = await _context.Autores
                    .Select(a => new SelectListItem
                    {
                        Value = a.CodAu.ToString(),
                        Text = a.Nome
                    })
                    .ToListAsync();

                Assuntos = await _context.Assuntos
                  .Select(a => new SelectListItem
                  {
                      Value = a.CodAs.ToString(),
                      Text = a.Descricao
                  })
                  .ToListAsync();

                return Page();
            }

            _context.Attach(Livro).State = EntityState.Modified;


            Livros = await _context.Livros.Include(l => l.Autores).ThenInclude(al => al.Autor)
                .Include(l => l.Assuntos)
                .ThenInclude(sl => sl.Assunto).ToListAsync();

            Livro.Autores.Clear();
            Livro.Assuntos.Clear();

            var autoresSelecionados = await _context.Autores
                .Where(a => AutoresSelecionados.Contains(a.CodAu))
                .ToListAsync();

            var assuntosSelecionados = await _context.Assuntos
            .Where(a => AssuntosSelecionados.Contains(a.CodAs))
            .ToListAsync();

            if (autoresSelecionados != null)
            {
                Livro.Autores = autoresSelecionados.Select(a => new Livro_Autor
                {
                    Autor = a
                }).ToList();
            }

            if (assuntosSelecionados != null)
            {
                Livro.Assuntos = assuntosSelecionados.Select(a => new Livro_Assunto
                {
                    Assunto = a
                }).ToList();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(Livro.Codl))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LivroExists(int id)
        {
            return _context.Livros.Any(e => e.Codl == id);
        }
    }
}