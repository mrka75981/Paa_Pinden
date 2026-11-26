using Shared.Models;

namespace PaaPindenAPI.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> GetProjectAsync(int id);
        Task<List<Project>> GetUserProjectsAsync(string userId);
        Task CreateProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int id);
    }
}
