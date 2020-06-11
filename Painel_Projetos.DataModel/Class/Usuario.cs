using Painel_Projetos.DomainModel.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
        public Coordenador Cordenador { get; set; }
        public int? CordenadorID { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public Perfil Perfil { get; set; }

        [NotMapped]
        public static string SenhaPadrao { get; } = "impacta2020";

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

        public static void EnviarEmailDeLogin(string nome, string email)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("painel.suport@gmail.com", "impacta2020");
            MailMessage mail = new MailMessage();
            mail.Sender = new MailAddress("painel.suport@gmail.com", "Painel De Projetos");
            mail.From = new MailAddress("painel.suport@gmail.com", "Painel De Projetos");
            mail.To.Add(new MailAddress(email, nome));
            mail.Subject = "Login e Senha";
            StringBuilder corpo = new StringBuilder();
            corpo.AppendFormat(
                                $"<h1>Olá</h1>" +
                                $"<p><b>{nome}</b> foi realizado um cadastro na nossa plataforma seu</p>" +
                                $"<p><b>Login:</b> {Usuario.SepararEmail(email)}</p>" +
                                $"<p><b>Senha:</b> {SenhaPadrao}</p>");
            mail.Body = corpo.ToString();
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            

            try
            {
                client.Send(mail);
            }
            catch (System.Exception erro)
            {
                throw new Exception(erro.ToString());
            }
            finally
            {
                mail = null;
            }
        }
    }

    #endregion
}

