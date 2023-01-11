using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class Character
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
