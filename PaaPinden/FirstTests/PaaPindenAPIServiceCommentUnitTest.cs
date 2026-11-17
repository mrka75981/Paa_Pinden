using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaaPindenAPI.Data;
using PaaPindenAPI.Repositories;
using PaaPindenBlazor.Services;
using Shared.DTOs;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Tests
{
    [TestClass]
    public sealed class PaaPindenAPIServiceCommentUnitTest
    {
        private PaaPindenContext? _paaPindenContextForInit;
        private PaaPindenContext? _paaPindenContextForRepo;
        private KnittingPost? _testInitializeKnittingPost;
        private Comment? _testMethodComment;
        private CommentDTO? _testMethodCommentDTO;
        private Comment? _testInitializeComment;
        private CommentDTO? _testInitializeCommentDTO;

        private ServiceProvider? _provider;
        private PaaPindenAPIService? _paaPindenAPIService;

        [TestInitialize]
        public async Task Init()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddHttpClient("PaaPindenClient", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5044/");
            });
            _provider = services.BuildServiceProvider();
            IHttpClientFactory factory = _provider.GetRequiredService<IHttpClientFactory>();
            _paaPindenAPIService = new PaaPindenAPIService(factory);

            await DatabaseInit();

            if (_testMethodComment != null)
            {
                _testMethodCommentDTO = new CommentDTO()
                {
                    Id = _testMethodComment.Id,
                    CommentKnittingPostId = _testMethodComment.CommentKnittingPostId,
                    Text = _testMethodComment.Text,
                    TimeStamp = _testMethodComment.TimeStamp,
                    Writer = _testMethodComment.Writer
                };
            }

            if (_testInitializeComment != null)
            {
                _testInitializeCommentDTO = new CommentDTO()
                {
                    Id = _testInitializeComment.Id,
                    CommentKnittingPostId = _testInitializeComment.CommentKnittingPostId,
                    Text = _testInitializeComment.Text,
                    TimeStamp = _testInitializeComment.TimeStamp,
                    Writer = _testInitializeComment.Writer
                };
            }
        }

        private async Task DatabaseInit()
        {
            _paaPindenContextForInit = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
                .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
            _paaPindenContextForRepo = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
                .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
            _testInitializeKnittingPost = new KnittingPost
            {
                Title = "Første opslag",
                TimeStamp = DateTime.Today,
                Author = "Jens",
                KnittingPostContent = new Content
                {
                    Text = "Mit første opslag!",
                    Pictures = new List<ContentPicture>()
                    {
                        new ContentPicture
                        {
                            Path = "~/path/to/picture"
                        }
                    },
                    Videos = new List<ContentVideo>()
                    {
                        new ContentVideo
                        {
                            Path = "~/path/to/video"
                        }
                    }
                }
            };
            _testMethodComment = new Comment()
            {
                CommentKnittingPostId = _testInitializeKnittingPost.Id,
                Text = "test kommentar",
                Writer = "jens fjende",
                TimeStamp = DateTime.Today
            };
            _testInitializeComment = new Comment()
            {
                CommentKnittingPostId = _testInitializeKnittingPost.Id,
                Text = "første kommentar",
                Writer = "jens fjende",
                TimeStamp = DateTime.Today
            };

            _paaPindenContextForInit.Database.EnsureDeleted();
            _paaPindenContextForInit.Database.EnsureCreated();
            _paaPindenContextForInit.KnittingPosts.RemoveRange(_paaPindenContextForInit.KnittingPosts.AsNoTracking());
            _paaPindenContextForInit.SaveChanges();
            _paaPindenContextForInit.KnittingPosts.Add(_testInitializeKnittingPost);
            _paaPindenContextForInit.SaveChanges();

            _testInitializeComment.CommentKnittingPostId = _testInitializeKnittingPost.Id;
            _testMethodComment.CommentKnittingPostId = _testInitializeKnittingPost.Id;

            _paaPindenContextForInit.Comments.Add(_testInitializeComment);
            _paaPindenContextForInit.SaveChanges();
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_provider != null)
            {
                _provider.Dispose();
            }
        }

        [TestMethod]
        public async Task CreateCommentAsync_Test()
        {
            // Arrange
            _testMethodComment.Id = 2; //it will be assigned two given the state of the board. 
            var expectedResult = _testMethodComment;

            // Act
            await _paaPindenAPIService.CreateCommentAsync(_testMethodCommentDTO);

            // Assert
            var actualResult = _paaPindenContextForRepo.Comments.Where(c => c.Id == _testMethodComment.Id).FirstOrDefault();
            Assert.AreEqual(expectedResult.CommentKnittingPostId, actualResult.CommentKnittingPostId);
            Assert.AreEqual(expectedResult.Text, actualResult.Text);
            Assert.AreEqual(expectedResult.TimeStamp, actualResult.TimeStamp);
            Assert.AreEqual(expectedResult.Writer, actualResult.Writer);
        }

        [TestMethod]
        public async Task UpdateCommentAsync_Test()
        {
            // Arrange
            _testInitializeComment.Text = "ændret kommentar";
            _testInitializeCommentDTO.Text = "ændret kommentar";
            var expectedResult = _testInitializeComment;

            // Act
            await _paaPindenAPIService.UpdateCommentAsync(_testInitializeCommentDTO);

            // Assert
            var actualResult = _paaPindenContextForRepo.Comments.Where(c => c.Id == _testInitializeComment.Id).FirstOrDefault();
            Assert.AreEqual(expectedResult.Text, actualResult.Text);
            Assert.AreEqual(expectedResult.TimeStamp, actualResult.TimeStamp);
            Assert.AreEqual(expectedResult.Writer, actualResult.Writer);
            Assert.AreEqual(expectedResult.CommentKnittingPostId, actualResult.CommentKnittingPostId);
        }

        [TestMethod]
        public async Task GetAllPostCommentsAsync_Test()
        {
            // Arrange
            List<Comment> expectedResults = new List<Comment>() { _testInitializeComment };

            // Act
            List<Comment> actualResults = await _paaPindenAPIService.GetAllPostCommentsAsync(_testInitializeComment.CommentKnittingPostId);

            // Assert
            Assert.IsTrue(actualResults.Select(c => c.Id).All(c => expectedResults.Select(c => c.Id).Contains(c))); //All actualResults members are contained within expectedResults. 
            Assert.HasCount(expectedResults.Count, actualResults); //expectedResults do not have more members than actualResults (rendering the prior assertion incomplete). 
        }

        [TestMethod]
        public async Task DeleteCommentAsync_Test()
        {
            // Arrange
            int testInitializeCommentId = _testInitializeComment.Id;
            Comment? expectedResult = null;

            // Act
            await _paaPindenAPIService.DeleteCommentAsync(_testInitializeComment.Id);

            // Assert
            var actualResult = _paaPindenContextForRepo.Comments.Where(c => c.Id == testInitializeCommentId).FirstOrDefault(); //FirstOrDefault surely returns null if the list is empty
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
