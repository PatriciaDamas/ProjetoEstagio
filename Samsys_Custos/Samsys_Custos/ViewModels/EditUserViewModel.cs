using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.ViewModels
{
    public class EditUserViewModel
    {
        [Key]
        public string ID { get; set; }
        [Display(Name = "Nome")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Nº Colaborador")]
        [Required]
        public int? id_colaborador { get; set; }
        [Display(Name = "Contribuinte")]
        public int Segsocial { get; set; }
        [Required]
        [ReadOnly(true)]
        public string Email { get; set; }
        [Display(Name = "Tipo de Utilizador")]
       
        public string[] SelectedRoles { get; set; }
        public SelectList RolesList { get; set; }
    }
}
