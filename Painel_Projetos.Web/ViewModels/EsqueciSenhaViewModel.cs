using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Painel_Projetos.Web.ViewModels
{
    public class EsqueciSenhaViewModel
    {
        [Required(ErrorMessage = "Informe sua senha atual")]
        public string Email { get; set; }
    }
}