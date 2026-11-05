using PaaPindenAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass]
    public sealed class CommentRepositoryUnitTest
    {
        [TestMethod]
        public async Task CreateKnittingPostAsync_Test()
        {
            // Arange 
            var testPost = expectedResult;

            // Act 
            await knittingPostRepository.CreateKnittingPostAsync(testPost);

            // Assert
            var actualResult = await knittingPostRepository.GetKnittingPostAsync(testPost.Id);
            Assert.AreEqual(expectedResult, actualResult);

        }
    }
}
