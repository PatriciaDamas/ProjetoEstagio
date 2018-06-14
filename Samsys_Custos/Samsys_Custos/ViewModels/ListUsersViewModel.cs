using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.ViewModels
{
    public class ListUsersViewModel
    {
        public string Id { get; set; }
        public int? id_colaborador { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Segsocial { get; set; }
        public string Roles { get; set; }

    }
}
