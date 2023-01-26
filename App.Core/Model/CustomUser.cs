using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Model
{
    public class CustomUser : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
