using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Data
{
    public class PERFIL
    {
        [Key]
        public int id_perfil { get; set; }
        public string nome { get; set; }
        public Boolean salario { get; set; }
        public Boolean premio { get; set; }

    }
}
