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

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _context.Comments.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<IEnumerable<Comment>> GetCommentByTask(int idTask)
        {
            return await _context.Comments.OrderBy(p => p.Id).Where(p => p.ID_Task == idTask).ToListAsync();
        }

        public async Task<Comment?> CreateAsync(Comment comment)
        {
            var task = await _context.Tasks.AnyAsync(p => p.Id == comment.ID_Task);

            if (!task) return null;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

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
