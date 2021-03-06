﻿using System;
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
        public string id_equipa { get; set; }
        [ForeignKey("COLABORADOR")]
        public int id_lider { get; set; }
        public COLABORADOR COLABORADOR { get; set; }
        public string nome { get; set; }
    }
}
