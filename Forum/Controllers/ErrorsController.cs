using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("Error/404-Not-Found")]
        public IActionResult ResourceNotFound()
        {
            return this.View();
        }
    }
}
