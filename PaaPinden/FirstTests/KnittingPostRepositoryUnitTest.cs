using PaaPindenAPI.Models;
using PaaPindenAPI.Repositories;

namespace Tests
{
    [TestClass]
    public sealed class KnittingPostRepositoryUnitTest
    {
        KnittingPostRepository knittingPostRepository;

        CommentRepository commentRepository;

        KnittingPost expectedResult;

        [TestInitialize]
        private async void Init()
        {
            KnittingPostRepository knittingPostRepository = new KnittingPostRepository();

            CommentRepository commentRepository = new CommentRepository();

            var expectedResult = new KnittingPost
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
        }
        [TestMethod]
        public async Task CreateKnittingPostAsync_Test()
        {
            // Arange 
            
            var testPost = expectedResult;

            // Act 
            
            knittingPostRepository.CreateKnittingPostAsync(testPost);

            // Assert

            var actualResult = await knittingPostRepository.GetKnittingPostAsync(testPost.Id);
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public async Task GetKnittingPostAsync_Test()
        {
            // Arrange 

            var testPost = expectedResult;

            // Act 
            var actualResult = await knittingPostRepository.GetKnittingPostAsync(testPost.Id);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public async Task GetAllKnittingPostsAsync_Test()
        {
            // Arrange 
           
            var testPost = expectedResult;

            List<KnittingPost> expectedResultPosts = new List<KnittingPost>() { testPost };

            // Act 
            var actualResult = await knittingPostRepository.GetAllKnittingPostAsync();

            // Assert
            Assert.AreEqual(expectedResultPosts, actualResult);
        }

        [TestMethod]
        public async Task DeleteKnittingPostAsync_Test()
        {
            // Arrange 

            var testPost = expectedResult;

            // Act 
            knittingPostRepository.DeleteKnittingPostAsync(testPost.Id);

            // Assert
            var actualResult = await knittingPostRepository.GetKnittingPostAsync(testPost.Id);
            Assert.AreEqual(null, actualResult);

        } 

        public async Task UpdateKnittingPostAsync_Test()
        {
            // Arrange 

            var testPost = expectedResult;

            // Act 
            knittingPostRepository.UpdateKnittingPostAsync(testPost);

            // Assert
            var expectedUpdate = new KnittingPost
            {
                Title = "Opdateret tekst til opslag",
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

            Assert.AreEqual(expectedUpdate, testPost);
        }
    }

    
}
