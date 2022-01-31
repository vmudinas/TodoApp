using Microsoft.AspNetCore.Mvc;
using TodoData.Models;
using TodoServices;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService, ILogger<TodoController> logger)
        {
            _todoService = todoService;
            _logger = logger;
        }

        [HttpGet(Name = "GetTodoItemById")]
        //[Route("GetALlTodos")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_todoService.GetAllTodos());
        }

        //// POST /api/todo
        [HttpPost]
        public async Task<IActionResult> AddTodo(string text)
        {
            if (string.IsNullOrEmpty(text)) return BadRequest();

            await _todoService.Add(text);
            return Ok();
        }

        //// POST /api/todo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(Guid id, [FromBody] TodoItem updatedData)
        {

            await _todoService.UpdateTodo(updatedData);

            return Ok();
        }

        //// DELETE /api/todo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {

            try
            {
                await _todoService.DeleteTodo(id);
            
            }
           catch (Exception ex)
           {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
           }

            return Ok();
        }
    }
}