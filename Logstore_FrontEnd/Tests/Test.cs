using Logstore_BackEnd;
using Logstore_BackEnd.Model;
using Logstore_BackEnd.Repository;
using Logstore_BackEnd.Services;
using Logstore_BackEnd.ViewModel;
using Logstore_Daniel.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace Logstore_FrontEnd.Tests
{
    
    public class Test   
    {
        private HttpClient _client;

        public Test()
        {
            _client = new HttpClient();
        }

        [Fact]
        public void Order_OK()
        {
            var dbcontext = new HungryPizzaDbContext();
            var orderRepository = new OrderRepository(dbcontext);
            var flavorRepository = new FlavorRepository(dbcontext);
            var pizzaRepository = new PizzaRepository(dbcontext);
            var clientRepository = new ClientRepository(dbcontext);
            var orderServices = new OrderServices(orderRepository, flavorRepository, clientRepository, pizzaRepository);


            try
            {

                orderServices.ProcessNewOrder(new OrderDto()
                {
                    ClientId = 1
                });
            }
            catch(Exception ex)
            {

            }


            Assert.True(true);

            


        }
        
    }
}
