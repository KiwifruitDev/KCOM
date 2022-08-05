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
using Fleck;

namespace LuaInterpreterPlugin
{
    public class LuaInterpreterPlugin : BasePlugin
    {
        public LuaInterpreterPlugin() : base()
        {
            Author = "KiwifruitDev";
            Name = "Lua Interpreter";
            Description = "Utilize Lua scripting within KCOM.";
        }
        public LuaInterpreterPlugin(PluginHandleType type, params object[]? vs)
        {
            string pluginHandleType = "";
            List<object> pluginParams = new();
            switch (type)
            {
                case PluginHandleType.UserInterface_Initialized:
                    pluginHandleType = "UserInterfaceInitialized";
                    // Load Lua files
                    LuaEnvironment.instance.LoadFiles();
                    break;
                case PluginHandleType.UserInterface_Exit:
                    pluginHandleType = "UserInterfaceExit";
                    break;
                case PluginHandleType.UserInterface_PreStart:
                    pluginHandleType = "UserInterfacePreStart";
                    break;
                case PluginHandleType.UserInterface_PostStart:
                    pluginHandleType = "UserInterfacePostStart";
                    break;
                case PluginHandleType.UserInterface_PreClose:
                    pluginHandleType = "UserInterfacePreStart";
                    break;
                case PluginHandleType.UserInterface_PostClose:
                    pluginHandleType = "UserInterfacePostStart";
                    break;
                case PluginHandleType.Client_PreStart:
                    pluginHandleType = "ClientPreStart";
                    break;
                case PluginHandleType.Client_PostStart:
                    pluginHandleType = "ClientPostStart";
                    break;
                case PluginHandleType.Client_PreClose:
                    pluginHandleType = "ClientPreClose";
                    break;
                case PluginHandleType.Client_PostClose:
                    pluginHandleType = "ClientPostClose";
                    break;
                case PluginHandleType.Client_PreResponse:
                    pluginHandleType = "ClientPreResponse";
                    if (vs != null)
                        pluginParams.Add(JsonConvert.SerializeObject((Response)(vs[0])));
                    break;
                case PluginHandleType.Client_PostResponse:
                    pluginHandleType = "ClientPostResponse";
                    if (vs != null)
                        pluginParams.Add(JsonConvert.SerializeObject((Response)(vs[0])));
                    break;
                case PluginHandleType.Server_PreGamemode_PreStart:
                    pluginHandleType = "ServerPreGamemodePreStart";
                    break;
                case PluginHandleType.Server_PostGamemode_PreStart:
                    pluginHandleType = "ServerPostGamemodePreStart";
                    if (vs != null)
                    {
                        pluginParams.Add(((Type)(vs[0])).Name);
                        List<string> pluginNames = new();
                        foreach(Type plugin in vs[1] as List<Type>)
                        {
                            pluginNames.Add(plugin.Name);
                        }
                        pluginParams.Add(pluginNames);
                    }
                    break;
                case PluginHandleType.Server_PreGamemode_PostStart:
                    pluginHandleType = "ServerPreGamemodePostStart";
                    if (vs != null)
                    {
                        pluginParams.Add(((Type)(vs[0])).Name);
                        List<string> pluginNames = new();
                        foreach(Type plugin in vs[1] as List<Type>)
                        {
                            pluginNames.Add(plugin.Name);
                        }
                        pluginParams.Add(pluginNames);
                    }
                    break;
                case PluginHandleType.Server_PostGamemode_PostStart:
                    pluginHandleType = "ServerPostGamemodePostStart";
                    if (vs != null)
                    {
                        pluginParams.Add(((Type)(vs[0])).Name);
                        List<string> pluginNames = new();
                        foreach(Type plugin in vs[1] as List<Type>)
                        {
                            pluginNames.Add(plugin.Name);
                        }
                        pluginParams.Add(pluginNames);
                    }
                    break;
                case PluginHandleType.Server_PreGamemode_PreClose:
                    pluginHandleType = "ServerPreGamemodePreClose";
                    if (vs != null)
                    {
                        pluginParams.Add(((Type)(vs[0])).Name);
                        List<string> pluginNames = new();
                        foreach(Type plugin in vs[1] as List<Type>)
                        {
                            pluginNames.Add(plugin.Name);
                        }
                        pluginParams.Add(pluginNames);
                    }
                    break;
                case PluginHandleType.Server_PostGamemode_PreClose:
                    pluginHandleType = "ServerPostGamemodePreClose";
                    if (vs != null)
                    {
                        pluginParams.Add(((Type)(vs[0])).Name);
                        List<string> pluginNames = new();
                        foreach(Type plugin in vs[1] as List<Type>)
                        {
                            pluginNames.Add(plugin.Name);
                        }
                        pluginParams.Add(pluginNames);
                    }
                    break;
                case PluginHandleType.Server_PreGamemode_PostClose:
                    pluginHandleType = "ServerPreGamemodePostClose";
                    if (vs != null)
                    {
                        pluginParams.Add(((Type)(vs[0])).Name);
                        List<string> pluginNames = new();
                        foreach(Type plugin in vs[1] as List<Type>)
                        {
                            pluginNames.Add(plugin.Name);
                        }
                        pluginParams.Add(pluginNames);
                    }
                    break;
                case PluginHandleType.Server_PostGamemode_PostClose:
                    pluginHandleType = "ServerPostGamemodePostClose";
                    if (vs != null)
                    {
                        pluginParams.Add(((Type)(vs[0])).Name);
                        List<string> pluginNames = new();
                        foreach(Type plugin in vs[1] as List<Type>)
                        {
                            pluginNames.Add(plugin.Name);
                        }
                        pluginParams.Add(pluginNames);
                    }
                    break;
                case PluginHandleType.Server_PreGamemode_PreResponse:
                    pluginHandleType = "ServerPreGamemodePreResponse";
                    if (vs != null)
                    {
                        pluginParams.Add(JsonConvert.SerializeObject((Response)(vs[0])));
                        List<string> indexedClients = new();
                        foreach(IndexedClient client in vs[1] as List<IndexedClient>)
                        {
                            indexedClients.Add(client.Username);
                        }
                        pluginParams.Add(indexedClients);
                        pluginParams.Add(((IWebSocketConnection)(vs[2])).ConnectionInfo.Id.ToString());
                    }
                    break;
                case PluginHandleType.Server_PostGamemode_PreResponse:
                    pluginHandleType = "ServerPostGamemodePreResponse";
                    if (vs != null)
                    {
                        pluginParams.Add(JsonConvert.SerializeObject((Response)(vs[0])));
                        List<string> indexedClients = new();
                        foreach(IndexedClient client in vs[1] as List<IndexedClient>)
                        {
                            indexedClients.Add(client.Username);
                        }
                        pluginParams.Add(indexedClients);
                        pluginParams.Add(((IWebSocketConnection)(vs[2])).ConnectionInfo.Id.ToString());
                    }
                    break;
                case PluginHandleType.Server_PreGamemode_PostResponse:
                    pluginHandleType = "ServerPreGamemodePostResponse";
                    if (vs != null)
                    {
                        pluginParams.Add(JsonConvert.SerializeObject((Response)(vs[0])));
                        List<string> indexedClients = new();
                        foreach(IndexedClient client in vs[1] as List<IndexedClient>)
                        {
                            indexedClients.Add(client.Username);
                        }
                        pluginParams.Add(indexedClients);
                        pluginParams.Add(((IWebSocketConnection)(vs[2])).ConnectionInfo.Id.ToString());
                    }
                    break;
                case PluginHandleType.Server_PostGamemode_PostResponse:
                    pluginHandleType = "ServerPostGamemodePostResponse";
                    if (vs != null)
                    {
                        pluginParams.Add(JsonConvert.SerializeObject((Response)(vs[0])));
                        List<string> indexedClients = new();
                        foreach(IndexedClient client in vs[1] as List<IndexedClient>)
                        {
                            indexedClients.Add(client.Username);
                        }
                        pluginParams.Add(indexedClients);
                        pluginParams.Add(((IWebSocketConnection)(vs[2])).ConnectionInfo.Id.ToString());
                    }
                    break;
                case PluginHandleType.Server_PreGamemode_ClientOpen:
                    pluginHandleType = "ServerPreGamemodeClientOpen";
                    break;
                case PluginHandleType.Server_PostGamemode_ClientOpen:
                    pluginHandleType = "ServerPostGamemodeClientOpen";
                    break;
                case PluginHandleType.Server_PreGamemode_ClientClose:
                    pluginHandleType = "ServerPreGamemodeClientClose";
                    break;
                case PluginHandleType.Server_PostGamemode_ClientClose:
                    pluginHandleType = "ServerPostGamemodeClientClose";
                    break;
                case PluginHandleType.Server_PreGamemode_Command:
                    pluginHandleType = "ServerPreGamemodeCommand";
                    break;
                case PluginHandleType.Server_PostGamemode_Command:
                    pluginHandleType = "ServerPostGamemodeCommand";
                    break;
            }
            // Call Lua function
            LuaEnvironment.instance.Initialize(pluginHandleType, vs); //JsonConvert.SerializeObject(pluginParams));
        }
    }
}