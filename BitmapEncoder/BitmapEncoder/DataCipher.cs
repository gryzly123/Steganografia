using System;
using System.IO;
using System.Security.Cryptography;

namespace BitmapEncoder
{
    public class DataCipher
    {
        public static MemoryStream BufferFromString(string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            return new MemoryStream(bytes, 0, bytes.Length, false, true);
        }

        public static void CalculateHashSha512(byte[] Input, out byte[] Output)
        {
            SHA512 HashAlgorithm = SHA512.Create();
            Output = HashAlgorithm.ComputeHash(Input);
            HashAlgorithm.Dispose();
        }

        public static void EncryptAes(byte[] InputKey, byte[] InputData, out byte[] OutputData)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(InputKey, new byte[128], "SHA512", 1);
            MemoryStream Output = new MemoryStream();

            //create and initialize AES object
            using (Aes CryptoAlgorithm = Aes.Create())
            {
                CryptoAlgorithm.Key = pdb.GetBytes(CryptoAlgorithm.KeySize / 8);
                CryptoAlgorithm.IV = pdb.GetBytes(CryptoAlgorithm.BlockSize / 8);
                CryptoAlgorithm.Padding = (InputData.Length % (CryptoAlgorithm.BlockSize / 8) == 0) ? PaddingMode.Zeros : PaddingMode.PKCS7;
                ICryptoTransform CryptoTransform = CryptoAlgorithm.CreateEncryptor(CryptoAlgorithm.Key, CryptoAlgorithm.IV);

                //pad input data length
                UInt32 OriginalLength = (UInt32)InputData.Length;

                //do the actual encryption
                using (MemoryStream EncryptionMS = new MemoryStream())
                {
                    //write the original message length from before and after AES-encrypted bytes
                    Output.Write(BitConverter.GetBytes(OriginalLength), 0, 4);

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

            UInt32 OriginalLength = 0;

            //create and initialize AES object
            using (MemoryStream DecryptionMS = new MemoryStream(InputData))
            {
                //read the original message length before AES-encrypted bytes
                byte[] OriginalLengthBytes = new byte[4];
                DecryptionMS.Read(OriginalLengthBytes, 0, 4);
                OriginalLength = BitConverter.ToUInt32(OriginalLengthBytes, 0);
                Output = new byte[OriginalLength];

                using (Aes CryptoAlgorithm = Aes.Create())
                {
                    CryptoAlgorithm.Key = pdb.GetBytes(CryptoAlgorithm.KeySize / 8);
                    CryptoAlgorithm.IV = pdb.GetBytes(CryptoAlgorithm.BlockSize / 8);
                    CryptoAlgorithm.Padding = (OriginalLength % (CryptoAlgorithm.BlockSize / 8) == 0) ? PaddingMode.Zeros : PaddingMode.PKCS7; ;
                    ICryptoTransform CryptoTransform = CryptoAlgorithm.CreateDecryptor(CryptoAlgorithm.Key, CryptoAlgorithm.IV);

                    //do the actual decryption
                    using (CryptoStream DecryptionCS = new CryptoStream(DecryptionMS, CryptoTransform, CryptoStreamMode.Read))
                    {
                        DecryptionCS.Read(Output, 0, Output.Length);
                    }
                }
            }
        }
    }
}
