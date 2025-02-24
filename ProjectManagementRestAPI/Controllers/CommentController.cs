using Microsoft.AspNetCore.Mvc;
using ProjectManagementRestAPI.Model;
using ProjectManagementRestAPI.Services;

namespace ProjectManagementRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private CommentService _service;

        public CommentController(CommentService service)
        {
            _service = service;
        }
        //Все комментарии
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAll(string? text,
            int? idTask,
            string? sortBy,
            bool desk = false,
            int limit = 10,
            int cursor = 0)
        {
            return Ok(await _service.GetAllAsync(text, idTask, sortBy, desk, limit, cursor));
        }

        // Получить комментарий по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _service.GetByIdAsync(id);
            return comment == null ? NotFound() : Ok(comment);
        }

        //Получить комментарии к задаче
        [HttpGet("CommentByTask/{idTask}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentByTask(int idTask)
        {
            return Ok(await _service.GetCommentByTask(idTask));
        }

        // Создать новый комментарий
        [HttpPost]
        public async Task<ActionResult<Comment?>> CreateComment(Comment comment)
        {
            var creationComment = await _service.CreateAsync(comment);

            if (creationComment == null) return BadRequest("Указанный Task не существует.");

            return CreatedAtAction(nameof(GetComment), new { id = creationComment.Id }, creationComment);
        }

        // Обновить комментарий
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, Comment comment)
        {

            if (id != comment.Id) return BadRequest();

            var updateComment = await _service.UpdateAsync(comment);
            return updateComment ? NoContent() : NotFound();

        }

        // Удалить комментарий
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var delete = await _service.DeleteAsync(id);
            return delete ? NoContent() : NotFound();
        }
    }
}
