using Forum.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    public class NutritionController : Controller
    {
        private readonly IPostService postService;

        public NutritionController(IPostService postService)
        {
            this.postService = postService;
        }

        [ActionName("Increase-muscle-mass")]
        public IActionResult MuscleMass()
        {
            var actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            var categoryName = postService.GetCategoryName(actionName);
            var posts = postService.GetAllPosts(categoryName);
            return View("MuscleMass", posts);
        }

        [ActionName("Weight-loss")]
        public IActionResult WeightLoss()
        {
            var actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            var categoryName = postService.GetCategoryName(actionName);
            var posts = postService.GetAllPosts(categoryName);
            return View("WeightLoss", posts);
        }

        [ActionName("Common-discussions")]
        public IActionResult CommonDiscussions()
        {
            var actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            var categoryName = postService.GetCategoryName(actionName);
            var posts = postService.GetAllPosts(categoryName);
            return View("CommonDiscussions", posts);
        }

    }
}
