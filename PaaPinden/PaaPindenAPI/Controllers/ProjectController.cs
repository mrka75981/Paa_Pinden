using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaaPindenAPI.Data;
using PaaPindenAPI.Repositories;
using Shared.DTOs;
using Shared.Models;

namespace PaaPindenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProject(Project sentProject)
        {
            if (sentProject == null)
            {
                return BadRequest();
            }
            await _projectRepository.CreateProjectAsync(sentProject);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectAsync(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            await _projectRepository.DeleteProjectAsync(id);
            return Ok();
        }

        [HttpGet("/user/{userId}")]
        public async Task<IActionResult> GetUserProjectsAsync(string userId)
        {
            if (userId == null)
            {
                return BadRequest();
            }
            var projects = await _projectRepository.GetUserProjectsAsync(userId);
            return Ok(projects);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectAsync(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            var project = await _projectRepository.GetProjectAsync(id);
            return Ok(project);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProjectAsync(Project project)
        {
            if (project == null || project.Id <= 0)
                return BadRequest();

            await _projectRepository.UpdateProjectAsync(project);
            return Ok();
        }
    }
}
