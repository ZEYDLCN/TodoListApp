using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListApp.Entities;

namespace TodoListApp.Business.Interfaces
{
    public interface ITodoService
    {
        Task<TodoItem?> GetTodoByIdAsync(int id);
        Task<IEnumerable<TodoItem>> GetAllTodosAsync();
        Task<TodoItem> AddTodoAsync(TodoItem item); // Geriye eklenen item'ı dönebiliriz
        Task<bool> UpdateTodoAsync(int id, TodoItem item); // Güncelleme başarılı mı?
        Task<bool> DeleteTodoAsync(int id); // Silme başarılı mı?
    }
}