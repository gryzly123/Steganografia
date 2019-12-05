using OnionCore;
using System;
using System.Windows.Forms;

namespace OnionClient
{
    public partial class EndpointEditor : Form
    {
        OnionEndpoint endpointRef;
        bool bAccepted = false;

        public bool bWasAccepted => bAccepted;

        public EndpointEditor(OnionEndpoint endpointRef)
        {
            InitializeComponent();
            this.endpointRef = endpointRef;

            tbHostname.Text = endpointRef.hostName;
            tbPort.Text = endpointRef.port.ToString();
            tbAes.Text = Convert.ToBase64String(endpointRef.aesKey);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbHostname.Text)) throw new Exception();
                UInt16 targetPort = UInt16.Parse(tbPort.Text);
                byte[] aesKey = Convert.FromBase64String(tbAes.Text);

                endpointRef.hostName = tbHostname.Text;
                endpointRef.port = targetPort;
                endpointRef.aesKey = aesKey;
                bAccepted = true;
                Close();
            }
            catch(Exception)
            {
                MessageBox.Show("Provided data appears to be invalid.");
            }
        }
    }
}
