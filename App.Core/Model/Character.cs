using App.Core.Model.Relationships;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Core.Model
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
                if (rel.RelativeRelationshipName == null
                    || rel.OppositeRelativeRelationshipName == null)
                {
                    rel.RelationshipType.UpdateNames(this, rel);
                }
            }
        }
    }
}
