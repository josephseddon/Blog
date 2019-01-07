using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    //This view model abstracts the view from the comment models for displaying on pages
    public class CommentViewModel
    {
        public int PostId { get; set; }
        public int MainCommentID { get; set; }
        [Required, MaxLength(240)]
        public string Message { get; set; }
        public string UserName { get; set; }
    }
}
