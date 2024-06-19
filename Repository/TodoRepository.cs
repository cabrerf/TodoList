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

        public Todo Create(Todo todo)
        {
            var t = todos.LastOrDefault();

            if(t!=null) {
                todo.Id = t.Id + 1;

                todos.Add(todo);
            }
           

            return todo;
        }

        public string Delete(int id)
        {
            var miElemento = todos.RemoveAll(todo => todo.Id == id);
            return "OK";
        }

        public List<Todo> Get()
        {
            return todos;
        }

        public Todo GetId(int id)
        {
            var miElemento = todos.FirstOrDefault(todo => todo.Id == id);
            return miElemento;  
        }

        public Todo Put(int id)
        {
            var miElemento = todos.FirstOrDefault(todo => todo.Id == id);
            miElemento.Descripcion = "example change";
            return miElemento;
        }
    }
}
