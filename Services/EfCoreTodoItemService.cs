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

        public async Task<bool> AddItem(NewTodoItem newItem)
        {
            var entity = new TodoItem
            {
                Id = Guid.NewGuid(),
                IsDone = false,
                Title = newItem.Title,
                DueAt = DateTimeOffset.Now.AddDays(3)
            };

            _context.Items.Add(entity);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            return _context.Items
                .Where(x => x.IsDone == false)
                .ToArrayAsync();
        }

        public async Task<bool> MarkDone(Guid id)
        {
            var item = await _context.Items
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (item == null) return false;

            item.IsDone = true;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1; // One entity should have been updated
        }
    }
}
