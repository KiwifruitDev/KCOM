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
        private Client? client;
        private string? gamemode;
        public static readonly DiscordRpcGlobalData instance = new();
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
