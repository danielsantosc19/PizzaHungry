using Logstore_BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Logstore_BackEnd.Repository
{
    
    public interface IPizzaRepository : IRepository<Pizza>
    {
    }

    public class PizzaRepository : RepositoryBase<Pizza>, IPizzaRepository
    {
        private readonly HungryPizzaDbContext _context;
        public PizzaRepository(HungryPizzaDbContext hungryPizzaDbContext) : base(hungryPizzaDbContext)
        {
            _context = hungryPizzaDbContext;
        }              
        
    }
}
