using System.ComponentModel.DataAnnotations;

namespace InGame.Web.UI.Models.AdminViewModels
{
    public class AddRoleViewModel
    {
        [Required]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }
    }
}