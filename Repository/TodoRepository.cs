using Entities;
using Repository.Interfaces;

namespace Repository
{
    public class TodoRepository  :IRepositoryTodo
    {

        List<Todo> todos = new List<Todo>
        {
            new Todo { Id = 1, Descripcion = "Study" },
            new Todo { Id = 2, Descripcion = "Drink coffee" },
            new Todo { Id = 3, Descripcion = "Play football" }
        };

        public Task<Todo> Create(Todo todo)
        {
            throw new NotImplementedException();
        }

        public Task<Todo> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Todo>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Todo> GetId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Todo> Put(int id)
        {
            throw new NotImplementedException();
        }
    }
}
