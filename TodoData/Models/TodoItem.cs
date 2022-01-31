using Microsoft.EntityFrameworkCore;

namespace TodoData.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public bool Completed { get; set; }
    }
}