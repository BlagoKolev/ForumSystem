using static Forum.Data.GlobalConstants;
using Forum.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data;

namespace Forum.Controllers
{
    public class AnswersController : Controller
    {
        private readonly IAnswerService answerService;

        public AnswersController(IAnswerService answerService)
        {
            this.answerService = answerService;
        }

        [Authorize]
        public async Task<IActionResult> Delete(int answerId)
        {
            var postId = await answerService.DeleteAnswer(answerId);

            if (postId == 0)
            {
                TempData[AlertMessageFailKey] = AlertMessageFail.CannotDeleteAnswer;
                return Redirect("/");
            }

            TempData[AlertMessageKey] = AlertMessage.AnswerDeleted;
            return Redirect($"/Post/Discussion/?postId={postId}");
        }
    }
}
