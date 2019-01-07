using Blog.Models.Comments;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Post
    {
        // Blog post model. 
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; } = "";
        [Required, MaxLength(4000)]
        public string Body { get; set; } = "";
        [Required, MaxLength(4000)]
        public string Image { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;

        public List<MainComment> MainComments { get; set; }

        public int Views { get; set; }
    }
}
