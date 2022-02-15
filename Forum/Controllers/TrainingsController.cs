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
            return View();
        }
    }
}
