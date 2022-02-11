/*
    Kiwi's Co-Op Mod for Half-Life: Alyx
    Copyright (c) 2022 KiwifruitDev
    All rights reserved.
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
using KiwisCoOpModCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;

namespace KiwisCoOpMod
{
    public class VConsole
    {
        private readonly WebsocketClient? ws;
        private TcpClient? client;
        private NetworkStream? stream;
        private bool killed = false;
        private readonly List<byte[]> commandQueue = new();
        private readonly StreamWatcher? watcher;
        private int vcAmount;
        public VConsole(WebsocketClient ws)
        {
            this.ws = ws;
            client = new TcpClient("127.0.0.1", Settings.Default.VconsolePort);
            stream = client.GetStream();
            WriteCommand("disconnect");
            watcher = new StreamWatcher(stream);
            watcher.MessageAvailable += MessageAvailable;
            PostConnect();
        }
        private void PostConnect()
        {
            if(watcher != null)
                watcher.SetWorking(true);
            killed = false;
            WriteWindowFocus(true);
            if (stream != null)
            {
                foreach (byte[] command in commandQueue)
                {
                    stream.Write(command);
                }
                commandQueue.Clear();
            }
        }

        private void MessageAvailable(object sender, MessageAvailableEventArgs e)
        {
            if(ws != null)
            {
                string command = e.MessageType.ToUpper();
                switch (command)
                {
                    case "PRNT":
                        byte[] message = e.Data.Skip(30).SkipLast(1).ToArray();
                        string newMessage = Encoding.ASCII.GetString(message).Replace("\n", "");
                        if(newMessage != "")
                        {
                            if (newMessage.Contains("Command buffer full"))
                            {
                                vcAmount += 1;
                                if (vcAmount >= 10)
                                {
                                    //Reconnect();
                                    vcAmount = 0;
                                }
                            }
                            else
                            {
                                Response input = new("print", newMessage);
                                ws.Send(JsonConvert.SerializeObject(input));
                            }
                        }
                        break;
                }
            }
        }

        public void Reconnect()
        {
            killed = true;
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
            if (watcher != null)
                watcher.SetWorking(false);
            client = new TcpClient("127.0.0.1", Settings.Default.VconsolePort);
            stream = client.GetStream();
            PostConnect();
        }

        public void Disconnect()
        {
            if(!killed)
            {
                killed = true;
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
                if (watcher != null)
                    watcher.SetWorking(false);
            }
        }

        public void WriteRaw(byte[] data)
        {
            if (stream != null)
                stream.Write(data);
        }

        public void WriteWindowFocus(bool focused = true)
        {
            if (stream != null)
            {
                byte[] vcmd = Encoding.ASCII.GetBytes("VFCS").Reverse().ToArray();
                byte protocol = Convert.ToByte(Settings.Default.VconsoleProtocol);
                byte dataLength = Convert.ToByte(13);
                List<byte> dataList = new()
                {
                    0,
                    protocol,
                    0,
                    0,
                    0,
                    dataLength,
                    0,
                    0,
                    (byte)(focused == true ? 1 : 0)
                };
                foreach (byte cmdByte in vcmd)
                {
                    dataList = dataList.Prepend(cmdByte).ToList();
                }
                byte[] completeData = dataList.ToArray();
                if (!killed)
                    stream.Write(completeData);
                else
                    commandQueue.Add(completeData);
            }
        }
        public void WriteCommand(string command, bool urgent = false)
        {
            if (stream != null)
            {
                byte[] vcmd = Encoding.ASCII.GetBytes("CMND").Reverse().ToArray();
                byte[] data = Encoding.ASCII.GetBytes(command);
                byte dataLength = Convert.ToByte(data.Length + 13);
                byte protocol = Convert.ToByte(Settings.Default.VconsoleProtocol);
                List<byte> dataList = new()
                {
                    0, protocol, 0, 0, 0, dataLength, 0, 0
                };
                foreach (byte cmdByte in vcmd)
                {
                    dataList = dataList.Prepend(cmdByte).ToList();
                }
                foreach (byte cmdByte in data)
                {
                    dataList = dataList.Append(cmdByte).ToList();
                }
                dataList.Add(0x00);
                byte[] completeData = dataList.ToArray();
                if (!killed)
                    stream.Write(completeData);
                else if (urgent)
                    commandQueue.Add(completeData);
            }
        }
    }
}
