using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoApp.Core.Entities;
using ToDoApp.Infrastructure.Data;
using ToDoApp.Infrastructure.Repositories;
using Xunit;

namespace ToDoApp.Tests
{
    public class ToDoRepositoryTests
    {
        private readonly ToDoRepository _repository; // Use concrete type instead of interface
        private readonly ToDoContext _context;

        public ToDoRepositoryTests()
        {
            // Use an in-memory database for testing
            var options = new DbContextOptionsBuilder<ToDoContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ToDoContext(options);
            _repository = new ToDoRepository(_context); // Initialize concrete type

            // Clear the database before each test
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddAsync_ShouldAddTodo()
        {
            // Arrange
            var todo = new ToDoItem { Content = "Test Todo", IsCompleted = false };

            // Act
            var result = await _repository.AddAsync(todo);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Todo", result.Content);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllTodos()
        {
            // Arrange
            await _repository.AddAsync(new ToDoItem { Content = "Todo 1", IsCompleted = false });
            await _repository.AddAsync(new ToDoItem { Content = "Todo 2", IsCompleted = false });

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnTodo()
        {
            // Arrange
            var todo = new ToDoItem { Content = "Test Todo", IsCompleted = false };
            await _repository.AddAsync(todo);

            // Act
            var result = await _repository.GetByIdAsync(todo.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(todo.Content, result.Content);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateTodo()
        {
            // Arrange
            var todo = new ToDoItem { Content = "Test Todo", IsCompleted = false };
            await _repository.AddAsync(todo);

            // Act
            await _repository.UpdateContentAsync(todo.Id, "Updated Todo");

            // Assert
            var updatedTodo = await _repository.GetByIdAsync(todo.Id);
            Assert.NotNull(updatedTodo); // Ensure updatedTodo is not null
            Assert.Equal("Updated Todo", updatedTodo.Content);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteTodo()
        {
            // Arrange
            var todo = new ToDoItem { Content = "Test Todo", IsCompleted = false };
            await _repository.AddAsync(todo);

            // Act
            await _repository.DeleteAsync(todo);

            // Assert
            var deletedTodo = await _repository.GetByIdAsync(todo.Id);
            Assert.Null(deletedTodo);
        }
    }
}