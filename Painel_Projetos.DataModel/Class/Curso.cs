using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Painel_Projetos.DomainModel.Class
{
    [Table("Cursos")]
    public class Curso : EntityBase
    {
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        public override void Validar()
        {
            base.Validar();
        }
    }
}
