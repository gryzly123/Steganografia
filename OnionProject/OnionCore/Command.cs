using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace OnionCore
{
    public abstract class Command
    {
        //BRO TRUST ME I DIDN'T OVER-ENGINEER THIS AT ALL, PLS BRO
        private static Dictionary<string, Type> commands;

        public static Command DetectCommand(byte[] InputData)
        {
            //cache known command types from the entire assembly
            if(commands == null)
            {
                commands = new Dictionary<string, Type>();
                IEnumerable<Type> subclassTypes = Assembly.GetAssembly(typeof(Command)).GetTypes().Where(t => t.IsSubclassOf(typeof(Command)));
                foreach(Type t in subclassTypes)
                {
                    Command archetype = (Command)Activator.CreateInstance(t);
                    string registeredCommandCode = archetype.CommandCode();

                    if (registeredCommandCode.StartsWith("$") && registeredCommandCode.EndsWith("\n") && registeredCommandCode.Length == 5)
                    {
                        commands.Add(registeredCommandCode, t);
                    }
                    else
                    {
                        throw new Exception("Invalid command type, fix the assembly");
                    }
                }
            }

            if (InputData.Length < 5) return null; //input data must AT LEAST contain 5-character command code ($ + 3 chars + \n)

            string commandCode = Encoding.ASCII.GetString(InputData.Take(5).ToArray());
            commands.TryGetValue(commandCode, out Type commandType);
            if (commandType == null) return null; //we don't know this command

            Command finalCommand = (Command)Activator.CreateInstance(commandType);
            if (!finalCommand.ParseInput(InputData.Skip(5).ToArray())) return null; //spawned command but it rejected the data

            return finalCommand; //spawned a valid command, return it for execution
        }

        public abstract string CommandCode();

        public abstract byte[] GenerateInput();

        public abstract bool ParseInput(byte[] inData);

        public abstract void Execute(out byte[] outData);
    }

    public class RelayMessage : Command
    {
        public OnionEndpoint targetEndpoint;
        public byte[] targetMessage;

        public override string CommandCode() { return "$REL\n"; }

        public override byte[] GenerateInput()
        {
            string header = CommandCode() + targetEndpoint.ToString(false) + "\r\n";
            byte[] result = new byte[header.Length + targetMessage.Length];
            Array.Copy(Encoding.ASCII.GetBytes(header), result, header.Length);
            Array.Copy(targetMessage, 0, result, header.Length, targetMessage.Length);
            return result; //i.e. $REL\n127.0.0.1:5555:\r\n<AES encoded message>
        }

        public override bool ParseInput(byte[] inData)
        {
            using (MemoryStream ms = new MemoryStream(inData))
            {
                //deserialize targetEndpoint
                string endpointString = "";
                using (StreamReader sr = new StreamReader(ms))
                {
                    endpointString = sr.ReadLine();
                }
                targetEndpoint = OnionEndpoint.FromString(endpointString, false);
                if (targetEndpoint == null) return false;

                //read message for the endpoint
                int memoryOffset = endpointString.Length + 2; // 2 = \r\n
                int messageLength = inData.Length - memoryOffset;
                if (messageLength == 0) return false;

                targetMessage = new byte[inData.Length - memoryOffset];
                Array.Copy(inData, memoryOffset, targetMessage, 0, messageLength);

                //everything was serialized
                return true;
            }
        }

        public override void Execute(out byte[] outData)
        {
            OnionTransport.SendRawData(targetEndpoint, targetMessage, out outData);
        }
    }

    public class DownloadFile : Command
    {
        public string targetUrl;

        public override string CommandCode() { return "$DLF\n"; }

        public override bool ParseInput(byte[] inData)
        {
            targetUrl = Encoding.UTF8.GetString(inData);
            Uri.TryCreate(targetUrl, UriKind.Absolute, out Uri uriResult);
            bool bIsValid = uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
            return bIsValid;
        }

        public override byte[] GenerateInput()
        {
            string header = CommandCode() + targetUrl;
            return Encoding.ASCII.GetBytes(header); //i.e. $DLF\nhttp://example.com/image.jpg
        }

        public override void Execute(out byte[] outData)
        {
            using (var client = new WebClient())
            {
                outData = client.DownloadData(targetUrl);
            }
        }
    }
}
