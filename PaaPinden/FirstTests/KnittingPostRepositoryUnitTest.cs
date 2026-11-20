using Microsoft.EntityFrameworkCore;
using PaaPindenAPI.Data;
using Shared.Models;
using PaaPindenAPI.Repositories;

namespace Tests
{
    [TestClass]
    public sealed class KnittingPostRepositoryUnitTest
    {
        private PaaPindenContext? _paaPindenContextForRepo;
        private PaaPindenContext? _paaPindenContextForInit;
        private IKnittingPostRepository? _testMethodRepo;
        private KnittingPost? _testMethodKnittingPost;
        private KnittingPost? _testInitializeKnittingPost;

        //[TestInitialize]
        //public async Task Init()
        //{
        //    _paaPindenContextForRepo = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
        //        .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
        //    _paaPindenContextForInit = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
        //        .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
        //    _testMethodRepo = new KnittingPostRepository(_paaPindenContextForRepo);
        //    _testMethodKnittingPost = new KnittingPost
        //    {
        //        Title = "test opslag",
        //        TimeStamp = DateTime.Today,
        //        Author = "Jens",
        //        KnittingPostContent = new Content
        //        {
        //            Text = "Mit første opslag!",
        //            Pictures = new List<ContentPicture>()
        //            {
        //                new ContentPicture
        //                {
        //                    Path = "~/path/to/testpicture"
        //                }
        //            },
        //            Videos = new List<ContentVideo>()
        //            {
        //                new ContentVideo
        //                {
        //                    Path = "~/path/to/testvideo"
        //                }
        //            }
        //        }
        //    };
        //    _testInitializeKnittingPost = new KnittingPost
        //    {
        //        Title = "Første opslag",
        //        TimeStamp = DateTime.Today,
        //        Author = "Jens",
        //        Comments = new List<Comment>()
        //        {
        //            new Comment
        //            {
        //                Text = "første kommentar",
        //                Writer = "jens fjende",
        //                TimeStamp = DateTime.Today
        //            }
        //        },
        //        KnittingPostContent = new Content
        //        {
        //            Text = "første opslag!",
        //            Pictures = new List<ContentPicture>()
        //            {
        //                new ContentPicture
        //                {
        //                    Path = "~/path/to/picture"
        //                }
        //            },
        //            Videos = new List<ContentVideo>()
        //            {
        //                new ContentVideo
        //                {
        //                    Path = "~/path/to/video"
        //                }
        //            }
        //        }
        //    };

        //    _paaPindenContextForInit.Database.EnsureDeleted();
        //    _paaPindenContextForInit.Database.EnsureCreated();
        //    _paaPindenContextForInit.KnittingPosts.RemoveRange(_paaPindenContextForInit.KnittingPosts.AsNoTracking());
        //    _paaPindenContextForInit.SaveChanges();
        //    _paaPindenContextForInit.KnittingPosts.Add(_testInitializeKnittingPost);
        //    _paaPindenContextForInit.SaveChanges();
        //}

        //[TestMethod]
        //public async Task CreateKnittingPostAsync_Test()
        //{
        //    // Arrange
        //    var expectedResult = _testMethodKnittingPost;

        //    // Act
        //    await _testMethodRepo.CreateKnittingPostAsync(_testMethodKnittingPost);

        //    // Assert
        //    var actualResult = _paaPindenContextForRepo.KnittingPosts.Where( kp => kp.Id == _testMethodKnittingPost.Id).FirstOrDefault();
        //    Assert.AreEqual(expectedResult.Id, actualResult.Id);
        //}

        //[TestMethod]
        //public async Task GetKnittingPostAsync_Test()
        //{
        //    // Arrange
        //    var expectedResult = _testInitializeKnittingPost;

        //    // Act
        //    var actualResult = await _testMethodRepo.GetKnittingPostAsync(_testInitializeKnittingPost.Id);

