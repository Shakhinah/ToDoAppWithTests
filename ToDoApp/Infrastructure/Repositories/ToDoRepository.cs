using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Core.Entities;
using ToDoApp.Core.Interfaces;
using ToDoApp.Infrastructure.Data;

namespace ToDoApp.Infrastructure.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoContext _context;

        public ToDoRepository(ToDoContext context)
        {
            _context = context;
        }

        public async Task<List<ToDoItem>> GetAllAsync()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        public async Task<ToDoItem?> GetByIdAsync(int id)
        {
            return await _context.ToDoItems.FindAsync(id);
        }

        public async Task<ToDoItem> AddAsync(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task UpdateContentAsync(int id, string content)
        {
            var item = await _context.ToDoItems.FindAsync(id);
            if (item != null)
            {
                item.Content = content;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ToggleCompletionAsync(int id, bool isCompleted)
        {
            var item = await _context.ToDoItems.FindAsync(id);
            if (item != null)
            {
                item.IsCompleted = isCompleted;
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAllAsCompleteAsync()
        {
            var todos = await _context.ToDoItems.ToListAsync();
            foreach (var todo in todos)
            {
                todo.IsCompleted = true;
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ToDoItem item)
        {
            _context.ToDoItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}