using System.ComponentModel.DataAnnotations;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailOrder
    {
        [Display(Name = "Sort by Rank")]
        public OrderByType? Rank { get; set; } = default;
    }
}
