using InGame.Core.Interfaces;

namespace InGame.Core.Entities
{
    public class Category : BaseEntity, IAggregateRoot
    {
        public int CategoryId { get; set; }
        //parent null olabilir tabloya eklen kayıt parent olmak zorunda değil.
        public int? ParentCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Uri { get; set; }
        public string PictureUri { get; set; }
        public string Description { get; set; }
        //public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
