using Shared.DTOs;
using Shared.Models;
using System.Text.Json;

namespace PaaPindenBlazor.Services
{
    public class PaaPindenAPIService : IPaaPindenAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PaaPindenAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task CreateCommentAsync(CommentDTO commentDTO)
        {
            using var httpClient = _httpClientFactory.CreateClient("PaaPindenClient");
            var productjson = JsonSerializer.Serialize(commentDTO);
            using var request = new HttpRequestMessage(HttpMethod.Post, $"api/Comment");
            request.Content = new StringContent(
                productjson,
                System.Text.Encoding.UTF8,
                "application/json");

            using var response = await httpClient.SendAsync(request);
        }

        public async Task CreateKnittingPost(KnittingPostDTO knittingPostDTO)
        {
            using var httpClient = _httpClientFactory.CreateClient("PaaPindenClient");
            var productjson = JsonSerializer.Serialize(knittingPostDTO);
            using var request = new HttpRequestMessage(HttpMethod.Post, $"api/KnttingPost");
            request.Content = new StringContent(
                productjson,
                System.Text.Encoding.UTF8,
                "application/json");

            using var response = await httpClient.SendAsync(request);
        }

        public async Task DeleteCommentAsync(int id)
        {
            using var httpClient = _httpClientFactory.CreateClient("PaaPindenClient");
            using var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Comment/{id}");
            using var response = await httpClient.SendAsync(request);
        }

        public async Task DeleteKnittingPostAsync(int id)
        {
            using var httpClient = _httpClientFactory.CreateClient("PaaPindenClient");
            using var request = new HttpRequestMessage(HttpMethod.Delete, $"api/KnittingPost/{id}");
            using var response = await httpClient.SendAsync(request);
        }

        public async Task<List<KnittingPost>> GetAllKnittingPostAsync()
        {
            using var httpClient = _httpClientFactory.CreateClient("PaaPindenClient");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/KnittingPost");
            using var response = await httpClient.SendAsync(request);
            List<KnittingPost> knittingPosts = await response.Content.ReadFromJsonAsync<List<KnittingPost>>();

            return knittingPosts;
        }

        public async Task<List<Comment>> GetAllPostCommentsAsync(int id)
        {
            using var httpClient = _httpClientFactory.CreateClient("PaaPindenClient");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/KnittingPost");
            using var response = await httpClient.SendAsync(request);
            List<Comment> comments = await response.Content.ReadFromJsonAsync<List<Comment>>();

            return comments;
        }

        public async Task<KnittingPost> GetKnittingPostAsync(int id)
        {
            using var httpClient = _httpClientFactory.CreateClient("PaaPindenClient");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/KnittingPost");
            using var response = await httpClient.SendAsync(request);
            KnittingPost knittingPost = await response.Content.ReadFromJsonAsync<KnittingPost>();

            return knittingPost;
        }

        public async Task UpdateCommentAsync(CommentDTO commentDTO)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            var productjson = JsonSerializer.Serialize(commentDTO);
            using var request = new HttpRequestMessage(HttpMethod.Put, $"api/Comment");
            request.Content = new StringContent(
                productjson,
                System.Text.Encoding.UTF8,
                "application/json");

            using var response = await httpClient.SendAsync(request);
        }

        public async Task UpdateKnittingPostAsync(KnittingPostDTO knittingPostDTO)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            var productjson = JsonSerializer.Serialize(knittingPostDTO);
            using var request = new HttpRequestMessage(HttpMethod.Put, $"api/KnittingPost");
            request.Content = new StringContent(
                productjson,
                System.Text.Encoding.UTF8,
                "application/json");

            using var response = await httpClient.SendAsync(request);
        }
    }
}
