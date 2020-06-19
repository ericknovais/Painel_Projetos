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
        public Coordenador Coordenador { get; set; }
        public int? CoordenadorID { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public Perfil Perfil { get; set; }

        [NotMapped]
        public static string URL { get; } = "http://52.149.211.185/"; //"http://localhost:8080";
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

        public static void EnviarEmailDeLogin(string nome, string email, string senha)
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
                                $"<p><b>Senha:</b> {senha}</p>" +
                                $"Click <a href='{URL}'>aqui</a> para efetuar seu Login!");
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

        public static string geraSenha()
        {

            // Determia as letras que poderão estar presente nas chaves
            string letras = "ABCDEFGHIJKLMNOPQRSTUVYWXZ";
            // Determia os numero que poderão estar presente nas chaves

            string numeros = "01234567689";

            // Determia o cataracter especial que poderão estar presente nas chaves
            string especiais = "!@#$%&";

            Random random = new Random();
            var armazenaChaves = letras.Substring(random.Next(1, 26), 1) +
                                    letras.Substring(random.Next(1, 26), 1) +
                                    letras.Substring(random.Next(1, 26), 1) +
                                    especiais.Substring(random.Next(1, 6), 1) +
                                    numeros.Substring(random.Next(1, 8), 1) +
                                    numeros.Substring(random.Next(1, 8), 1) +
                                    numeros.Substring(random.Next(1, 8), 1);

            // Chamada da função que determinará o indice em que a chave gerada deverá ser colocada
            return armazenaChaves;
        }

        public static void EnviarEmailDeNovaSenha(string nome, string email, string novaSenha)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("painel.suport@gmail.com", "impacta2020");
            MailMessage mail = new MailMessage();
            mail.Sender = new MailAddress("painel.suport@gmail.com", "Painel De Projetos");
            mail.From = new MailAddress("painel.suport@gmail.com", "Painel De Projetos");
            mail.To.Add(new MailAddress(email, nome));
            mail.Subject = "Nova senha";
            StringBuilder corpo = new StringBuilder();
            corpo.AppendFormat(
                                $"<h1>Olá</h1>" +
                                $"<p><b>{nome}</b> foi solicitado uma nova senha para acessar o Painel de projetos</p>" +
                                $"<p><b>Nova senha:</b> {novaSenha}</p>" +
                                $"Click <a href='{URL}'>aqui</a> para efetuar seu Login!");
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

        public static void EnviarEmailDeConvite(string nomeAdmin, string emailAdmin, string nomeConvidado, string emailConvidade, string nomeGrupo)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("painel.suport@gmail.com", "impacta2020");
            MailMessage mail = new MailMessage();
            mail.Sender = new MailAddress(emailAdmin, "Painel De Projetos");
            mail.From = new MailAddress(emailAdmin, nomeAdmin);
            mail.To.Add(new MailAddress(emailConvidade, nomeConvidado));
            mail.Subject = $"Convite para participar do grupo {nomeGrupo}";
            StringBuilder corpo = new StringBuilder();
            corpo.AppendFormat(
                                $"<h1>Olá</h1>" +
                                $"<p><b>{nomeConvidado}</b></p>" +
                                $"<p>O aluno <b>{nomeAdmin}</b> está te convidado para participar do grupo: {nomeGrupo}</p>" +
                                $"Click <a href='{URL}/Aluno/List'>aqui</a> se desejá fazer parte do grupo");
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

