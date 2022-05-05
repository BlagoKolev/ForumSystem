using Forum.Controllers;
using Forum.Data;
using Forum.Models.Home;
using Forum.Services;
using Forum.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Forum.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexReturnsNotNull()
        {
            //Arrange
            var homeService = HomeServiceMock.Instance;
            var homeController = new HomeController(homeService);

            //Act
            var result = homeController.Index();

            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public void IndexReturnsViewAndResult()
        {
            //Arrange
            var homeService = HomeServiceMock.Instance;
            var homeController = new HomeController(homeService);

            //Act
            var result = homeController.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }
        private static HomeController CallHomeController(IHomeService homeServiceMock)
        {
            var homeController = new HomeController(homeServiceMock);
            return homeController;
        }

    }
}
