using Logstore_BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logstore_BackEnd.Repository
{
    
    public interface IClientRepository : IRepository<Client>
    {
    }

    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        private readonly HungryPizzaDbContext _context;
        public ClientRepository(HungryPizzaDbContext hungryPizzaDbContext) : base(hungryPizzaDbContext)
        {
            _context = hungryPizzaDbContext;
        }

        public override Task<Client> GetById(int id)
        {
            return base.GetById(id);
        }



    }
}
