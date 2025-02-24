using Microsoft.AspNetCore.Mvc;
using ProjectManagementRestAPI.Model;
using ProjectManagementRestAPI.Services;

namespace ProjectManagementRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusTaskController : ControllerBase
    {
        private StatusTaskService _service;

        public StatusTaskController(StatusTaskService service)
        {
            _service = service;
        }
        //Список статусов задач
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusTask>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // Получить статус задачи по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusTask>> GetById(int id)
        {
            var comment = await _service.GetByIdAsync(id);
            return comment == null ? NotFound() : Ok(comment);
        }

        // Создать новый статус задачи
        [HttpPost]
        public async Task<ActionResult<StatusTask>> Create(StatusTask comment)
        {
            var creationStatusTask = await _service.CreateAsync(comment);

            return CreatedAtAction(nameof(GetById), new { id = creationStatusTask.Id }, creationStatusTask);
        }

        // Обновить статус задачи
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, StatusTask comment)
        {

            if (id != comment.Id) return BadRequest();

            var updateStatusTask = await _service.UpdateAsync(comment);
            return updateStatusTask ? NoContent() : NotFound();

        }

        // Удалить статус задачи
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _service.DeleteAsync(id);
            return delete ? NoContent() : NotFound();
        }
    }

}

