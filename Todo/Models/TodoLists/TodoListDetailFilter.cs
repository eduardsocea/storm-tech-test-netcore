using System.ComponentModel.DataAnnotations;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailFilter
    {
        [Display(Name = "Hide Done Items")]
        public bool HideDoneItems { get; set; } = false;
    }
}
