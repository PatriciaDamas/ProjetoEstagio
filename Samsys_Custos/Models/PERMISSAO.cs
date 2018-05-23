using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Models
{
    public class PERMISSAO
    {
        [Key]
        public int id_permissao { get; set; }
        public string nome { get; set; }
    }
}
