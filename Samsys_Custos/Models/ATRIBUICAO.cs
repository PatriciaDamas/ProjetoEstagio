using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Data
{
    public class ATRIBUICAO
    {
        [Key]
        public int id_atribuicao { get; set; }
        [ForeignKey("VIATURA")]
        public int id_viatura { get; set; }
        [ForeignKey("GSM")]
        public int id_gsm { get; set; }
        [ForeignKey("UTILIZADOR")]
        public int id_colaborador { get; set; }
        public DateTime data_inicio { get; set; }
        public DateTime data_fim { get; set; }

    }
}
