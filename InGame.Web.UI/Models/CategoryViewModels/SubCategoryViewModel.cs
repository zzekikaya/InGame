using InGame.Web.UI.Models.ProductViewModels;
using System.Collections.Generic;

namespace InGame.Web.UI.Models.CategoryViewModels
{
    public class SubCategoryViewModel
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public int? CategoryID { get; set; }
        public virtual CategoryViewModel Category { get; set; }
        public virtual ICollection<ProductViewModel> Products { get; set; }

    }
}
