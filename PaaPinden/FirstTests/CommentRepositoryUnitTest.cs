using PaaPindenAPI.Models;
using PaaPindenAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Tests
{
    [TestClass]
    public sealed class CommentRepositoryUnitTest
    {
        IKnittingPostRepository? testInitializeKnittingPostRepo;
        KnittingPost? testInitializeKnittingPost;
        ICommentRepository? testMethodCommentRepo;
        ICommentRepository? testInitializeCommentRepo;
        Comment? testMethodComment;
        Comment? testInitializeComment;

        [TestInitialize]
        public async Task Init()
        {
            testInitializeKnittingPostRepo = new KnittingPostRepository();
            testInitializeKnittingPost = new KnittingPost
            {
                Title = "Første opslag",
                TimeStamp = DateTime.Today,
                Author = "Jens",
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
            testMethodCommentRepo = new CommentRepository();
            testInitializeCommentRepo = new CommentRepository();
            testMethodComment = new Comment()
            {
                CommentKnittingPost = testInitializeKnittingPost,
                Text = "test kommentar",
                Writer = "jens fjende",
                TimeStamp = DateTime.Today
            };
            testInitializeComment = new Comment()
            {
                CommentKnittingPost = testInitializeKnittingPost,
                Text = "første kommentar",
                Writer = "jens fjende",
                TimeStamp = DateTime.Today
            };

            //await testInitializeRepo.DeleteAllKnittingPostsAsync();
            await testInitializeKnittingPostRepo.CreateKnittingPostAsync(testInitializeKnittingPost);
            await testInitializeCommentRepo.CreateCommentAsync(testInitializeComment);
        }

        [TestMethod]
        public async Task CreateCommentAsync_Test(Comment comment)
        {
            // Arrange
            var expectedResult = testMethodComment;

            // Act
            await testMethodCommentRepo.CreateCommentAsync(testMethodComment);

            // Assert
            var actualResultList = await testMethodCommentRepo.GetAllPostCommentsAsync(testMethodComment.CommentKnittingPost.Id);
            var actualResult = actualResultList.First();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task UpdateCommentAsync_Test(int id)
        {
            // Arrange
            testInitializeComment.Text = "ændret kommentar";
            var expectedResult = testInitializeComment;

            // Act
            await testMethodCommentRepo.UpdateCommentAsync(testInitializeComment, testInitializeComment.Id);

            // Assert
            var actualResultList = await testMethodCommentRepo.GetAllPostCommentsAsync(testInitializeComment.CommentKnittingPost.Id);
            var actualResult = actualResultList.First();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task GetAllPostCommentsAsync_Test(int id)
        {
            // Arrange
            List<Comment> expectedResult = new List<Comment>() { testInitializeComment };

            // Act
            List<Comment> actualResult = await testMethodCommentRepo.GetAllPostCommentsAsync(testInitializeComment.CommentKnittingPost.Id);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task DeleteCommentAsync_Test(int id)
        {
            // Arrange
            int testInitializeCommentCommentKnittingPostId = testInitializeComment.CommentKnittingPost.Id;
            Comment? expectedResult = null;

            // Act
            await testMethodCommentRepo.DeleteCommentAsync(testInitializeComment.Id);

            // Assert
            List<Comment> actualResultList = await testMethodCommentRepo.GetAllPostCommentsAsync(testInitializeCommentCommentKnittingPostId);
            Comment? actualResult;
            if (actualResultList.Count == 0)
            {
                actualResult = null;
            }
            else
            {
                actualResult = actualResultList.First();
            }
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
