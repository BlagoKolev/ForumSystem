using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Forum.Models.Post;
using Forum.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Forum.Models.SubCategories;

namespace Forum.Controllers
{
    public class PostController : Controller
    {
        private readonly IHomeService homeService;
        private readonly IPostService postService;

        public PostController(IHomeService homeService, IPostService postService)
        {
            this.homeService = homeService;
            this.postService = postService;
        }

        public IActionResult Discussion(int postId)
        {
            var searchedPost = postService.GetPostById(postId);
            if (searchedPost == null)
            {
               // return View("Error"); TODO
               
            }
            return this.View(searchedPost);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.CategoryId = homeService
                .GetCategories()
                .Select(c => new SelectListItem
                { Value = c.CategoryId.ToString(), Text = c.CategoryName })
                .ToList();


            return View();
        }

        public IActionResult GetSubCategory(int mainCategoryId)
        {
            var SubCategoryList = homeService.GetCategories()
                .Where(x => x.CategoryId == mainCategoryId)
                .FirstOrDefault()
                .SubCategories
                .Select(c => new { Id = c.Id, Name = c.Name }).ToList();
            return Json(SubCategoryList);
        }

        [HttpPost]
        public IActionResult Create(CreatePostViewModel createPostModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction();
            }

            var userId = GetUserId();
            postService.CreatePost(createPostModel, userId);
            

            return this.Redirect("/Home/Index");
        }

        private string GetUserId()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}
