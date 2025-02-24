using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ProjectManagementRestAPI.DataBase;
using ProjectManagementRestAPI.Model;

namespace ProjectManagementRestAPI.Repositories
{
    public class CommentRepository
    {
        private AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        //Получение комментариев
        public async Task<IEnumerable<Comment>> GetAllAsync(
            string? text, 
            int? idTask, 
            string? sortBy,
            bool desk,  
            int limit, 
            int cursor)
        {
            var query = _context.Comments.AsQueryable();

            //Фильтрация 
            if (!string.IsNullOrWhiteSpace(text)) query = query.Where(p => p.Text == text);
            if (idTask.HasValue) query = query.Where(p => p.ID_Task == idTask);

            //Сортировка
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "text" => desk ? query.OrderByDescending(p => p.Text) : query.OrderBy(p => p.Text),
                    "idTask" => desk ? query.OrderByDescending(p => p.ID_Task) : query.OrderBy(p => p.ID_Task),
                    _ => query.OrderBy(p => p.Id)
                };
            }

            //Пагинация
            return await query
                .Skip(limit * cursor)
                .Take(limit)
                .ToListAsync();
        }
        
        //Получение комментария по ID
        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        //Получение комментариев к задаче
        public async Task<IEnumerable<Comment>> GetCommentByTask(int idTask)
        {
            return await _context.Comments.OrderBy(p => p.Id).Where(p => p.ID_Task == idTask).ToListAsync();
        }

        //Создание комменатрия
        public async Task<Comment?> CreateAsync(Comment comment)
        {
            var task = await _context.Tasks.AnyAsync(p => p.Id == comment.ID_Task);

            if (!task) return null;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        //Обновление комментария
        public async Task<bool> UpdateAsync(Comment comment)
        {
            var task = await _context.Tasks.AnyAsync(p => p.Id == comment.ID_Task);
            var existingComment = await _context.Comments.FindAsync(comment.Id);

            if (!task) return false;
            if (existingComment == null) return false;

            existingComment.Text = comment.Text;
            existingComment.ID_Task = comment.ID_Task;

            await _context.SaveChangesAsync();
            return true;
        }

        //Удаление комментария
        public async Task<bool> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return false;

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
