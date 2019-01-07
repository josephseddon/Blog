using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    //This view model abstracts the view from the ApplicationRole model for editting roles in the ApplicationRole partial views as well as providing some interface messages
    public class ApplicationRoleViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string SubmitButtonText { get; set; } = "Submit";
        public string CancelButtonText { get; set; } = "Cancel";
        public string SubmitButtonID { get; set; } = "btn-submit";
        public string CancelButtonID { get; set; } = "btn-cancel";
    }
}