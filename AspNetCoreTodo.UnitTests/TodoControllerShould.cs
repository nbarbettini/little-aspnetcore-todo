using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreTodo.Controllers;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace AspNetCoreTodo.UnitTests
{
    public class TodoControllerShould
    {
        [Fact]
        public async Task ReturnBadRequest_ForMarkDoneWithBadId()
        {
            // Arrange
            var mockTodoItemService = Substitute.For<ITodoItemService>();
            var mockUserManager = MockUserManager.Create();

            var controller = new TodoController(mockTodoItemService, mockUserManager);

            // Act
            var result = await controller.MarkDone(Guid.Empty);
            var statusCodeResult = result as StatusCodeResult;

            // Assert
            Assert.NotNull(statusCodeResult);
            Assert.Equal(400, statusCodeResult.StatusCode);
        }
        
        [Fact]
        public async Task ReturnUnauthorized_ForMarkDoneWithNoUser()
        {
            // Arrange
            var mockTodoItemService = Substitute.For<ITodoItemService>();
            var mockUserManager = MockUserManager.Create();

            // Make the mockUserManager always return null for GetUserAsync
            mockUserManager.GetUserAsync(Arg.Any<ClaimsPrincipal>())
                .Returns(Task.FromResult<ApplicationUser>(null));

            var controller = new TodoController(mockTodoItemService, mockUserManager);

            // Act
            var randomId = Guid.NewGuid();
            var result = await controller.MarkDone(randomId);
            var statusCodeResult = result as StatusCodeResult;

            // Assert
            Assert.NotNull(statusCodeResult);
            Assert.Equal(401, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task ReturnSuccess_ForMarkDone()
        {
            // Arrange
            var mockTodoItemService = Substitute.For<ITodoItemService>();
            var mockUserManager = MockUserManager.Create();

            // Make the mockUserManager return a fake user
            var fakeUser = new ApplicationUser();
            mockUserManager.GetUserAsync(Arg.Any<ClaimsPrincipal>())
                .Returns(Task.FromResult(fakeUser));

            // Make the mockTodoItemService always succeed
            mockTodoItemService.MarkDoneAsync(Arg.Any<Guid>(), Arg.Any<ApplicationUser>())
                .Returns(Task.FromResult(true));

            var controller = new TodoController(mockTodoItemService, mockUserManager);

            // Act
            var randomId = Guid.NewGuid();
            var result = await controller.MarkDone(randomId);
            var statusCodeResult = result as StatusCodeResult;

            // Assert
            Assert.NotNull(statusCodeResult);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
    }
}
