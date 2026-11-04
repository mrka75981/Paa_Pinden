using PaaPindenAPI.Models;

namespace PaaPindenAPI.Repositories
{
    public interface ICommentRepository
    {
        void CreateCommentAsync(Comment comment);

        Comment UpdateCommentAsync(int id);

        List<Comment> GetAllCommentsAsync();

        void DeleteCommentAsync(int id);

    }

}

