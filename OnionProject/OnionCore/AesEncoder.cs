using System;
using System.IO;
using System.Security.Cryptography;

namespace OnionCore
{
    public class AesEncoder
    {
        public static void EncryptAes(byte[] InputKey, byte[] InputData, out byte[] OutputData)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(InputKey, new byte[128], "SHA512", 1);
            MemoryStream Output = new MemoryStream();

            //create and initialize AES object
            using (Aes CryptoAlgorithm = Aes.Create())
            {
                CryptoAlgorithm.Key = pdb.GetBytes(CryptoAlgorithm.KeySize / 8);
                CryptoAlgorithm.IV = pdb.GetBytes(CryptoAlgorithm.BlockSize / 8);
                ICryptoTransform CryptoTransform = CryptoAlgorithm.CreateEncryptor(CryptoAlgorithm.Key, CryptoAlgorithm.IV);

                //do the actual encryption
                using (MemoryStream EncryptionMS = new MemoryStream())
                {
                    using (CryptoStream EncryptionCS = new CryptoStream(Output, CryptoTransform, CryptoStreamMode.Write))
                    {
                        using (StreamWriter EncryptionSW = new StreamWriter(EncryptionCS))
                        {
                            EncryptionCS.Write(InputData, 0, InputData.Length);
                        }
                    }
                }
            }
            OutputData = Output.ToArray();
        }

        public static void DecryptAes(byte[] InputKey, byte[] InputData, out byte[] Output)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(InputKey, new byte[128], "SHA512", 1);


            //create and initialize AES object
            using (MemoryStream DecryptionMS = new MemoryStream(InputData))
            {
                Output = new byte[InputData.Length];

                using (Aes CryptoAlgorithm = Aes.Create())
                {
                    CryptoAlgorithm.Key = pdb.GetBytes(CryptoAlgorithm.KeySize / 8);
                    CryptoAlgorithm.IV = pdb.GetBytes(CryptoAlgorithm.BlockSize / 8);
                    ICryptoTransform CryptoTransform = CryptoAlgorithm.CreateDecryptor(CryptoAlgorithm.Key, CryptoAlgorithm.IV);

                    //do the actual decryption
                    using (CryptoStream DecryptionCS = new CryptoStream(DecryptionMS, CryptoTransform, CryptoStreamMode.Read))
                    {
                        int ResultLength = DecryptionCS.Read(Output, 0, Output.Length);
                        Array.Resize(ref Output, ResultLength);
                    }
                }
            }
        }
    }
}
