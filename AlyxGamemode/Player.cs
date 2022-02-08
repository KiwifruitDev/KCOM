using KiwisCoOpModCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlyxGamemode
{
    public class Player : Location
    {
        public int Index = 0;
        public IndexedClient Client;
        public Player(int index, IndexedClient client)
        {
            this.Index = index;
            this.Client = client;
        }
    }
}
