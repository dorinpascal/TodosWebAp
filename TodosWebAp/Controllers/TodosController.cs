
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodosWebAp.Model;

namespace TodosWebAp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodosController : ControllerBase
    {
        public ITodoData TodoService;

        public TodosController(ITodoData todoService)
        {
            TodoService = todoService;
        }   


        [HttpGet]
        public async Task<ActionResult<IList<Todo>>> GetTodos([FromQuery] bool? isCompleted, [FromQuery] int? userId)
        {
            try
            {
                IList<Todo> todos = await TodoService.GetTodos();
                Console.WriteLine("lalalalal"+todos[0].UserId);
                if (userId!=null)
                {
                    todos=todos.Where(todo => todo.UserId == userId).ToList();
                }
                if (isCompleted != null)
                {
                    todos = todos.Where(todo => todo.IsCompleted == isCompleted).ToList();
                }
                return Ok(todos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteTodo([FromRoute] int id) {
            try
            {
                await TodoService.RemoveTodo(id);
                return Ok();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> AddTodo([FromBody] Todo todo) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Todo added = await TodoService.AddTodo(todo);
                return Created($"/{added.TodoId}",added); // return newly added to-do, to get the auto generated id
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult<Todo>> UpdateTodo([FromBody] Todo todo) {
            try
            {
                Todo updatedTodo = await TodoService.Update(todo);
                return Ok(updatedTodo); 
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        



    }
}