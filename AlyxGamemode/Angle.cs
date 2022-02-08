using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlyxGamemode
{
    public class Angle
    {
        public float Pitch, Yaw, Roll;
        public Angle()
        { }
        public Angle(float Pitch, float Yaw, float Roll)
        {
            this.Pitch = Pitch;
            this.Yaw = Yaw;
            this.Roll = Roll;
        }
        public override string ToString()
        {
            return Pitch + " " + Yaw + " " + Roll;
        }
    }
}
