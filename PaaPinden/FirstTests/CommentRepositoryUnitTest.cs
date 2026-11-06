using Microsoft.EntityFrameworkCore;
using PaaPindenAPI.Data;
using PaaPindenAPI.Models;
using PaaPindenAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass]
    public sealed class CommentRepositoryUnitTest
    {
        PaaPindenContext? paaPindenContext;
        KnittingPost? testInitializeKnittingPost;
        ICommentRepository? testMethodCommentRepo;
        Comment? testMethodComment;
        Comment? testInitializeComment;

        [TestInitialize]
        public async Task Init()
        {
            paaPindenContext = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
                .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
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

            paaPindenContext.Database.EnsureDeleted();
            paaPindenContext.Database.EnsureCreated();
            paaPindenContext.KnittingPosts.RemoveRange(paaPindenContext.KnittingPosts.AsNoTracking());
            paaPindenContext.SaveChanges();
            paaPindenContext.KnittingPosts.Add(testInitializeKnittingPost);
            paaPindenContext.SaveChanges();
            paaPindenContext.Comments.Add(testInitializeComment);
            paaPindenContext.SaveChanges();
        }

        [TestMethod]
        public async Task CreateCommentAsync_Test(Comment comment)
        {
            // Arrange
            var expectedResult = testMethodComment;

            // Act
            await testMethodCommentRepo.CreateCommentAsync(testMethodComment);

            // Assert
            var actualResult = paaPindenContext.Comments.Where(c => c.Id == testMethodComment.Id).FirstOrDefault();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task UpdateCommentAsync_Test(int id)
        {
            // Arrange
            testInitializeComment.Text = "ændret kommentar";
            var expectedResult = testInitializeComment;

            // Act
            await testMethodCommentRepo.UpdateCommentAsync(testInitializeComment);

            // Assert
            var actualResult = paaPindenContext.Comments.Where(c => c.Id == testInitializeComment.Id).FirstOrDefault();
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
            int testInitializeCommentId = testInitializeComment.Id;
            Comment? expectedResult = null;

            // Act
            await testMethodCommentRepo.DeleteCommentAsync(testInitializeComment.Id);

            // Assert
            var actualResult = paaPindenContext.Comments.Where(c => c.Id == testInitializeCommentId).FirstOrDefault(); //FirstOrDefault surely returns null if the list is empty
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
