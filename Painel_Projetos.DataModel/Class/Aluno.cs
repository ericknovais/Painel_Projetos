using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    [Table("Alunos")]
    public class Aluno : EntityBase
    {
        public int RA { get; set; }
        public string Nome { get; set; }
        public Curso Curso { get; set; }
        public int CursoID { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }

        public override void Validar()
        {
            base.Validar();
        }
    }
}
