using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BitmapEncoder
{
    public partial class MainWindow : Form
    {
        Bitmap loadedImg;
        Bitmap encodedImg;

        public MainWindow()
        {
            InitializeComponent();
            //TestMethods(); // <-- uncomment to enable automatic test
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

        private void btnOpen2_Click(object sender, EventArgs e)
        {
            fdOpen.ShowDialog();
            if (fdOpen.CheckFileExists) fdOpen_OnFileSelected(true);
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

        private void TestMethods()
        {
            byte[] inputKey = Encoding.UTF8.GetBytes("ayyylmao nice key");
            byte[] inputMessage = Encoding.UTF8.GetBytes("super secret string to encode");

            //SHA test
            DataCipher.CalculateHashSha512(inputKey, out byte[] output);
            MessageBox.Show("SHA result in bits: " + (output.Length * 8).ToString());

            //AES+Image encoding test
            DataCipher.EncryptAes(inputKey, inputMessage, out byte[] output2);
            Bitmap image = (Bitmap)ImageConversion.LoadImageFromPath("C:\\Users\\knied\\Desktop\\60464685_1279936355495345_8126159015446052864_n.jpg");

            ImageConversion.EncodeMessageInImage(image, output2, out Bitmap cipheredImage);
            ImageConversion.DecodeMessageInImageUsingReference(cipheredImage, image, out byte[] output3);

            DataCipher.DecryptAes(inputKey, output3, out byte[] output4);
            MessageBox.Show("Message after operations: " + Encoding.UTF8.GetString(output4));
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbEncodeKey.Text))
            {
                MessageBox.Show("No encryption key provided");
                return;
            }
            if (string.IsNullOrEmpty(tbMessage.Text))
            {
                MessageBox.Show("No message to encrypt provided");
                return;
            }
            if(loadedImg == null)
            {
                MessageBox.Show("No image to encrypt messages in provided");
                return;
            }

            byte[] inputKey = Encoding.UTF8.GetBytes(tbEncodeKey.Text);
            byte[] inputMessage = Encoding.UTF8.GetBytes(tbMessage.Text);

            DataCipher.EncryptAes(inputKey, inputMessage, out byte[] bytesToEncode);
            if (ImageConversion.EncodeMessageInImage(loadedImg, bytesToEncode, out Bitmap cipheredImage))
            {
                encodedImg = cipheredImage;
                pbImage2.Image = encodedImg;
            }
            else
            {
                MessageBox.Show("The provided image was not big enough to encrypt the message in.");
            }
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbEncodeKey.Text))
            {
                MessageBox.Show("No decryption key provided");
                return;
            }
            if (loadedImg == null || encodedImg == null)
            {
                MessageBox.Show("[temporary] decoding from an image requires the original image to be present");
                return;
            }
            if (loadedImg.Size != encodedImg.Size)
            {
                MessageBox.Show("Images don't match - different resolutions");
                return;
            }

            byte[] inputKey = Encoding.UTF8.GetBytes(tbEncodeKey.Text);

            ImageConversion.DecodeMessageInImageUsingReference(encodedImg, loadedImg, out byte[] bytesToDecode);
            byte[] decodedBytes = null;
            try
            {
                DataCipher.DecryptAes(inputKey, bytesToDecode, out decodedBytes);
            }
            catch(Exception)
            {
                MessageBox.Show("AES decryption failed. Is the key valid?");
                return;
            }

            tbMessage.Text = Encoding.UTF8.GetString(decodedBytes);
        }

    }
}
