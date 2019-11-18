using System;
using System.Drawing;
using System.Windows.Forms;

namespace JpegDetector
{
    public partial class MainWindow : Form
    {
        Bitmap loadedImg;
        Bitmap encodedImg;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(encodedImg == null)
            {
                MessageBox.Show("No image to save.");
                return;
            }

            fdSave.ShowDialog();
            if(!string.IsNullOrEmpty(fdSave.FileName)) encodedImg.Save(fdSave.FileName);
        }

        private void btnOpen1_Click(object sender, EventArgs e)
        {
            fdOpen.ShowDialog();
            if (fdOpen.CheckFileExists) fdOpen_OnFileSelected(false);
        }

        private void fdOpen_OnFileSelected(bool IsRight)
        {
            try
            {
                Bitmap tmpImage = (Bitmap)Image.FromFile(fdOpen.FileName, true);
                if(IsRight)
                {
                    encodedImg = tmpImage;
                    pbImage2.Image = encodedImg;
                }
                else
                {
                    loadedImg = tmpImage;
                    pbImage1.Image = tmpImage;
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Image load failed");
            }
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            if(loadedImg == null)
            {
                MessageBox.Show("No image loaded for analysis");
                return;
            }

            JpegAnalysis.InsignificanceResult result = JpegAnalysis.DetectInsignificantPixels(loadedImg);
            pbImage2.Image = result.diffImage;
            float percentage = ((float)result.GetNumChangedPixels() / (float)result.GetNumTotalPixels()) * 100.0f;

            MessageBox.Show(string.Format("Insignificant pixels: {0} out of {1} ({2}%)"
                , result.GetNumChangedPixels()
                , result.GetNumTotalPixels()
                , (int)percentage));
        }

        bool bViewToggled = false;
        private void btnToggle_Click(object sender, EventArgs e)
        {
            bViewToggled = !bViewToggled;
            pbImage1.SizeMode = bViewToggled ? PictureBoxSizeMode.Zoom : PictureBoxSizeMode.Normal;
            pbImage2.SizeMode = bViewToggled ? PictureBoxSizeMode.Zoom : PictureBoxSizeMode.Normal;
        }
    }
}
