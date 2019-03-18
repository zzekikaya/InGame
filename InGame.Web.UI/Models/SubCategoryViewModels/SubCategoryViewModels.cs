using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using InGame.Core.Entities;

namespace InGame.Web.UI.Models.SubCategoryViewModels
{
    public class SubCategoryViewModels
    {
      
        [Required]
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        [Required]
        public int? CategoryID { get; set; } 
        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
