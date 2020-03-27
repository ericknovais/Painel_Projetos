using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    public class Representante : EntityBase
    {
        public string Nome { get; set; }
        [Required(ErrorMessage ="Informe um o e-mail")]
        public string Email { get; set; }
    }
}
