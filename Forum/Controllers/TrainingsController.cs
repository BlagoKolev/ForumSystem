using Forum.Services;
using Microsoft.AspNetCore.Mvc;


namespace Forum.Controllers
{
    public class TrainingsController : Controller
    {
      
        private readonly IPostService postService;

        public TrainingsController(IPostService postService)
        {
            this.postService = postService;
        }
        public IActionResult Exercises()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            var posts = this.postService.GetAllPosts(actionName);
            return View(posts);
        }

        [ActionName("Street-fitness")]
        public IActionResult StreetFitness()
        {
            var actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            var categoryName = postService.GetCategoryName(actionName);
            var posts = postService.GetAllPosts(categoryName);
            return View("StreetFitness", posts);
        }

        [ActionName("Injuries-and-health-problems")]
        public IActionResult Injuries()
        {
            var actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            var categoryName = postService.GetCategoryName(actionName);
            var posts = postService.GetAllPosts(categoryName);
            return View("Injuries", posts);
        }

        [ActionName("Weight-loss-training-programs")]
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
        
        [ActionName("Мuscle-mass-increase-training-programs")]
        public IActionResult MuscleMass()
        {
            var actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            var categoryName = postService.GetCategoryName(actionName);
            var posts = postService.GetAllPosts(categoryName);
            return this.View("МuscleMass", posts);
        }

    }
}
