using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaaPindenAPI.Repositories;

namespace PaaPindenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectRepository _projectRepository;

    }
}
