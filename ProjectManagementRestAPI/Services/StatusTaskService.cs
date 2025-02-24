using ProjectManagementRestAPI.Model;
using ProjectManagementRestAPI.Repositories;

namespace ProjectManagementRestAPI.Services
{
    public class StatusTaskService
    {
        private StatusTaskRepository _repository;

        public StatusTaskService(StatusTaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StatusTask>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<StatusTask?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<StatusTask> CreateAsync(StatusTask comment)
        {
            return await _repository.CreateAsync(comment);
        }

        public async Task<bool> UpdateAsync(StatusTask comment)
        {
            return await _repository.UpdateAsync(comment);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
