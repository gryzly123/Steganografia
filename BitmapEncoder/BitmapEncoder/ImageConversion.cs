using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace BitmapEncoder
{
    public class ImageConversion
    {
        private const int ByteSizeInBits = 40; // 8*5 (hamming)

        public static Image LoadImageFromPath(string Filepath)
        {
            try
            {
                return System.Drawing.Image.FromFile(Filepath, true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }

        private static readonly bool[] Hamming0 = { false, false, true , false, true  };
        private static readonly bool[] Hamming1 = { true , true , false, true , false };

        public static int[] GetPermutation(int NumElements, int ElementMax, int RndSeed)
        {
            int[] Permutation = new int[NumElements];
            Random rngEngine = new Random(RndSeed);
            for(int i = 0; i < NumElements; ++i)
            {
                int tmp = rngEngine.Next(0, ElementMax);
                for (int j = i; j >= 0; --j) if (Permutation[j] == tmp) continue;
                Permutation[i] = tmp;
            }
            return Permutation;
        }

        public static bool[] ByteToHammingBits(byte inByte)
        {
            bool[] result = new bool[40];
            for(byte i = 0; i < 8; ++i)
                (((1 << i) & inByte) != 0 ? Hamming1 : Hamming0).CopyTo(result, i * 5); //i hate myself
            return result;
        }

        public static byte HammingBitsToByte(bool[] inBits)
        {
            int result = 0;
            bool[] bit = new bool[5];
            int tmp;
            int ptr = 0;

            for(int i = 0; i < 8; ++i)
            {
                tmp = 0;
                for (int j = 0; j < 5; ++j)
                {
                    if (Hamming1[j] == inBits[ptr++]) ++tmp;
                }
                result += (((tmp > 2) ? 1 : 0) << i);
            }
            return (byte)result;
        }

        public static bool EncodeMessageInImage(Image inImage, byte[] inMessage, byte[] steganographicKey, out Bitmap outImage)
        {
            //don't work with the original bitmap, memcpy it instead
            outImage = (Bitmap)inImage.Clone();

            //these variables should be const but C# is a bad language and doesn't support this feature
            /*const*/ int msgLen = inMessage.Length;
            /*const*/ int msgLenBitPairs = (msgLen * ByteSizeInBits) / 2;
            /*const*/ int x = inImage.Size.Width;
            /*const*/ int y = inImage.Size.Height;

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(steganographicKey, new byte[128], "SHA512", 1);
            int rngSeed = Math.Abs(BitConverter.ToInt32(pdb.GetBytes(4), 0));
            int[] PixelOrder = GetPermutation(msgLen * ByteSizeInBits, x * y, rngSeed);

            //seek variables
            int bitsLoc = 0;
            int msgLoc = 0;
            bool[] bits = ByteToHammingBits(inMessage[0]);

            for (int i = 0; i < msgLenBitPairs; ++i)
            {
                //get next byte from message to encode
                if (bitsLoc == ByteSizeInBits)
                {
                    ++msgLoc;
                    bits = ByteToHammingBits(inMessage[msgLoc]);
                    bitsLoc = 0;
                }

                //get the a1,a2,a3 from bitmap pixel
                int iX = PixelOrder[i] % x;
                int iY = PixelOrder[i] / x;
                Color p = outImage.GetPixel(iX, iY);
                bool a1 = (p.R & 1) != 0;
                bool a2 = (p.G & 1) != 0;
                bool a3 = (p.B & 1) != 0;

                //parity based encoding:
                int action = ((bits[bitsLoc] == (a1 ^ a3)) ? 0 : 1) + ((bits[bitsLoc+1] == (a2 ^ a3)) ? 0 : 2);
                bitsLoc += 2;

                //invert byte based on XORing result
                /*  switch (action) {
                      case 0: do nothing
                      case 1: invert a1
                      case 2: invert a2
                      case 3: invert a3 }  */
                outImage.SetPixel(iX, iY, Color.FromArgb(
                        ((action == 1) ? p.R ^ 1 : p.R),
                        ((action == 2) ? p.G ^ 1 : p.G),
                        ((action == 3) ? p.B ^ 1 : p.B)
                    ));
            }
            return true;
        }

        public static bool DecodeMessageInImage(Bitmap inImage, byte[] steganographicKey, out byte[] outMessage)
        {
            int x = inImage.Size.Width;
            int y = inImage.Size.Height;

            int msgLen = 4;
            outMessage = new byte[4];
            bool[] bits = new bool[ByteSizeInBits];

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(steganographicKey, new byte[128], "SHA512", 1);
            int rngSeed = Math.Abs(BitConverter.ToInt32(pdb.GetBytes(4), 0));
            int[] PixelOrder = GetPermutation(4 * ByteSizeInBits, x * y, rngSeed);
            int msgLenBitPairs = 2 * ByteSizeInBits;
            //seek counters
            int bitsLoc = 0;
            int msgLoc = 0;

            for (int i = 0; i <= msgLenBitPairs; ++i)
            {
                //we collected an entire byte to decode, quit if message ended
                if (bitsLoc == ByteSizeInBits)
                {
                    outMessage[msgLoc] = HammingBitsToByte(bits);
                    bitsLoc = 0;
                    ++msgLoc;

                    if (msgLoc == 4)
                    {
                        msgLen = (int)BitConverter.ToUInt32(outMessage, 0);
                        if (msgLen % 16 != 0) msgLen = 4 + ((((msgLen / 16) + 1) * 16));
                        else msgLen += 4;
                        PixelOrder = GetPermutation(msgLen * ByteSizeInBits, x * y, rngSeed);
                        msgLenBitPairs = (msgLen * ByteSizeInBits) / 2;
                        Array.Resize(ref outMessage, msgLen);
                    }
                }

                //get the a1,a2,a3 from bitmap pixel
                int iX = PixelOrder[i] % x;
                int iY = PixelOrder[i] / x;
                Color pi = inImage.GetPixel(iX, iY);
                bool a1a2 = ((pi.R & 1) ^ (pi.G & 1)) == 1;
                bool a1a3 = ((pi.R & 1) ^ (pi.B & 1)) == 1;
                bits[bitsLoc++] = a1a3;
                bits[bitsLoc++] = a1a3 ^ a1a2;
            }
            return true;
        }
    }
}
