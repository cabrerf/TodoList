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

        List<Todo> Get();

        Todo Create(string description);

        int Delete(int id);
        Todo Put(int id);
        Todo GetId(int id);

    }
}
