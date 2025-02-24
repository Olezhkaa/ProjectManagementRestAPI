using ProjectManagementRestAPI.Model;
using ProjectManagementRestAPI.Repositories;

namespace ProjectManagementRestAPI.Services
{
    public class UsersService
    {
        private UsersRepository _repository;

        public UsersService(UsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Users?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Users> CreateAsync(Users users)
        {
            return await _repository.CreateAsync(users);
        }

        public async Task<bool> UpdateAsync(Users users)
        {
            return await _repository.UpdateAsync(users);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UsersProject>> GetUsersFromProjectAsync(int idProject)
        {
            return await _repository.GetUsersFromProjectAsync(idProject);
        }
    }
}

