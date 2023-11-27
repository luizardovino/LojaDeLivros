using System.Collections.Generic;
using System.Linq;
using LojaDeLivros.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaDeLivros.Pages
{
    public class ServicoBancoDeDados
    {
        private readonly LojaDbContext _dbContext;

        public ServicoBancoDeDados(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}