using Microsoft.AspNetCore.Mvc;
using Todo.Models.TodoItems;

namespace Todo.Views.TodoList.Components.TodoItemSummary
{
    public class TodoItemSummaryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(TodoItemSummaryViewmodel model)
            => View(model);
    }
}
