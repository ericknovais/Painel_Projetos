using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Painel_Projetos.Web.ViewModels
{
    public class EmpresaViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Informe o CNPJ")]
        [MinLength(14, ErrorMessage = "O CNPJ deve conter 14 caracteres")]
        public string CNPJ { get; set; }
        [Required(ErrorMessage = "Informe a razão social")]
        public string RazaoSocial { get; set; }
        [Required(ErrorMessage = "Informe o nome do representante")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o e-mai do representante")]
        public string Email { get; set; }
    }
}