using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Data
{
    public class CATEGORIA
    {
        [Key]
        public int id_categoria { get; set; }
        [ForeignKey("CATEGORIAPAI")]
        public int? id_pai { get; set; }
        public CATEGORIA CATEGORIAPAI { get; set; }
        public string nome { get; set; }

    }
}
