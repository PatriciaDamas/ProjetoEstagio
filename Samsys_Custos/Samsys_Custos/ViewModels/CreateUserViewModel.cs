using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Samsys_Custos.ViewModels
{
    public class CreateUserViewModel
    {
        [Key]
        public string Id { get; set; }

       [Display(Name = "Nome")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Nº Colaborador")]
        [Required]
        public int? id_colaborador { get; set; }
        [Display(Name  = "Contribuinte")]
        public int contribuinte { get; set; }
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As palavras-passe introduzida não coincidem.")]
        [Required]
        [Display(Name = "Confirme Password")]
        public string ConfirmedPassword { get; set; }
        [Display(Name = "Tipo de Utilizador")]
      
        public string[] SelectedRoles { get; set; }
        public SelectList RolesList { get; set; }
    }
}
