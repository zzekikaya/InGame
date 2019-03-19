using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using InGame.Core.Entities;

namespace InGame.Web.UI.Models.ParentCategoryViewModels
{
    public class CategoryViewModels
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ParentCategoryId { get; set; } 
        public string CategoryName { get; set; }
        public string Uri { get; set; }
        public string PictureUri { get; set; }
        public string Description { get; set; }
    }
}
