using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OnionCore;

namespace OnionClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            ConsoleKeyInfo key = Console.ReadKey();

            List<OnionEndpoint> endpoints = new List<OnionEndpoint> {
                new OnionEndpoint { hostName = "127.0.0.1", port = 1111, aesKey = Encoding.ASCII.GetBytes("ayyylmaoooooooo") },
                new OnionEndpoint { hostName = "127.0.0.1", port = 2222, aesKey = Encoding.ASCII.GetBytes("twojstarypijany") },
                new OnionEndpoint { hostName = "127.0.0.1", port = 3333, aesKey = Encoding.ASCII.GetBytes("kek100lmao69420") },
            };

            switch (key.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.D2:
                case ConsoleKey.D3:
                    OnionEndpoint thisRelay = endpoints[(int)(key.Key - 49)];
                    Console.WriteLine("Running OnionRelay at {0}:{1} with key {2}", thisRelay.hostName, thisRelay.port.ToString(), Convert.ToBase64String(thisRelay.aesKey));
                    OnionTransport.RunSecureServer(thisRelay, OnionMessage.ExecuteReceivedCommand);
                    break;

                case ConsoleKey.C:
                    DownloadFile dfCommand = new DownloadFile() { targetUrl = "https://drupal.kniedzwiecki.eu/" };
                    OnionMessage.ExecutePackedCommand(endpoints, dfCommand, out byte[] outBytes);
                    Console.WriteLine(Encoding.UTF8.GetString(outBytes));
                    break;

                default:
                    return;
            }
        }

        private static void HandleData(byte[] inBytes, out byte[] outBytes)
        {
            outBytes = Encoding.UTF8.GetBytes((Encoding.UTF8.GetString(inBytes).ToUpper()));
        }
    }
}
