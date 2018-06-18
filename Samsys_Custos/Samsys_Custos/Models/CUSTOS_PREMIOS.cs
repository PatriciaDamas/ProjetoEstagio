using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Models
{
    public class CUSTOS_PREMIOS
    {
        public int ano { get; set; }
        public string mes { get; set; }
        public string designacao { get; set; }
        public string Colaborador { get; set; }
        public decimal valor { get; set; }
        public int id_custo { get; set; }
        public int id_categoria { get; set; }


    }
}
