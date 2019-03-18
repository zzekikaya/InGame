using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using InGame.Core.Interfaces;

namespace InGame.Core.Entities
{
    public class ParentCategory: BaseEntity, IAggregateRoot
    {
        public string ParentCategoryName { get; set; }
        public string Description { get; set; }
        public int? CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
