using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    //This ViewModel is used to store number of comments per user and abstract the analytics view from the comments models for presentation in a list
    public class CommentListViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int NumberOfComments { get; set; }

    }
}
