using Logstore_BackEnd;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logstore_FrontEnd
{
    public static class InitializeDatabase
    {
        public static void InitializeDb(this IApplicationBuilder app, bool development = false)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var context = serviceScope.ServiceProvider.GetService<HungryPizzaDbContext>())
            {
                //var count = context.Flavors.Count();

                /*if (context.Flavors.Count() == 0) {
                    context.Flavors.Add(new Logstore_BackEnd.Model.Flavor() { Id = 1, Name = "3 Queijos", Price = 50m });
                    context.Flavors.Add(new Logstore_BackEnd.Model.Flavor() { Id = 2, Name = "Frango com requeijão", Price = 59.99m });
                    context.Flavors.Add(new Logstore_BackEnd.Model.Flavor() { Id = 3, Name = "Mussarela", Price = 42.50m });
                    context.Flavors.Add(new Logstore_BackEnd.Model.Flavor() { Id = 4, Name = "Calabresa", Price = 42.50m });
                    context.Flavors.Add(new Logstore_BackEnd.Model.Flavor() { Id = 5, Name = "Pepperoni", Price = 55m });
                    context.Flavors.Add(new Logstore_BackEnd.Model.Flavor() { Id = 6, Name = "Portuguesa", Price = 45m });
                    context.Flavors.Add(new Logstore_BackEnd.Model.Flavor() { Id = 7, Name = "Veggie", Price = 45m });
                }*/

                
                //context.Flavors.Add(new Logstore_BackEnd.Model.Flavor() { Name = "3 Queijos", Price = 50m });
                //context.Flavors.Add(new Logstore_BackEnd.Model.Flavor() { Name = "Frango com requeijão", Price = 59.99m });
                //context.Flavors.Add(new Logstore_BackEnd.Model.Flavor() { Name = "Mussarela", Price = 42.50m });
                //context.Flavors.Add(new Logstore_BackEnd.Model.Flavor() { Name = "Calabresa", Price = 42.50m });
                //context.Flavors.Add(new Logstore_BackEnd.Model.Flavor() { Name = "Pepperoni", Price = 55m });
                //context.Flavors.Add(new Logstore_BackEnd.Model.Flavor() { Name = "Portuguesa", Price = 45m });
                //context.Flavors.Add(new Logstore_BackEnd.Model.Flavor() { Name = "Veggie", Price = 45m });
                //context.SaveChanges();
            }
        }
    }
}
