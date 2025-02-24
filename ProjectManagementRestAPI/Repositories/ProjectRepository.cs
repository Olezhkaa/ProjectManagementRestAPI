using Microsoft.EntityFrameworkCore;
using ProjectManagementRestAPI.DataBase;
using ProjectManagementRestAPI.Model;

namespace ProjectManagementRestAPI.Repositories
{
    public class ProjectRepository
    {
        private AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        //Получения всех проектов
        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects.OrderBy(p => p.Id).ToListAsync();
        } 

        //Получение проекта по ID
        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        //Создание проекта
        public async Task<Project> CreateAsync(Project project)
        {
            project.DateCreate = DateTime.Now;
            project.DateChange = DateTime.Now;
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        //Обновление проекта
        public async Task<bool> UpdateAsync(Project project)
        {
            var existingProject = await _context.Projects.FindAsync(project.Id);
            if (existingProject == null) return false;

            existingProject.Title = project.Title;
            existingProject.Description = project.Description;
            existingProject.DateChange = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        //Удаление проекта
        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        //Добавление пользователя в проект
        public async Task<UsersProject?> AddUserProjectAsync(UsersProject usersProject)
        {
            var user = await _context.Users.AnyAsync(p => p.Id == usersProject.ID_User);
            var project = await _context.Users.AnyAsync(p => p.Id == usersProject.ID_Project);
            if (!user || !project) return null;

            _context.UsersProjects.Add(usersProject);
            await _context.SaveChangesAsync();
            return usersProject;
        }

        //Удаление пользователя из проекта
        public async Task<bool> DeleteUsersProjectAsync(int id)
        {
            var userProject = await _context.UsersProjects.FindAsync(id);
            if (userProject == null) return false;

            _context.UsersProjects.Remove(userProject);
            await _context.SaveChangesAsync();
            return true;
        }

        //Получение всех пользователей в проекте
        public async Task<IEnumerable<UsersProject>> GetUsersFromProjectAsync(int idProject)
        {
            return await _context.UsersProjects
                .OrderBy(p => p.Id)
                .Where(p => p.ID_Project == idProject)
                .ToListAsync();
        }

        //Получение Пользовтель-Проект по ID
        public async Task<UsersProject?> GetUserProjectAsync(int id)
        {
            return await _context.UsersProjects.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
