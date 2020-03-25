using Painel_Projetos.DomainModel.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    [Table("Usuarios")]
    public class Usuario : EntityBase
    {
        public Aluno Aluno { get; set; }
        public int? AlunoID { get; set; }
        public Representante Representante { get; set; }
        public int? RepresentanteID { get; set; }
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public Perfil Perfil { get; set; }

        #region Metodos publicos 

        /// <summary>
        /// Método para separar o endereço de e-mail do seu dominio.
        /// Ex: passando joao.silva@aluno.faculdadeimpacta.cpm.br, o retorno será (joao.silva) 
        /// </summary>
        public static string SepararEmail(string email)
        {
            string[] dividido = email.Split('@');
            return dividido[0];
        }

        public static string Encriptar(string senha)
        {
            return Hash.GerarHash(senha);
        }

        #endregion
    }
}


public enum Perfil
{
    Cordenador = 1,
    Aluno = 2,
    Representante = 3
}

