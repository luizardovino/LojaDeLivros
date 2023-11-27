using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LojaDeLivros.Models;
using System.Threading.Tasks;
using System;
using Microsoft.Data.SqlClient;

namespace LojaDeLivros.Pages.Livros
{
    public class CreateModel : PageModel
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

        public CreateModel(LojaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
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

            var autoresSelecionados = await _context.Autores
                .Where(a => AutoresSelecionados.Contains(a.CodAu))
                .ToListAsync();

            Livro.Autores = autoresSelecionados.Select(a => new Livro_Autor
            {
                Autor = a
            }).ToList();

            var assuntosSelecionados = await _context.Assuntos
                .Where(a => AssuntosSelecionados.Contains(a.CodAs))
                .ToListAsync();

            Livro.Assuntos = assuntosSelecionados.Select(a => new Livro_Assunto
            {
                Assunto = a
            }).ToList();

            try
            {
                _context.Livros.Add(Livro);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2601)
                {
                    ModelState.AddModelError(string.Empty, "Já existe um livro com esse código.");
                    return Page();
                }

                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o livro. Por favor, tente novamente.");
                return Page();
            }
        }
    }
}