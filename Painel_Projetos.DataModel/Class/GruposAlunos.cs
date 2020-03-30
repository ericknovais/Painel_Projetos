using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    [Table("GruposAlunos")]
    public class GruposAlunos : EntityBase
    {
        public Aluno Aluno { get; set; }
        public int AlunoID { get; set; }
        public Grupo Grupo { get; set; }
        public int GrupoID { get; set; }
        public bool Administrador { get; set; }
    }
}
