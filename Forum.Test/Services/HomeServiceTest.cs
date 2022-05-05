using Forum.Models.Home;
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
    public class HomeServiceTest
    {
     
        [Fact]
        public void GetCatagoriesReturnsListHomeCategoriesViewModel()
        {
            //Arrange
            var homeService = new HomeService(DatabaseMock.Instance());

            //Act
            var result = homeService.GetCategories();
            //Assert
            Assert.IsType<List<HomeCategoriesViewModel>>(result);
        }
    }
}
