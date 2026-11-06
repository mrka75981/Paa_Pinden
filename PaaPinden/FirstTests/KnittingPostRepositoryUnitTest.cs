using Microsoft.EntityFrameworkCore;
using PaaPindenAPI.Data;
using PaaPindenAPI.Models;
using PaaPindenAPI.Repositories;

namespace Tests
{
    [TestClass]
    public sealed class KnittingPostRepositoryUnitTest
    {
        PaaPindenContext paaPindenContext = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
            .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
        IKnittingPostRepository? testMethodRepo;
        KnittingPost? testMethodKnittingPost;
        KnittingPost? testInitializeKnittingPost;

        [TestInitialize]
        public async Task Init()
        {
            testMethodRepo = new KnittingPostRepository();
            testMethodKnittingPost = new KnittingPost
            {
                Title = "test opslag",
                TimeStamp = DateTime.Today,
                Author = "Jens",
                KnittingPostContent = new Content
                {
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
            testInitializeKnittingPost = new KnittingPost
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

            paaPindenContext.Database.EnsureDeleted();
            paaPindenContext.Database.EnsureCreated();

            paaPindenContext.AddRange(testInitializeKnittingPost);
            paaPindenContext.SaveChanges();


        }

        [TestMethod]
        public async Task CreateKnittingPostAsync_Test()
        {
            // Arrange
            var expectedResult = testMethodKnittingPost;

            // Act
            await testMethodRepo.CreateKnittingPostAsync(testMethodKnittingPost);

            // Assert
            //find actualResult the board with paapindencontext

            var actualResult = paaPindenContext.KnittingPosts.Where( kp => kp.Id == testMethodKnittingPost.Id).FirstOrDefault();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task GetKnittingPostAsync_Test()
        {
            // Arrange
            var expectedResult = testInitializeKnittingPost;

            // Act
            var actualResult = await testMethodRepo.GetKnittingPostAsync(testInitializeKnittingPost.Id);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task GetAllKnittingPostsAsync_Test()
        {
            // Arrange
            List<KnittingPost> expectedResult = new List<KnittingPost>() { testInitializeKnittingPost };

            // Act
            var actualResult = await testMethodRepo.GetAllKnittingPostAsync();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task DeleteKnittingPostAsync_Test()
        {
            // Arrange
            int testInitializeKnittingPostId = testInitializeKnittingPost.Id;
            KnittingPost? expectedResult = null;

            // Act
            await testMethodRepo.DeleteKnittingPostAsync(testInitializeKnittingPost.Id);

            // Assert
            var actualResult = paaPindenContext.KnittingPosts.Where(kp => kp.Id == testInitializeKnittingPost.Id).FirstOrDefault();
            Assert.AreEqual(expectedResult, actualResult);

        }

        public async Task UpdateKnittingPostAsync_Test()
        {
            // Arrange
            testInitializeKnittingPost.Title = "edited title";
            testInitializeKnittingPost.KnittingPostContent.Pictures[0].Path = "~/path/to/editedpicture";
            testInitializeKnittingPost.KnittingPostContent.Videos[0].Path = "~/path/to/editedvideo";
            var expectedResult = testInitializeKnittingPost;

            // Act
            await testMethodRepo.UpdateKnittingPostAsync(testInitializeKnittingPost);

            // Assert
            var actualResult = paaPindenContext.KnittingPosts.Where(kp => kp.Id == testInitializeKnittingPost.Id).FirstOrDefault();
            Assert.AreEqual(expectedResult, actualResult);
        }
    }


}
