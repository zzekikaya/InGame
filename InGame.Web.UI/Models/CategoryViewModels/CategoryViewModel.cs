using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Web.UI.Models.CategoryViewModels
{
    public class CategoryViewModel
    {
      
        public int Id { get;  set; }
        [Required]
        public string CategoryName { get;  set; }
        public string Uri { get; private set; }
        public string PictureUri { get;  set; }
        public string Description { get; set; }
        public virtual ICollection<SubCategoryViewModel> SubCategories { get; set; }
    }
}
