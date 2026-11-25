using Shared.DTOs;
using Shared.Models;

namespace PaaPindenBlazor.Services
{
    public interface IConversionService
    {
        Project ToProject(ProjectFormDTO projectFormDTO);
        ProjectFormDTO ToProjectFormDTO(Project project);
    }
}
