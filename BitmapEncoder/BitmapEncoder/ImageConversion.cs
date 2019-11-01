using System;
using System.Drawing;
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

        public static bool EncodeMessageInImage(Image inImage, byte[] inMessage, out Bitmap outImage)
        {
            //don't work with the original bitmap, memcpy it instead
            outImage = (Bitmap)inImage.Clone();

            //these variables should be const but C# is a bad language and doesn't support this feature
            /*const*/ int msgLen = inMessage.Length;
            /*const*/ int x = inImage.Size.Width;
            /*const*/ int y = inImage.Size.Height;

            //seek variables
            int bitsLoc = 0;
            int msgLoc = 0;
            bool[] bits = ByteToHammingBits(inMessage[0]);

            for (int iY = 0; iY < y; ++iY)
                for (int iX = 0; iX < x; ++iX)
                {
                    //get next byte from message to encode, quit if message ended
                    if (bitsLoc == ByteSizeInBits)
                    {
                        ++msgLoc;
                        if (msgLoc == msgLen) return true;
                        bits = ByteToHammingBits(inMessage[msgLoc]);
                        bitsLoc = 0;
                    }

                    //get the a1,a2,a3 from bitmap pixel
                    Color p = outImage.GetPixel(iX, iY);
                    bool a1 = (p.R & 1) != 0;
                    bool a2 = (p.G & 1) != 0;
                    bool a3 = (p.B & 1) != 0;

                    //get the x1,x2 from message
                    bool x1 = bits[bitsLoc];
                    bool x2 = bits[bitsLoc + 1];
                    bitsLoc += 2;

                    //parity based encoding:
                    int action = (x1 == (a1 ^ a3)) ? 0 : 1;
                    action += (x2 == (a2 ^ a3)) ? 0 : 2;

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

            //returns false because the message didn't fit inside the image.
            //returns true in an edge case where the message fits perfectly
            return (msgLoc + 1 == msgLen && bitsLoc == ByteSizeInBits);
        }

        public static bool DecodeMessageInImage(Bitmap inImage, out byte[] outMessage)
        {
            int x = inImage.Size.Width;
            int y = inImage.Size.Height;

            int msgLen = 4;
            outMessage = new byte[4];
            bool[] bits = new bool[ByteSizeInBits];

            //seek counters
            int bitsLoc = 0;
            int msgLoc = 0;

            for (int iY = 0; iY < y; ++iY)
                for (int iX = 0; iX < x; ++iX)
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
                            Array.Resize(ref outMessage, msgLen);
                        }
                        if (msgLoc == msgLen) return true;
                    }

                    //get the a1,a2,a3 from bitmap pixel
                    Color pi = inImage.GetPixel(iX, iY);
                    bool a1a2 = ((pi.R & 1) ^ (pi.G & 1)) == 1;
                    bool a1a3 = ((pi.R & 1) ^ (pi.B & 1)) == 1;
                    bits[bitsLoc++] = a1a3;
                    bits[bitsLoc++] = a1a3 ^ a1a2;
                }
            return false;
        }
    }
}
