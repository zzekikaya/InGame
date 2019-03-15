using InGame.Core.Interfaces;

namespace InGame.Core.Entities
{
    public class Product:BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUri { get; set; }
        public bool IsActive { get; set; }
        public int? SubCategoryID { get; set; }
        public virtual SubCategory Subcategory { get; set; }
    }
}
