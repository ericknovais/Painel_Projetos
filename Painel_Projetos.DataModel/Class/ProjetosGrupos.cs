using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    [Table("ProjetosGrupos")]
    public class ProjetosGrupos : EntityBase
    {
        public Representante Representante { get; set; }
        public int RepresentanteId{ get; set; }
        public Projeto Projeto{ get; set; }
        
        public int ProjetoID { get; set; }
        public Grupo Grupo { get; set; }
        
        public int? GrupoID { get; set; }
    }
}
