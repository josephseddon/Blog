using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    //This view model abstracts the view from the application user model for editting existing users
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Required, EmailAddress, MaxLength(256), Display(Name = "Email Address")]
        public string UserName { get; set; }
        [Required, EmailAddress, MaxLength(256), Display(Name = "Email Address")]
        public string Email { get; set; }
        public List<SelectListItem> ApplicationRoles { get; set; }
        [Required, MaxLength(256), Display(Name = "Role")]
        public string ApplicationRoleId { get; set; }
    }
}
