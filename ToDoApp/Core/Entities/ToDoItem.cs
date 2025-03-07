using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Core.Entities
{
    public class ToDoItem
    {
        [Key] // Mark as primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int Id { get; set; }

        [Required] // Ensure content is required
        public required string Content { get; set; }

        public bool IsCompleted { get; set; } = false; // Default value

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Auto-set to current UTC time
    }
}