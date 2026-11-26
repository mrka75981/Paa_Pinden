using Microsoft.EntityFrameworkCore;
using PaaPindenAPI.Data;
using Shared.Models;

namespace PaaPindenAPI.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly PaaPindenContext _paaPindenContext;

        public ProjectRepository(PaaPindenContext paaPindenContext)
        {
            _paaPindenContext = paaPindenContext;
        }
        public async Task CreateProjectAsync(Project project)
        {
            await _paaPindenContext.Projects.AddAsync(project);
            await _paaPindenContext.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await _paaPindenContext.Projects.FindAsync(id);
            if (project != null)
            {
                _paaPindenContext.Projects.Remove(project);
                await _paaPindenContext.SaveChangesAsync();
            }
        }

        public async Task<Project> GetProjectAsync(int id)
        {
            var project = await _paaPindenContext.Projects.FindAsync(id);
            if (project == null) return null;
            return project;
        }

        public async Task<List<Project>> GetUserProjectsAsync(string userId)
        {
            var projects = await _paaPindenContext.Projects.Where(c => c.UserId == userId).ToListAsync();
            if (projects != null)
            {
                return projects;
            }
            return null;
        }

        public async Task UpdateProjectAsync(Project project)
        {
            var changeProject = await GetProjectAsync(project.Id);

            changeProject.Needle = project.Needle;
            changeProject.Note = project.Note;
            changeProject.PicturePath = project.PicturePath;
            changeProject.Recipe = project.Recipe;
            changeProject.Round = project.Round;
            changeProject.Stitch = project.Stitch;
            changeProject.Title = project.Title;
            changeProject.YarnType = project.YarnType;

            await _paaPindenContext.SaveChangesAsync();
        }
    }
}
