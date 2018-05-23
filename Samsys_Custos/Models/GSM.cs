using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Data
{
    public class GSM
    {
        [Key]
        public int id_gsm { get; set; }
        public int numero { get; set; }
        public String equipamento { get; set; }
    }
}
