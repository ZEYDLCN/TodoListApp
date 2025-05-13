using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListApp.Entities;

namespace TodoListApp.DataAccess.Interfaces
{
    public interface ITodoRepository
    {
        Task<TodoItem?> GetByIdAsync(int id);
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task AddAsync(TodoItem item);
        Task UpdateAsync(TodoItem item);
        Task DeleteAsync(int id);
    }
}