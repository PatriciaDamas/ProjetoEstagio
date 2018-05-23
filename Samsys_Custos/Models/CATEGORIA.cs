using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Data
{
    public class CATEGORIA
    {
        [Key]
        public int id_categoria { get; set; }
        public int? id_pai { get; set; }
        public String nome { get; set; }

    }
}
