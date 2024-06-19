using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepositoryTodo
    {

        Task<IEnumerable<Todo>> Get();

        Task<Todo> Create(Todo todo);

        Task<Todo> Delete(int id);
        Task<Todo> Put(int id);
        Task<Todo> GetId(int id);

    }
}
