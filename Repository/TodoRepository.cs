using Entities;
using Repository.Interfaces;

namespace Repository
{
    public class TodoRepository : IRepositoryTodo
    {

        private static List<Todo> todos = new List<Todo>
        {
            new Todo { Id = 1, Description = "Study" },
            new Todo { Id = 2, Description = "Drink coffee" },
            new Todo { Id = 3, Description = "Play football" }
        };

        public Todo Create(string description)
        {

            try
            {

                var addTodo = new Todo()
                {
                    Id = todos.Max(todo => todo.Id) + 1,
                    Description = description
                };

                todos.Add(addTodo);
                return addTodo;

            }
            catch (Exception)
            {

                throw;
            }


        }

        public int Delete(int id)
        {
            try
            {
                var miElemento = todos.RemoveAll(todo => todo.Id == id);
                return miElemento;

            }
            catch (Exception)
            {

                throw;
            }


        }

        public List<Todo> Get()
        {

            try
            {
                return todos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Todo GetId(int id)
        {
            try
            {

                var miElemento = todos.FirstOrDefault(todo => todo.Id == id);
                return miElemento;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public Todo Put(int id, string description)
        {

            try
            {

                var miElemento = todos.FirstOrDefault(todo => todo.Id == id);

                if (miElemento == null)
                {
                    return null;
                }

                miElemento.Description = description;
                return miElemento;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
