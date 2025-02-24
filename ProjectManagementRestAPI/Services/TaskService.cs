using ProjectManagementRestAPI.Model;
using ProjectManagementRestAPI.Repositories;

namespace ProjectManagementRestAPI.Services
{
    public class TaskService
    {
        private TaskRepository _repository;

        public TaskService(TaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Model.Task>> GetAllAsync(string? title,
            int? idStatusTask,
            int? idProject,
            string? sortBy,
            bool desk,
            int limit,
            int cursor)
        {
            return await _repository.GetAllAsync(title, idProject, idStatusTask, sortBy, desk, limit, cursor);
        }

        public async Task<Model.Task?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Model.Task?> CreateAsync(Model.Task task)
        {
            return await _repository.CreateAsync(task);
        }

        public async Task<bool> UpdateAsync(Model.Task task)
        {
            return await _repository.UpdateAsync(task);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Model.Task>> GetTaskByProjectAsync(int idProject)
        {
            return await _repository.GetTaskByProjectAsync(idProject);
        }
        public async Task<IEnumerable<Model.Task>> GetTaskByStatusAsync(int idStatusTask)
        {
            return await _repository.GetTaskByStatusAsync(idStatusTask);
        }
    }
}
