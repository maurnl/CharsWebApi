using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Model
{
    public class Battle : EntityBase
    {
        [Required]
        public AppUser UserOne { get; set; }
        [Required]
        public Character UserOneCharacter { get; set; }
        [Required]
        public AppUser UserTwo { get; set; }
        [Required]
        public Character UserTwoCharacter { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public int CharacterOneScore { get; set; }
        public int CharacterTwoScore { get; set; }

        [NotMapped]
        public Character Winner
        {
            get
            {
                return CharacterOneScore > CharacterTwoScore ? UserOneCharacter : UserTwoCharacter;
            }
        }
    }
}
