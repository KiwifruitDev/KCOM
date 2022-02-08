using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KiwisCoOpModCore;
using Fleck;

namespace KiwisCoOpMod
{
    public class ServerProgram
    {
        public static readonly ServerProgram instance = new ServerProgram();
        public string map = "";
        public WebSocketServer? wss;
        public List<IndexedClient> connections = new List<IndexedClient> { };
        public Type? gamemodeType;
        public List<Type> plugins = new List<Type>();
        public void Start(Type type, List<Type> plugins)
        {
            if (wss == null)
            {
                PluginHandler.Handle(plugins, PluginHandleType.Server_PreGamemode_PreStart);
                if (GamemodeHandler.Handle(type, GamemodeHandleType.PreStart) == HandleState.Continue)
                {
                    map = Settings.Default.ServerMap;
                    this.plugins = plugins;
                    PluginHandler.Handle(plugins, PluginHandleType.Server_PostGamemode_PreStart, type, plugins);
                    gamemodeType = type;
                    wss = new WebSocketServer("ws://" + Settings.Default.ServerIpAddress + ":" + Settings.Default.ServerPort);
                    wss.Start(socket => {
                        socket.OnOpen = () => OnClose(socket);
                        socket.OnClose = () => OnClose(socket);
                        socket.OnMessage = message => OnMessage(message, socket);
                    });
                    if (gamemodeType != null)
                    {
                        PluginHandler.Handle(plugins, PluginHandleType.Server_PreGamemode_PostStart, gamemodeType, plugins);
                        GamemodeHandler.Handle(gamemodeType, GamemodeHandleType.PostStart, gamemodeType, plugins);
                        PluginHandler.Handle(plugins, PluginHandleType.Server_PostGamemode_PostStart, gamemodeType, plugins);
                    }
                }
            }
        }
        public void Close()
        {
            if (wss != null && gamemodeType != null)
            {
                PluginHandler.Handle(plugins, PluginHandleType.Server_PreGamemode_PreClose);
                if (GamemodeHandler.Handle(gamemodeType, GamemodeHandleType.PreClose, wss) == HandleState.Continue)
                {
                    wss.Dispose();
                    wss = null;
                    PluginHandler.Handle(plugins, PluginHandleType.Server_PreGamemode_PostClose, gamemodeType, plugins);
                    GamemodeHandler.Handle(gamemodeType, GamemodeHandleType.PostClose, gamemodeType, plugins);
                    PluginHandler.Handle(plugins, PluginHandleType.Server_PostGamemode_PostClose, gamemodeType, plugins);
                }
            }
        }
        public void ChangeMap(string map)
        {
            this.map = map;
            Response output2 = new Response("map", map);
            foreach (IndexedClient con in connections)
            {
                con.Session.Send(JsonConvert.SerializeObject(output2));
            }
        }
        public void OnOpen(IWebSocketConnection socket)
        {
            if (gamemodeType != null)
            {
                PluginHandler.Handle(plugins, PluginHandleType.Server_PreGamemode_ClientOpen, connections, socket);
                GamemodeHandler.Handle(gamemodeType, GamemodeHandleType.ClientOpen, connections, socket);
                PluginHandler.Handle(plugins, PluginHandleType.Server_PostGamemode_ClientOpen, connections, socket);
            }
        }
        public void OnClose(IWebSocketConnection socket)
        {
            if (gamemodeType != null)
            {
                PluginHandler.Handle(plugins, PluginHandleType.Server_PreGamemode_ClientClose, connections, socket);
                GamemodeHandler.Handle(gamemodeType, GamemodeHandleType.ClientClose, connections, socket);
                PluginHandler.Handle(plugins, PluginHandleType.Server_PostGamemode_ClientClose, connections, socket);
            }
        }
        public void OnMessage(string message, IWebSocketConnection socket)
        {
            Response? response = JsonConvert.DeserializeObject<Response>(message);
            if (gamemodeType != null && response != null && response.type != null)
            {
                PluginHandler.Handle(plugins, PluginHandleType.Server_PreGamemode_PreResponse, response, connections, socket);
                if (GamemodeHandler.Handle(gamemodeType, GamemodeHandleType.PreResponse, response, connections, socket) == HandleState.Continue)
                {
                    PluginHandler.Handle(plugins, PluginHandleType.Server_PostGamemode_PreResponse, response, connections, socket);
                    switch (response.type.ToLower())
                    {
                        case "client":
                            if (response.clientUsername != null && response.clientAuthId != null && response.password != null)
                            {
                                if (Settings.Default.ServerPassword != "" && response.password != Settings.Default.ServerPassword)
                                {
                                    Response output2 = new Response("status", "Invalid server password! Closing connection...");
                                    socket.Send(JsonConvert.SerializeObject(output2));
                                }
                                else
                                {
                                    foreach (IndexedClient indexed1 in connections)
                                    {
                                        if (indexed1.Username == response.clientUsername && indexed1.AuthId != response.clientAuthId)
                                        {
                                            Response output2 = new Response("status", "A client is already connected with this username: " + response.clientUsername);
                                            socket.Send(JsonConvert.SerializeObject(output2));
                                            socket.Close();
                                            break;
                                        }
                                        else if (indexed1.Username == response.clientUsername)
                                        {
                                            Response output2 = new Response("status", "Client reconnected elsewhere, closing connection...");
                                            socket.Send(JsonConvert.SerializeObject(output2));
                                            socket.Close();
                                            connections.Remove(indexed1);
                                            break;
                                        }
                                    }
                                    if(response.clientUsername != null && response.clientAuthId != null)
                                    {
                                        Response output2 = new Response("authenticated");
                                        output2.clientUsername = response.clientUsername;
                                        output2.clientAuthId = response.clientAuthId;
                                        output2.map = map;
                                        connections.Add(new IndexedClient(socket, response.clientUsername, response.clientAuthId, map));
                                        socket.Send(JsonConvert.SerializeObject(output2));
                                    }
                                }
                            }
                            else
                            {
                                Response output2 = new Response("status", "Invalid username and/or AuthID! Closing connection...");
                                socket.Send(JsonConvert.SerializeObject(output2));
                                socket.Close();
                            }
                            break;
                        case "chat":
                            IndexedClient? indexedClient = connections.Find(c => c.Session.ConnectionInfo.Id == socket.ConnectionInfo.Id);
                            if (indexedClient != null)
                            {
                                if (response.data != null)
                                {
                                    if (response.data.StartsWith("/"))
                                    {
                                        string command = response.data.Split(" ").ToArray().First().Replace("/", "").ToLower();
                                        string[] args = response.data.Split(" ").Skip(1).ToArray();
                                        Response output3 = new Response("status", response.data);
                                        output3.clientUsername = indexedClient.Username;
                                        output3.clientAuthId = indexedClient.AuthId;
                                        output3.data = "Unknown command '" + command + "'";
                                        switch (command)
                                        {
                                            case "vc":
                                                if (!Settings.Default.ServerDisableUserVconsoleInput)
                                                {
                                                    output3 = new Response("command", response.data);
                                                    output3.data = string.Join(" ", args);
                                                    output3.urgent = false;
                                                }
                                                else
                                                {
                                                    output3 = new Response("status", response.data);
                                                    output3.data = "VConsole input is disabled on this server.";
                                                }
                                                break;
                                        }
                                        socket.Send(JsonConvert.SerializeObject(output3));
                                    }
                                    else
                                    {
                                        Response output4 = new Response("chat", response.data);
                                        output4.remoteClientUsername = indexedClient.Username;
                                        connections.ForEach(c => c.Session.Send(JsonConvert.SerializeObject(output4)));
                                    }
                                }
                            }
                            break;
                        case "print":
                            IndexedClient? indexed2 = connections.Find(c => c.Session.ConnectionInfo.Id == socket.ConnectionInfo.Id);
                            if (indexed2 != null)
                            {
                                if (response.data != null)
                                {
                                    if (indexed2 != null && indexed2 != null)
                                    {
                                        Response output5 = new Response("vconsole", response.data);
                                        output5.clientUsername = indexed2.Username;
                                        output5.clientAuthId = indexed2.AuthId;
                                        socket.Send(JsonConvert.SerializeObject(output5));
                                    }
                                }
                            }
                            break;
                        default:
                            Response output = new Response("status", "The server did not recognize a command: " + response.type.ToLower());
                            socket.Send(JsonConvert.SerializeObject(output));
                            IndexedClient? indexed = connections.Find(c => c.Session.ConnectionInfo.Id == socket.ConnectionInfo.Id);
                            if (indexed != null) connections.Remove(indexed);
                            socket.Close();
                            break;
                    }
                    PluginHandler.Handle(plugins, PluginHandleType.Server_PreGamemode_PostResponse, response, connections, socket);
                    GamemodeHandler.Handle(gamemodeType, GamemodeHandleType.PostResponse, response, connections, socket);
                    PluginHandler.Handle(plugins, PluginHandleType.Server_PostGamemode_PostResponse, response, connections, socket);
                }
            }
        }
    }
}
