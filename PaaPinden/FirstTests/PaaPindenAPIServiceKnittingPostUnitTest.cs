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

namespace Tests
{
    [TestClass]
    public sealed class PaaPindenAPIServiceKnittingPostUnitTest
    {
        private PaaPindenContext? _paaPindenContextForInit;
        private PaaPindenContext? _paaPindenContextForRepo;
        private KnittingPost? _testMethodKnittingPost;
        private KnittingPostDTO _testMethodKnittingPostDTO;
        private KnittingPost? _testInitializeKnittingPost;
        private KnittingPostDTO? _testInitializeKnittingPostDTO;


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

            if (_testMethodKnittingPost != null)
            {
                _testMethodKnittingPostDTO = new KnittingPostDTO()
                {
                    Id = _testMethodKnittingPost.Id,
                    Title = _testMethodKnittingPost.Title,
                    TimeStamp = _testMethodKnittingPost.TimeStamp,
                    Author = _testMethodKnittingPost.Author,
                    KnittingPostContent = _testMethodKnittingPost.KnittingPostContent
                };
            }

            if (_testInitializeKnittingPost != null)
            {
                _testInitializeKnittingPostDTO = new KnittingPostDTO()
                {
                    Id = _testInitializeKnittingPost.Id,
                    Title = _testInitializeKnittingPost.Title,
                    TimeStamp = _testInitializeKnittingPost.TimeStamp,
                    Author = _testInitializeKnittingPost.Author,
                    KnittingPostContent = _testInitializeKnittingPost.KnittingPostContent
                };
            }
        }

