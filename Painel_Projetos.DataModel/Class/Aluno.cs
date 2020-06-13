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
        #region Propriedades
        public Aluno()
        {
            GrupoAlunos = new HashSet<GruposAlunos>();
        }

        [Required(ErrorMessage = "Informe o número do Ra ")]
        public int RA { get; set; }
        [Required(ErrorMessage = "Informe o nome do aluno")]
        public string Nome { get; set; }
        public Curso Curso { get; set; }
        [Required(ErrorMessage = "Selecione um curso")]
        public int CursoID { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Informe a data de nascimento")]
        public DateTime DataNascimento { get; set; }
        [Required(ErrorMessage = "Informe um E-mail")]
        public string Email { get; set; }
        public ICollection<GruposAlunos> GrupoAlunos { get; set; }

        public Turma Turma { get; set; }
        [Required(ErrorMessage = "Selecione um turma")]
        public int TurmaId { get; set; }

        [Required(ErrorMessage = "Selecione um periodo")]
        public Periodo Periodo { get; set; }

        [NotMapped]
        public string MsgEmail { get; } = "E-mail invalido";
        [NotMapped]
        public string MsgRA { get; } = "RA deve conter 7 digitos";
        #endregion

        public override void Validar()
        {
            ValidaRA();
            ValidaEmail();
            base.Validar();
        }

        private void ValidaEmail()
        {
            if (!Email.Contains("@") || !Email.Contains(".com"))
            {
                _msgErro.Append($"{MsgEmail} {Environment.NewLine}");
            }
        }

        private void ValidaRA()
        {
            if (RA < 1000000 || RA > 9999999)
            {
                _msgErro.Append($"{MsgRA} {Environment.NewLine}");
            }
        }
    }
}
