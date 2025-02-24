using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementRestAPI.Model;
using ProjectManagementRestAPI.Services;

namespace ProjectManagementRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private TaskService _service;

        public TaskController(TaskService service)
        {
            _service = service;
        }

        //Получение всех задач
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model.Task>>> GetAll(string? title,
            int? idStatusTask,
            int? idProject,
            string? sortBy,
            bool desk = false,
            int limit = 10,
            int cursor = 0)
        {
            return Ok(await _service.GetAllAsync(title, idProject, idStatusTask, sortBy, desk, limit, cursor));
        }

        //Получение задачи по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Model.Task?>> GetTask(int id)
        {
            var task = await _service.GetByIdAsync(id);
            return task == null ? NotFound() : Ok(task);
        }

        //Получение всех задач в проекте
        [HttpGet("TaskByProject/{idProject}")]
        public async Task<ActionResult<IEnumerable<Model.Task>>> GetTaskByProject(int idProject)
        {
            return Ok(await _service.GetTaskByProjectAsync(idProject));
        }

        //Получение задач по статусу выполнения
        [HttpGet("TaskByStatus/{idStatusTask}")]
        public async Task<ActionResult<IEnumerable<Model.Task>>> GetTaskByStatus(int idStatusTask)
        {
            return Ok(await _service.GetTaskByStatusAsync(idStatusTask));
        }

        //Создание задачи
        [HttpPost]
        public async Task<ActionResult<Model.Task>> Create(Model.Task task)
        {
            var creationTask = await _service.CreateAsync(task);

            if (creationTask == null) return BadRequest("Указанный Project или StatusTask не существует.");

            return CreatedAtAction(nameof(GetTask), new { id = creationTask.Id }, creationTask);
        }

        //Обновление задачи
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Model.Task task)
        {
            if (id != task.Id) return BadRequest();

            var updateTask = await _service.UpdateAsync(task);
            return updateTask ? NoContent() : NotFound();
        }

        //Удаление задачи
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteTask = await _service.DeleteAsync(id);
            return deleteTask ? NoContent() : NotFound();
        }
    }
}
