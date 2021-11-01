using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using TodosWebAp.Model;


namespace TodosWebAp
{
    public interface ITodoData
    {
        Task<IList<Todo>> GetTodos();
        Task<Todo> AddTodo(Todo toDo);
        Task RemoveTodo(int todoId);
        Task<Todo> Update(Todo todo);
        Task<Todo> Get(int id);
    }
}