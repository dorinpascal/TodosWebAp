using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using TodosWebAp.Model;

namespace TodosWebAp
{
    public class ToDoJSONData : ITodoData
    {
        private string todoFile = "todos.json";
        private IList<Todo> ToDos;

        public ToDoJSONData()
        {
            if (!File.Exists(todoFile))
            {
                Seed();
                WriteTodosToFile();
            }
            else
            {
                string content = File.ReadAllText(todoFile);
                ToDos = JsonSerializer.Deserialize<List<Todo>>(content);
            }
        }


        public async Task<IList<Todo>> GetTodos()
        {
            List<Todo> temp = new List<Todo>(ToDos);
            return temp;
        }

        public async Task<Todo> AddTodo(Todo toDo)
        {
            int max = ToDos.Max(toDo => toDo.TodoId);
            toDo.TodoId = (++max);
            ToDos.Add(toDo);
            WriteTodosToFile();
            return toDo;
        }

        private void WriteTodosToFile()
        {
            string todoJson = JsonSerializer.Serialize(ToDos);
            File.WriteAllText(todoFile, todoJson);
        }

        private void Seed()
        {
            Todo[] ts =
            {
                new Todo {UserId = 1, TodoId = 1, Title = "Do dishes", IsCompleted = false},
                new Todo {UserId = 1, TodoId = 2, Title = "Walk the dog", IsCompleted = false},
                new Todo{UserId = 2, TodoId = 3, Title = "Do DNP homework", IsCompleted = true},
                new Todo {UserId = 3, TodoId = 4, Title = "Eat breakfast", IsCompleted = false},
                new Todo{UserId = 4, TodoId = 5, Title = "Mow lawn", IsCompleted = true},
            };
            ToDos = ts.ToList();
        }

        public async Task RemoveTodo(int todoId)
        {
            //instead of if statemnt
            Todo toRemove = ToDos.First(t => t.TodoId == todoId);
            ToDos.Remove(toRemove);
            WriteTodosToFile();
        }

        public async Task<Todo> Update(Todo todo)
        {
            //instead of if statemnt
            Todo toTupdate = ToDos.First(t => t.TodoId == todo.TodoId);
            toTupdate.IsCompleted = todo.IsCompleted;
            toTupdate.Title = todo.Title;
            WriteTodosToFile();
            return toTupdate;
        }

        public async Task<Todo> Get(int id)
        {
            return ToDos.FirstOrDefault(t => t.TodoId == id);
        }
    }
}