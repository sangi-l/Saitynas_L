using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas_L1.Data.Entities
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string Description { get; set; }
    }
}
