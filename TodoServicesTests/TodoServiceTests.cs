using Microsoft.Extensions.Logging;
using Moq;
using System;
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

        private Mock<ApplicationContext> mockApplicationContext;
        private Mock<ILogger<TodoService>> mockLogger;

        public TodoServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Loose);

            this.mockApplicationContext = this.mockRepository.Create<ApplicationContext>();
            this.mockLogger = this.mockRepository.Create<ILogger<TodoService>>();
        }

        private TodoService CreateService()
        {
            return new TodoService(
                this.mockApplicationContext.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task DeleteTodo_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid todoGuid = default(global::System.Guid);

            // Act
            await service.DeleteTodo(
                todoGuid);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Add_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string text = "testtask";

            // Act
            var result = await service.Add(
                text);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateTodo_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            TodoItem updatedData = new TodoItem { Id = Guid.NewGuid(), Text = "UpdateThis" };

            // Act
            await service.UpdateTodo(
                updatedData);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetAllTodos_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = service.GetAllTodos();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
