using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Data
{
    public class EQUIPA
    {
        [Key]
        public int id_equipa { get; set; }
        [ForeignKey("UTILIZADOR")]
        public int id_lider { get; set; }
        public String nome { get; set; }
    }
}
