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
using DiscordRPC;

namespace DiscordRpcPlugin
{
    public class DiscordRpcPlugin : BasePlugin
    {
        public DiscordRpcPlugin() : base()
        {
            Author = "KiwifruitDev";
            Name = "Discord Rich Presence";
            Description = "Share your session status on Discord!";
            Default = true;
        }
        public DiscordRpcPlugin(PluginHandleType type, params object[]? vs)
        {
            switch (type)
            {
                case PluginHandleType.UserInterface_Initialized:

                    DiscordRpcClient discord = new(DiscordRpcGlobalData.Application);
                    DiscordRpcGlobalData.instance.SetDiscordRpcClient(discord);
                    discord.Initialize();
                    RichPresence richPresence = new();
                    DiscordRpcGlobalData.instance.SetRichPresence(richPresence);
                    richPresence.Details = "Configuring options";
                    richPresence.State = "Inactive";
                    richPresence.Assets = new Assets()
                    {
                        LargeImageKey = "kcom",
                        LargeImageText = "Kiwi's Co-Op Mod",
                    };
                    discord.SetPresence(richPresence);
                    break;
                case PluginHandleType.UserInterface_Exit:
                    DiscordRpcClient? discord2 = DiscordRpcGlobalData.instance.GetDiscordRpcClient();
                    if (discord2 != null)
                        discord2.Dispose();
                    break;
                case PluginHandleType.Client_PreResponse:
                    DiscordRpcClient? discord3 = DiscordRpcGlobalData.instance.GetDiscordRpcClient();
                    RichPresence? richPresence2 = DiscordRpcGlobalData.instance.GetRichPresence();
                    bool serverStarted = DiscordRpcGlobalData.instance.GetServerStarted();
                    if (discord3 != null && richPresence2 != null)
                    {
                        if (vs != null)
                        {
                            Response response = (Response)vs[0];
                            switch (response.type)
                            {
                                case "authenticated":
                                    if (serverStarted)
                                    {
                                        richPresence2.State = "Running listen server";
                                    }
                                    else
                                    {
                                        richPresence2.State = "Running client";
                                        richPresence2.Details = "Connected to a server";
                                    }
                                    if (response.clientUsername != null && response.clientAuthId != null)
                                        DiscordRpcGlobalData.instance.SetClient(new Client(response.clientUsername, response.clientAuthId));
                                    discord3.SetPresence(richPresence2);
                                    break;
                            }
                        }
                    }
                    break;
                case PluginHandleType.Server_PostGamemode_PreStart:
                    DiscordRpcClient? discord4 = DiscordRpcGlobalData.instance.GetDiscordRpcClient();
                    RichPresence? richPresence3 = DiscordRpcGlobalData.instance.GetRichPresence();
                    if (discord4 != null && richPresence3 != null)
                    {
                        if (vs != null)
                        {
                            Type gamemodeType = (Type)vs[0];
                            List<Type> pluginTypes = (List<Type>)vs[1];
                            IGamemode? gamemode = GamemodeHandler.Instance(gamemodeType);
                            if (gamemode != null && gamemode.Name != null)
                            {
                                richPresence3.Details = "0 players on " + gamemode.Name;
                                DiscordRpcGlobalData.instance.SetGamemode(gamemode.Name);
                            }
                            richPresence3.State = "Running server with " + pluginTypes.Count + " plugin" + (pluginTypes.Count == 1 ? "" : "s");
                            richPresence3.Assets = new Assets()
                            {
                                LargeImageKey = "kcom",
                                LargeImageText = "Kiwi's Co-Op Mod",
                            };
                            discord4.SetPresence(richPresence3);
                        }
                    }
                    break;
                case PluginHandleType.Server_PostGamemode_PostResponse:
                    DiscordRpcClient? discord5 = DiscordRpcGlobalData.instance.GetDiscordRpcClient();
                    RichPresence? richPresence4 = DiscordRpcGlobalData.instance.GetRichPresence();
                    if (discord5 != null && richPresence4 != null)
                    {
                        if (vs != null)
                        {
                            List<IndexedClient> connections = (List<IndexedClient>)vs[1];
                            richPresence4.Details = connections.Count + "/16 players on " + DiscordRpcGlobalData.instance.GetGamemode();
                            discord5.SetPresence(richPresence4);
                        }
                    }
                    break;
                case PluginHandleType.Server_PostGamemode_PostClose:
                case PluginHandleType.Client_PostClose:
                    DiscordRpcClient? discord6 = DiscordRpcGlobalData.instance.GetDiscordRpcClient();
                    RichPresence? richPresence5 = DiscordRpcGlobalData.instance.GetRichPresence();
                    if (discord6 != null && richPresence5 != null)
                    {
                        richPresence5.Details = "Configuring options";
                        richPresence5.State = "Inactive";
                        richPresence5.Assets = new Assets()
                        {
                            LargeImageKey = "kcom",
                            LargeImageText = "Kiwi's Co-Op Mod",
                        };
                        discord6.SetPresence(richPresence5);
                    }
                    break;

            }
        }
    }
}