//https://dotnetcodr.com/2016/10/21/overview-of-symmetric-encryption-in-net/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace WebApiCore.Utils
{
    public class SymmetricEncryptionResult
    {
        public byte[] Cipher { get; set; }
        public string CipherBase64 { get; set; }
        public byte[] IV { get; set; }
        public byte[] SymmetricKey { get; set; }
        public bool Success { get; set; }
        public string ExceptionMessage { get; set; }
    }

    public class SymmetricEncryptionService
    {
        public SymmetricEncryptionResult Encrypt(string messageToEncrypt, int symmetricKeyLengthBits,
            SymmetricAlgorithm algorithm)
        {
            SymmetricEncryptionResult encryptionResult = new SymmetricEncryptionResult();
            try
            {
                //first test if bit length is valid
                if (algorithm.ValidKeySize(symmetricKeyLengthBits))
                {
                    algorithm.KeySize = symmetricKeyLengthBits;
                    using (MemoryStream mem = new MemoryStream())
                    {
                        CryptoStream crypto = new CryptoStream(mem, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
                        byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(messageToEncrypt);
                        crypto.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
                        crypto.FlushFinalBlock();
                        byte[] encryptedBytes = mem.ToArray();
                        string encryptedBytesBase64 = Convert.ToBase64String(encryptedBytes);
                        encryptionResult.Success = true;
                        encryptionResult.Cipher = encryptedBytes;
                        encryptionResult.CipherBase64 = encryptedBytesBase64;
                        encryptionResult.IV = algorithm.IV;
                        encryptionResult.SymmetricKey = algorithm.Key;
                    }
                }
                else
                {
                    string NL = Environment.NewLine;
                    StringBuilder exceptionMessageBuilder = new StringBuilder();
                    exceptionMessageBuilder.Append("The provided key size - ")
                        .Append(symmetricKeyLengthBits).Append(" bits - is not valid for this algorithm.");
                    exceptionMessageBuilder.Append(NL)
                        .Append("Valid key sizes: ").Append(NL);
                    KeySizes[] validKeySizes = algorithm.LegalKeySizes;
                    foreach (KeySizes keySizes in validKeySizes)
                    {
                        exceptionMessageBuilder.Append("Min: ")
                            .Append(keySizes.MinSize).Append(NL)
                            .Append("Max: ").Append(keySizes.MaxSize).Append(NL)
                            .Append("Step: ").Append(keySizes.SkipSize);
                    }
                    throw new CryptographicException(exceptionMessageBuilder.ToString());
                }
            }
            catch (Exception ex)
            {
                encryptionResult.Success = false;
                encryptionResult.ExceptionMessage = ex.Message;
            }

            return encryptionResult;
        }

        public string Decrypt(byte[] cipherTextBytes, byte[] key, byte[] iv, SymmetricAlgorithm algorithm)
        {
            algorithm.IV = iv;
            algorithm.Key = key;
            using (MemoryStream mem = new MemoryStream())
            {
                CryptoStream crypto = new CryptoStream(mem, algorithm.CreateDecryptor(), CryptoStreamMode.Write);
                crypto.Write(cipherTextBytes, 0, cipherTextBytes.Length);
                crypto.FlushFinalBlock();
                return Encoding.UTF8.GetString(mem.ToArray());
            }
        }
    }
}
