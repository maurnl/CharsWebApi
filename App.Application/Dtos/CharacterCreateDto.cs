using System.ComponentModel.DataAnnotations;

namespace App.Application.Dtos
{
    public class CharacterCreateDto
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; } = "unnamed";
        [Required]
        public string Gender { get; set; } = "unknown";

    }
}
