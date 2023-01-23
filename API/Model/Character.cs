using API.Model.Relationships;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    public class Character : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Relationship> Relationships { get; set; } = new List<Relationship>();
        public virtual ICollection<Relationship> RelatedTo { get; set; } = new List<Relationship>();

        public void UpdateRelationshipNames()
        {
            foreach (var rel in Relationships)
            {
                rel.RelationshipType.UpdateNames(this, rel);
            }
        }
    }
}
