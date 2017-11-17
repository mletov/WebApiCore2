//https://dotnetcodr.com/2016/10/21/overview-of-symmetric-encryption-in-net/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WebApiCore.Utils;

namespace WebApiCore.Models
{
    public class UserResult : User
    {
        public override string email
        {
            get
            {
                SymmetricEncryptionService symmService = new SymmetricEncryptionService();
                SymmetricAlgorithm symmAlg = new AesCryptoServiceProvider() { KeySize = 128 };
                SymmetricEncryptionResult encryptionResult = symmService.Encrypt(base.email, symmAlg.KeySize, symmAlg);
                if (encryptionResult.Success)
                {
                    //string decrypted = symmService.Decrypt(encryptionResult.Cipher, encryptionResult.SymmetricKey, encryptionResult.IV, symmAlg);
                    //return decrypted;
                    return encryptionResult.CipherBase64;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
