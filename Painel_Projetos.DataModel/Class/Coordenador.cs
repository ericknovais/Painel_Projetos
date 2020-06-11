using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    [Table("Coordenadores")]
    public class Coordenador : EntityBase
    {
        [Required(ErrorMessage = "Informe um nome do cordenador")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe um e-mail do cordenador")]
        public string Email { get; set; }

        [NotMapped]
        public string MsgEmail { get; } = "E-mail invalido";

        public override void Validar()
        {
            if (!Email.Contains("@") || !Email.Contains(".com"))
            {
                _msgErro.Append($"{MsgEmail} {Environment.NewLine}");
            }
            base.Validar();
        }
    }
}
