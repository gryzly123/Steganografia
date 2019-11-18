using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace JpegDetector
{
    public class JpegAnalysis
    {
        public struct InsignificanceResult
        {
            public readonly Image inImage;
            public readonly Image jpegImage;
            public readonly Image diffImage;
            public readonly List<int> diffPixels;
            public int GetNumChangedPixels() => diffPixels.Count;
            public int GetNumTotalPixels() => (inImage.Width * inImage.Height);

            public InsignificanceResult(Image inImage, Image jpegImage, Image diffImage, List<int> diffPixels)
            {
                this.inImage = inImage;
                this.jpegImage = jpegImage;
                this.diffImage = diffImage;
                this.diffPixels = diffPixels;
            }
        }

        public static InsignificanceResult DetectInsignificantPixels(Bitmap inImage)
        {
            MemoryStream jpegStream = new MemoryStream();
            inImage.Save(jpegStream, ImageFormat.Jpeg);

            Bitmap jpegImage = (Bitmap)Image.FromStream(jpegStream);
            jpegImage.Save(jpegStream, ImageFormat.Bmp);

            int x = inImage.Width;
            int y = inImage.Height;
            Bitmap diffImage = new Bitmap(inImage.Width, inImage.Height);
            List<int> changedPixels = new List<int>();

            for(int iy = 0; iy < y; ++iy)
                for(int ix = 0; ix < x; ++ix)
                {
                    bool bEqual = (jpegImage.GetPixel(ix, iy) == inImage.GetPixel(ix, iy));
                    if (bEqual)
                    {
                        diffImage.SetPixel(ix, iy, Color.Red);
                        changedPixels.Add(iy * x + ix);
                    }
                }

            return new InsignificanceResult(inImage, jpegImage, diffImage, changedPixels);
        }
    }
}
