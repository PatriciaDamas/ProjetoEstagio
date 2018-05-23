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
        public Decimal liquido { get; set; }
        public Decimal subsidio_alimentacao { get; set; }
        public Decimal outros { get; set; }
        public Decimal seguranca_social { get; set; }
        public Decimal irs { get; set; }
        public Decimal outras_despesas { get; set; }
        public Decimal outras_regalias { get; set; }
    }
}
