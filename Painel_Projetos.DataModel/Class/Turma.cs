using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    [Table("Turmas")]
    public class Turma : EntityBase
    {
        [Required(ErrorMessage = "Descreva uma turma")]
        public string Descricao { get; set; }
    }
}
