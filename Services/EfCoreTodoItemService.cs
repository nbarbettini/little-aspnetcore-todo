using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class EfCoreTodoItemService : ITodoItemService
    {
        private readonly TodoContext _context;

        public EfCoreTodoItemService(TodoContext context)
        {
            _context = context;
        }

        public Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            return _context.Items
                .Where(x => x.IsDone == false)
                .ToArrayAsync();
        }
    }
}
