using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InGame.Web.UI.Models.ProductViewModels;

namespace InGame.Web.UI.Models.CategoryViewModels
{
    public class SubCategoryViewModel
    {
      
            public string SubCategoryName { get; set; }
            public string Description { get; set; }
            public int? CategoryID { get; set; }
            public virtual CagetoryViewModel Category { get; set; }
            public virtual ICollection<ProductViewModel> Products { get; set; }
        
    }
}