        private async Task DatabaseInit()
        {
            _paaPindenContextForInit = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
                .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
            _paaPindenContextForRepo = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
                .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
            _testMethodKnittingPost = new KnittingPost
            {
                Title = "test opslag",
                TimeStamp = DateTime.Today,
                Author = "Jens",
                KnittingPostContent = new Content
                {
                    Text = "Mit første opslag!",
                    Pictures = new List<ContentPicture>()
                    {
                        new ContentPicture
                        {
                            Path = "~/path/to/testpicture"
                        }
                    },
                    Videos = new List<ContentVideo>()
                    {
                        new ContentVideo
                        {
                            Path = "~/path/to/testvideo"
                        }
                    }
                }
            };
            _testInitializeKnittingPost = new KnittingPost
            {
                Title = "Første opslag",
                TimeStamp = DateTime.Today,
                Author = "Jens",
                Comments = new List<Comment>()
                {
                    new Comment
                    {
                        Text = "første kommentar",
                        Writer = "jens fjende",
                        TimeStamp = DateTime.Today
                    }
                },
                KnittingPostContent = new Content
                {
                    Text = "første opslag!",
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

            _paaPindenContextForInit.Database.EnsureDeleted();
            _paaPindenContextForInit.Database.EnsureCreated();
            _paaPindenContextForInit.KnittingPosts.RemoveRange(_paaPindenContextForInit.KnittingPosts.AsNoTracking());
            _paaPindenContextForInit.SaveChanges();
            _paaPindenContextForInit.KnittingPosts.Add(_testInitializeKnittingPost);
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
        public async Task CreateKnittingPostAsync_Test()
        {
            // Arrange
            _testMethodKnittingPost.Id = 2; //it will be assigned two given the state of the board. 
            var expectedResult = _testMethodKnittingPost;

            // Act
            await _paaPindenAPIService.CreateKnittingPostAsync(_testMethodKnittingPostDTO);

            // Assert
            var actualResult = _paaPindenContextForRepo.KnittingPosts.Where(kp => kp.Id == _testMethodKnittingPost.Id).FirstOrDefault();
            Assert.AreEqual(expectedResult.Id, actualResult.Id);
        }

        [TestMethod]
        public async Task GetKnittingPostAsync_Test()
        {
            // Arrange
            _testInitializeKnittingPost.Id = 1; //it will be assigned one given the state of the board. 
            var expectedResult = _testInitializeKnittingPost;

            // Act
            var actualResult = await _paaPindenAPIService.GetKnittingPostAsync(_testInitializeKnittingPost.Id);

            // Assert
            Assert.AreEqual(expectedResult.Id, actualResult.Id);
            Assert.AreEqual(expectedResult.Title, actualResult.Title);
            Assert.AreEqual(expectedResult.TimeStamp, actualResult.TimeStamp);
            Assert.AreEqual(expectedResult.Author, actualResult.Author);

            Assert.IsTrue(actualResult.Comments.Select(c => c.Id).All(c => expectedResult.Comments.Select(c => c.Id).Contains(c))); //All actualResults members are contained within expectedResults. 
            Assert.HasCount(expectedResult.Comments.Count, actualResult.Comments); //expectedResults do not have more members than actualResults (rendering the prior assertion incomplete). 

            Assert.AreEqual(expectedResult.KnittingPostContent.Id, actualResult.KnittingPostContent.Id);
            Assert.AreEqual(expectedResult.KnittingPostContent.Text, actualResult.KnittingPostContent.Text);

            Assert.IsTrue(actualResult.KnittingPostContent.Pictures.Select(p => p.Path).All(p => expectedResult.KnittingPostContent.Pictures.Select(p => p.Path).Contains(p))); //All actualResults members are contained within expectedResults. 
            Assert.HasCount(expectedResult.KnittingPostContent.Pictures.Count, actualResult.KnittingPostContent.Pictures); //expectedResults do not have more members than actualResults (rendering the prior assertion incomplete). 

            Assert.IsTrue(actualResult.KnittingPostContent.Videos.Select(p => p.Path).All(p => expectedResult.KnittingPostContent.Videos.Select(p => p.Path).Contains(p))); //All actualResults members are contained within expectedResults. 
            Assert.HasCount(expectedResult.KnittingPostContent.Videos.Count, actualResult.KnittingPostContent.Videos); //expectedResults do not have more members than actualResults (rendering the prior assertion incomplete). 
        }

        [TestMethod]
        public async Task GetAllKnittingPostsAsync_Test()
        {
            // Arrange
            List<KnittingPost> expectedResults = new List<KnittingPost>() { _testInitializeKnittingPost };

            // Act
            var actualResults = await _paaPindenAPIService.GetAllKnittingPostAsync();

            // Assert
            Assert.IsTrue(actualResults.Select(kp => kp.Id).All(kp => expectedResults.Select(kp => kp.Id).Contains(kp))); //All actualResults members are contained within expectedResults. 
            Assert.HasCount(expectedResults.Count, actualResults); //expectedResults do not have more members than actualResults (rendering the prior assertion incomplete). 
        }

        [TestMethod]
        public async Task DeleteKnittingPostAsync_Test()
        {
            // Arrange
            int testInitializeKnittingPostId = _testInitializeKnittingPost.Id;
            KnittingPost? expectedResult = null;

            // Act
            await _paaPindenAPIService.DeleteKnittingPostAsync(_testInitializeKnittingPost.Id);

            // Assert
            var actualResult = _paaPindenContextForRepo.KnittingPosts.Where(kp => kp.Id == testInitializeKnittingPostId).FirstOrDefault(); //FirstOrDefault surely returns null if the list is empty
            Assert.AreEqual(expectedResult, actualResult);

        }

        public async Task UpdateKnittingPostAsync_Test()
        {
            // Arrange
            _testInitializeKnittingPost.Title = "edited title";
            _testInitializeKnittingPost.KnittingPostContent.Pictures[0].Path = "~/path/to/editedpicture";
            _testInitializeKnittingPost.KnittingPostContent.Videos[0].Path = "~/path/to/editedvideo";
            var expectedResult = _testInitializeKnittingPost;

            // Act
            await _paaPindenAPIService.UpdateKnittingPostAsync(_testInitializeKnittingPostDTO);

            // Assert
            var actualResult = _paaPindenContextForRepo.KnittingPosts.Where(kp => kp.Id == _testInitializeKnittingPost.Id).FirstOrDefault();
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
