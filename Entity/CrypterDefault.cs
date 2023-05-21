using System.Security.Cryptography;
using System.Text;

namespace Entity
{
    public static class CrypterDefault
    {
        private static readonly byte[] Key = Convert.FromBase64String("QuSs0h/2tVnNz1EklGvGXNYWKAdnDwR4z6uZrxsqWfc="); // Clave de encriptación de 32 bytes (256 bits)

        public static string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.GenerateIV(); // Generar vector de inicialización aleatorio

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedData;

                // Encriptar el texto plano
                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }

                        encryptedData = msEncrypt.ToArray();
                    }
                }

                // Combinar el vector de inicialización y los datos encriptados
                byte[] combinedData = new byte[aesAlg.IV.Length + encryptedData.Length];
                Array.Copy(aesAlg.IV, 0, combinedData, 0, aesAlg.IV.Length);
                Array.Copy(encryptedData, 0, combinedData, aesAlg.IV.Length, encryptedData.Length);

                // Devolver la representación Base64 de los datos encriptados
                return Convert.ToBase64String(combinedData);
            }
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] cipherText = Convert.FromBase64String(encryptedText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;

                byte[] iv = new byte[aesAlg.BlockSize / 8];
                byte[] encryptedData = new byte[cipherText.Length - iv.Length];

                // Extraer el vector de inicialización y los datos encriptados
                Array.Copy(cipherText, 0, iv, 0, iv.Length);
                Array.Copy(cipherText, iv.Length, encryptedData, 0, encryptedData.Length);

                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                string plainText;

                // Desencriptar los datos
                using (var msDecrypt = new System.IO.MemoryStream(encryptedData))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                        {
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }

                return plainText;
            }

        }
    }
}
