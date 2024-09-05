using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoTask.Application.DTOs;
using ToDoTask.Application.Interfaces;

namespace ToDoTask.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]

    public class TodoController : ControllerBase
    { 
        private readonly IToDoService _toDoService;
     
    public TodoController(IToDoService toDoService)
    {
            _toDoService = toDoService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoById(int id)
    {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var existingTodo = await _toDoService.GetToDoByIdAsync(id);
            if (existingTodo == null) return NotFound();

            if (existingTodo.UserId != userId) return Forbid();
            
        return Ok(existingTodo);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTodos(int pageNumber = 1, int pageSize = 10)
    {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if (pageNumber <= 0) return BadRequest("Page number must be a positive integer.");
            

            if (pageSize <= 0) return BadRequest("Page size must be a positive integer.");
           
            var todos = await _toDoService.GetAllToDosAsync(pageNumber, pageSize, userId);
            return Ok(todos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo(CreateToDoDto todoDto )
    {
          var userId= Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type ==ClaimTypes.NameIdentifier)?.Value);
           await _toDoService.AddToDoAsync(todoDto, userId);
        return Ok();
    }

        [HttpPut]
        public async Task<IActionResult> UpdateTodo(UpdateToDoDto todoDto)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            // Fetch existing ToDo to validate ownership
            var existingTodo = await _toDoService.GetToDoByIdAsync(todoDto.Id);
            if (existingTodo == null)
            {
                return NotFound();
            }

            if (existingTodo.UserId != userId)
            {
                return Forbid();
            }

            // Update the ToDo item
            var updatedTodo = await _toDoService.UpdateToDoAsync( todoDto, userId);
            if (updatedTodo == null)
            {
                return BadRequest("Failed to update the ToDo.");
            }

            return Ok(updatedTodo);
        }

        [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoAsync(int id)
    {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var ToDo =  await _toDoService.DeleteToDoAsync(id);
            if (ToDo == null)
                return NotFound();
            if (ToDo.UserId != userId)
            {
                return Forbid();
            }

            return Ok();
    }
 }
}

