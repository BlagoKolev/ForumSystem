using Forum.Services;
using Microsoft.AspNetCore.Mvc;


namespace Forum.Controllers
{
    public class TrainingsController : Controller
    {
        private readonly ITrainingService trainingService;

        public TrainingsController(ITrainingService trainingService)
        {
            this.trainingService = trainingService;
        }
        public IActionResult Exercises()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            var posts = this.trainingService.GetAllPosts(actionName);
            return View(posts);
        }

        [ActionName("Street Fitness")]
        public IActionResult StreetFitness()
        {
            var actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            var posts = trainingService.GetAllPosts(actionName);
            return View(posts);
        }

        [ActionName("Injuries and Health problems")]
        public IActionResult Injuries()
        {
            var actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            var posts = trainingService.GetAllPosts(actionName);
            return View(posts);
        }

        [ActionName("Weight loss training programs")]
        public IActionResult WeightLoss()
        {
            var actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            var posts = trainingService.GetAllPosts(actionName);
            return View(posts);
        }

        [ActionName("Trainings - Common discussions")]
        public IActionResult CommonDiscussions()
        {
            var actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            var posts = trainingService.GetAllPosts(actionName);
            return View(posts);
        }

        [ActionName("Мuscle mass increase training programs")]
        public IActionResult MuscleMass()
        {
            var actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            var posts = trainingService.GetAllPosts(actionName);
            return this.View(posts);
        }
    }
}
