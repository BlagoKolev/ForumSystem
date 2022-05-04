using Forum.Controllers;
using Forum.Data;
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
        public void IndexReturnsView()
        {
            //Arrange
            var homeService = HomeServiceMock.Instance;
            var homeController = new HomeController(homeService);

            //Act
            var result = homeController.Index();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
