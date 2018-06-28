using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public int contribuinte { get; set; }
        public int? id_colaborador { get; set; }
    }
}
