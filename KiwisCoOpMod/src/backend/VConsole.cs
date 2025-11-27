/*
    Kiwi's Co-Op Mod for Half-Life: Alyx
    Copyright (c) 2024 KiwifruitDev. All rights reserved.
    This software is licensed under the MIT License.
    -----------------------------------------------------------------------------
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
    -----------------------------------------------------------------------------
*/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Reflection;

namespace KiwisCoOpMod
{
    public enum VConsoleSource
    {
        Unknown,
        Game,
        Console
    }
    public enum VConsoleOperationType
    {
        NULL,
        CMND,
        PRNT,
        VFCS
    }
    public class VConsoleOperation
    {
        public VConsoleOperationType Type { get; set; } = VConsoleOperationType.NULL;
        public VConsoleOperation(VConsoleOperationType operation = VConsoleOperationType.NULL)
        {
            Type = operation;
        }
        public VConsoleOperation(string operation)
        {
            switch(operation)
            {
                case "CMND":
                    Type = VConsoleOperationType.CMND;
                    break;
                case "PRNT":
                    Type = VConsoleOperationType.PRNT;
                    break;
                case "VFCS":
                    Type = VConsoleOperationType.VFCS;
                    break;
            }
        }
        public static VConsoleOperation NULL => new(VConsoleOperationType.NULL);
        public static VConsoleOperation CMND => new(VConsoleOperationType.CMND);
        public static VConsoleOperation PRNT => new(VConsoleOperationType.PRNT);
        public static VConsoleOperation VFCS => new(VConsoleOperationType.VFCS);
        public static List<VConsoleOperation> GetOperations()
        {
            return new List<VConsoleOperation> {NULL, CMND, PRNT, VFCS};
        }
        public static implicit operator string(VConsoleOperation operation)
        {
            return operation.Type.ToString();
        }
    }
    public class VConsoleQuery
    {
        public readonly VConsoleOperation Operation = VConsoleOperation.NULL;
        public VConsoleSource Source {
            get
            {
                switch(Operation.Type)
                {
                    case VConsoleOperationType.VFCS:
                    case VConsoleOperationType.CMND:
                        return VConsoleSource.Console;
                    case VConsoleOperationType.PRNT:
                        return VConsoleSource.Game;
                }
                return VConsoleSource.Unknown;
            }
        }
        public string CMND_Command { get; set; } = "";
        public string PRNT_Message { get; set; } = "";
        public bool VFCS_Focused { get; set; } = true;
        public VConsoleQuery(VConsoleOperation? operation = null)
        {
            if(operation == null)
                Operation = VConsoleOperation.NULL;
            else
                Operation = operation;
        }
        public byte[] QueryParameters()
        {
            switch(Operation.Type)
            {
                case VConsoleOperationType.VFCS:
                {
                    List<byte> dataList = new() {0x00, (byte)(VFCS_Focused == true ? 1 : 0) };
                    return dataList.ToArray();
                }
                case VConsoleOperationType.CMND:
                {
                    List<byte> dataList = new() {0x00};
                    byte[] data = Encoding.ASCII.GetBytes(CMND_Command);
                    foreach (byte cmdByte in data)
                    {
                        dataList = dataList.Append(cmdByte).ToList();
                    }
                    dataList.Add(0x00);
                    return dataList.ToArray();
                }
                case VConsoleOperationType.PRNT:
                {
                    byte[] dummy = new byte[20];
                    List<byte> dataList = new() {0x00};
                    byte[] data = Encoding.ASCII.GetBytes(PRNT_Message).Reverse().ToArray();
                    foreach (byte cmdByte in data)
                    {
                        dataList = dataList.Prepend(cmdByte).ToList();
                    }
                    foreach (byte cmdByte in dummy)
                    {
                        dataList = dataList.Prepend(cmdByte).ToList();
                    }
                    return dataList.ToArray();
                }
            }
            return new byte[0];
        }
        public byte[] GetBytes()
        {
            byte[] parameters = QueryParameters();
            byte[] vcmd = Encoding.ASCII.GetBytes(Operation).Reverse().ToArray();
            byte dataLength = Convert.ToByte(parameters.Length + 11);
            byte protocol = Convert.ToByte(int.Parse(SaveData.saveValues["VConsoleProtocol"], CultureInfo.InvariantCulture));
            List<byte> dataList = new()
            {
                0, protocol, 0, 0, 0, dataLength, 0
            };
            foreach (byte cmdByte in vcmd)
            {
                dataList = dataList.Prepend(cmdByte).ToList();
            }
            foreach (byte cmdByte in parameters)
            {
                dataList = dataList.Append(cmdByte).ToList();
            }
            return dataList.ToArray();
        }
        public static VConsoleQuery? Create(byte[] data)
        {
            byte[] operation = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                operation[i] = data[i];
            }
            VConsoleOperation operationType = new VConsoleOperation(Encoding.ASCII.GetString(operation));
            VConsoleQuery query = new(operationType);
            bool failed = false;
            // Read the data (queryparameters but backwards)
            switch(operationType.Type)
            {
                case VConsoleOperationType.CMND:
                {
                    if(data.Length < 12)
                    {
                        failed = true;
                        break;
                    }
                    // Command starts at 41st byte and ends at 0x00
                    List<byte> command = new();
                    for (int i = 11; i < data.Length; i++)
                    {
                        if(data[i] == 0)
                            break;
                        command.Add(data[i]);
                    }
                    query.CMND_Command = Encoding.ASCII.GetString(command.ToArray());
                    Console.WriteLine(operationType + " " + query.CMND_Command);
                    break;
                }
                case VConsoleOperationType.PRNT:
                {
                    if(data.Length < 41)
                    {
                        failed = true;
                        break;
                    }
                    // Message starts at 41st byte and ends at 0x00
                    List<byte> message = new();
                    for (int i = 39; i < data.Length; i++)
                    {
                        if(data[i] == 0)
                            break;
                        message.Add(data[i]);
                    }
                    query.PRNT_Message = Encoding.ASCII.GetString(message.ToArray());
                    Console.WriteLine(operationType + " " + query.PRNT_Message.Replace("\n", ""));
                    break;
                }
            }
            return failed ? null : query;
        }
        public void Write(Stream stream)
        {
            byte[] data = GetBytes();
            switch(Operation.Type)
            {
                case VConsoleOperationType.CMND:
                    Console.WriteLine("CMND " + CMND_Command);
                    break;
                case VConsoleOperationType.PRNT:
                    Console.WriteLine("PRNT " + PRNT_Message);
                    break;
            }
            stream.Write(data, 0, data.Length);
        }
    }
    public static class VConsole
    {
        public static bool Connected => client != null && client.Connected;
        private static TcpClient? client;
        private static NetworkStream? stream;
        private static List<List<byte>> templates = new();
        public static event EventHandler<VConsoleQuery>? OnQuery;
        private static byte[] DataStack = new byte[9];
        public static void Focus(bool focused = true)
        {
            // Write VFCS to keep window focused
            VConsoleQuery focusQuery = new(VConsoleOperation.VFCS) { VFCS_Focused = focused };
            focusQuery.Write(stream);
        }
        public static void Command(string command)
        {
            // Write CMND to execute a command
            VConsoleQuery commandQuery = new(VConsoleOperation.CMND) { CMND_Command = command };
            commandQuery.Write(stream);
        }
        public static void Print(string message)
        {
            // Write PRNT to print a message
            VConsoleQuery printQuery = new(VConsoleOperation.PRNT) { PRNT_Message = message };
            printQuery.Write(stream);
        }
        public static bool Connect()
        {
            if(Connected)
                return false;
            int vconsolePort = int.Parse(SaveData.saveValues["VConsolePort"], CultureInfo.InvariantCulture);
            try
            {
                client = new TcpClient(SaveData.saveValues["VConsoleIP"], vconsolePort);
                if(client.Connected)
                {
                    stream = client.GetStream();
                    SetupTemplates();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
        public static void Read()
        {
            if(stream == null || !Connected || !stream.DataAvailable)
                return;

            // Read and compare to templates
            // If a template matches, the next byte is the length of the data
            // Read the data into a list of bytes using the length (minus 11)
            
            // A stack-based approach is used to read the data bytes within 11 bytes
            // 1 byte is read at a time, and the stack is popped when the length is reached
            // Comparing the 11 bytes to the templates is done to determine the operation

            // Read one byte and push/pop the stack
            int buffer = stream.ReadByte();
            if (buffer == -1)
                return;
            
            // Shift the bytes in the data array
            for (int i = 0; i < DataStack.Length - 1; i++)
            {
                DataStack[i] = DataStack[i + 1];
            }

            // Add the new byte to the data array
            DataStack[DataStack.Length - 1] = (byte)buffer;

            // Compare the data array to the templates
            foreach (List<byte> template in templates)
            {
                bool match = true;
                for (int i = 0; i < template.Count; i++)
                {
                    if (template[i] != DataStack[i])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    // Read the data length
                    int length = stream.ReadByte();
                    if (length <= 0)
                        continue;
                    byte[] dataBuffer = new byte[length];
                    for (int i = 0; i < DataStack.Length; i++)
                    {
                        dataBuffer[i] = DataStack[i];
                    }
                    for (int i = DataStack.Length; i < length; i++)
                    {
                        dataBuffer[i] = (byte)stream.ReadByte();
                    }
                    // Parse the data
                    VConsoleQuery? query = VConsoleQuery.Create(dataBuffer);
                    if (query == null)
                        continue;
                    // Raise the event
                    OnQuery?.Invoke(null, query);
                }
            }
        }
        public static void Disconnect()
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
        }
        public static void SetupTemplates()
        {
            templates.Clear();
            List<VConsoleOperation> operations = VConsoleOperation.GetOperations();
            byte protocol = Convert.ToByte(int.Parse(SaveData.saveValues["VConsoleProtocol"], CultureInfo.InvariantCulture));
            byte[] baseTemplate = new byte[] {0, protocol, 0, 0, 0};
            for (int i = 0; i < operations.Count; i++)
            {
                byte[] operation = Encoding.ASCII.GetBytes(operations[i]).ToArray();
                List<byte> operationTemplate = new();
                foreach (byte cmdByte in operation)
                {
                    operationTemplate.Add(cmdByte);
                }
                foreach (byte cmdByte in baseTemplate)
                {
                    operationTemplate.Add(cmdByte);
                }
                templates.Add(operationTemplate);
            }
        }
    }
}
