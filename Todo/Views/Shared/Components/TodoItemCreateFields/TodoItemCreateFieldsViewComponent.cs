using Microsoft.AspNetCore.Mvc;

namespace Todo.Views.TodoList.Components.TodoItemCreateFields
{
    public class TodoItemCreateFieldsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Models.TodoItems.TodoItemCreateFields model = default)
            => View(model ?? new Models.TodoItems.TodoItemCreateFields());
    }
}
