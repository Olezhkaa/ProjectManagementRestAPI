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

        public async Task<IEnumerable<Users>> GetAllAsync(string? login,
            string? surname, 
            string? name,
            string? email,
            string? sortBy,
            bool desk,
            int limit,
            int cursor)
        {
            var query = _context.Users.AsQueryable();

            //Фильтрация
            if (!string.IsNullOrWhiteSpace(login)) query = query.Where(p => p.Login == login);
            if (!string.IsNullOrWhiteSpace(surname)) query = query.Where(p => p.Surname == surname);
            if (!string.IsNullOrWhiteSpace(name)) query = query.Where(p => p.Name == name);
            if (!string.IsNullOrWhiteSpace(email)) query = query.Where(p => p.Email == email);

            //Сортировка
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "login" => desk ? query.OrderByDescending(p => p.Login) : query.OrderBy(p => p.Login),
                    "surname" => desk ? query.OrderByDescending(p => p.Surname) : query.OrderBy(p => p.Surname),
                    "name" => desk ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                    "email" => desk ? query.OrderByDescending(p => p.Email) : query.OrderBy(p => p.Email),
                    _ => query.OrderBy(p => p.Id),
                };
            }

            //Пагинация8
            return await query
                .Skip(limit * cursor)
                .Take(limit)
                .ToListAsync();
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

