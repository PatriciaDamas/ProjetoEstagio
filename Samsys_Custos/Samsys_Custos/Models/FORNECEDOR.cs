using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Data
{
    public class FORNECEDOR
    {
        [Key]
        public int id_fornecedor { get; set; }

        [ForeignKey("CATEGORIA")]
        public int? sugestao_categoria { get; set; }
        public  CATEGORIA CATEGORIA { get; set; }

        public Boolean? sugestao_custo { get; set; }
        public string nome { get; set; }

    }
}
