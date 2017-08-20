using System;
using AspNetCoreTodo.Controllers;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Xunit;

namespace AspNetCoreTodo.UnitTest
{
    public class TodoControllerShould
    {
        [Fact]
        public void ReturnUnauthorizedInMarkDone()
        {
            var mockTodoItemService = Substitute.For<ITodoItemService>();
            var mockUserManager = Substitute.For<UserManager<ApplicationUser>>();

            var controller = new TodoController(mockTodoItemService, mockUserManager);

            
        }
    }
}
