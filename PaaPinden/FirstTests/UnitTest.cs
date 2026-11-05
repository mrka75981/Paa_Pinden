using PaaPindenAPI.Models;
using PaaPindenAPI.Repositories;

namespace FirstTests
{
    [TestClass]
    public sealed class UnitTest
    {
        KnittingPostRepository knittingPostRepository;

        CommentRepository commentRepository;

        KnittingPost expectedResult;

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


            Init();
            

            var testPost = expectedResult;

            // Act 
            
            await knittingPostRepository.CreateKnittingPostAsync(testPost);




            // Assert

            var actualResult = await knittingPostRepository.GetKnittingPostAsync(testPost.Id);
            Assert.AreEqual(expectedResult, actualResult);

        }
    }
}
