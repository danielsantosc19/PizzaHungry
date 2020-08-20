using Logstore_BackEnd.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Logstore_BackEnd.Repository
{
    
    public interface IFlavorRepository : IRepository<Flavor>
    {
    }

    public class FlavorRepository : RepositoryBase<Flavor>, IFlavorRepository
    {
        private readonly HungryPizzaDbContext _context;
        public FlavorRepository(HungryPizzaDbContext hungryPizzaDbContext) : base(hungryPizzaDbContext)
        {
            _context = hungryPizzaDbContext;
        }
        /*
        public IEnumerable<Flavor> Get()
        {
            return _context.Flavors.ToList();
        }

        public IEnumerable<Flavor> Get(Expression<Func<Flavor, bool>> predicate)
        {
            return _context.Flavors.Where(predicate);
        }

        public Flavor GetById(Expression<Func<Flavor, bool>> predicate)
        {
            return _context.Flavors.FirstOrDefault(predicate);
        }
        public void Add(Flavor entity)
        {
            _context.Flavors.Add(entity);
        }

        public void Update(Flavor entity)
        {
            _context.Flavors.Update(entity);
        }

        public void Delete(Flavor entity)
        {
            _context.Flavors.Remove(entity);
        }        
        */
    }
}
