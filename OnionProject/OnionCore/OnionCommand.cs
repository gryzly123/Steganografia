using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OnionCore
{
    public abstract class OnionCommandFactory
    {
        //CommandCode to OnionCommand class cache
        private static Dictionary<string, Type> commands;

        /* Deserialization method for Commands. Caches all known command types in assembly during first execution
         * and then matches 5-character command code with an existing class. (BRO TRUST ME I DIDN'T OVER-ENGINEER THIS
         * AT ALL, PLS BRO) */
        public static OnionCommand SpawnCommand(byte[] InputData)
        {
            //cache known command types from the entire assembly
            if(commands == null)
            {
                commands = new Dictionary<string, Type>();
                IEnumerable<Type> subclassTypes = Assembly.GetAssembly(typeof(OnionCommand)).GetTypes().Where(t => t.IsSubclassOf(typeof(OnionCommand)));
                foreach(Type t in subclassTypes)
                {
                    OnionCommand archetype = (OnionCommand)Activator.CreateInstance(t);
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

            OnionCommand finalCommand = (OnionCommand)Activator.CreateInstance(commandType);
            if (!finalCommand.DeserializeFromBytes(InputData.Skip(5).ToArray())) return null; //spawned command but it rejected the data

            return finalCommand; //spawned a valid command, return it for execution
        }
    }

    /* Base class for all Onion commands. */
    public abstract class OnionCommand
    {
        /* 5-character command code ('$' sign + 3 unique letters + '\n'). */
        public abstract string CommandCode();

        /* Serializes command to a byte array and returns it. INCLUDES command code. */
        public abstract byte[] SerializeToBytes();

        /* Deserializes command from a byte array. Expects the byte array NOT TO INCLUDE command code
         * as it is stripped by OnionCommandFactory during class detection. Returns whether
         * the deseriaziation succeeded. */
        public abstract bool DeserializeFromBytes(byte[] inData);

        /* Executes command and returns the result as a byte array. Returns empty array if failed. */
        public abstract void Execute(out byte[] outData);
    }
}
