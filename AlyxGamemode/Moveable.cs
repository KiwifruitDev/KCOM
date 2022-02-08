using KiwisCoOpModCore;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Text;
using Newtonsoft.Json;

namespace AlyxGamemode
{
    public class Moveable
    {
        private List<IndexedClient> Connections;
        private Guid ClientID;
        private Entity MovedObject;
        private System.Timers.Timer Tracker;
        private bool TimedOut;
        public Moveable(Entity MovedObject, Guid ClientID, List<IndexedClient> Connections)
        {
            this.MovedObject = MovedObject;
            this.ClientID = ClientID;
            this.Connections = Connections;
            System.Timers.Timer timer = new System.Timers.Timer(5000);
            timer.AutoReset = false;
            timer.Elapsed += Track;
            Tracker = timer;
            Tracker.Start();
        }
        public void Update(Entity MovedObject, Guid ClientID, List<IndexedClient> Connections)
        {
            this.MovedObject = MovedObject;
            this.ClientID = ClientID;
            this.Connections = Connections;
            TimedOut = false;
            Tracker.Stop();
            Tracker.Start();
        }
        private void Track(object? sender, System.Timers.ElapsedEventArgs e)
        {
            IndexedClient? client = Connections.Find(c => c.Session.ConnectionInfo.Id != ClientID);
            Response response = new Response("command", "kcom_grace " + MovedObject.Name);
            if (client != null)
            {
                client.Session.Send(JsonConvert.SerializeObject(response));
            }
            TimedOut = true;
        }
        public Entity GetMovedObject()
        {
            return MovedObject;
        }
        public Guid GetClientID()
        {
            return ClientID;
        }
        public bool GetTimedOut()
        {
            return TimedOut;
        }
    }
}
