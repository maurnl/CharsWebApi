using App.Core.Model;
using System.Reflection;

namespace App.Core.Model.Relationships.Types
{
    public class ParentalRelationship : IRelationshipType
    {
        public void UpdateNames(Character character, Relationship rel)
        {
            if (rel.Character.ReferenceEquals(character))
            {
                rel.RelativeRelationshipName = SetPaternalNameByGender(character.Gender);

            }
            else
            {
                rel.OppositeRelativeRelationshipName = SetChildrenNameByGender(character.Gender);
            }
        }

        private string SetPaternalNameByGender(Gender gender)
        {
            return gender switch
            {
                Gender.Male => "Father",
                Gender.Female => "Mother",
                Gender.Unknown => "Parent",
                _ => ""
            };
        }

        private string SetChildrenNameByGender(Gender gender)
        {
            return gender switch
            {
                Gender.Male => "Son",
                Gender.Female => "Daugther",
                Gender.Unknown => "Children",
                _ => ""
            };
        }
    }
}
