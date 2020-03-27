using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Painel_Projetos.Web.ViewModels
{
    public class GrupoViewModel
    {
        [Required]
        public string Nome { get; set; }
        public int AlunoID { get; set; }
    }
}