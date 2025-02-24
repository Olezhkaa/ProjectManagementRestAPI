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

        public async Task<IEnumerable<Users>> GetAllAsync(string? login,
            string? surname,
            string? name,
            string? email,
            string? sortBy,
            bool desk,
            int limit,
            int cursor)
        {
            return await _repository.GetAllAsync(login, surname, name, email, sortBy, desk, limit, cursor);
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

