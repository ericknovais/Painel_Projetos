using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    [Table("Projetos")]
    public class Projeto : EntityBase
    {
        public string Descricao { get; set; }
    }
}
