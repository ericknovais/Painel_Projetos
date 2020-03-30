using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    [Table("Cordenadores")]
    public class Cordenador : EntityBase
    {
        public string Nome{ get; set; }
        public string Email { get; set; }
    }
}
