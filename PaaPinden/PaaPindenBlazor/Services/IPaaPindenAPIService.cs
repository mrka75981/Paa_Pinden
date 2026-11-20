using Shared.DTOs;
using Shared.Models;

namespace PaaPindenBlazor.Services
{
    public interface IPaaPindenAPIService
    {
        Task CreateKnittingPostAsync(KnittingPostDTO knittingPostDTO);

        Task<KnittingPost> GetKnittingPostAsync(int id);

        Task<List<KnittingPost>> GetAllKnittingPostAsync();

        Task UpdateKnittingPostAsync(KnittingPostDTO knittingPostDTO);

        Task DeleteKnittingPostAsync(int id);

        Task CreateCommentAsync(CommentDTO commentDTO);
        Task<List<Comment>> GetAllPostCommentsAsync(int id);

        Task UpdateCommentAsync(CommentDTO commentDTO);

        Task DeleteCommentAsync(int id);

        Task<Project> GetProjectAsync(int id);

        Task<List<Project>> GetUserProjectsAsync(string userid);

        Task CreateProjectAsync(Project project);

        Task UpdateProjectAsync(Project project);

        Task DeleteProjectAsync(int projectid);

    }
}
