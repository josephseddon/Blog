using Blog.Data;
using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Models;
using Blog.Models.Comments;
using Blog.ViewModels;
using Ganss.XSS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Authorize]
    public class HomeController : Controller   
    {
        private IRepository _repo;
        private IFileManager _fileManager;
        private UserManager<ApplicationUser> _userManager;
        private IHttpContextAccessor _httpContextAccessor;

        public HomeController(
            IRepository repo,
            IFileManager fileManager,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _fileManager = fileManager;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        //returns a list of all blog posts
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        //returns contents of a post. Increments page view count. 
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Post(int id)
        {
            var post = _repo.GetPost(id);
            post.Views++;
            _repo.UpdatePost(post);
            await _repo.SaveChangesAsync();
            var sanitizer = new HtmlSanitizer();
            sanitizer.AllowedTags.Remove("form");
            sanitizer.AllowedTags.Remove("a");
            sanitizer.AllowedTags.Remove("input");
            var html = post.Body;
            var sanitised = sanitizer.Sanitize(html);
            post.Body = sanitised;
            return View(post);
        }

        //fetches image file for blog post
        [HttpGet("/Image/{image}")]
        [AllowAnonymous]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"Image/{mime}");
        }


        //create comment and subcomment
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment(CommentViewModel commentvm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Post", new { id = commentvm.PostId });

            var post = _repo.GetPost(commentvm.PostId);
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            if (commentvm.MainCommentID == 0)
            {
                post.MainComments = post.MainComments ?? new List<MainComment>();

                post.MainComments.Add(new MainComment
                {
                    Message = commentvm.Message,
                    Created = DateTime.Now,
                    UserName = user.UserName

                });

                _repo.UpdatePost(post);
            }
            else
            {
                var comment = new SubComment
                {
                    MainCommentId = commentvm.MainCommentID,
                    Message = commentvm.Message,
                    Created = DateTime.Now,
                    UserName = user.UserName
                };
                _repo.AddSubComment(comment);
            }

            await _repo.SaveChangesAsync();
            return RedirectToAction("Post", new { id = commentvm.PostId });
        }
    }
}
