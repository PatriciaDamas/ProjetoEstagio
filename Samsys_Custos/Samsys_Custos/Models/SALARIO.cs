using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Models
{
    public class SALARIO
    {
        [Key]
        public int id_salario { get; set; }
        public decimal liquido { get; set; }
        public decimal subsidio_alimentacao { get; set; }
        public decimal outros { get; set; }
        public decimal seguranca_social { get; set; }
        public decimal irs { get; set; }
        public decimal outras_despesas { get; set; }
        public decimal outras_regalias { get; set; }
    }
}
