using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Models;
using Blog.ViewModels;
using Ganss.XSS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private IRepository _repo;
        private IFileManager _fileManager;

        public PanelController(
            IRepository repo,
            IFileManager fileManager
            )
        {
            _repo = repo;
            _fileManager = fileManager;
        }

        //returns list of all posts to admin post panel
        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        //fetches post data and returns PostView model to the post editting screen
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new PostViewModel());
            }
            else
            {
                var post = _repo.GetPost((int)id);
                var sanitizer = new HtmlSanitizer();
                sanitizer.AllowedTags.Remove("form");
                sanitizer.AllowedTags.Remove("a");
                sanitizer.AllowedTags.Remove("input");
                var html = post.Body;
                var sanitised = sanitizer.Sanitize(html);
                return View(new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    CurrentImage = post.Image,
                    Body = sanitised,
                    Views = post.Views
                });
            }
        }

        //recieves PostViewModel from post editting screena and updates database entry
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostViewModel postvm)
        {
            var sanitizer = new HtmlSanitizer();
            sanitizer.AllowedTags.Remove("form");
            sanitizer.AllowedTags.Remove("a");
            sanitizer.AllowedTags.Remove("input");
            var html = postvm.Body;
            var sanitised = sanitizer.Sanitize(html);
            var post = new Post
            {   
                Id = postvm.Id,
                Title = postvm.Title,
                Body = sanitised,
                Image = await _fileManager.SaveImage(postvm.Image),
                Views = postvm.Views
            };

            if (postvm.Image == null)
                post.Image = postvm.CurrentImage;
            else
                post.Image = await _fileManager.SaveImage(postvm.Image);

            if (post.Id > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(post);
        }

        //deletes posts
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
