using Microsoft.EntityFrameworkCore;
using ProjectManagementRestAPI.DataBase;
using ProjectManagementRestAPI.Model;

namespace ProjectManagementRestAPI.Repositories
{
    public class StatusTaskRepository
    {
        private AppDbContext _context;

        public StatusTaskRepository(AppDbContext context)
        {
            _context = context;
        }

        //Список статусов задач
        public async Task<IEnumerable<StatusTask>> GetAllAsync()
        {
            return await _context.StatusTasks
                .OrderBy(t => t.Id)
                .ToListAsync();
        }

        // Получить статус задачи по ID
        public async Task<StatusTask?> GetByIdAsync(int id)
        {
            return await _context.StatusTasks.FindAsync(id);
        }

        // Создать Статус задачи
        public async Task<StatusTask> CreateAsync(StatusTask statusTask)
        {
            _context.StatusTasks.Add(statusTask);
            await _context.SaveChangesAsync();
            return statusTask;
        }

        // Обновить статус задачи
        public async Task<bool> UpdateAsync(StatusTask task)
        {
            var existingTask = await _context.StatusTasks.FindAsync(task.Id);
            if (existingTask == null) return false;

            existingTask.Title = task.Title;

            await _context.SaveChangesAsync();
            return true;
        }

        // Удалить статус задачи
        public async Task<bool> DeleteAsync(int id)
        {
            var statusTask = await _context.StatusTasks.FindAsync(id);
            if (statusTask == null) return false;

            _context.StatusTasks.Remove(statusTask);
            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}
