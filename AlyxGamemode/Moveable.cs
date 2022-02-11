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
        private readonly System.Timers.Timer Tracker;
        private bool TimedOut;
        public Moveable(Entity MovedObject, Guid ClientID, List<IndexedClient> Connections)
        {
            this.MovedObject = MovedObject;
            this.ClientID = ClientID;
            this.Connections = Connections;
            System.Timers.Timer timer = new(5000);
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
            Response response = new("command", "kcom_grace " + MovedObject.Name);
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
