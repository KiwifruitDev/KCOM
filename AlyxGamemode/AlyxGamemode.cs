using Fleck;
using KiwisCoOpModCore;
using Newtonsoft.Json;
using System.Globalization;

namespace AlyxGamemode
{
    public class AlyxGamemode : CoreGamemode
    {
        public AlyxGamemode() : base()
        {
            Author = "KiwifruitDev";
            Name = "Half-Life: Alyx";
            Description = "Play Half-Life: Alyx with up to 16 players!";
        }
        public AlyxGamemode(GamemodeHandleType type, params object[]? vs)
        {
            State = HandleState.Continue;
            try
            {
                switch (type)
                {
                    case GamemodeHandleType.ClientClose:
                        List<IndexedClient> closeConnections = (List<IndexedClient>)vs[0];
                        IWebSocketConnection closeSocket = (IWebSocketConnection)vs[1];
                        AlyxGlobalData.instance.RemovePlayer(closeSocket.ConnectionInfo.Id);
                        break;
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
                                        if (response.data.Contains("has joined the game"))
                                        {
                                            Response vconsoleInput = new Response("command", "sv_cheats 1;echo INIT KCOM");
                                            socket.Send(JsonConvert.SerializeObject(vconsoleInput));
                                        }
                                        else if (response.data.Contains("KCOM"))
                                        {
                                            List<string> packetList = response.data.Split(" ").ToList();
                                            string packetType = packetList[0];
                                            packetList.Remove(packetType);
                                            Packet packet = new Packet(packetType, packetList.ToArray());
                                            if (packet.IsValid())
                                            {
                                                Player? pair = AlyxGlobalData.instance.GetPlayer(socket.ConnectionInfo.Id);
                                                if (pair != null)
                                                {
                                                    Player player = pair;
                                                    switch (packet.type)
                                                    {
                                                        case PacketType.PlayerPosAng:
                                                            player.Origin = new Vector(
                                                                float.Parse(packet.args[0], CultureInfo.InvariantCulture.NumberFormat),
                                                                float.Parse(packet.args[1], CultureInfo.InvariantCulture.NumberFormat),
                                                                float.Parse(packet.args[2], CultureInfo.InvariantCulture.NumberFormat)
                                                            );
                                                            player.Angles = new Angle(
                                                                0,
                                                                float.Parse(packet.args[4], CultureInfo.InvariantCulture.NumberFormat),
                                                                0
                                                            );
                                                            Response npcmove = new Response("command", "kcom_setlocation kcom_npc_" + (player.Index + 1) + " " + player.Origin + " " + player.Angles);
                                                            foreach (IndexedClient broadcastClient2 in connections)
                                                            {
                                                                Player? keyValuePair = AlyxGlobalData.instance.GetPlayer(broadcastClient2.Session.ConnectionInfo.Id);
                                                                if (keyValuePair != null && broadcastClient2.Session.ConnectionInfo.Id != socket.ConnectionInfo.Id)
                                                                {
                                                                    broadcastClient2.Session.Send(JsonConvert.SerializeObject(npcmove));
                                                                }
                                                            }
                                                            break;
                                                        case PacketType.HeadPosAng:
                                                            Vector HeadOrigin = new Vector(
                                                                float.Parse(packet.args[0], CultureInfo.InvariantCulture.NumberFormat),
                                                                float.Parse(packet.args[1], CultureInfo.InvariantCulture.NumberFormat),
                                                                float.Parse(packet.args[2], CultureInfo.InvariantCulture.NumberFormat)
                                                            );
                                                            Angle HeadAngles = new Angle(
                                                                float.Parse(packet.args[3], CultureInfo.InvariantCulture.NumberFormat),
                                                                float.Parse(packet.args[4], CultureInfo.InvariantCulture.NumberFormat),
                                                                float.Parse(packet.args[5], CultureInfo.InvariantCulture.NumberFormat)
                                                            );
                                                            Response movement = new Response("command", "kcom_setlocation kcom_head_" + (player.Index + 1) + " " + HeadOrigin + " " + HeadAngles);
                                                            foreach (IndexedClient broadcastClient2 in connections)
                                                            {
                                                                Player? keyValuePair = AlyxGlobalData.instance.GetPlayer(broadcastClient2.Session.ConnectionInfo.Id);
                                                                if (keyValuePair != null && broadcastClient2.Session.ConnectionInfo.Id != socket.ConnectionInfo.Id)
                                                                {
                                                                    broadcastClient2.Session.Send(JsonConvert.SerializeObject(movement));
                                                                }
                                                            }
                                                            break;
                                                        case PacketType.HandPosAng:
                                                            Vector LeftHandOrigin = new Vector(
                                                                float.Parse(packet.args[0], CultureInfo.InvariantCulture.NumberFormat),
                                                                float.Parse(packet.args[1], CultureInfo.InvariantCulture.NumberFormat),
                                                                float.Parse(packet.args[2], CultureInfo.InvariantCulture.NumberFormat)
                                                            );
                                                            Angle LeftHandAngles = new Angle(
                                                                float.Parse(packet.args[3], CultureInfo.InvariantCulture.NumberFormat),
                                                                float.Parse(packet.args[4], CultureInfo.InvariantCulture.NumberFormat),
                                                                float.Parse(packet.args[5], CultureInfo.InvariantCulture.NumberFormat)
                                                            );
                                                            Vector RightHandOrigin = new Vector(
                                                                float.Parse(packet.args[6], CultureInfo.InvariantCulture.NumberFormat),
                                                                float.Parse(packet.args[7], CultureInfo.InvariantCulture.NumberFormat),
                                                                float.Parse(packet.args[8], CultureInfo.InvariantCulture.NumberFormat)
                                                            );
                                                            Angle RightHandAngles = new Angle(
                                                                float.Parse(packet.args[9], CultureInfo.InvariantCulture.NumberFormat),
                                                                float.Parse(packet.args[10], CultureInfo.InvariantCulture.NumberFormat),
                                                                float.Parse(packet.args[11], CultureInfo.InvariantCulture.NumberFormat)
                                                            );
                                                            Response movementLeftHand = new Response("command", "kcom_setlocation kcom_lefthand_" + (player.Index + 1) + " " + LeftHandOrigin + " " + LeftHandAngles);
                                                            Response movementRightHand = new Response("command", "kcom_setlocation kcom_righthand_" + (player.Index + 1) + " " + RightHandOrigin + " " + RightHandAngles);
                                                            foreach (IndexedClient broadcastClient2 in connections)
                                                            {
                                                                Player? keyValuePair = AlyxGlobalData.instance.GetPlayer(broadcastClient2.Session.ConnectionInfo.Id);
                                                                if (keyValuePair != null && broadcastClient2.Session.ConnectionInfo.Id != socket.ConnectionInfo.Id)
                                                                {
                                                                    broadcastClient2.Session.Send(JsonConvert.SerializeObject(movementLeftHand));
                                                                    broadcastClient2.Session.Send(JsonConvert.SerializeObject(movementRightHand));
                                                                }
                                                            }
                                                            break;
                                                        case PacketType.Initialization:
                                                            Response vconsoleInput2 = new Response("command", "ent_create logic_script {targetname kcom_script};ent_create logic_timer {targetname kcom_timer refiretime 0.01};echo IENT KCOM");
                                                            Response output3 = new Response("status", "Initializing co-op...");
                                                            socket.Send(JsonConvert.SerializeObject(output3));
                                                            socket.Send(JsonConvert.SerializeObject(vconsoleInput2));
                                                            break;
                                                        case PacketType.InitializedEntities:
                                                            Thread thr = new Thread(new ThreadStart(() =>
                                                            {
                                                                Thread.Sleep(2500);
                                                                Response vconsoleInput5 = new Response("command", "ent_fire kcom_timer addoutput OnTimer>kcom_script>RunScriptFile>kcom_interval>0>-1");
                                                                Response output4 = new Response("status", "Co-op initialized!");
                                                                socket.Send(JsonConvert.SerializeObject(output4));
                                                                socket.Send(JsonConvert.SerializeObject(vconsoleInput5));
                                                            }));
                                                            thr.Start();
                                                            break;
                                                        case PacketType.RightHandIndexes:
                                                        case PacketType.LeftHandIndexes:
                                                        case PacketType.HeadsetIndexes:
                                                        case PacketType.TextIndexes:
                                                        case PacketType.ColliderIndexes:
                                                        case PacketType.Prefix:
                                                        case PacketType.Alive:
                                                        case PacketType.ColliderDamage:
                                                        case PacketType.PhysicsObjectIndexStartPos:
                                                            break;
                                                        case PacketType.PhysicsObjectPosAng:
                                                            if (packet.args.Length >= 7)
                                                            {
                                                                Entity entity = new Entity(packet.args[0], float.Parse(packet.args[1]), float.Parse(packet.args[2]), float.Parse(packet.args[3]), float.Parse(packet.args[4]), float.Parse(packet.args[5]), float.Parse(packet.args[6]));
                                                                if (AlyxGlobalData.instance.AddManipulatedEntity(entity, socket.ConnectionInfo.Id, connections))
                                                                {
                                                                    Response output4 = new Response("command", "kcom_setlocation " + string.Join(" ", packet.args));
                                                                    foreach (IndexedClient broadcastClient2 in connections)
                                                                    {
                                                                        Player? keyValuePair = AlyxGlobalData.instance.GetPlayer(broadcastClient2.Session.ConnectionInfo.Id);
                                                                        if (keyValuePair != null && broadcastClient2.Session.ConnectionInfo.Id != socket.ConnectionInfo.Id)
                                                                        {
                                                                            broadcastClient2.Session.Send(JsonConvert.SerializeObject(output4));
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            break;
                                                        case PacketType.MapName:
                                                            string suffix = "";
                                                            if (packet.args[0] != player.Client.Map)
                                                            {
                                                                Response mapOutput = new Response("map", packet.args[0]);
                                                                foreach (IndexedClient broadcast in connections)
                                                                {
                                                                    Player? keyValuePair = AlyxGlobalData.instance.GetPlayer(broadcast.Session.ConnectionInfo.Id);
                                                                    if (keyValuePair != null && broadcast.Session.ConnectionInfo.Id != socket.ConnectionInfo.Id)
                                                                    {
                                                                        broadcast.Session.Send(JsonConvert.SerializeObject(mapOutput));
                                                                    }
                                                                }
                                                                suffix = " - Telling all clients to switch...";
                                                            }
                                                            Response output2 = new Response("status", "Detected map: " + packet.args[0] + suffix);
                                                            socket.Send(JsonConvert.SerializeObject(output2));
                                                            break;
                                                        case PacketType.ButtonIndexStartPos:
                                                        case PacketType.ButtonPressIndex:
                                                        case PacketType.DoorIndexStartPos:
                                                        case PacketType.BrokenPropIndex:
                                                        case PacketType.TriggerIndexStartPos:
                                                        case PacketType.TriggerActivateIndex:
                                                            break;
                                                        case PacketType.EntityRemoved:
                                                            Response remove = new Response("command", "ent_remove " + string.Join(" ", packet.args));
                                                            foreach (IndexedClient broadcast in connections)
                                                            {
                                                                Player? keyValuePair = AlyxGlobalData.instance.GetPlayer(broadcast.Session.ConnectionInfo.Id);
                                                                if (keyValuePair != null && broadcast.Session.ConnectionInfo.Id != socket.ConnectionInfo.Id)
                                                                {
                                                                    broadcast.Session.Send(JsonConvert.SerializeObject(remove));
                                                                }
                                                            }
                                                            break;
                                                        case PacketType.EntityFired:
                                                            Response fire = new Response("command", "kcom_fireoutput " + string.Join(" ", packet.args));
                                                            foreach (IndexedClient broadcast in connections)
                                                            {
                                                                Player? keyValuePair = AlyxGlobalData.instance.GetPlayer(broadcast.Session.ConnectionInfo.Id);
                                                                if (keyValuePair != null && broadcast.Session.ConnectionInfo.Id != socket.ConnectionInfo.Id)
                                                                {
                                                                    broadcast.Session.Send(JsonConvert.SerializeObject(fire));
                                                                }
                                                            }
                                                            break;
                                                    }
                                                }
                                                State = HandleState.Handled;
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case GamemodeHandleType.PostResponse:
                        if (vs != null)
                        {
                            Response response = (Response)vs[0];
                            List<IndexedClient> connections = (List<IndexedClient>)vs[1];
                            IWebSocketConnection socket = (IWebSocketConnection)vs[2];
                            switch (response.type)
                            {
                                case "client":
                                    if (AlyxGlobalData.instance.GetPlayer(socket.ConnectionInfo.Id) == null)
                                    {
                                        foreach (IndexedClient client in connections)
                                        {
                                            if (client.Session.ConnectionInfo.Id == socket.ConnectionInfo.Id)
                                            {
                                                Player? player = AlyxGlobalData.instance.AddPlayer(client);
                                                if (player != null)
                                                {
                                                    Response output2 = new Response("status", client.Username + " joined as index " + player.Index);
                                                    connections.ForEach(c => c.Session.Send(JsonConvert.SerializeObject(output2)));
                                                }
                                                else
                                                {
                                                    Response output2 = new Response("status", "The server is currently at maximum capacity, please try again later.");
                                                    socket.Send(JsonConvert.SerializeObject(output2));
                                                    socket.Close();
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                case "chat":
                                    if (AlyxGlobalData.instance.GetPlayer(socket.ConnectionInfo.Id) != null)
                                    {
                                        if (response.data != null)
                                        {
                                            if (!response.data.StartsWith("/"))
                                            {
                                                Response output = new Response("command", "play sounds/ui/hint.vsnd");
                                                connections.ForEach(c => c.Session.Send(JsonConvert.SerializeObject(output)));
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
            catch
            {}
        }
    }
}