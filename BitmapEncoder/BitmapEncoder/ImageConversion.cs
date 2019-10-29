using System;
using System.Drawing;
using System.Windows.Forms;

namespace BitmapEncoder
{
    public class ImageConversion
    {
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

        //  public static byte[] ImageToBytes(Image image)
        //  {
        //      MessageBox.Show((image.HorizontalResolution * image.VerticalResolution).ToString());
        //  
        //      //using (MemoryStream ms = new MemoryStream())
        //      //{
        //      //    image.Save(ms, ImageCodecInfo.GetImageDecoders().,  ImageFormat.MemoryBmp);
        //      //    MessageBox.Show((ms.ToArray().Length).ToString());
        //      //    return ms.ToArray();
        //      //}
        //  }
        //  
        //  public static Image BytesToImage(byte[] bytes, Point resolution)
        //  {
        //      using (MemoryStream ms = new MemoryStream(bytes))
        //      {
        //          return Image.FromStream(ms);
        //      }
        //  }

        public static bool[] ByteToBits(byte inByte)
        {
            return new bool[]
            {
               (inByte & 1)   != 0,
               (inByte & 2)   != 0,
               (inByte & 4)   != 0,
               (inByte & 8)   != 0,
               (inByte & 16)  != 0,
               (inByte & 32)  != 0,
               (inByte & 64)  != 0,
               (inByte & 128) != 0
            };
        }

        public static byte BitsToByte(bool[] inBits)
        {
            return (byte)((inBits[0] ? 1 : 0)
                        + (inBits[1] ? 2 : 0)
                        + (inBits[2] ? 4 : 0)
                        + (inBits[3] ? 8 : 0)
                        + (inBits[4] ? 16 : 0)
                        + (inBits[5] ? 32 : 0)
                        + (inBits[6] ? 64 : 0)
                        + (inBits[7] ? 128 : 0));
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
            bool[] bits = ByteToBits(inMessage[0]);

            for (int iX = 0; iX < x; ++iX)
                for (int iY = 0; iY < y; ++iY)
                {
                    //get next byte from message to encode, quit if message ended
                    if (bitsLoc == 8)
                    {
                        ++msgLoc;
                        if (msgLoc == msgLen) return true;
                        bits = ByteToBits(inMessage[msgLoc]);
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
            return (msgLoc + 1 == msgLen && bitsLoc == 8);
        }

        public static bool DecodeMessageInImageUsingReference(Bitmap inImage, Bitmap refImage, out byte[] outMessage)
        {
            if (inImage.Size != refImage.Size) throw new Exception("resolution mismatch");

            int x = inImage.Size.Width;
            int y = inImage.Size.Height;

            int msgLen = 4;
            outMessage = new byte[4];
            bool[] bits = new bool[8];

            //seek counters
            int bitsLoc = 0;
            int msgLoc = 0;

            for (int iX = 0; iX < x; ++iX)
                for (int iY = 0; iY < y; ++iY)
                {
                    //we collected an entire byte to decode, quit if message ended
                    if (bitsLoc == 8)
                    {
                        outMessage[msgLoc] = BitsToByte(bits);
                        bitsLoc = 0;
                        ++msgLoc;

                        if (msgLoc == 4)
                        {
                            msgLen = (int)BitConverter.ToUInt32(outMessage, 0);
                            if (msgLen % 16 != 0) msgLen = 4 + (((msgLen / 16) + 1) * 16);
                            else msgLen += 4;
                            Array.Resize(ref outMessage, msgLen);
                        }
                        if (msgLoc == msgLen) return true;
                    }

                    //get the a1,a2,a3 from bitmap pixel
                    Color pi = inImage.GetPixel(iX, iY);
                    Color pr = refImage.GetPixel(iX, iY);

                    int action = (pi.R != pr.R) ? 1
                               :((pi.G != pr.G) ? 2
                               :((pi.B != pr.B) ? 3
                               : 0));

                    bool a1 = (pr.R & 1) != 0;
                    bool a2 = (pr.G & 1) != 0;
                    bool a3 = (pr.B & 1) != 0;

                    //parity based decoding with reference:
                    bool x1 = (a1 ^ a3);
                    if (action % 2 == 1) x1 = !x1;

                    bool x2 = (a2 ^ a3);
                    if (action > 1) x2 = !x2;

                    bits[bitsLoc++] = x1;
                    bits[bitsLoc++] = x2;
                }
            return false;
        }
    }

}


//classic 3 bit LSB replacing method loop:
//  for(int iX = 0; iX < x; ++iX)
//      for(int iY = 0; iY < y; ++iY)
//      {
//          Color p = outImage.GetPixel(iX, iY);
//          col = new byte[3] { p.R, p.G, p.B };
//          for (int iP = 0; iP < 3; ++iP)
//          {
//              if (bitsLoc == 8)
//              {
//                  ++msgLoc;
//                  if (msgLoc == msgLen) break;
//                  bits = ByteToBits(inMessage[msgLoc]);
//                  bitsLoc = 0;
//              }
//              byte val = bits[bitsLoc++] ? (byte)255 : (byte)254;
//              switch(iP)
//              {
//                  case 0: col[0] = (byte)(col[0] & val); break;
//                  case 1: col[1] = (byte)(col[1] & val); break;
//                  case 2: col[2] = (byte)(col[2] & val); break;
//              }
//          }
//          outImage.SetPixel(iX, iY, Color.FromArgb(col[0], col[1], col[2]));
//      }
//
