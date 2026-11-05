using Microsoft.AspNetCore.Mvc;
using PaaPindenAPI.Repositories;

namespace PaaPindenAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController
    {
        private readonly IKnittingPostRepository _knittingPostRepository;

        public Task<IActionResult> CreateKnittingPost()
        {

        }

        public Task<IActionResult> GetKnittingPostAsync()
        {

        }

        public Task<IActionResult> GetAllKnittingPostAsync()
        {

        }

        public Task<IActionResult> UpdateKnittingPostAsync()
        {

        }
        public Task<IActionResult> DeleteKnittingPostAsync()
        {

        }
    }
}
