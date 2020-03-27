using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    [Table("Grupos")]
    public class Grupo : EntityBase
    {
        public Grupo()
        {
            GrupoAlunos = new HashSet<GruposAlunos>();
        }
        public string Nome { get; set; }
        public ICollection<GruposAlunos> GrupoAlunos { get; set; }
    }
}
