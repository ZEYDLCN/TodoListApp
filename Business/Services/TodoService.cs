using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListApp.Business.Interfaces;
using TodoListApp.DataAccess.Interfaces;
using TodoListApp.Entities;

namespace TodoListApp.Business.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<TodoItem?> GetTodoByIdAsync(int id)
        {
            return await _todoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodosAsync()
        {
            return await _todoRepository.GetAllAsync();
        }

        public async Task<TodoItem> AddTodoAsync(TodoItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Title))
            {
                throw new ArgumentException("Title cannot be empty.", nameof(item.Title));
            }
           
            await _todoRepository.AddAsync(item);
            return item; 
        }

        public async Task<bool> UpdateTodoAsync(int id, TodoItem item)
        {
            var existingItem = await _todoRepository.GetByIdAsync(id);
            if (existingItem == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(item.Title))
            {
                throw new ArgumentException("Title cannot be empty.", nameof(item.Title));
            }

            existingItem.Title = item.Title;
            existingItem.IsCompleted = item.IsCompleted;
           

            await _todoRepository.UpdateAsync(existingItem);
            return true;
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            var itemToDelete = await _todoRepository.GetByIdAsync(id);
            if (itemToDelete == null)
            {
                return false; 
            }
            await _todoRepository.DeleteAsync(id);
            return true;
        }
    }
}