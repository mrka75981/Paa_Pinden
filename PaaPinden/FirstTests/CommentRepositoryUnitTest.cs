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
        PaaPindenContext? paaPindenContextForRepo;
        PaaPindenContext? paaPindenContextForInit;
        KnittingPost? testInitializeKnittingPost;
        ICommentRepository? testMethodCommentRepo;
        Comment? testMethodComment;
        Comment? testInitializeComment;

        [TestInitialize]
        public async Task Init()
        {
            paaPindenContextForRepo = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
                .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
            paaPindenContextForInit = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
                .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
            testInitializeKnittingPost = new KnittingPost
            {
                Title = "Første opslag",
                TimeStamp = DateTime.Today,
                Author = "Jens",
                KnittingPostContent = new Content
                {
                    Text = "Mit første opslag!",
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
            testMethodCommentRepo = new CommentRepository(paaPindenContextForRepo);
            testMethodComment = new Comment()
            {
                CommentKnittingPostId = testInitializeKnittingPost.Id,
                Text = "test kommentar",
                Writer = "jens fjende",
                TimeStamp = DateTime.Today
            };
            testInitializeComment = new Comment()
            {
                CommentKnittingPostId = testInitializeKnittingPost.Id,
                Text = "første kommentar",
                Writer = "jens fjende",
                TimeStamp = DateTime.Today
            };

            paaPindenContextForInit.Database.EnsureDeleted();
            paaPindenContextForInit.Database.EnsureCreated();
            paaPindenContextForInit.KnittingPosts.RemoveRange(paaPindenContextForRepo.KnittingPosts.AsNoTracking());
            paaPindenContextForInit.SaveChanges();
            paaPindenContextForInit.KnittingPosts.Add(testInitializeKnittingPost);
            paaPindenContextForInit.SaveChanges();

            testInitializeComment.CommentKnittingPostId = testInitializeKnittingPost.Id;
            testMethodComment.CommentKnittingPostId = testInitializeKnittingPost.Id;

            paaPindenContextForInit.Comments.Add(testInitializeComment);
            paaPindenContextForInit.SaveChanges();
        }

        [TestMethod]
        public async Task CreateCommentAsync_Test()
        {
            // Arrange
            var expectedResult = testMethodComment;

            // Act
            await testMethodCommentRepo.CreateCommentAsync(testMethodComment);

            // Assert
            var actualResult = paaPindenContextForRepo.Comments.Where(c => c.Id == testMethodComment.Id).FirstOrDefault();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public async Task UpdateCommentAsync_Test()
        {
            // Arrange
            testInitializeComment.Text = "ændret kommentar";
            var expectedResult = testInitializeComment;

            // Act
            await testMethodCommentRepo.UpdateCommentAsync(testInitializeComment);

            // Assert
            var actualResult = paaPindenContextForRepo.Comments.Where(c => c.Id == testInitializeComment.Id).FirstOrDefault();
            Assert.AreEqual(expectedResult.Text, actualResult.Text);
            Assert.AreEqual(expectedResult.TimeStamp, actualResult.TimeStamp);
            Assert.AreEqual(expectedResult.Writer, actualResult.Writer);
            Assert.AreEqual(expectedResult.CommentKnittingPostId, actualResult.CommentKnittingPostId);
        }

        [TestMethod]
        public async Task GetAllPostCommentsAsync_Test()
        {
            // Arrange
            List<Comment> expectedResults = new List<Comment>() { testInitializeComment };

            // Act
            List<Comment> actualResults = await testMethodCommentRepo.GetAllPostCommentsAsync(testInitializeComment.CommentKnittingPostId);

            // Assert
            Assert.IsTrue(actualResults.Select(c => c.Id).All(c => expectedResults.Select(c => c.Id).Contains(c))); //All actualResults members are contained within expectedResults. 
            Assert.HasCount(expectedResults.Count, actualResults); //expectedResults do not have more members than actualResults (rendering the prior assertion incomplete). 
        }

        [TestMethod]
        public async Task DeleteCommentAsync_Test()
        {
            // Arrange
            int testInitializeCommentId = testInitializeComment.Id;
            Comment? expectedResult = null;

            // Act
            await testMethodCommentRepo.DeleteCommentAsync(testInitializeComment.Id);

            // Assert
            var actualResult = paaPindenContextForRepo.Comments.Where(c => c.Id == testInitializeCommentId).FirstOrDefault(); //FirstOrDefault surely returns null if the list is empty
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
