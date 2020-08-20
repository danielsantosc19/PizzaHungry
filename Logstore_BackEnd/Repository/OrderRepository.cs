using Logstore_BackEnd.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logstore_BackEnd.Repository
{
    
    public interface IOrderRepository : IRepository<Order>
    {
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly HungryPizzaDbContext _context;

        public OrderRepository(HungryPizzaDbContext hungryPizzaDbContext) : base(hungryPizzaDbContext)
        {
            _context = hungryPizzaDbContext;
        }
        public override Task Add(Order entity)
        {

            return base.Add(entity);
        }
        public override async Task<IList<Order>> Get()
        {
            //return await base.Get();
            return await _context.Orders
                .Include(o => o.Items).ThenInclude(f=> f.Flavor1)
                .Include(o => o.Items).ThenInclude(f => f.Flavor2)
                .Include(c => c.Client)
                .ToListAsync();
        }
        public override async Task<Order> GetById(int id)
        {
            return await _context.Orders.Where(o => o.Id == id)
                .Include(o => o.Items).ThenInclude(f => f.Flavor1)
                .Include(o => o.Items).ThenInclude(f => f.Flavor2)
                .Include(c => c.Client)
                .SingleAsync();
        }
        public override async Task<IList<Order>> Get(Expression<Func<Order, bool>> predicate)
        {
            return await _context.Orders.Where(predicate)
                .Include(o => o.Items).ThenInclude(f => f.Flavor1)
                .Include(o => o.Items).ThenInclude(f => f.Flavor2)
                .Include(c => c.Client)
                .ToListAsync();
            //return base.Get(predicate);
        }


    }
}
