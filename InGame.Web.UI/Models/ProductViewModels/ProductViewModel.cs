using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InGame.Web.UI.Models.CategoryViewModels;

namespace InGame.Web.UI.Models.ProductViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUri { get; set; }
        public bool IsActive { get; set; }
        public int? SubCategoryID { get; set; }
        public virtual SubCategoryViewModel Subcategory { get; set; }
    }
}
