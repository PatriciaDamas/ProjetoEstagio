using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Data
{
    public class COLABORADOR
    {
        [Key]
        public int id_colaborador { get; set; }
        public string nome { get; set; }
        [ForeignKey("EQUIPA")]
        public string id_equipa { get; set; }
        public EQUIPA EQUIPA { get; set; }
        public DateTime data_admissao { get; set; }

    }
}
