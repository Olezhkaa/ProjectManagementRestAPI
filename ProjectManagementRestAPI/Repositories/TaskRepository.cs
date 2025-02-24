using Microsoft.EntityFrameworkCore;
using ProjectManagementRestAPI.DataBase;
using ProjectManagementRestAPI.Model;
using System.Security.Cryptography.X509Certificates;

namespace ProjectManagementRestAPI.Repositories
{
    public class TaskRepository
    {
        private AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        // Получить все задачи с проектами и статусами
        public async Task<IEnumerable<Model.Task>> GetAllAsync(
            string? title,
            int? idStatusTask,
            int? idProject,
            string sortBy,
            bool desk,
            int limit,
            int cursor)
        {
            var query = _context.Tasks.AsQueryable();

            // Фильтрация
            if (!string.IsNullOrWhiteSpace(title)) query = query.Where(p => p.Title == title);
            if (idStatusTask.HasValue) query = query.Where(p => p.ID_Status_Task == idStatusTask);
            if (idProject.HasValue) query = query.Where(p => p.ID_Project == idProject);

            //Сортировка
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "title" => desk ? query.OrderByDescending(p => p.Title) : query.OrderBy(p => p.Title),
                    "ID_Project" => desk ? query.OrderByDescending(p => p.ID_Project) : query.OrderBy(p => p.ID_Project),
                    "ID_Status_Task" => desk ? query.OrderByDescending(p => p.ID_Status_Task) : query.OrderBy(p => p.ID_Status_Task),
                    _ => query.OrderBy(p => p.Id)
                };
            }

            //Пагинация
            return await query
                .Skip(limit * cursor)
                .Take(limit)
                .ToListAsync();
        }

        // Получить задачу по ID
        public async Task<Model.Task?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        // Создать задачу
        public async Task<Model.Task?> CreateAsync(Model.Task task)
        {
            var project = await _context.Projects.AnyAsync(p => p.Id == task.ID_Project);
            var statusTask = await _context.StatusTasks.AnyAsync(p => p.Id == task.ID_Status_Task);

            if (!project) return null;
            if (!statusTask) return null;

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        // Обновить задачу
        public async Task<bool> UpdateAsync(Model.Task task)
        {
            var project = await _context.Projects.AnyAsync(p => p.Id == task.ID_Project);
            var statusTask = await _context.StatusTasks.AnyAsync(p => p.Id == task.ID_Status_Task);
            var existingTask = await _context.Tasks.FindAsync(task.Id);

            if (!project) return false;
            if (!statusTask) return false;
            if (existingTask == null) return false;

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.ID_Project = task.ID_Project;
            existingTask.ID_Status_Task = task.ID_Status_Task;

            await _context.SaveChangesAsync();
            return true;
        }

        // Удалить задачу
        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        //Список задач в проекте
        public async Task<IEnumerable<Model.Task>> GetTaskByProjectAsync(int idProject)
        {
            return await _context.Tasks.
                OrderBy(p => p.Id).
                Where(p => p.ID_Project == idProject).
                ToListAsync();
        }

        //Список задач по статусу
        public async Task<IEnumerable<Model.Task>> GetTaskByStatusAsync(int idStatusTask)
        {
            return await _context.Tasks.
                OrderBy(p => p.Id).
                Where(p => p.ID_Status_Task == idStatusTask).
                ToListAsync();
        }
    }
}
