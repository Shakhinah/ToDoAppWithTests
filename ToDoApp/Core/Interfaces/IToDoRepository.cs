using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Core.Entities;

namespace ToDoApp.Core.Interfaces
{
    public interface IToDoRepository
    {
        Task<List<ToDoItem>> GetAllAsync(); // Get all todos
        Task<ToDoItem?> GetByIdAsync(int id); // Get todo by ID
        Task<ToDoItem> AddAsync(ToDoItem item); // Add a new todo
        Task UpdateContentAsync(int id, string content); // Update todo content
        Task ToggleCompletionAsync(int id, bool isCompleted); // Toggle completion status
        Task MarkAllAsCompleteAsync(); // Mark all todos as complete
        Task DeleteAsync(ToDoItem item); // Delete a todo
    }
}