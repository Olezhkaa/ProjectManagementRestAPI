using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementRestAPI.DataBase;
using ProjectManagementRestAPI.Model;
using ProjectManagementRestAPI.Services;

namespace ProjectManagementRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private ProjectService _service;

        public ProjectController(ProjectService service)
        {
            _service = service;
        }

        // Получить все проекты
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return Ok(await _service.GetAllAsync());
        }

        // Получить проект по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _service.GetByIdAsync(id);
            return project == null ? NotFound() : Ok(project);
        }

        // Создать новый проект
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(Project project)
        {
            var createProject = await _service.CreateAsync(project);
            return CreatedAtAction(nameof(GetProject), new { id = createProject.Id }, createProject);
        }

        // Обновить проект
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Project project)
        {

            if (id != project.Id) return BadRequest();

            var updateProject = await _service.UpdateAsync(project);
            return updateProject ? NoContent() : NotFound();

        }

        // Удалить проект
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var delete = await _service.DeleteAsync(id);
            return delete ? NoContent() : NotFound();
        }

        [HttpPost("UserProject")]
        public async Task<ActionResult<UsersProject?>> AddUserProject(UsersProject usersProject)
        {
            var createUsersProject = await _service.AddUserProjectAsync(usersProject);
            if (createUsersProject == null) return BadRequest("Указанный Project или User не существует.");
            return CreatedAtAction(nameof(GetUserProject), new { id = createUsersProject.Id }, createUsersProject);
        }

        [HttpDelete("UserProject/{id}")]
        public async Task<IActionResult> DeleteUsersProjectAsync(int id)
        {
            var delete = await _service.DeleteAsync(id);
            return delete ? NoContent() : NotFound();
        }

        [HttpGet("UserProject/Project/{idProject}")]
        public async Task<ActionResult<IEnumerable<UsersProject>>> GetUsersFromProject(int idProject)
        {
            return Ok(await _service.GetUsersFromProjectAsync(idProject));
        }

        [HttpGet("UsersProject/{id}")]
        public async Task<ActionResult<IEnumerable<UsersProject>>> GetUserProject(int id)
        {
            var userProject = await _service.GetUserProjectAsync(id);
            return userProject == null ? NotFound() : Ok(userProject);
        }
    }
}
