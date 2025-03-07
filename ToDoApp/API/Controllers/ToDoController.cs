using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Core.Entities;
using ToDoApp.Core.Interfaces;

namespace ToDoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoRepository _repository;
        private readonly ILogger<ToDoController> _logger;

        public ToDoController(IToDoRepository repository, ILogger<ToDoController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: api/ToDo
        [HttpGet]
        public async Task<ActionResult<List<ToDoItem>>> GetAll()
        {
            var todos = await _repository.GetAllAsync();
            _logger.LogInformation("Retrieved {Count} todos", todos.Count);
            return todos;
        }

        // GET: api/ToDo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                _logger.LogWarning("Todo with ID {Id} not found", id);
                return NotFound();
            }
            return Ok(item);
        }

        // POST: api/ToDo
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> Create(ToDoItem item)
        {
            if (string.IsNullOrEmpty(item.Content))
            {
                _logger.LogWarning("Attempted to create a todo with empty content.");
                return BadRequest("Content cannot be empty");
            }

            var createdItem = await _repository.AddAsync(item);
            _logger.LogInformation("Created a new todo with ID {Id}", createdItem.Id);
            return createdItem;
        }

        // PUT: api/ToDo/5 (Update Content)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContent(int id, [FromBody] ToDoItem item)
        {
            if (string.IsNullOrEmpty(item.Content))
            {
                _logger.LogWarning("Attempted to update a todo with empty content.");
                return BadRequest("Content cannot be empty");
            }

            // Update only the content using the ID from the URL
            await _repository.UpdateContentAsync(id, item.Content);
            _logger.LogInformation("Updated content for todo with ID {Id}", id);
            return NoContent();
        }

        // PATCH: api/ToDo/5/toggle (Toggle Completion Status)
        [HttpPatch("{id}/toggle")]
        public async Task<IActionResult> ToggleCompletion(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                _logger.LogWarning("Todo with ID {Id} not found for toggling completion status", id);
                return NotFound();
            }

            // Toggle the completion status
            await _repository.ToggleCompletionAsync(id, !item.IsCompleted);
            _logger.LogInformation("Toggled completion status for todo with ID {Id}", id);
            return NoContent();
        }

        // PATCH: api/ToDo/mark-all-complete (Mark All as Complete)
        [HttpPatch("mark-all-complete")]
        public async Task<IActionResult> MarkAllAsComplete()
        {
            await _repository.MarkAllAsCompleteAsync();
            _logger.LogInformation("Marked all todos as complete");
            return NoContent();
        }

        // DELETE: api/ToDo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                _logger.LogWarning("Todo with ID {Id} not found for deletion", id);
                return NotFound();
            }

            await _repository.DeleteAsync(item);
            _logger.LogInformation("Deleted todo with ID {Id}", id);
            return NoContent();
        }
    }
}