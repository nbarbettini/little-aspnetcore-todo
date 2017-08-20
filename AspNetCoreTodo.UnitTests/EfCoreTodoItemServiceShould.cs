using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AspNetCoreTodo.UnitTest
{
    public class EfCoreTodoItemServiceShould
    {
        [Fact]
        public async Task AddNewItem()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_AddNewItem")
                .Options;

            // Set up a context (connection to the DB) for writing
            using (var context = new ApplicationDbContext(options))
            {
                var service = new EfCoreTodoItemService(context);
                await service.AddItem(new NewTodoItem { Title = "Testing?" }, null);
            }

            // Use a separate context to read the data back from the DB
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(1, await context.Items.CountAsync());
                
                var item = await context.Items.SingleAsync();
                Assert.Equal("Testing?", item.Title);
                Assert.Equal(false, item.IsDone);
            }
        }
    }
}
