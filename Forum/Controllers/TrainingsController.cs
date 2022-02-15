using Microsoft.AspNetCore.Mvc;


namespace Forum.Controllers
{
    public class TrainingsController : Controller
    {
        public IActionResult Exercises()
        {
            return View();
        }

        [ActionName("Street Fitness")]
        public IActionResult StreetFitness()
        {
            return View();
        }
    }
}
