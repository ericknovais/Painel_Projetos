using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    [Table("Projetos")]
    public class Projeto : EntityBase
    {
        [Required(ErrorMessage = "Informe um titulo ao projeto")]
        [Column(Order =1)]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Descreva objetivo do projeto")]
        [Column(Order = 2)]
        public string Descricao { get; set; }
    }
}
