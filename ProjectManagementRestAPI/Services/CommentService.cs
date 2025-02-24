using ProjectManagementRestAPI.Model;
using ProjectManagementRestAPI.Repositories;

namespace ProjectManagementRestAPI.Services
{
    public class CommentService
    {
        private CommentRepository _repository;

        public CommentService(CommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Comment>> GetCommentByTask(int idTask)
        {
            return await _repository.GetCommentByTask(idTask);
        }
        public async Task<Comment?> CreateAsync(Comment comment)
        {
            return await _repository.CreateAsync(comment);
        }

        public async Task<bool> UpdateAsync(Comment comment)
        {
            return await _repository.UpdateAsync(comment);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
