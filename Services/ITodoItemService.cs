using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user);

        Task<bool> MarkDone(Guid id, ApplicationUser user);

        Task<bool> AddItem(NewTodoItem newItem, ApplicationUser user);
    }
}
