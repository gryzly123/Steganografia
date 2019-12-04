using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OnionCore;

namespace OnionClient
{
    public partial class MainWindow : Form
    {
        List<OnionEndpoint> endpoints = new List<OnionEndpoint> {
                new OnionEndpoint { hostName = "127.0.0.1", port = 1111, aesKey = Encoding.ASCII.GetBytes("supersecretkey1") },
                new OnionEndpoint { hostName = "127.0.0.1", port = 2222, aesKey = Encoding.ASCII.GetBytes("supersecretkey2") },
                new OnionEndpoint { hostName = "127.0.0.1", port = 3333, aesKey = Encoding.ASCII.GetBytes("supersecretkey3") },
            };

        public MainWindow()
        {
            InitializeComponent();
        }

        //Execute_DownloadFile(endpoints, "https://drupal.kniedzwiecki.eu/", out byte[] CommandResult);
        private static void Execute_DownloadFile(List<OnionEndpoint> endpoints, string urlToDownload, out byte[] outBytes)
        {
            DownloadFileCommand dfCommand = new DownloadFileCommand() { targetUrl = urlToDownload };
            OnionMessaging.ExecutePackedCommand(endpoints, dfCommand, out outBytes);
        }
    }
}
