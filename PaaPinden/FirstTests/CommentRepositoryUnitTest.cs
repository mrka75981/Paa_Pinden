using Microsoft.EntityFrameworkCore;
using PaaPindenAPI.Data;
using Shared.Models;
using PaaPindenAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass]
    public sealed class CommentRepositoryUnitTest
    {
        private PaaPindenContext? _paaPindenContextForRepo;
        private PaaPindenContext? _paaPindenContextForInit;
        private KnittingPost? _testInitializeKnittingPost;
        private ICommentRepository? _testMethodCommentRepo;
        private Comment? _testMethodComment;
        private Comment? _testInitializeComment;

    //    [TestInitialize]
    //    public async Task Init()
    //    {
    //        _paaPindenContextForRepo = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
    //            .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
    //        _paaPindenContextForInit = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
    //            .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
    //        _testInitializeKnittingPost = new KnittingPost
    //        {
    //            Title = "Første opslag",
    //            TimeStamp = DateTime.Today,
    //            Author = "Jens",
    //            KnittingPostContent = new Content
    //            {
    //                Text = "Mit første opslag!",
    //                Pictures = new List<ContentPicture>()
    //                {
    //                    new ContentPicture
    //                    {
    //                        Path = "~/path/to/picture"
    //                    }
    //                },
    //                Videos = new List<ContentVideo>()
    //                {
    //                    new ContentVideo
    //                    {
    //                        Path = "~/path/to/video"
    //                    }
    //                }
    //            }
    //        };
    //        _testMethodCommentRepo = new CommentRepository(_paaPindenContextForRepo);
    //        _testMethodComment = new Comment()
    //        {
    //            CommentKnittingPostId = _testInitializeKnittingPost.Id,
    //            Text = "test kommentar",
    //            Writer = "jens fjende",
    //            TimeStamp = DateTime.Today
    //        };
    //        _testInitializeComment = new Comment()
    //        {
    //            CommentKnittingPostId = _testInitializeKnittingPost.Id,
    //            Text = "første kommentar",
    //            Writer = "jens fjende",
    //            TimeStamp = DateTime.Today
    //        };

    //        _paaPindenContextForInit.Database.EnsureDeleted();
    //        _paaPindenContextForInit.Database.EnsureCreated();
    //        _paaPindenContextForInit.KnittingPosts.RemoveRange(_paaPindenContextForInit.KnittingPosts.AsNoTracking());
    //        _paaPindenContextForInit.SaveChanges();
    //        _paaPindenContextForInit.KnittingPosts.Add(_testInitializeKnittingPost);
    //        _paaPindenContextForInit.SaveChanges();

    //        _testInitializeComment.CommentKnittingPostId = _testInitializeKnittingPost.Id;
    //        _testMethodComment.CommentKnittingPostId = _testInitializeKnittingPost.Id;

    //        _paaPindenContextForInit.Comments.Add(_testInitializeComment);
    //        _paaPindenContextForInit.SaveChanges();
    //    }

    //    [TestMethod]
    //    public async Task CreateCommentAsync_Test()
    //    {
    //        // Arrange
    //        var expectedResult = _testMethodComment;

    //        // Act
    //        await _testMethodCommentRepo.CreateCommentAsync(_testMethodComment);

    //        // Assert
    //        var actualResult = _paaPindenContextForRepo.Comments.Where(c => c.Id == _testMethodComment.Id).FirstOrDefault();
    //        Assert.AreEqual(expectedResult, actualResult);
    //    }

    //    [TestMethod]
    //    public async Task UpdateCommentAsync_Test()
    //    {
    //        // Arrange
    //        _testInitializeComment.Text = "ændret kommentar";
    //        var expectedResult = _testInitializeComment;

    //        // Act
    //        await _testMethodCommentRepo.UpdateCommentAsync(_testInitializeComment);

    //        // Assert
    //        var actualResult = _paaPindenContextForRepo.Comments.Where(c => c.Id == _testInitializeComment.Id).FirstOrDefault();
    //        Assert.AreEqual(expectedResult.Text, actualResult.Text);
    //        Assert.AreEqual(expectedResult.TimeStamp, actualResult.TimeStamp);
    //        Assert.AreEqual(expectedResult.Writer, actualResult.Writer);
    //        Assert.AreEqual(expectedResult.CommentKnittingPostId, actualResult.CommentKnittingPostId);
    //    }

    //    [TestMethod]
    //    public async Task GetAllPostCommentsAsync_Test()
    //    {
    //        // Arrange
    //        List<Comment> expectedResults = new List<Comment>() { _testInitializeComment };

    //        // Act
    //        List<Comment> actualResults = await _testMethodCommentRepo.GetAllPostCommentsAsync(_testInitializeComment.CommentKnittingPostId);

    //        // Assert
    //        Assert.IsTrue(actualResults.Select(c => c.Id).All(c => expectedResults.Select(c => c.Id).Contains(c))); //All actualResults members are contained within expectedResults. 
    //        Assert.HasCount(expectedResults.Count, actualResults); //expectedResults do not have more members than actualResults (rendering the prior assertion incomplete). 
    //    }

    //    [TestMethod]
    //    public async Task DeleteCommentAsync_Test()
    //    {
    //        // Arrange
    //        int testInitializeCommentId = _testInitializeComment.Id;
    //        Comment? expectedResult = null;

    //        // Act
    //        await _testMethodCommentRepo.DeleteCommentAsync(_testInitializeComment.Id);

    //        // Assert
    //        var actualResult = _paaPindenContextForRepo.Comments.Where(c => c.Id == testInitializeCommentId).FirstOrDefault(); //FirstOrDefault surely returns null if the list is empty
    //        Assert.AreEqual(expectedResult, actualResult);
    //    }
    }
}
