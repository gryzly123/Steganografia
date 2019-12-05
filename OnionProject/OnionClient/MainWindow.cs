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
            if (Index < 0 || Index > endpoints.Count) return;
            endpoints.RemoveAt(Index);
            RefreshListboxes();
        }

        private void btnEnqueueRelay_Click(object sender, EventArgs e)
        {
            int Index = listKnownRelays.SelectedIndex;
            if (Index < 0 || Index > endpoints.Count) return;
            OnionEndpoint oe = endpoints[Index];
            endpointsInUse.Add(oe);
            endpoints.Remove(oe);
            RefreshListboxes();
        }

        private void btnDequeueRelay_Click(object sender, EventArgs e)
        {
            int Index = listUsedRelays.SelectedIndex;
            if (Index < 0 || Index > endpointsInUse.Count) return;
            OnionEndpoint oe = endpointsInUse[Index];
            endpoints.Add(oe);
            endpointsInUse.Remove(oe);
            RefreshListboxes();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int Index = listKnownRelays.SelectedIndex;
            if (Index < 0 || Index > endpoints.Count) return;
            EndpointEditor ee = new EndpointEditor(endpoints[Index]);
            ee.ShowDialog();
            if (ee.bWasAccepted) RefreshListboxes();
        }

        private void listKnownRelays_DoubleClick(object sender, EventArgs e)
        {
            btnEnqueueRelay_Click(sender, e);
        }

        private void listUsedRelays_DoubleClick(object sender, EventArgs e)
        {
            btnDequeueRelay_Click(sender, e);
        }

        private void btnRunRequest_Click(object sender, EventArgs e)
        {
            Execute_DownloadFile(endpointsInUse, tbFileLocation.Text, out byte[] CommandResult);
            if(CommandResult.Length == 0)
            {
                MessageBox.Show("Received an empty response - this is most likely an error");
                return;
            }

            if(cbDisplayHtml.Checked) wbHtmlPreview.DocumentStream = new MemoryStream(CommandResult);
            if(cbSaveToDisk.Checked)
            {
                try
                {
                    if(tbDiskPath.Text.Length < 0)
                    {
                        MessageBox.Show("Requested save to file but no path was selected");
                    }
                    else using (FileStream fs = File.OpenWrite(tbDiskPath.Text))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            bw.Write(CommandResult);
                        }
                    }
                }
                catch(Exception)
                {
                    MessageBox.Show("File could not be opened for writing");
                }
            }
        }

        

        private void btnImportRelays_Click(object sender, EventArgs e)
        {
            ofdImportRelays.ShowDialog();
        }

        private void ofdImportRelays_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                using (StreamReader sr = new StreamReader(ofdImportRelays.OpenFile()))
                {
                    while (!sr.EndOfStream)
                    {
                        try
                        {
                            string endpoint = sr.ReadLine();
                            if (endpoint.Equals("--")) continue; //ignore relay list separator
                            OnionEndpoint oe = OnionEndpoint.FromString(endpoint, true);
                            AddEndpoint(oe);
                        }
                        catch (Exception) { /* do nothing, just ignore the invalid line */ }
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("File could not be opened");
            }
        }

        private void btnExportRelays_Click(object sender, EventArgs e)
        {
            sfdExportRelays.ShowDialog();
        }

        private void sfdExportRelays_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(sfdExportRelays.OpenFile()))
                {
                    foreach (OnionEndpoint oe in endpoints)
                    {
                        sw.WriteLine(oe.ToString(true));
                    }
                    sw.WriteLine("--");
                    foreach (OnionEndpoint oe in endpointsInUse)
                    {
                        sw.WriteLine(oe.ToString(true));
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("File could not be opened for save");
            }
        }

        private void btnChoosePath_Click(object sender, EventArgs e)
        {
            sfdTargetFile.ShowDialog();
        }

        private void sfdTargetFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tbDiskPath.Text = sfdTargetFile.FileName;
        }
    }
}
