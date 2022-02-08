using DiscordRPC;
using KiwisCoOpModCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordRpcPlugin
{
    public sealed class DiscordRpcGlobalData
    {
        public static readonly string Application = "939559645754843166";
        public DiscordRpcClient? discordRpcClient;
        private RichPresence? presence;
        private bool serverStarted = false;
        private Client? client;
        private string? gamemode;
        public static readonly DiscordRpcGlobalData instance = new DiscordRpcGlobalData();
        private DiscordRpcGlobalData()
        { }
        public DiscordRpcClient? GetDiscordRpcClient()
        {
            return discordRpcClient;
        }
        public void SetDiscordRpcClient(DiscordRpcClient discordRpcClient)
        {
            this.discordRpcClient = discordRpcClient;
        }
        public RichPresence? GetRichPresence()
        {
            return presence;
        }
        public void SetRichPresence(RichPresence presence)
        {
            this.presence = presence;
        }
        public bool GetServerStarted()
        {
            return presence;
        }
        public void SetServerStarted(bool serverStarted)
        {
            this.serverStarted = serverStarted;
        }
        public Client? GetClient()
        {
            return client;
        }
        public void SetClient(Client client)
        {
            this.client = client;
        }
        public string? GetGamemode()
        {
            return gamemode;
        }
        public void SetGamemode(string gamemode)
        {
            this.gamemode = gamemode;
        }
    }
}
