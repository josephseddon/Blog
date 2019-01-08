using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class AnalyticsViewModel
    {
        public List<CommentListViewModel> CommentList {get; set;}
        public int PageViews { get; set; }
    }
}
