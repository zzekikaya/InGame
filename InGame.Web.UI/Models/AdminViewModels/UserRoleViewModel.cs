using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace InGame.Web.UI.Models.AdminViewModels
{
    public class UserRoleViewModel
    {
        public UserRoleViewModel()
        {
            Users = new List<IdentityUser>();
        }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public List<IdentityUser> Users { get; set; }

    }
}