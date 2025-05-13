
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApp.DataAccess.Context;
using TodoListApp.DataAccess.Interfaces;
using TodoListApp.Entities;

namespace TodoListApp.DataAccess.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _context;

        public TodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.OrderByDescending(t => t.CreatedAt).ToListAsync();
        }

        public async Task AddAsync(TodoItem item)
        {
            
            await _context.TodoItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TodoItem item)
        {
            _context.TodoItems.Update(item);
           
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _context.TodoItems.FindAsync(id);
            if (itemToDelete != null)
            {
                _context.TodoItems.Remove(itemToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}