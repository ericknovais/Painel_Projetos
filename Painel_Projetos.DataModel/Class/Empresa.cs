using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    [Table("Empresas")]
    public class Empresa : EntityBase
    {
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public Representante Representante { get; set; }
        public int RepresentanteId { get; set; }
    }
}
