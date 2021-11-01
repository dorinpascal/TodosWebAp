using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodosWebAp.Model;
using TodosWebAp.Persistence;

namespace TodosWebAp
{
    public class SqliteTodoService:ITodoData
    {
        private TodoContext TodoContext;
        
        public SqliteTodoService(TodoContext todoContext)
        {
            TodoContext = todoContext;
        }
        public async Task<IList<Todo>> GetTodos()
        {
            return await TodoContext.Todos.ToListAsync();
        }

        public async Task<Todo> AddTodo(Todo toDo)
        {
        
            EntityEntry<Todo> added = await TodoContext.Todos.AddAsync(toDo);
            await TodoContext.SaveChangesAsync();
            return added.Entity;
        }

        public async  Task RemoveTodo(int todoId)
        {
            Todo todoToRemove = await TodoContext.Todos.FirstAsync(f => f.TodoId == todoId);
            if (todoToRemove!=null)
            {
                
                TodoContext.Todos.Remove(todoToRemove);
               await TodoContext.SaveChangesAsync();
            }

        }

        public async Task<Todo> Update(Todo todo)
        {
            Todo toDo = await TodoContext.Todos.FirstAsync(f => f.Equals(todo));
            if (toDo!=null)
            {
                TodoContext.Todos.Update(toDo);
                await TodoContext.SaveChangesAsync();
                return todo;
            }

            return null;
        }

        public async Task<Todo> Get(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}