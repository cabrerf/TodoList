using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
  
        public interface IRepository<T> where T : class
        {
            Task<IEnumerable<T>> Get();
            Task<T> Create(string description);
            Task<bool> Delete(int id);
            Task<T?> Put(int id, string description);
            Task<T?> GetId(int id);
        }

   
}
