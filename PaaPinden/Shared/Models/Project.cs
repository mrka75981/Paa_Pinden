using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models
{
   public class Project
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Note { get; set; }
    public string? PicturePath { get; set; }
    public string Needle { get; set; }
    public string YarnType { get; set; }
    public string Recipe { get; set; }
    public int Round { get; set; }
    public int Stitch { get; set; }
    public string UserId { get; set; }
}
}
