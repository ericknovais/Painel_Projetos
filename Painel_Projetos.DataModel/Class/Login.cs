using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    [Table("Logins")]
    public class Login : EntityBase
    {
        public Aluno Aluno { get; set; }
        public int AlunoId { get; set; }
        public Representante Representante { get; set; }
        public int RepresentanteID { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public Perfil Perfil { get; set; }
    }

    public enum Perfil
    {
        Cordenador = 1,
        Aluno = 2,
        Representante = 3
    }
}
