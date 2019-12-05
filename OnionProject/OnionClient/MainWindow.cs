using System;
using System.Collections.Generic;
using System.IO;
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

        List<OnionEndpoint> endpointsInUse = new List<OnionEndpoint>();

        public MainWindow()
        {
            InitializeComponent();
            RefreshListboxes();
        }

        private void AddEndpoint(OnionEndpoint newEndpoint)
        {
            foreach(OnionEndpoint oe in endpoints)
            {
                if (oe.hostName != newEndpoint.hostName) continue;
                if (oe.port == newEndpoint.port) return;
            }
            foreach (OnionEndpoint oe in endpointsInUse)
            {
                if (oe.hostName != newEndpoint.hostName) continue;
                if (oe.port == newEndpoint.port) return;
            }
            endpoints.Add(newEndpoint);
            RefreshListboxes();
        }

        private void RefreshListboxes()
        {
            listKnownRelays.Items.Clear();
            listUsedRelays.Items.Clear();
            foreach (OnionEndpoint oe in endpoints)
            {
                listKnownRelays.Items.Add(string.Format("{0}:{1}", oe.hostName, oe.port.ToString()));
            }
            foreach (OnionEndpoint oe in endpointsInUse)
            {
                listUsedRelays.Items.Add(string.Format("{0}:{1}", oe.hostName, oe.port.ToString()));
            }
        }

        private static void Execute_DownloadFile(List<OnionEndpoint> endpoints, string urlToDownload, out byte[] outBytes)
        {
            DownloadFileCommand dfCommand = new DownloadFileCommand() { targetUrl = urlToDownload };
            OnionMessaging.ExecutePackedCommand(endpoints, dfCommand, out outBytes);
        }

        private void btnAddRelay_Click(object sender, System.EventArgs e)
        {
            OnionEndpoint oe = new OnionEndpoint() { hostName = "127.0.0.1", port = 1111, aesKey = new byte[0] };
            EndpointEditor ee = new EndpointEditor(oe);
            ee.ShowDialog();
            if(ee.bWasAccepted) AddEndpoint(oe);
        }

        private void btnRemoveRelay_Click(object sender, EventArgs e)
        {
            int Index = listKnownRelays.SelectedIndex;
            endpoints.RemoveAt(Index);
            RefreshListboxes();
        }

        private void btnEnqueueRelay_Click(object sender, EventArgs e)
        {
            int Index = listKnownRelays.SelectedIndex;
            OnionEndpoint oe = endpoints[Index];
            endpointsInUse.Add(oe);
            endpoints.Remove(oe);
            RefreshListboxes();
        }

        private void btnDequeueRelay_Click(object sender, EventArgs e)
        {
            int Index = listUsedRelays.SelectedIndex;
            OnionEndpoint oe = endpointsInUse[Index];
            endpoints.Add(oe);
            endpointsInUse.Remove(oe);
            RefreshListboxes();
        }

        private void listKnownRelays_DoubleClick(object sender, EventArgs e)
        {
            int Index = listKnownRelays.SelectedIndex;
            EndpointEditor ee = new EndpointEditor(endpoints[Index]);
            ee.ShowDialog();
            if (ee.bWasAccepted) RefreshListboxes();
        }

        private void btnRunRequest_Click(object sender, EventArgs e)
        {
            Execute_DownloadFile(endpointsInUse, tbFileLocation.Text, out byte[] CommandResult);

            if(cbDisplayHtml.Checked) wbHtmlPreview.DocumentStream = new MemoryStream(CommandResult);
        }


    }
}
