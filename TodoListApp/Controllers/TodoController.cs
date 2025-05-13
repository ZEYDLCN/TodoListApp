
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListApp.Business.Interfaces;
using TodoListApp.Entities;

namespace TodoListApp.TodoListApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
        {
            var todos = await _todoService.GetAllTodosAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetById(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> Add([FromBody] TodoItemCreateDto itemDto)
        {
            if (!ModelState.IsValid) // Temel validasyon
            {
                return BadRequest(ModelState);
            }

            var newItem = new TodoItem
            {
                Title = itemDto.Title,
                IsCompleted = itemDto.IsCompleted 
            };

            try
            {
                var addedItem = await _todoService.AddTodoAsync(newItem);
                
                return CreatedAtAction(nameof(GetById), new { id = addedItem.Id }, addedItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TodoItemUpdateDto itemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = new TodoItem
            {
              
                Title = itemDto.Title,
                IsCompleted = itemDto.IsCompleted
            };

            try
            {
                var success = await _todoService.UpdateTodoAsync(id, itemToUpdate);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent(); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _todoService.DeleteTodoAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent(); 
        }
    }

  
    public class TodoItemCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
    }

    public class TodoItemUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}