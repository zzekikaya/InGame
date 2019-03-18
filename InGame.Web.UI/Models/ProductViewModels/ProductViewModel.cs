using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using InGame.Web.UI.Models.CategoryViewModels;

namespace InGame.Web.UI.Models.ProductViewModels
{
    public class ProductViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string PictureUri { get; set; }
        [DisplayName("Status")]
        [Required]
        public bool IsActive { get; set; }
        [DisplayName("Category")]
        public int? SubCategoryID { get; set; }
        public virtual SubCategoryViewModel Subcategory { get; set; }
    }
}
