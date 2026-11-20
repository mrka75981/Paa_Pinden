using Shared.Models;

namespace PaaPindenAPI.Repositories
{
    public interface IProjectRepository
    {
        public Task<Project?> GetProjectAsync(int projectId);

        public Task<List<Project>> GetUserProjectsAsync(int userId);

        public Task CreateProjectAsync(Project project);

        public Task UpdateProjectAsync(Project project);

        public Task DeleteProjectAsync(int projectId);
    }
}
