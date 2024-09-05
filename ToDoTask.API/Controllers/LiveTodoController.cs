using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ToDoTask.Application.DTOs;

namespace ToDoTask.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]

    public class LiveTodoController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LiveTodoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetLiveTodos(int pageNumber = 1, int pageSize = 10)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetStringAsync("https://jsonplaceholder.typicode.com/todos");
            var todos = JsonConvert.DeserializeObject<List<ToDoDto>>(response);
            if (pageNumber <= 0)
            {
                return BadRequest("Page number must be a positive integer.");
            }

            if (pageSize <= 0)
            {
                return BadRequest("Page size must be a positive integer.");
            }
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var MyTodos = todos.Where(t => t.UserId == userId);
             var paginatedTodos = MyTodos.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);



            return Ok(new
            {
                count = MyTodos.Count(),
                data = paginatedTodos

            });
        }
    }
}
