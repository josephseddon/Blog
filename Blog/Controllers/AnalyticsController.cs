using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.ViewModels;
using Blog.Models;
using Blog.Data;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Controllers
{
    [Authorize(Policy = "ViewAnalytics")]
    public class AnalyticsController : Controller
    {
        private readonly BlogContext _db;

        public AnalyticsController(BlogContext db)
        {
            _db = db;
        }

        //counts all comments and subcomment from a given user and returns information to analytics view index
        [HttpGet]
        public IActionResult Index()
        {
            var allUserComments = _db.MainComments.ToList();
            var allUserSubComments = _db.SubComments.ToList();
            List<CommentListViewModel> model = new List<CommentListViewModel>();

            model = _db.MainComments.Select(r => new CommentListViewModel
            {
                UserName = r.UserName,
                Id = r.Id,
                NumberOfComments = allUserComments.Count(ur => ur.Id == r.Id) + allUserSubComments.Count(ur => ur.Id == r.Id)
            }).ToList();

            
            return View(model);
        }
    }
}