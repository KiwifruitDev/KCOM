using KiwisCoOpModCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlyxGamemode
{
    public sealed class AlyxGlobalData
    {
        public static readonly AlyxGlobalData instance = new AlyxGlobalData();
        private List<Player> players = new List<Player>();
        private Location startLocation = new Location();
        private List<Moveable> manipulatedEntities = new List<Moveable>();
        public bool AddManipulatedEntity(Entity MovedObject, Guid ClientID, List<IndexedClient> Connections)
        {
            Moveable? oldMoveable = manipulatedEntities.Find(m => m.GetMovedObject().Name == MovedObject.Name);
            if (oldMoveable == null)
            {
                manipulatedEntities.Add(new Moveable(MovedObject, ClientID, Connections));
                return true;
            }
            else if(oldMoveable.GetClientID() == ClientID || oldMoveable.GetTimedOut())
            {
                oldMoveable.Update(MovedObject, ClientID, Connections);
                return true;
            }
            return false;
        }
        public List<Player> GetPlayers()
        {
            return players;
        }
        public Player? GetPlayer(Guid id)
        {
            Player? oldPlayer = players.Find(p => {
                if (p != null)
                    return p.Client.Session.ConnectionInfo.Id == id;
                else
                    return false;
            });
            if (oldPlayer != null)
                return oldPlayer;
            return null;
        }
        public Player? AddPlayer(IndexedClient client)
        {
            if(players.Count < 16)
            {
                Player player = new Player(players.Count, client);
                players.Add(player);
                return player;
            }
            return null;
        }
        public bool RemovePlayer(Guid id)
        {
            Player? oldPlayer = players.Find(p => p.Client.Session.ConnectionInfo.Id == id);
            if (oldPlayer != null)
                return players.Remove(oldPlayer);
            return false;
        }
        public bool RemovePlayer(int index)
        {
            Player? oldPlayer = players.Find(p => p.Index == index);
            if (oldPlayer != null)
                players.Remove(oldPlayer);
            return false;
        }
    }
}
