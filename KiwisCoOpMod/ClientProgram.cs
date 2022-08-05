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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using KiwisCoOpModCore;
using Websocket.Client;
using System.Diagnostics;

namespace KiwisCoOpMod
{
    public class ClientProgram
    {
        private WebsocketClient? ws;
        private readonly UserInterface ui;
        private readonly Channel channel = new("CL", "Client", Color.Green);
        private readonly Channel chatChannel = new("CHAT", "Chat", Color.Black);
        private readonly Channel statusChannel = new("STATUS", "Status", Color.DeepPink);
        private readonly Channel vConsoleChannel = new("VC", "VConsole", Color.Maroon);
        private readonly VConsole? vConsole = new();
        private List<Type> plugins = new();
        public int version = 0; // Update version if netcode changes.
        public string map = "";
        public ClientProgram(UserInterface ui)
        {
            this.ui = ui;
        }
        public void Start(List<Type> pluginTypes)
        {
            if (ws == null)
            {
                PluginHandler.Handle(pluginTypes, PluginHandleType.Client_PreStart, ui);
                plugins = pluginTypes;
                ws = new WebsocketClient(new Uri("ws://" + Settings.Default.ClientIpAddress + ":" + Settings.Default.ClientPort))
                {
                    ReconnectTimeout = TimeSpan.FromSeconds(60),
                    IsReconnectionEnabled = false
                };
                ws.Start();
                ws.DisconnectionHappened.Subscribe(info =>
                {
                    ui.Invoke(() => ui.LogToOutput(channel, "Disconnected: " + info.Type.ToString()));
                });
                ws.MessageReceived.Subscribe(msg =>
                {
                    Response? response = JsonConvert.DeserializeObject<Response>(msg.Text);
                    if (response != null && response.type != null)
                    {
                        PluginHandler.Handle(plugins, PluginHandleType.Client_PreResponse, response);
                        switch (response.type)
                        {
                            case "authenticated":
                                if (response.version > Response.internalVersion)
                                {
                                    ui.Invoke(() => ui.LogToOutput(channel, "Client is running an older version! Please update your client."));
                                }
                                else if (response.version > Response.internalVersion)
                                {
                                    ui.Invoke(() => ui.LogToOutput(channel, "Server is running an older version! Please ask the owner to update their server."));
                                }
                                if (vConsole != null && response.map != null)
                                {
                                    if (map != response.map)
                                    {
                                        ui.Invoke(() => ui.LogToOutput(channel, "Changing map to " + response.map));
                                        vConsole.WriteCommand("addon_play " + response.map + ";addon_tools_map " + response.map);
                                        map = response.map;
                                    }
                                }
                                break;
                            case "chat":
                                if (response.data != null && response.remoteClientUsername != null)
                                {
                                    ui.Invoke(() => ui.LogToOutput(chatChannel, response.remoteClientUsername + ": " + response.data));
                                }
                                break;
                            case "command":
                                if (vConsole != null && response.data != null)
                                {
                                    vConsole.WriteCommand(response.data, response.urgent);
                                }
                                break;
                            case "status":
                                if (vConsole != null && response.data != null)
                                {
                                    if (ui != null)
                                        ui.Invoke(() => ui.LogToOutput(statusChannel, response.data));
                                }
                                break;
                            case "vconsole":
                                if (vConsole != null && response.data != null && Settings.Default.ClientPrintVconsole)
                                {
                                    if (ui != null)
                                        ui.Invoke(() => ui.LogToOutput(vConsoleChannel, response.data));
                                }
                                break;
                            default:
                                ui.Invoke(() => ui.LogToOutput(channel, "UNIMPLEMENTED TYPE: " + response.type + "!"));
                                break;
                        }
                        PluginHandler.Handle(plugins, PluginHandleType.Client_PostResponse, response);
                    }
                    else
                    {
                        ui.Invoke(() => ui.LogToOutput(channel, "SERVER SENT INVALID DATA!"));
                    }
                });
                ws.ReconnectionHappened.Subscribe(recinfo =>
                {
                    Response input = new("client")
                    {
                        clientUsername = Settings.Default.ClientUsername,
                        password = Settings.Default.ClientPassword,
                        timestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds
                };
                    ws.Send(JsonConvert.SerializeObject(input));
                    ui.Invoke(() => ui.LogToOutput(channel, "Client attempted connection to IP " + Settings.Default.ClientIpAddress + ":" + Settings.Default.ServerPort));
                });
                if (vConsole != null)
                {
                    vConsole.Connect(ws);
                }
                PluginHandler.Handle(plugins, PluginHandleType.Client_PostStart, ui, ws);
            }
        }
        public void Close()
        {
            if (ws != null)
            {
                PluginHandler.Handle(plugins, PluginHandleType.Client_PreClose, ui, ws);
                ws.Stop(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "Closed by KCOM.");
                ws.Dispose();
                ws = null;
                ui.Invoke(() => ui.LogToOutput(channel, "Client closed"));
            }
            if (vConsole != null)
            {
                vConsole.Disconnect();
            }
        }
        public void Chat(string text)
        {
            if (ws != null && ws.IsStarted && text.Length > 0)
            {
                Response chatResponse = new("chat", text);
                chatResponse.timestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalMilliseconds;
                ws.Send(chatResponse.ToString());
            }
        }
    }
}
