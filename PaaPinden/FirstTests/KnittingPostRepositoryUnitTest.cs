using PaaPindenAPI.Models;
using PaaPindenAPI.Repositories;

namespace Tests
{
    [TestClass]
    public sealed class KnittingPostRepositoryUnitTest
    {
        IKnittingPostRepository? testMethodRepo;
        IKnittingPostRepository? testInitializeRepo;
        KnittingPost? testMethodKnittingPost;
        KnittingPost? testInitializeKnittingPost;

        [TestInitialize]
        public async Task Init()
        {
            testMethodRepo = new KnittingPostRepository();
            testInitializeRepo = new KnittingPostRepository();
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

            //await testInitializeRepo.DeleteAllKnittingPostsAsync();
            await testInitializeRepo.CreateKnittingPostAsync(testInitializeKnittingPost);
        }

        [TestMethod]
        public async Task CreateKnittingPostAsync_Test()
        {
            // Arange
            var expectedResult = testMethodKnittingPost;

            // Act
            await testMethodRepo.CreateKnittingPostAsync(testMethodKnittingPost);

            // Assert
            var actualResult = await testMethodRepo.GetKnittingPostAsync(testMethodKnittingPost.Id);
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
            testMethodRepo.DeleteKnittingPostAsync(testInitializeKnittingPost.Id);

            // Assert
            var actualResult = await testMethodRepo.GetKnittingPostAsync(testInitializeKnittingPostId);
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
            testMethodRepo.UpdateKnittingPostAsync(testInitializeKnittingPost);

            // Assert
            var actualResult = await testMethodRepo.GetKnittingPostAsync(testInitializeKnittingPost.Id);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }


}
