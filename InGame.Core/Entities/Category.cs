using System.ComponentModel.DataAnnotations;
using InGame.Core.Interfaces;

namespace InGame.Core.Entities
{
    public class Category : BaseEntity, IAggregateRoot
    {
        public int CategoryId { get; set; }
        //parent null olabilir tabloya eklen kayıt parent olmak zorunda değil.
        public int? ParentCategoryId { get; set; }

        [MaxLength(150)]
        public string CategoryName { get; set; }
        [MaxLength(250)]
        public string Uri { get; set; }
        [MaxLength(250)]
        public string PictureUri { get; set; }
        
        public string Description { get; set; }
    
    }
}
