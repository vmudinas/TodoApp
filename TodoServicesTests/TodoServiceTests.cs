using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoData;
using TodoData.Models;
using TodoServices;
using Xunit;

namespace TodoServicesTests
{
    public class TodoServiceTests
    {
        private MockRepository mockRepository;
        private Mock<ILogger<TodoService>> mockLogger;
        private Guid taskGuidToDelete { get; set; }
        private Guid taskGuidToUpdate { get; set; }

        public TodoServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Loose);
            this.mockLogger = this.mockRepository.Create<ILogger<TodoService>>();
        }

        private ApplicationContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            var dbContext = new ApplicationContext(options); dbContext.TodoItem.AddRange(GetFakeData().AsQueryable());
            dbContext.SaveChanges();
            return dbContext;
        }
        private List<TodoItem> GetFakeData()
        {
            taskGuidToDelete = Guid.NewGuid();
            taskGuidToUpdate = Guid.NewGuid();

            var todoTasks = new List<TodoItem>
              { 
                new TodoItem { Id = taskGuidToDelete, Text ="Data", Completed = true },
                new TodoItem { Id = taskGuidToUpdate, Text ="Data 2", Completed = false }
              };
            return todoTasks;
        }
        private TodoService CreateService()
        {
            return new TodoService(
                CreateDbContext(),
                this.mockLogger.Object);
        }

        [Fact]
        public async Task DeleteTodo_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();

            var listOfTask = service.GetAllTodos();
            // Act
            await service.DeleteTodo(
                taskGuidToDelete);

            var listOfTaskAfterDelete = service.GetAllTodos();
            // Assert
            Assert.True(listOfTask.Count() > listOfTaskAfterDelete.Count());

        }

        [Fact]
        public async Task Add_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string text = "testtask";
            var listOfTask = service.GetAllTodos();
            // Act
            var result = await service.Add(
                text);

            // Assert
            Assert.Equal("testtask", result.Text);
            Assert.False(result.Completed);
            var listOfTaskAfterAdd = service.GetAllTodos();
            // Assert
            Assert.True(listOfTask.Count() < listOfTaskAfterAdd.Count());

        }

        [Fact]
        public async Task UpdateTodo_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            TodoItem updatedData = new() { Id = taskGuidToUpdate, Text = "UpdateThis", Completed = true };

            // Act
            await service.UpdateTodo(
                updatedData);

            var listOfTask = service.GetAllTodos();
            var updatedTask = listOfTask.FirstOrDefault(t => t.Id == taskGuidToUpdate);
            // Assert
            Assert.Equal("UpdateThis", updatedTask?.Text ?? "");
            Assert.True(updatedTask.Completed);
        }

        [Fact]
        public void GetAllTodos_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = service.GetAllTodos();

            // Assert
            Assert.True(result.Count() > 0);
        }
    }
}
