using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
