using Forum.Models.SubCategories;
using Forum.Services;
using Forum.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Forum.Test.Services
{
    public class PostServiceTest
    {
        [Fact]
        public void GetAllPostsReturnsListSubCategoryAllPostsViewModel()
        {
            //Arrange
            var mockDb = DatabaseMock.Instance();
            var postService = new PostService(mockDb);
            var actionName = "Common-discussions";

            //Act 
            var result = postService.GetAllPosts(actionName);

            //Assert
            Assert.IsType<List<SubCategoryAllPostsViewModel>>(result);
        }
    }
}
