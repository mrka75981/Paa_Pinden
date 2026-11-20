using PaaPindenAPI.Data;
using Shared.Models;

namespace PaaPindenAPI.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly PaaPindenContext _context;
        public ProjectRepository(PaaPindenContext context)
        {
            _context = context;
        }
        public async Task<Project?> GetProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Project>> GetUserProjectsAsync(int userId)
        {
            throw new NotImplementedException();
        }
        public async Task CreateProjectAsync(Project project)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateProjectAsync(Project project)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }
    }
}
