using System;

namespace AspNetCoreTodo.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }

        public string OwnerId { get; set; }
        
        public bool IsDone { get; set; }

        public string Title { get; set; }

        public DateTimeOffset? DueAt { get; set; }
    }
}
