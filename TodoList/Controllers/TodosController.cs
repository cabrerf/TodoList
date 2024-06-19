using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Repository;
using Repository.Interfaces;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {


        private readonly IRepositoryTodo _TodoRepository;

        public TodosController(IRepositoryTodo todoRepository)
        {
           _TodoRepository = todoRepository;
        }

        [HttpGet]
        [FeatureGate("FeatureGet")]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("CustomError", "Disabled.");
                return BadRequest(ModelState);
            }


            List<Todo> todos = _TodoRepository.Get();
            return Ok(todos);

           
        }

        [HttpGet]
        [Route("GetId")]
        public async Task<IActionResult> GetId(int id)
        {
            var myTodo = _TodoRepository.GetId(id);
            
            return Ok(myTodo);
        }

        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> Create(Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            _TodoRepository.Create(todo);


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


           _TodoRepository.Put(id);

            return Ok("Id" + id.ToString() + "updated");



        }


        [HttpDelete]
        [Route("Delete")]

        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            _TodoRepository.Delete(id);

            return Ok("Id" + id.ToString() + "removed");


        }





    }
}
