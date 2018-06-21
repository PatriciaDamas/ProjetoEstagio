using Samsys_Custos.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Models
{
    public class MEDIA_CUSTOS
    {
        [Key]
        public int id_media { get; set; }


        [ForeignKey("EQUIPA")]
        public string id_equipa { get; set; }
        public EQUIPA EQUIPA { get; set; }


        public string nome { get; set; }
        public int qtd_colaboradores { get; set; }
        public decimal Total { get; set; }
        public string mes { get; set; }
        public string ano { get; set; }
    }
}
