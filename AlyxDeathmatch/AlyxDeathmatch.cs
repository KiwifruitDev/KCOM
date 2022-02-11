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
using AlyxGamemode;
using Fleck;
using Newtonsoft.Json;

namespace AlyxDeathmatch
{
    public class AlyxDeathmatch : AlyxGamemode.AlyxGamemode
    {
        public AlyxDeathmatch() : base()
        {
            Author = "KiwifruitDev";
            Name = "Alyx Deathmatch";
            Description = "Play against other players in Half-Life: Alyx!";
        }
        public AlyxDeathmatch(GamemodeHandleType type, params object[]? vs) : base(type, vs)
        {
            try
            {
                switch (type)
                {
                    case GamemodeHandleType.PreResponse:
                        if (vs != null)
                        {
                            Response response = (Response)vs[0];
                            List<IndexedClient> connections = (List<IndexedClient>)vs[1];
                            IWebSocketConnection socket = (IWebSocketConnection)vs[2];
                            switch (response.type)
                            {
                                case "print":
                                    if (response.data != null)
                                    {
                                        if (response.data.Contains("KCOM"))
                                        {
                                            List<string> packetList = response.data.Split(" ").ToList();
                                            string packetType = packetList[0];
                                            packetList.Remove(packetType);
                                            DeathmatchPacket packet = new(packetType, packetList.ToArray());
                                            if (packet.IsValid())
                                            {
                                                Player? pair = AlyxGlobalData.instance.GetPlayer(socket.ConnectionInfo.Id);
                                                if (pair != null)
                                                {
                                                    Player player = pair;
                                                    switch (packet.type)
                                                    {
                                                        case DeathmatchPacketType.Initialization:
                                                            Response vconsoleInput2 = new("command", "impulse 101;hlvr_addresources 100 100 100 100;ent_remove kcom_timer; ent_create logic_timer {targetname kcom_timer_deathmatch refiretime 0.01}");
                                                            socket.Send(JsonConvert.SerializeObject(vconsoleInput2));
                                                            break;
                                                        case DeathmatchPacketType.InitializedEntities:
                                                            Thread thr = new(new ThreadStart(() =>
                                                            {
                                                                Thread.Sleep(2500);
                                                                Response vconsoleInput5 = new("command", "ent_fire kcom_timer_deathmatch addoutput OnTimer>kcom_script>RunScriptFile>kcom_deathmatch_interval>0>-1");
                                                                socket.Send(JsonConvert.SerializeObject(vconsoleInput5));
                                                            }));
                                                            thr.Start();
                                                            break;
                                                        case DeathmatchPacketType.ColliderDamage:
                                                            int index = int.Parse(packet.args[0].Replace("kcom_npc_", ""));
                                                            int damage = int.Parse(packet.args[1]);
                                                            Player? keyValuePair = AlyxGlobalData.instance.GetPlayers().Find(p => p.Index == index);
                                                            Player? me = AlyxGlobalData.instance.GetPlayer(socket.ConnectionInfo.Id);
                                                            if (keyValuePair != null && me != null)
                                                            {
                                                                Response vconsoleInput5 = new("command", "play sounds/player/damage/bullet_01.vsnd;kcom_hurt " + me.Index + " " + damage);
                                                                socket.Send(JsonConvert.SerializeObject(vconsoleInput5));
                                                            }
                                                            break;
                                                        case DeathmatchPacketType.PlayerDeath:
                                                            Player? deadPlayer = AlyxGlobalData.instance.GetPlayers().Find(p => p.Index == int.Parse(packet.args[0]));
                                                            if (deadPlayer != null)
                                                            {
                                                                Response vconsoleInput5 = new("command", "play sounds/damage/last_hit.vsnd");
                                                                deadPlayer.Client.Session.Send(JsonConvert.SerializeObject(vconsoleInput5));
                                                            }
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
            catch { }
        }
    }
}