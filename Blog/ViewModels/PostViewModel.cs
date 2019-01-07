using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

        //Google Search result max title length is 60-70 characters
        [Required, MaxLength(70)]
        public string Title { get; set; } = "";

        //To ensure cross compatibility with Ocarcle Server varchar max length
        [Required, MaxLength(4000)]
        public string Body { get; set; } = "";

        //To ensure cross compatibility with Ocarcle Server varchar max length
        [Required, MaxLength(4000)]
        public string SanitizedBody { get; set; } = null ;
        
        //MAX_PATH is 260 characters
        [Required, MaxLength(260)]
        public string CurrentImage { get; set; } = "";
        public IFormFile Image{ get; set; } = null;
    }
}
