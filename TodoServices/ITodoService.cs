using TodoData.Models;

namespace TodoServices
{
    public interface ITodoService
    {
        Task DeleteTodo(Guid todoGuid);
        TodoItem[] GetAllTodos();
        Task Add(string text);
        Task UpdateTodo(TodoItem updatedData);
    }
}