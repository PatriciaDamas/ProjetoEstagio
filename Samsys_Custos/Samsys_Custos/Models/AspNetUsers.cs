using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Data
{
    public class AspNetUsers : IdentityUser
    {
        public string Name { get; set; }
        public int Segsocial { get; set; }
        public int id_colaborador {get;set;}
    }
}
