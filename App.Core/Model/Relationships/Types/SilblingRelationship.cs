using App.Core.Model;
using App.Core.Model.Relationships;

namespace App.Core.Model.Relationships.Types
{
    public class SilblingRelationship : IRelationshipType
    {
        public void UpdateNames(Character character, Relationship rel)
        {
            if (rel.Character == character)
            {
                rel.RelativeRelationshipName = SetSilbingsNameByGender(character.Gender);

            }
            else
            {
                rel.OppositeRelativeRelationshipName = SetSilbingsNameByGender(character.Gender);
            }
        }

        private string SetSilbingsNameByGender(Gender gender)
        {
            return gender switch
            {
                Gender.Male => "Brother",
                Gender.Female => "Sister",
                Gender.Unknown => "Silbling",
                _ => ""
            };
        }
    }
}
