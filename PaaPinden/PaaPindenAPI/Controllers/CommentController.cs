using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Models;
using PaaPindenAPI.Repositories;

namespace PaaPindenAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IKnittingPostRepository _knittingPostRepository;

        public CommentController(ICommentRepository commentRepository, IKnittingPostRepository knittingPostRepository)
        {
            _commentRepository = commentRepository;
            _knittingPostRepository = knittingPostRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync(CommentDTO commentDTO)
        {
            if (commentDTO == null)
                return BadRequest();

            var knittingPost = await _knittingPostRepository.GetKnittingPostAsync(commentDTO.CommentKnittingPostId);
            if (knittingPost == null)
            {
                return BadRequest();
            }

            Comment comment = new Comment()
            {
                TimeStamp = commentDTO.TimeStamp,
                Text = commentDTO.Text,
                Writer = commentDTO.Writer,
                CommentKnittingPostId = commentDTO.CommentKnittingPostId
            };

            await _commentRepository.CreateCommentAsync(comment);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllPostCommentsAsync(int id)
        {
            if (id <= 0) return BadRequest();

            var comments = await _commentRepository.GetAllPostCommentsAsync(id);

            if (comments == null) return NotFound();

            return Ok(comments);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCommentAsync(CommentDTO commentDTO)
        {
            if (commentDTO == null || commentDTO.Id <= 0)
                return BadRequest();

            var comment = await _commentRepository.GetCommentAsync(commentDTO.Id);
            if (comment == null) return NotFound();

            comment.Text = commentDTO.Text;
      
            await _commentRepository.UpdateCommentAsync(comment);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentAsync(int id)
        {
            if (id <= 0)
                return BadRequest();
            if (await  _commentRepository.GetCommentAsync(id) == null)
                return NotFound();

            await _commentRepository.DeleteCommentAsync(id);
            return Ok();
        }
    }
}
