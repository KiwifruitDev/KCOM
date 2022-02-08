using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlyxGamemode
{
    public enum PacketType
    {
        None = 0,
        PlayerPosAng,
        HeadPosAng,
        HandPosAng,
        Initialization,
        InitializedEntities,
        RightHandIndexes,
        LeftHandIndexes,
        HeadsetIndexes,
        TextIndexes,
        ColliderIndexes,
        Prefix,
        Alive,
        ColliderDamage,
        PhysicsObjectIndexStartPos,
        PhysicsObjectPosAng,
        MapName,
        ButtonIndexStartPos,
        ButtonPressIndex,
        DoorIndexStartPos,
        BrokenPropIndex,
        TriggerIndexStartPos,
        TriggerActivateIndex,
        EntityRemoved,
        EntityFired,
    }
    public class Packet
    {
        public PacketType type = PacketType.None;
        public string[] args = new string[0];
        public Packet(string type)
        {
            this.type = ParseType(type);
        }
        public Packet(string type, string args)
        {
            this.type = ParseType(type);
            this.args = args.Split(" ");
        }
        public Packet(string type, string[] args)
        {
            this.type = ParseType(type);
            this.args = args;
        }
        public Packet(PacketType type)
        {
            this.type = type;
        }
        public Packet(PacketType type, string args)
        {
            this.type = type;
            this.args = args.Split(" ");
        }
        public Packet(PacketType type, string[] args)
        {
            this.type = type;
            this.args = args;
        }
        public bool IsValid()
        {
            switch(type)
            {
                case PacketType.None:
                    return false;
            }
            return true;
        }
        private PacketType ParseType(string type)
        {
            switch(type.ToUpper())
            {
                case "NONE":
                    return PacketType.None;
                case "PLYR":
                    return PacketType.PlayerPosAng;
                case "HEAD":
                    return PacketType.HeadPosAng;
                case "HAND":
                    return PacketType.HandPosAng;
                case "INIT":
                    return PacketType.Initialization;
                case "IENT":
                    return PacketType.InitializedEntities;
                case "RHND":
                    return PacketType.RightHandIndexes;
                case "LHND":
                    return PacketType.LeftHandIndexes;
                case "HSET":
                    return PacketType.HeadsetIndexes;
                case "TAGS":
                    return PacketType.TextIndexes;
                case "NPCS":
                    return PacketType.ColliderIndexes;
                case "PRFX":
                    return PacketType.Prefix;
                case "ALIV":
                    return PacketType.Alive;
                case "DMGE":
                    return PacketType.ColliderDamage;
                case "PROP":
                    return PacketType.PhysicsObjectIndexStartPos;
                case "PHYS":
                    return PacketType.PhysicsObjectPosAng;
                case "MAPN":
                    return PacketType.MapName;
                case "BUTN":
                    return PacketType.ButtonIndexStartPos;
                case "BPRS":
                    return PacketType.ButtonPressIndex;
                case "DOOR":
                    return PacketType.DoorIndexStartPos;
                case "BRAK":
                    return PacketType.BrokenPropIndex;
                case "TRIG":
                    return PacketType.TriggerIndexStartPos;
                case "TRGD":
                    return PacketType.TriggerActivateIndex;
                case "EREM":
                    return PacketType.EntityRemoved;
                case "FIRE":
                    return PacketType.EntityFired;
                default:
                    return PacketType.None;
            }
        }
        public override string ToString()
        {
            switch (type)
            {
                case PacketType.PlayerPosAng:
                    return "PLYR";
                case PacketType.HeadPosAng:
                    return "HEAD";
                case PacketType.HandPosAng:
                    return "HAND";
                case PacketType.Initialization:
                    return "INIT";
                case PacketType.InitializedEntities:
                    return "IENT";
                case PacketType.RightHandIndexes:
                    return "RHND";
                case PacketType.LeftHandIndexes:
                    return "LHND";
                case PacketType.HeadsetIndexes:
                    return "HSET";
                case PacketType.TextIndexes:
                    return "TAGS";
                case PacketType.ColliderIndexes:
                    return "NPCS";
                case PacketType.Prefix:
                    return "PRFX";
                case PacketType.Alive:
                    return "ALIV";
                case PacketType.ColliderDamage:
                    return "DMGE";
                case PacketType.PhysicsObjectIndexStartPos:
                    return "PROP";
                case PacketType.PhysicsObjectPosAng:
                    return "PHYS";
                case PacketType.MapName:
                    return "MAPN";
                case PacketType.ButtonIndexStartPos:
                    return "BUTN";
                case PacketType.ButtonPressIndex:
                    return "BUTN";
                case PacketType.DoorIndexStartPos:
                    return "DOOR";
                case PacketType.BrokenPropIndex:
                    return "BRAK";
                case PacketType.TriggerIndexStartPos:
                    return "TRIG";
                case PacketType.TriggerActivateIndex:
                    return "TRGD";
                case PacketType.EntityRemoved:
                    return "EREM";
                case PacketType.EntityFired:
                    return "FIRE";
                default:
                    return "NONE";
            }
        }
    }
}
