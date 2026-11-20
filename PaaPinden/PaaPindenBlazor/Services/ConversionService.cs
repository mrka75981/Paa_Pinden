using Shared.DTOs;
using Shared.Models;

namespace PaaPindenBlazor.Services
{
    public class ConversionService : IConversionService
    {
        public Project ToProject(ProjectFormDTO projectFormDTO)
        {
            Project project = new Project()
            {
                Id = projectFormDTO.Id, 
                Title = projectFormDTO.Title, 
                Note = projectFormDTO.Note, 
                PicturePath = projectFormDTO.PicturePath,
                UserId = projectFormDTO.UserId
            };

            if (projectFormDTO.Needle != null) 
                project.Needle = projectFormDTO.Needle;
            
            if (projectFormDTO.YarnType != null)
                project.YarnType = projectFormDTO.YarnType;
            if (projectFormDTO.Recipe != null)
                project.Recipe = projectFormDTO.Recipe;
            if (projectFormDTO.Round != null)
                project.Round = projectFormDTO.Round.Value;
            if (projectFormDTO.Stitch != null)
                project.Stitch = projectFormDTO.Stitch.Value;

            return project;
        }

        public ProjectFormDTO ToProjectFormDTO(Project project)
        {
            return new ProjectFormDTO()
            {
                Id = project.Id,
                Title = project.Title,
                Note = project.Note,
                PicturePath = project.PicturePath,
                Needle = project.Needle,
                YarnType = project.YarnType,
                Recipe = project.Recipe,
                Round = project.Round,
                Stitch = project.Stitch, 
                UserId = project.UserId
            };
        }
    }
}
