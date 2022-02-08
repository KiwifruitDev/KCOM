using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlyxGamemode
{
    public class Location
    {
        public Vector Origin = new Vector();
        public Angle Angles = new Angle();
        public Location()
        { }
        public Location(float X, float Y, float Z)
        {
            Origin = new Vector(X, Y, Z);
        }
        public Location(float X, float Y, float Z, float Pitch, float Yaw, float Roll)
        {
            Origin = new Vector(X, Y, Z);
            Angles = new Angle(Pitch, Yaw, Roll);
        }
    }
}
