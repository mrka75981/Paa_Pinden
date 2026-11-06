//using Microsoft.EntityFrameworkCore;
//using PaaPindenAPI.Data;
//using PaaPindenAPI.Models;
//using PaaPindenAPI.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Tests
//{
//    [TestClass]
//    public sealed class CommentRepositoryUnitTest
//    {
//        PaaPindenContext paaPindenContext = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
//            .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
//        KnittingPost? testInitializeKnittingPost;
//        ICommentRepository? testMethodCommentRepo;
//        Comment? testMethodComment;
//        Comment? testInitializeComment;

//        [TestInitialize]
//        public async Task Init()
//        {
//            testInitializeKnittingPost = new KnittingPost
//            {
//                Title = "Første opslag",
//                TimeStamp = DateTime.Today,
//                Author = "Jens",
//                KnittingPostContent = new Content
//                {
//                    Pictures = new List<ContentPicture>()
//                    {
//                        new ContentPicture
//                        {
//                            Path = "~/path/to/picture"
//                        }
//                    },
//                    Videos = new List<ContentVideo>()
//                    {
//                        new ContentVideo
//                        {
//                            Path = "~/path/to/video"
//                        }
//                    }
//                }
//            };
//            testMethodCommentRepo = new CommentRepository();
//            testMethodComment = new Comment()
//            {
//                CommentKnittingPost = testInitializeKnittingPost,
//                Text = "test kommentar",
//                Writer = "jens fjende",
//                TimeStamp = DateTime.Today
//            };
//            testInitializeComment = new Comment()
//            {
//                CommentKnittingPost = testInitializeKnittingPost,
//                Text = "første kommentar",
//                Writer = "jens fjende",
//                TimeStamp = DateTime.Today
//            };

//            //reset the board with paapindencontext
//        }

//        [TestMethod]
//        public async Task CreateCommentAsync_Test(Comment comment)
//        {
//            // Arrange
//            var expectedResult = testMethodComment;

//            // Act
//            await testMethodCommentRepo.CreateCommentAsync(testMethodComment);

//            // Assert
//            //find actualResult the board with paapindencontext
//            Assert.AreEqual(expectedResult, actualResult);
//        }

//        [TestMethod]
//        public async Task UpdateCommentAsync_Test(int id)
//        {
//            // Arrange
//            testInitializeComment.Text = "ændret kommentar";
//            var expectedResult = testInitializeComment;

//            // Act
//            await testMethodCommentRepo.UpdateCommentAsync(testInitializeComment, testInitializeComment.Id);

//            // Assert
//            //find actualResult the board with paapindencontext
//            Assert.AreEqual(expectedResult, actualResult);
//        }

//        [TestMethod]
//        public async Task GetAllPostCommentsAsync_Test(int id)
//        {
//            // Arrange
//            List<Comment> expectedResult = new List<Comment>() { testInitializeComment };

//            // Act
//            List<Comment> actualResult = await testMethodCommentRepo.GetAllPostCommentsAsync(testInitializeComment.CommentKnittingPost.Id);

//            // Assert
//            Assert.AreEqual(expectedResult, actualResult);
//        }

//        [TestMethod]
//        public async Task DeleteCommentAsync_Test(int id)
//        {
//            // Arrange
//            int testInitializeCommentCommentKnittingPostId = testInitializeComment.CommentKnittingPost.Id;
//            Comment? expectedResult = null;

//            // Act
//            await testMethodCommentRepo.DeleteCommentAsync(testInitializeComment.Id);

//            // Assert
//            //find actualResult the board with paapindencontext
//            Assert.AreEqual(expectedResult, actualResult);
//        }
//    }
//}
