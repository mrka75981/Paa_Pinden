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
        private User? _testInitializeUser;
        private KnittingPost? _testInitializeKnittingPost;
        private ICommentRepository? _testMethodCommentRepo;
        private Comment? _testMethodComment;
        private Comment? _testInitializeComment;

        [TestInitialize]
        public async Task Init()
        {
            _paaPindenContextForRepo = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
                .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
            _paaPindenContextForInit = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
                .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
            _testInitializeUser = new User()
            {
                Id = "84cd148c-e055-4d01-8a32-d06e7489e2a3",
                UserName = "test",
                NormalizedUserName = "TEST",
                Email = "test@test.test",
                NormalizedEmail = "TEST@TEST.TEST",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEDU5ss3/xPgY9SmYXgjbzqrnwgKE4cQpHkYP+zu0cfUKVes0+fsTo/Y1micgg+lFrQ==",
                SecurityStamp = "E6PKSVRQKLTX6JM23QOOG7NSEMZAT47O",
                ConcurrencyStamp = "451190ea-b605-4320-980a-6b863f169b73",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };
            _testInitializeKnittingPost = new KnittingPost
            {
                Title = "Første opslag",
                TimeStamp = DateTime.Today,
                UserId = "84cd148c-e055-4d01-8a32-d06e7489e2a3",
                KnittingPostContent = new Content
                {
                    Text = "Mit første opslag!",
                    Pictures = new List<ContentPicture>()
                    {
                        new ContentPicture
                        {
                            Path = "C:\\repositories\\Paa_Pinden\\PaaPinden\\PaaPindenBlazor\\wwwroot\\images\\test1.png"
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
            _testMethodCommentRepo = new CommentRepository(_paaPindenContextForRepo);
            _testMethodComment = new Comment()
            {
                CommentKnittingPostId = _testInitializeKnittingPost.Id,
                Text = "test kommentar",
                UserId = "84cd148c-e055-4d01-8a32-d06e7489e2a3",
                TimeStamp = DateTime.Today
            };
            _testInitializeComment = new Comment()
            {
                CommentKnittingPostId = _testInitializeKnittingPost.Id,
                Text = "første kommentar",
                UserId = "84cd148c-e055-4d01-8a32-d06e7489e2a3",
                TimeStamp = DateTime.Today
            };

            _paaPindenContextForInit.Database.EnsureDeleted();
            _paaPindenContextForInit.Database.EnsureCreated();
            _paaPindenContextForInit.Users.RemoveRange(_paaPindenContextForInit.Users.AsNoTracking());
            _paaPindenContextForInit.SaveChanges();
            _paaPindenContextForInit.KnittingPosts.RemoveRange(_paaPindenContextForInit.KnittingPosts.AsNoTracking());
            _paaPindenContextForInit.SaveChanges();
            _paaPindenContextForInit.Users.Add(_testInitializeUser);
            _paaPindenContextForInit.SaveChanges();
            _paaPindenContextForInit.KnittingPosts.Add(_testInitializeKnittingPost);
            _paaPindenContextForInit.SaveChanges();

    

            // Assert
            var actualResult = _paaPindenContextForRepo.Comments.Where(c => c.Id == _testInitializeComment.Id).FirstOrDefault();
            Assert.AreEqual(expectedResult.Text, actualResult.Text);
            Assert.AreEqual(expectedResult.TimeStamp, actualResult.TimeStamp);
            Assert.AreEqual(expectedResult.UserId, actualResult.UserId);
            Assert.AreEqual(expectedResult.CommentKnittingPostId, actualResult.CommentKnittingPostId);
        }

    }
}
