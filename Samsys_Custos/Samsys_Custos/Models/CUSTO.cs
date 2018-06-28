using Samsys_Custos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Data
{
    public class CUSTO
    {
        [Key]
        public int id_custo { get; set; }

        [ForeignKey("COLABORADOR")]
        public int id_colaborador { get; set; }
        public COLABORADOR COLABORADOR { get; set; }

        [ForeignKey("CATEGORIA")]
        public int id_categoria { get; set; }
        public CATEGORIA CATEGORIA { get; set; }

        [ForeignKey("GSM")]
        public int? id_gsm { get; set; }
        public GSM GSM { get; set; }

       [ForeignKey("DADOS_PHC")]
        public string id_phc { get; set; }
        public DADOS_PHC DADOS_PHC { get; set; }

        [ForeignKey("VIATURA")]
        public int? id_viatura { get; set; }
        public VIATURA VIATURA { get; set; } 

        public DateTime data { get; set; }
        public int ano { get; set; }
        public string mes { get; set; }
        public string designacao { get; set; }
        public decimal valor { get; set; }
    }
}
