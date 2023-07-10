using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoItems;
using Todo.Models.TodoItems.Api;
using Todo.Services;

namespace Todo.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoItemApiController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TodoItemApiController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get(int todoItemId)
        {
            var todoItem = dbContext.SingleTodoItem(todoItemId);
            if (todoItem is null)
            {
                return NotFound();
            }

            return Ok(TodoItemSummaryViewmodelFactory.Create(todoItem));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTodoItemDto payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var responsibleParty = await dbContext.Users.FindAsync(payload.ResponsiblePartyId);
            if (responsibleParty is null)
            {
                return BadRequest(nameof(payload.ResponsiblePartyId));
            }

            var item = new TodoItem(payload.TodoListId, payload.ResponsiblePartyId, payload.Title, payload.Importance);

            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync();

            return Created(Url.Action("Get", "TodoItemApi", new { todoItemId = item.TodoItemId }), item.TodoItemId);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateRank(int todoItemId, int rank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var item = await dbContext.TodoItems.FindAsync(todoItemId);
            if (item is null)
            {
                return BadRequest(nameof(todoItemId));
            }

            item.Rank = rank;
            await dbContext.SaveChangesAsync();

            return Ok();
        }


    }
}
