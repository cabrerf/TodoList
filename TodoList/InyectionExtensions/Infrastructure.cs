using Entities;
using Repository.Interfaces;
using Repository;

namespace TodoList.InyectionExtensions
{
    
        public static class Infrastructure
        {
            public static void AddInfrastructure(this WebApplicationBuilder builder)
            {
                builder.Services.AddScoped<IRepository<Todo>, Repository<Todo>>();

            }
        }
    
}
