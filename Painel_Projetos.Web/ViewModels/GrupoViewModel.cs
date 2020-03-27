using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Painel_Projetos.Web.ViewModels
{
    public class GrupoViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = " Informe o nome do grupo")]
        [MinLength(3, ErrorMessage ="No mínimo de 3 caracteres")]
        [Display(Name = "Grupo")]
        public string NomeGrupo { get; set; }
        [Display(Name ="Administrador")]
        public string NomeAluno { get; set; }
    }
}