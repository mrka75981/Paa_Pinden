using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.DTOs
{
    public class ProjectFormDTO
    {
        //intended to be hidden on the form
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Note { get; set; }
        public string? PicturePath { get; set; }
        public IFormFile? ImageFile { get; set; }
        [Required]
        public string? Needle { get; set; }
        [Required]
        public string? YarnType { get; set; }
        [Required]
        public string? Recipe { get; set; }
        [Required]
        public int? Round { get; set; }
        [Required]
        public int? Stitch { get; set; }
        //intended to be hidden on the form
        public string UserId { get; set; }
    }
}
