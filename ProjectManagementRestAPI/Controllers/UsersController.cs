using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementRestAPI.DataBase;
using ProjectManagementRestAPI.Model;
using ProjectManagementRestAPI.Services;

namespace ProjectManagementRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UsersService _service;

        public UsersController(UsersService service)
        {
            _service = service;
        }

        // Получить все проекты
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUserss()
        {
            return Ok(await _service.GetAllAsync());
        }

        // Получить проект по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _service.GetByIdAsync(id);
            return users == null ? NotFound() :Ok(users);
        }

        // Создать новый проект
        [HttpPost]
        public async Task<ActionResult<Users>> CreateUsers(Users users)
        {
            var createUsers = await _service.CreateAsync(users);
            return CreatedAtAction(nameof(GetUsers), new { id = createUsers.Id }, createUsers);
        }

        // Обновить проект
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsers(int id, Users users)
        {
            if (id != users.Id) return BadRequest();

            var updateUsers = await _service.UpdateAsync(users);
            return updateUsers ? NoContent() : NotFound();

        }

        // Удалить проект
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var delete = await _service.DeleteAsync(id);
            return delete ? NoContent() : NotFound();
        }

        //Получить пользователей в проекте
        [HttpGet("UserProject/Project/{idProject}")]
        public async Task<ActionResult<IEnumerable<UsersProject>>> GetUsersFromProject(int idProject)
        {
            return Ok(await _service.GetUsersFromProjectAsync(idProject));
        }
    }
}

