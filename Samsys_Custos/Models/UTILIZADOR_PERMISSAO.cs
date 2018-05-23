using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Data
{
    public class UTILIZADOR_PERMISSAO
    {
        [Key]
        public int id_utilizador_permissao { get; set; }
        [ForeignKey("UTILIZADOR")]
        public int id_colaborador { get; set; }
        [ForeignKey("PERMISSAO")]
        public int id_permissao { get; set; }
    }
}
