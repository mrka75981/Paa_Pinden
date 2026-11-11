using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaaPindenAPI.DTOs;
using PaaPindenAPI.Models;
using PaaPindenAPI.Repositories;

namespace PaaPindenAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KnittingPostController : ControllerBase
    {
        private readonly IKnittingPostRepository _knittingPostRepository;

        public KnittingPostController(IKnittingPostRepository knittingPostRepository)
        {
            _knittingPostRepository = knittingPostRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateKnittingPost(KnittingPostDTO knittingPostDTO)
        {
            if (knittingPostDTO == null)
            {   
                return BadRequest();
            }
            KnittingPost knittingPost = new KnittingPost()
            {
                Author = knittingPostDTO.Author,
                KnittingPostContent = knittingPostDTO.KnittingPostContent,
                TimeStamp = knittingPostDTO.TimeStamp,
                Title = knittingPostDTO.Title,
                Comments = null,
                Id = 0
            };
            await _knittingPostRepository.CreateKnittingPostAsync(knittingPost);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKnittingPostAsync(int id)
        {
            var knittingPost = await _knittingPostRepository.GetKnittingPostAsync(id);
            if (knittingPost == null)
            {
                return BadRequest();
            }
            return Ok(knittingPost);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKnittingPostAsync()
        {
            var knittingPosts = await _knittingPostRepository.GetAllKnittingPostAsync();
            if (knittingPosts == null)
                return BadRequest();
            return Ok(knittingPosts);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateKnittingPostAsync(KnittingPostDTO knittingPostDTO)
        {
            if (knittingPostDTO == null || knittingPostDTO.Id <= 0)
                return BadRequest();
            var knittingPost = await _knittingPostRepository.GetKnittingPostAsync(knittingPostDTO.Id);

            if (knittingPost == null)
                return NotFound();

            //Remap changable variables
            knittingPost.KnittingPostContent = knittingPostDTO.KnittingPostContent;
            knittingPost.Title = knittingPostDTO.Title;


            await _knittingPostRepository.UpdateKnittingPostAsync(knittingPost);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKnittingPostAsync(int id)
        {
            if (id <= 0)
                return BadRequest();
            if (await _knittingPostRepository.GetKnittingPostAsync(id) == null)
                return NotFound();

            await _knittingPostRepository.DeleteKnittingPostAsync(id);
            return Ok();
        }
    }
}
