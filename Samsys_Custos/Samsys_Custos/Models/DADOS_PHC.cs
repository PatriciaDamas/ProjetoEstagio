﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.Data
{
    public class DADOS_PHC
    {
        [Key]
        public string id_phc { get; set; }

        [ForeignKey("FORNECEDOR")]
        public string id_fornecedor { get; set; }
        public FORNECEDOR FORNECEDOR { get; set; }
        public Boolean custo_interno { get; set; }
        public Boolean validado { get; set; }
    }
}
