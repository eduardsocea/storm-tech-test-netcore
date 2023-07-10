using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Todo.Data.Entities;

namespace Todo.Models.TodoItems.Api
{
    public class CreateTodoItemDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int TodoListId { get; set; }

        [Required]
        public string ResponsiblePartyId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Importance Importance { get; set; } = Importance.Medium;
    }
}
