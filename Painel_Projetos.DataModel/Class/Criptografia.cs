using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    class Criptografia
    {
        static byte[] vs = { 0, 1, 2, 3, 4, 5, 6, 5, 4, 3, 2, 1, 0};
        static byte[] salt = vs; //Encoding.ASCII.GetBytes(vs.ToString());
        static byte[] textoCifrado;

        public static string Encriptar(string texto)
        {
            Rfc2898DeriveBytes chave = new Rfc2898DeriveBytes(texto, salt);

            RijndaelManaged algoritimo = new RijndaelManaged();

            algoritimo.Key = chave.GetBytes(16);
            algoritimo.IV = chave.GetBytes(16);

            byte[] fonteBytes = UnicodeEncoding.UTF8.GetBytes(texto);

            MemoryStream streamFonte = new MemoryStream(fonteBytes);

            MemoryStream streamDestino = new MemoryStream();

            CryptoStream crypto = new CryptoStream(streamFonte, algoritimo.CreateEncryptor(), CryptoStreamMode.Read);

            moveBytes(ref crypto, ref streamDestino);
            textoCifrado = streamDestino.ToArray();
            return Convert.ToBase64String(textoCifrado);

        }

        private static void moveBytes(ref CryptoStream fonte, ref MemoryStream destino)
        {
            byte[] bytes = Encoding.ASCII.GetBytes("2048");
            int contador = fonte.Read(bytes, 0, bytes.Length - 1);
            while (0 != contador)
            {
                destino.Write(bytes, 0, contador);
                contador = fonte.Read(bytes, 0, bytes.Length - 1);
            }
        }

        public static string Desencriptar(string texto)
        {
            Rfc2898DeriveBytes chave = new Rfc2898DeriveBytes(texto, salt);
            RijndaelManaged algoritimo = new RijndaelManaged();

            algoritimo.Key = chave.GetBytes(16);
            algoritimo.IV = chave.GetBytes(16);

            textoCifrado = Encoding.UTF8.GetBytes(texto);
            MemoryStream streamFonte = new MemoryStream(textoCifrado);

            MemoryStream streamDestino = new MemoryStream();
            CryptoStream crypto = new CryptoStream(streamFonte, algoritimo.CreateDecryptor(), CryptoStreamMode.Read);
            moveBytes(ref crypto, ref streamDestino);
            byte[] bytesDescriptografados = streamDestino.ToArray();
            string textoDescriptografado = new UnicodeEncoding().GetString(bytesDescriptografados);
            return textoDescriptografado;

        }
    }
}
