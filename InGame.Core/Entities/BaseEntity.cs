using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace InGame.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
