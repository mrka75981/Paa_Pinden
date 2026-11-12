using Microsoft.EntityFrameworkCore;
using PaaPindenAPI.Data;
using Shared.Models;
using PaaPindenAPI.Repositories;

namespace Tests
{
    [TestClass]
    public sealed class KnittingPostRepositoryUnitTest
    {
        PaaPindenContext? paaPindenContextForRepo;
        PaaPindenContext? paaPindenContextForInit;
        IKnittingPostRepository? testMethodRepo;
        KnittingPost? testMethodKnittingPost;
        KnittingPost? testInitializeKnittingPost;

        [TestInitialize]
        public async Task Init()
        {
            paaPindenContextForRepo = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
                .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
            paaPindenContextForInit = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
                .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
            testMethodRepo = new KnittingPostRepository(paaPindenContextForRepo);
            testMethodKnittingPost = new KnittingPost
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

            paaPindenContextForInit.Database.EnsureDeleted();
            paaPindenContextForInit.Database.EnsureCreated();
            paaPindenContextForInit.KnittingPosts.RemoveRange(paaPindenContextForInit.KnittingPosts.AsNoTracking());
            paaPindenContextForInit.SaveChanges();
            paaPindenContextForInit.KnittingPosts.Add(testInitializeKnittingPost);
            paaPindenContextForInit.SaveChanges();
        }

        [TestMethod]
        public async Task CreateKnittingPostAsync_Test()
        {
            // Arrange
            var expectedResult = testMethodKnittingPost;

            // Act
            await testMethodRepo.CreateKnittingPostAsync(testMethodKnittingPost);

            // Assert
            var actualResult = paaPindenContextForInit.KnittingPosts.Where( kp => kp.Id == testMethodKnittingPost.Id).FirstOrDefault();
            Assert.AreEqual(expectedResult.Id, actualResult.Id);
        }

        [TestMethod]
        public async Task GetKnittingPostAsync_Test()
        {
            // Arrange
            var expectedResult = testInitializeKnittingPost;

            // Act
            var actualResult = await testMethodRepo.GetKnittingPostAsync(testInitializeKnittingPost.Id);

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
            List<KnittingPost> expectedResults = new List<KnittingPost>() { testInitializeKnittingPost };

            // Act
            var actualResults = await testMethodRepo.GetAllKnittingPostAsync();

            // Assert
            Assert.IsTrue(actualResults.Select(kp => kp.Id).All(kp => expectedResults.Select(kp => kp.Id).Contains(kp))); //All actualResults members are contained within expectedResults. 
            Assert.HasCount(expectedResults.Count, actualResults); //expectedResults do not have more members than actualResults (rendering the prior assertion incomplete). 
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
            var actualResult = paaPindenContextForInit.KnittingPosts.Where(kp => kp.Id == testInitializeKnittingPostId).FirstOrDefault(); //FirstOrDefault surely returns null if the list is empty
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
            var actualResult = paaPindenContextForInit.KnittingPosts.Where(kp => kp.Id == testInitializeKnittingPost.Id).FirstOrDefault();
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
