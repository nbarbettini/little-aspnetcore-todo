using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class EfCoreTodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public EfCoreTodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddItem(NewTodoItem newItem, ApplicationUser user)
        {
            var entity = new TodoItem
            {
                Id = Guid.NewGuid(),
                Owner = user,
                IsDone = false,
                Title = newItem.Title,
                DueAt = DateTimeOffset.Now.AddDays(3)
            };

            _context.Items.Add(entity);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user)
        {
            return _context.Items
                .Where(x => x.IsDone == false && x.Owner.Id == user.Id)
                .ToArrayAsync();
        }

        public async Task<bool> MarkDone(Guid id, ApplicationUser user)
        {
            var item = await _context.Items
                .Where(x => x.Id == id && x.Owner.Id == user.Id)
                .SingleOrDefaultAsync();

            if (item == null) return false;

            item.IsDone = true;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1; // One entity should have been updated
        }
    }
}
