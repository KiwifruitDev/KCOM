using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlyxGamemode
{
    public class Entity : Location
    {
        public string Name { get; set; }
        public Entity(string Name) : base()
        {
            this.Name = Name;
        }
        public Entity(string Name, float X, float Y, float Z) : base(X, Y, Z)
        {
            this.Name = Name;
        }
        public Entity(string Name, float X, float Y, float Z, float Pitch, float Yaw, float Roll) : base(X, Y, Z, Pitch, Yaw, Roll)
        {
            this.Name = Name;
        }
    }
}
