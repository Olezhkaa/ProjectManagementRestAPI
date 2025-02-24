using Microsoft.EntityFrameworkCore;
using ProjectManagementRestAPI.DataBase;
using ProjectManagementRestAPI.Model;

namespace ProjectManagementRestAPI.Repositories
{
    public class UsersRepository
    {
        private AppDbContext _context;

        public UsersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await _context.Users.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Users?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<Users> CreateAsync(Users users)
        {
            users.DateCreate = DateTime.Now;
            users.DateChange = DateTime.Now;
            _context.Users.Add(users);
            await _context.SaveChangesAsync();
            return users;
        }

        public async Task<bool> UpdateAsync(Users users)
        {
            var existingUsers = await _context.Users.FindAsync(users.Id);
            if (existingUsers == null) return false;

            existingUsers.Login = users.Login;
            existingUsers.Surname = users.Surname;
            existingUsers.Name = users.Name;
            existingUsers.Email = users.Email;
            existingUsers.DateChange = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null) return false;

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UsersProject>> GetUsersFromProjectAsync(int idProject)
        {
            return await _context.UsersProjects
                .OrderBy(p => p.Id)
                .Where(p => p.ID_Project == idProject)
                .ToListAsync();
        }
    }
}

