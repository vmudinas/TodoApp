using Microsoft.Extensions.Logging;
using TodoData;
using TodoData.Models;

namespace TodoServices
{
    public class TodoService : ITodoService
    {
        private ApplicationContext _context;
        private readonly ILogger<TodoService> _logger;

        public TodoService(ApplicationContext context, ILogger<TodoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task DeleteTodo(Guid todoGuid)
        {
            var itemToDelete = _context.TodoItem.Where(x => x.Id == todoGuid).FirstOrDefault();
            if (itemToDelete != null)
                _ = _context.TodoItem.Remove(entity: itemToDelete);

            await _context.SaveChangesAsync();
        }

        public async Task<TodoItem> Add(string text)
        {
            var todo = new TodoItem { Id = Guid.NewGuid(), Completed = false, Text = text };
            await _context.TodoItem.AddAsync(todo);
            await _context.SaveChangesAsync();
            return todo;

        }

        public async Task UpdateTodo(TodoItem updatedData)
        {
            _context.TodoItem.Update(updatedData);
            await _context.SaveChangesAsync();

        }

        public TodoItem[] GetAllTodos()
        {
            return _context.TodoItem.ToArray();
        }
    }
}