using ProjectManagementRestAPI.Model;
using ProjectManagementRestAPI.Repositories;

namespace ProjectManagementRestAPI.Services
{
    public class ProjectService
    {
        private ProjectRepository _repository;

        public ProjectService(ProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Project> CreateAsync(Project project)
        {
            return await _repository.CreateAsync(project);
        }

        public async Task<bool> UpdateAsync(Project project)
        {
            return await _repository.UpdateAsync(project);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<UsersProject?> AddUserProjectAsync(UsersProject usersProject)
        {
            return await _repository.AddUserProjectAsync(usersProject);
        }

        public async Task<bool> DeleteUsersProjectAsync(int id)
        {
            return await _repository.DeleteUsersProjectAsync(id);
        }

        public async Task<IEnumerable<UsersProject>> GetUsersFromProjectAsync(int idProject)
        {
            return await _repository.GetUsersFromProjectAsync(idProject);
        }

        public async Task<UsersProject?> GetUserProjectAsync(int id)
        {
            return await _repository.GetUserProjectAsync(id);
        }
    }
}
