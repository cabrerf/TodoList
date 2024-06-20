using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using Repository;
using Repository.Interfaces;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {


        private readonly IRepositoryTodo _todoRepository;
        private readonly IFeatureManager _featureManager;

        public TodosController(IRepositoryTodo todoRepository,IFeatureManager featureManager)
        {
            _todoRepository = todoRepository;
            _featureManager = featureManager;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                if (!await _featureManager.IsEnabledAsync(FeatureFlags.FeatureGet))
                {
                    return NotFound("Feature not enabled"); 
                }



                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);
                }

                var todos = _todoRepository.Get();
                return Ok(todos);


            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet]
        [Route("GetId")]
        public async Task<IActionResult> GetId(int id)
        {
            try
            {

                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);
                }

                var myItem = _todoRepository.GetId(id);

                if (myItem == null)
                {
                    return NotFound($"Item with ID {id} not found.");
                }

                return Ok(myItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> Create(string description)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Todo todo = _todoRepository.Create(description);


                return Ok(todo);


            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }


        }

        [HttpPut]
        [Route("Put")]

        public async Task<IActionResult> Put(int id)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }


                Todo todo = _todoRepository.Put(id);

                if (todo == null)
                {
                    return NotFound($"Item with ID {id} not found.");
                }

                return Ok("Id" + id.ToString() + "updated");


            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }


        [HttpDelete]
        [Route("Delete")]

        public async Task<IActionResult> Delete(int id)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }

                var remove = _todoRepository.Delete(id);

                if (remove == 0)
                {
                    return NotFound($"Item with ID {id} not found.");
                }

                return Ok("Id" + id.ToString() + "removed");

            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
