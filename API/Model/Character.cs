using API.Model.Relationships;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    public class Character : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Character> RelatedChars { get; set; }
        public ICollection<Character> RelatedToChars { get; set; }
        public virtual ICollection<Relationship> Relationships { get; set; } = new List<Relationship>();
        public virtual ICollection<Relationship> RelatedTo { get; set; } = new List<Relationship>();
    }
}
