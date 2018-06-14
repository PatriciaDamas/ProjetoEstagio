using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Models
{
    public class CUSTOS_EQUIPA_DETALHE
    {
        public string Equipas { get; set; }
        public decimal Total { get; set; }
        public string mes { get; set; }
        public int ano { get; set; }
        public string designacao { get; set; }
        public string colaborador { get; set; }
        public string categoria { get; set; }
    }
}
