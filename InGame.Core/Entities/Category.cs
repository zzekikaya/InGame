using InGame.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InGame.Core.Entities
{
    public class Category :BaseEntity, IAggregateRoot
    {
        public string CategoryName { get; private set; }
        public string Uri { get; private set; }
        public string PictureUri { get; private set; }
        public string Description { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
