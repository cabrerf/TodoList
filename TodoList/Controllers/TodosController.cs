using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Interfaces;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {

        List<Todo> todos = new List<Todo>
        {
            new Todo { Id = 1, Descripcion = "Study" },
            new Todo { Id = 2, Descripcion = "Drink coffee" },
            new Todo { Id = 3, Descripcion = "Play football" }
        };


        private readonly IRepositoryTodo _TodoRepository;

        public TodosController(IRepositoryTodo todoRepository)
        {
           _TodoRepository = todoRepository;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            
            return Ok(todos);
        }

        [HttpGet]
        [Route("GetId")]
        public async Task<IActionResult> GetId(int id)
        {
            var miElemento = todos.FirstOrDefault(todo => todo.Id == id);
            
            return Ok(miElemento);
        }

        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> Create(Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var t=  todos.LastOrDefault();

            todo.Id = t.Id + 1;
           
            todos.Add(todo);


            return Ok(todo);
        }

        [HttpPut]
        [Route("Put")]

        public async Task<IActionResult> Put(int id)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }


            var miElemento = todos.FirstOrDefault(todo => todo.Id == id);
            miElemento.Descripcion = "change";

            return Ok(miElemento);



        }


        [HttpDelete]
        [Route("Delete")]

        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var miElemento = todos.RemoveAll(todo => todo.Id == id);

            return Ok("Id" + id.ToString() + "removed");


        }





    }
}
