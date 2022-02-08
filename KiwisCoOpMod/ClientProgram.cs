using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using KiwisCoOpModCore;
using Websocket.Client;

namespace KiwisCoOpMod
{
    public class ClientProgram
    {
        private WebsocketClient? ws;
        private UserInterface ui;
        private Channel channel = new Channel("CL", Color.Green);
        private Channel chatChannel = new Channel("CHAT", Color.Black);
        private Channel statusChannel = new Channel("STATUS", Color.DeepPink);
        private Channel vConsoleChannel = new Channel("VC", Color.Maroon);
        private VConsole? vConsole;
        private List<Type> plugins = new List<Type>();
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
                ws = new WebsocketClient(new Uri("ws://" + Settings.Default.ClientIpAddress + ":" + Settings.Default.ClientPort));
                ws.ReconnectTimeout = TimeSpan.FromSeconds(30);
                ws.Start();
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
                                if (ws != null && Settings.Default.ClientMemo != "")
                                {
                                    Response input = new Response("chat", "(Memo) " + Settings.Default.ClientMemo);
                                    ws.Send(JsonConvert.SerializeObject(input));
                                }
                                if (vConsole != null && response.map != null)
                                {
                                    Thread thr = new Thread(new ThreadStart(() =>
                                    {
                                        Thread.Sleep(5000);
                                        vConsole.WriteCommand("addon_play " + response.map + ";addon_tools_map " + response.map);
                                    }));
                                    thr.Start();
                                }
                                if (response.map != null)
                                    map = response.map;
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
                            case "map":
                                if (ws != null && vConsole != null && response.data != null && Settings.Default.ClientPrintVconsole)
                                {
                                    if (map != response.data)
                                    {
                                        if (ui != null)
                                            ui.Invoke(() => ui.LogToOutput(vConsoleChannel, "Changing map to", response.data + "..."));
                                        vConsole.WriteCommand("addon_play " + response.data + "; addon_tools_map " + response.data);
                                        Response input = new Response("map", response.data);
                                        ws.Send(JsonConvert.SerializeObject(input));
                                        map = response.data;
                                    }
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
                Response input = new Response("client");
                input.clientUsername = Settings.Default.ClientUsername;
                input.clientAuthId = Settings.Default.ClientAuthId;
                input.password = Settings.Default.ClientPassword;
                ws.Send(JsonConvert.SerializeObject(input));
                ui.Invoke(() => ui.LogToOutput(channel, "Client attempted connection to IP " + Settings.Default.ClientIpAddress + ":" + Settings.Default.ServerPort));
                vConsole = new VConsole(ws, ui);
                PluginHandler.Handle(plugins, PluginHandleType.Client_PostStart, ui, ws);
            }
        }
        public void Close()
        {
            if (ws != null && ws.IsStarted)
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
            if (ws != null && ws.IsStarted)
            {
                ws.Send(new Response("chat", text).ToString());
            }
        }
    }
}
