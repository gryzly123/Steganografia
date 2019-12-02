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
            OnionEndpoint Endpoint = new OnionEndpoint { targetIp = IPAddress.Parse("127.0.0.1"), port = 1111, aesKey = Encoding.ASCII.GetBytes("ayyylmaooooooo") };
            
            switch (key.Key)
            {
                case ConsoleKey.S:
                    TcpSupport.RunServer(Endpoint, HandleData);
                    break;

                case ConsoleKey.C:
                    byte[] inData = Encoding.UTF8.GetBytes("test message");
                    TcpSupport.SendData(Endpoint, inData, out byte[] outData);
                    Console.WriteLine(Encoding.UTF8.GetString(outData));
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
