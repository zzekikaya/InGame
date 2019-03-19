using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InGame.Core.Interfaces;

namespace InGame.Core.Entities
{
    public class Product:BaseEntity, IAggregateRoot
    {
        [MaxLength(250)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUri { get; set; }
        public bool IsActive { get; set; }
        public int? CategoryId { get; set; }
    }
}
