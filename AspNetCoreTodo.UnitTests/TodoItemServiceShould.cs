using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AspNetCoreTodo.UnitTests
{
    public class TodoItemServiceShould
    {
        [Fact]
        public async Task AddNewItem()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_AddNewItem").Options;

            // Set up a context (connection to the DB) for writing
            using (var inMemoryContext = new ApplicationDbContext(options))
            {
                var service = new TodoItemService(inMemoryContext);

                var fakeUser = new ApplicationUser
                {
                    Id = "fake-000",
                    UserName = "fake@fake"
                };

                await service.AddItemAsync(new TodoItem { Title = "Testing?" }, fakeUser);
            }

            // Use a separate context to read the data back from the DB
            using (var inMemoryContext = new ApplicationDbContext(options))
            {
                Assert.Equal(1, await inMemoryContext.Items.CountAsync());
                
                var item = await inMemoryContext.Items.FirstAsync();
                Assert.Equal("fake-000", item.OwnerId);
                Assert.Equal("Testing?", item.Title);
                Assert.Equal(false, item.IsDone);
                Assert.True(DateTimeOffset.Now.AddDays(3) - item.DueAt < TimeSpan.FromSeconds(1));
            }
        }
    }
}
