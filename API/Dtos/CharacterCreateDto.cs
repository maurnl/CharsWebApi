using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CharacterCreateDto
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; } = "unnamed";

    }
}
