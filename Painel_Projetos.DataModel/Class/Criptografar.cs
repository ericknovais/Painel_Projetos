using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    class Criptografar
    {
        public static string Encriptar(string texto)
        {
            string[] vs = { "&H0", "&H1", "&H2", "&H3", "&H4", "&H5", "&H6", "&H5", "&H4", "&H3", "&H2", "&H1", "&H0" };
            byte[] salt = Encoding.ASCII.GetBytes(vs.ToString());
            Rfc2898DeriveBytes chave = new Rfc2898DeriveBytes(texto, salt);
            RijndaelManaged algoritimo = new RijndaelManaged();

            algoritimo.Key = chave.GetBytes(16);
            algoritimo.IV = chave.GetBytes(16);

            byte[] fonteBytes = UnicodeEncoding.UTF8.GetBytes(texto);

            MemoryStream streamFonte = new MemoryStream(fonteBytes);

            MemoryStream streamDestino = new MemoryStream();

            CryptoStream crypto = new CryptoStream(streamFonte, algoritimo.CreateEncryptor(), CryptoStreamMode.Read);

            moveBytes(ref crypto, ref streamDestino);
            var textoCifrado = streamDestino.ToArray();
            return Convert.ToBase64String(textoCifrado);

        }

        private static void moveBytes(ref CryptoStream fonte, ref MemoryStream destino)
        {
            byte[] bytes = { 255, 255, 255 };
            int contador = fonte.Read(bytes, 0, bytes.Length - 1);
            while (0 != contador)
            {
                destino.Write(bytes, 0, contador);
                contador = fonte.Read(bytes, 0, bytes.Length - 1);
            }
        }
    }
}
