using PaaPindenAPI.Models;
using PaaPindenAPI.Repositories;

namespace FirstTests
{
    [TestClass]
    public sealed class UnitTest
    {
        [TestMethod]
        public void CreateKnittingPostAsync_Test()
        {



            // Arange 

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

            var testPost = expectedResult;

            // Act 
            
            var actualResult = await knittingPostRepository.CreateKnittingPostAsync(testPost);

            
            

            // Assert

            Assert.AreEqual(expectedResult, actualResult);

        }
    }
}
