using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Models
{
    public class UTILIZADOR
    {
        [Key]
        public int id_colaborador { get; set; }
        public string nome { get; set; }
        public int id_perfil { get; set; }
        [ForeignKey("PERFIL")]  
        public int id_equipa { get; set; }
        [ForeignKey("EQUIPA")]
        public DateTime data_admissao { get; set; }
    }
}
