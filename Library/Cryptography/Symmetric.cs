using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Library.Cryptography
{
    public static class Symmetric
    {
        private const string CHAVE_CRIPTOGRAFIA = "|@N&h}\\s==&$@#P!D)C[A}S|E";

        // Define o vetor de byte para rotina TripleDES
        private static byte[] ivTripleDES = { 224, 29, 248, 119, 175, 245, 143, 251 };

        /// <summary>
        /// Criptrografa uma string utilizando TripleDESCryptoServiceProvider class
        /// </summary>
        public static string Encrypt(string textToEncrypt)
        
        {
            try
            {
                //Remove caracteres nulos da string
                textToEncrypt = textToEncrypt.Replace(Convert.ToChar(0x0).ToString(), string.Empty);

                byte[] buffer = Encoding.UTF8.GetBytes(textToEncrypt);
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
                des.Key = MD5.ComputeHash(ASCIIEncoding.UTF8.GetBytes(CHAVE_CRIPTOGRAFIA));
                des.IV = ivTripleDES;
                string returnValue = Convert.ToBase64String(
                        des.CreateEncryptor().TransformFinalBlock(
                        buffer,
                        0,
                        buffer.Length
                        )
                    );
                return returnValue.Replace("+", "-").Replace("/", "_");
            }
            catch (CryptographicException ex)
            {
                throw new InvalidOperationException("CryptographicException: " + ex.Message);
            }
            catch (FormatException ex)
            {
                throw new InvalidOperationException("FormatException: " + ex.Message);
            }

        }

        /// <summary>
        /// Descriptrografa a senha atraves do TripleDESCryptoServiceProvider
        /// </summary>
        public static string Decrypt(string textToDecrypt)
        {
            try
            {
                textToDecrypt = textToDecrypt.Replace("_", "/").Replace("-", "+");

                byte[] buffer = Convert.FromBase64String(textToDecrypt);
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
                des.Key = MD5.ComputeHash(ASCIIEncoding.UTF8.GetBytes(CHAVE_CRIPTOGRAFIA));
                des.IV = ivTripleDES;
                string returnValue = Encoding.UTF8.GetString(
                        des.CreateDecryptor().TransformFinalBlock(
                        buffer,
                        0,
                        buffer.Length
                        )
                    );

                return returnValue.Replace(Convert.ToChar(0x0).ToString(), string.Empty);
            }
            catch (CryptographicException ex)
            {
                throw new InvalidOperationException("CryptographicException: " + ex.Message);
            }
            catch (FormatException ex)
            {
                throw new InvalidOperationException("FormatException: " + ex.Message);
            }
        }

        /// <summary>
        /// Retorna true se tiver criptografada
        /// </summary>
        /// <param name="textToDecrypt">string criptografada para teste</param>
        /// <returns></returns>
        public static bool IsEncrypted(string textToDecrypt)
        {
            try
            {
                Decrypt(textToDecrypt);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