        //    // Assert
        //    Assert.AreEqual(expectedResult.Id, actualResult.Id);
        //    Assert.AreEqual(expectedResult.Title, actualResult.Title);
        //    Assert.AreEqual(expectedResult.TimeStamp, actualResult.TimeStamp);
        //    Assert.AreEqual(expectedResult.Author, actualResult.Author);

        //    Assert.IsTrue(actualResult.Comments.Select(c => c.Id).All(c => expectedResult.Comments.Select(c => c.Id).Contains(c))); //All actualResults members are contained within expectedResults. 
        //    Assert.HasCount(expectedResult.Comments.Count, actualResult.Comments); //expectedResults do not have more members than actualResults (rendering the prior assertion incomplete). 

        //    Assert.AreEqual(expectedResult.KnittingPostContent.Id, actualResult.KnittingPostContent.Id);
        //    Assert.AreEqual(expectedResult.KnittingPostContent.Text, actualResult.KnittingPostContent.Text);

        //    Assert.IsTrue(actualResult.KnittingPostContent.Pictures.Select(p => p.Path).All(p => expectedResult.KnittingPostContent.Pictures.Select(p => p.Path).Contains(p))); //All actualResults members are contained within expectedResults. 
        //    Assert.HasCount(expectedResult.KnittingPostContent.Pictures.Count, actualResult.KnittingPostContent.Pictures); //expectedResults do not have more members than actualResults (rendering the prior assertion incomplete). 

        //    Assert.IsTrue(actualResult.KnittingPostContent.Videos.Select(p => p.Path).All(p => expectedResult.KnittingPostContent.Videos.Select(p => p.Path).Contains(p))); //All actualResults members are contained within expectedResults. 
        //    Assert.HasCount(expectedResult.KnittingPostContent.Videos.Count, actualResult.KnittingPostContent.Videos); //expectedResults do not have more members than actualResults (rendering the prior assertion incomplete). 
        //}

        //[TestMethod]
        //public async Task GetAllKnittingPostsAsync_Test()
        //{
        //    // Arrange
        //    List<KnittingPost> expectedResults = new List<KnittingPost>() { _testInitializeKnittingPost };

        //    // Act
        //    var actualResults = await _testMethodRepo.GetAllKnittingPostAsync();

        //    // Assert
        //    Assert.IsTrue(actualResults.Select(kp => kp.Id).All(kp => expectedResults.Select(kp => kp.Id).Contains(kp))); //All actualResults members are contained within expectedResults. 
        //    Assert.HasCount(expectedResults.Count, actualResults); //expectedResults do not have more members than actualResults (rendering the prior assertion incomplete). 
        //}

        //[TestMethod]
        //public async Task DeleteKnittingPostAsync_Test()
        //{
        //    // Arrange
        //    int testInitializeKnittingPostId = _testInitializeKnittingPost.Id;
        //    KnittingPost? expectedResult = null;

        //    // Act
        //    await _testMethodRepo.DeleteKnittingPostAsync(_testInitializeKnittingPost.Id);

        //    // Assert
        //    var actualResult = _paaPindenContextForRepo.KnittingPosts.Where(kp => kp.Id == testInitializeKnittingPostId).FirstOrDefault(); //FirstOrDefault surely returns null if the list is empty
        //    Assert.AreEqual(expectedResult, actualResult);

        //}

        //public async Task UpdateKnittingPostAsync_Test()
        //{
        //    // Arrange
        //    _testInitializeKnittingPost.Title = "edited title";
        //    _testInitializeKnittingPost.KnittingPostContent.Pictures[0].Path = "~/path/to/editedpicture";
        //    _testInitializeKnittingPost.KnittingPostContent.Videos[0].Path = "~/path/to/editedvideo";
        //    var expectedResult = _testInitializeKnittingPost;

        //    // Act
        //    await _testMethodRepo.UpdateKnittingPostAsync(_testInitializeKnittingPost);

        //    // Assert
        //    var actualResult = _paaPindenContextForRepo.KnittingPosts.Where(kp => kp.Id == _testInitializeKnittingPost.Id).FirstOrDefault();
        //    Assert.AreEqual(expectedResult, actualResult);
        //}
    }
}
