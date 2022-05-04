using Forum.Data.Models;
using Forum.Models.Home;
using Forum.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Test.Mocks
{
    public static class HomeServiceMock
    {
        public static IHomeService Instance
        {
            get
            {
                var homeService = new Mock<IHomeService>();

                homeService.Setup(x => x.GetCategories())
                     .Returns(new List<HomeCategoriesViewModel>
                     {
                        new HomeCategoriesViewModel
                        {
                            CategoryId = 1,
                            CategoryName = "FirstCategory",
                            SubCategories = new List<SubCategory>
                            {
                                new SubCategory
                                {
                                    Id =1,
                                    Name = "Subcategory1",
                                    ParentCategoryId = 1,
                                    IsDeleted = false,
                                },
                                new SubCategory
                                {
                                    Id =2,
                                    Name = "Subcategory2",
                                    ParentCategoryId = 1,
                                    IsDeleted = false
                                }
                            }
                        },
                         new HomeCategoriesViewModel
                        {
                            CategoryId = 2,
                            CategoryName = "SecondCategory",
                            SubCategories = new List<SubCategory>
                            {
                                new SubCategory
                                {
                                    Id =1,
                                    Name = "Subcategory3",
                                    ParentCategoryId = 2,
                                    IsDeleted = false,
                                },
                                new SubCategory
                                {
                                    Id =2,
                                    Name = "Subcategory4",
                                    ParentCategoryId = 2,
                                    IsDeleted = false
                                }
                            }
                        }
                     });
                return homeService.Object;
            }
        }
    }
}
