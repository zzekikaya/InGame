using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Web.UI.Models.CategoryViewModels
{
    public class CagetoryViewModel
    {
        public int CagetoryId { get; private set; }
        public string CagetoryName { get; private set; }
        public string Uri { get; private set; }
        public string PictureUri { get; private set; }
        public string Description { get; set; }
        public virtual ICollection<SubCategoryViewModel> SubCategories { get; set; }
    }
}
