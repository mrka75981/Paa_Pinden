using Microsoft.AspNetCore.Identity;
using Shared.Models;

namespace PaaPindenAPI.Data
{
    public class User : IdentityUser
    {

        public List<KnittingPost>? KnittingPosts { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Project>? Projects { get; set; }
    }
}
