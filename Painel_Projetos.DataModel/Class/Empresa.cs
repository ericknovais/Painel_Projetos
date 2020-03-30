using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    [Table("Empresas")]
    public class Empresa : EntityBase
    {
        public Empresa()
        {

        }

        public Empresa(Representante representante)
        {
            representante = new Representante();
        }
        [Required(ErrorMessage ="Informe o CNPJ")]
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public Representante Representante { get; set; }
        public int RepresentanteId { get; set; }
    }
}
