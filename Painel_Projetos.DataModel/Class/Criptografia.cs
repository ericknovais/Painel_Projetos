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
        static string passPhrase = "Pas5pr@se";
        static string saltValue = "s@1tValue";
        static string hashAlgorithm = "SHA1";
        static int passwordIterations = 2;
        static string initVector = "@1B2c3D4e5F6g7H8";
        static int keySize = 256;

        /// <summary>
        /// Metodo pra criptografar um texto
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string Criptografando(string texto)
        {
            /* 
             *  Converte seqüências de caracteres em matrizes de bytes.
                Suponhamos que as strings contenham apenas códigos ASCII.
                Se as strings incluem caracteres Unicode, use Unicode, UTF7 ou UTF8
                codificação. 
            */
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            /*  
                Converte nosso texto sem formatação em uma matriz de bytes.
                Suponhamos que o texto simples contenha caracteres codificados em UTF8 
            */
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(texto);

            /*  
                Primeiro, devemos criar uma senha, da qual a chave será derivada.
                Essa senha será gerada a partir da senha especificada e
                valor do sal. A senha será criada usando o hash especificado
                algoritmo. A criação da senha pode ser feita em várias iterações.
            */

            PasswordDeriveBytes senha = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

            /*
             *  Use a senha para gerar bytes pseudo-aleatórios para a criptografia
                chave. Especifique o tamanho da chave em bytes (em vez de bits).
            */
            byte[] chaveBytes = senha.GetBytes(keySize / 8);

            // Criar objeto de criptografia Rijndael não inicializado.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // É razoável definir o modo de criptografia como Encadeamento de blocos de criptografia
            // (CBC). Use opções padrão para outros parâmetros de chave simétrica.
            symmetricKey.Mode = CipherMode.CBC;

            /*
                Gere criptografador a partir dos bytes-chave e inicialização existentes vetor. 
                O tamanho da chave será definido com base no número da chave bytes.
            */
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(chaveBytes, initVectorBytes);

            // Defina o fluxo de memória que será usado para 
            // armazenar dados criptografados.
            MemoryStream memory = new MemoryStream();

            // Defina o fluxo criptográfico (sempre use o modo de gravação para criptografia).
            CryptoStream crypto = new CryptoStream(memory, encryptor, CryptoStreamMode.Write);
            // Começar encrypting.
            crypto.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Terminar encrypting.
            crypto.FlushFinalBlock();

            // Converte nossos dados criptografados 
            // de um fluxo de memória em uma matriz de bytes.
            byte[] textoCifrado = memory.ToArray();

            // Feche os dois fluxos.
            memory.Close();
            crypto.Close();

            // Converta dados criptografados em uma sequência codificada em base64.
            // Retornar string criptografada.
            return Convert.ToBase64String(textoCifrado);
        }
        
        /// <summary>
        /// Metodo pra desencriptar um texto
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string Descriptografar(string texto)
        {
            /*
                Converter cadeias que definem as características 
                da chave de criptografia em byte
                matrizes. Vamos supor que as strings contenham apenas códigos ASCII.
                Se as strings incluem caracteres Unicode, use Unicode, UTF7 ou UTF8
                codificação.
            */
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Converte nosso texto cifrado em uma matriz de bytes.
            byte[] cipherTextBytes = Convert.FromBase64String(texto.Replace(" ", "+"));

            /*  Primeiro, precisamos criar uma senha, a partir da qual a chave será
                derivado.Essa senha será gerada a partir do especificado
                senha e valor do sal.A senha será criada usando
                o algoritmo de hash especificado.A criação da senha pode ser feita em
                várias iterações.
            */
            PasswordDeriveBytes senha = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

            /*  Use a senha para gerar bytes pseudo - aleatórios para a criptografia
                chave.Especifique o tamanho da chave em bytes(em vez de bits).
            */
            byte[] chaveBytes = senha.GetBytes(keySize / 8);

            // Criar objeto de criptografia Rijndael não inicializado.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            /*
                É razoável definir o modo de criptografia como
                Encadeamento de blocos de criptografia
                (CBC).Use opções padrão para outros parâmetros de chave simétrica.
            */
            symmetricKey.Mode = CipherMode.CBC;

            /*
                Gere o decodificador a partir dos bytes - 
                chave e inicialização existentes
                vetor.O tamanho da chave será definido com base no número da chave
                bytes.
            */
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(chaveBytes, initVectorBytes);

            // Define o fluxo de memória que será usado para armazenar 
            // dados criptografados.
            MemoryStream memory = new MemoryStream(cipherTextBytes);

            //Defina o fluxo criptográfico(sempre use o modo de leitura para criptografia).
            CryptoStream crypto = new CryptoStream(memory, decryptor, CryptoStreamMode.Read);

            /*
                Como neste momento não sabemos qual o tamanho dos dados descriptografados
                será, aloque o buffer por tempo suficiente para conter o texto cifrado;
                texto simples nunca é maior que texto cifrado.
            */
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            // Começar decrypting.
            int decryptedByteCount = crypto.Read(plainTextBytes, 0, plainTextBytes.Length);

            //Feche os dois fluxos.
            memory.Close();
            crypto.Close();
            /*
                Converte os dados descriptografados em uma string.
                Suponhamos que a string de texto simples
                original tenha sido codificada em UTF8.
                Retornar string descriptografada.
            */
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}
