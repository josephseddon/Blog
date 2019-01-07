using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
        [Authorize(Policy = "AdminInterface")]
    public class AdminController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public AdminController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        //checks if the user is authorised to access role and user management
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ModifyRolesViewModel check = new ModifyRolesViewModel();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User,"ModifyRoles");

            check.ModifyRoles = authorizationResult.Succeeded;

            return View(check);
        }
    }
}
