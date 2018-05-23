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
        [ForeignKey("UTILIZADOR")]
        public int id_colaborador { get; set; }
        [ForeignKey("CATEGORIA")]
        public int id_categoria { get; set; }
        [ForeignKey("GSM")]
        public int id_gsm { get; set; }
        [ForeignKey("DADOS_PHC")]
        public int id_phc { get; set; }
        [ForeignKey("VIATURA")]
        public int id_viatura { get; set; }
        [ForeignKey("SALARIO")]
        public int id_salario { get; set; }
        public DateTime data { get; set; }
        public int ano { get; set; }
        public String mes { get; set; }
        public String designacao { get; set; }
        public Decimal valor { get; set; }
    }
}
