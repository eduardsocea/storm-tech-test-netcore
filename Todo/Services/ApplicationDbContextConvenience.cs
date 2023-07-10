using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Models.TodoLists;

namespace Todo.Services
{
    public static class ApplicationDbContextConvenience
    {
        public static IQueryable<TodoList> RelevantTodoLists(this ApplicationDbContext dbContext, string userId)
        {
            return dbContext.TodoLists.Include(tl => tl.Owner)
                .Include(tl => tl.Items)
                .Where(tl => tl.Owner.Id == userId || tl.Items.Any(i => i.ResponsiblePartyId == userId));
        }

        public static TodoList SingleTodoList(this ApplicationDbContext dbContext, int todoListId, TodoListDetailFilter filter = default)
        {
            filter ??= new TodoListDetailFilter();
            return dbContext.TodoLists.Include(tl => tl.Owner)
                .Include(tl => tl.Items.Where(x => filter.HideDoneItems == false || x.IsDone == false))
                .ThenInclude(ti => ti.ResponsibleParty)
                .Single(tl => tl.TodoListId == todoListId);
        }

        public static TodoItem SingleTodoItem(this ApplicationDbContext dbContext, int todoItemId)
        {
            return dbContext.TodoItems.Include(ti => ti.TodoList).Include(x => x.ResponsibleParty).Single(ti => ti.TodoItemId == todoItemId);
        }
    }
}