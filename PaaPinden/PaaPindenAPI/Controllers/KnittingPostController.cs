using Microsoft.AspNetCore.Mvc;
using PaaPindenAPI.Models;
using PaaPindenAPI.Repositories;

namespace PaaPindenAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KnittingPostController
    {
        private readonly ICommentRepository _commentRepository;

        public KnittingPostController()
        {

        }

        public Task<IActionResult> CreateCommentAsync(Comment comment)
        {
            
        }
        public Task<IActionResult> UpdateCommentAsync(Comment comment)
        {

        }
        public Task<IActionResult> DeleteCommentAsync(int id)
        {

        }
    }
}
