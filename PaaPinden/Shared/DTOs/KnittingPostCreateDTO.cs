using Microsoft.AspNetCore.Http;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOs
{
    public class KnittingPostCreateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Author { get; set; }
        public Content KnittingPostContent { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
