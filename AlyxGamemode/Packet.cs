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
        NPCHealth,
    }
    public class Packet
    {
        public PacketType type = PacketType.None;
        public string[] args = Array.Empty<string>();
        public Packet()
        {
        }
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
            return type switch
            {
                PacketType.None => false,
                _ => true,
            };
        }
        private static PacketType ParseType(string type)
        {
            return type.ToUpper() switch
            {
                "PLYR" => PacketType.PlayerPosAng,
                "HEAD" => PacketType.HeadPosAng,
                "HAND" => PacketType.HandPosAng,
                "INIT" => PacketType.Initialization,
                "IENT" => PacketType.InitializedEntities,
                "RHND" => PacketType.RightHandIndexes,
                "LHND" => PacketType.LeftHandIndexes,
                "HSET" => PacketType.HeadsetIndexes,
                "TAGS" => PacketType.TextIndexes,
                "NPCS" => PacketType.ColliderIndexes,
                "PRFX" => PacketType.Prefix,
                "ALIV" => PacketType.Alive,
                "DMGE" => PacketType.ColliderDamage,
                "PROP" => PacketType.PhysicsObjectIndexStartPos,
                "PHYS" => PacketType.PhysicsObjectPosAng,
                "MAPN" => PacketType.MapName,
                "BUTN" => PacketType.ButtonIndexStartPos,
                "BPRS" => PacketType.ButtonPressIndex,
                "DOOR" => PacketType.DoorIndexStartPos,
                "BRAK" => PacketType.BrokenPropIndex,
                "TRIG" => PacketType.TriggerIndexStartPos,
                "TRGD" => PacketType.TriggerActivateIndex,
                "EREM" => PacketType.EntityRemoved,
                "FIRE" => PacketType.EntityFired,
                "NPHP" => PacketType.NPCHealth,
                _ => PacketType.None,
            };
        }
        public override string ToString()
        {
            return type switch
            {
                PacketType.PlayerPosAng => "PLYR",
                PacketType.HeadPosAng => "HEAD",
                PacketType.HandPosAng => "HAND",
                PacketType.Initialization => "INIT",
                PacketType.InitializedEntities => "IENT",
                PacketType.RightHandIndexes => "RHND",
                PacketType.LeftHandIndexes => "LHND",
                PacketType.HeadsetIndexes => "HSET",
                PacketType.TextIndexes => "TAGS",
                PacketType.ColliderIndexes => "NPCS",
                PacketType.Prefix => "PRFX",
                PacketType.Alive => "ALIV",
                PacketType.ColliderDamage => "DMGE",
                PacketType.PhysicsObjectIndexStartPos => "PROP",
                PacketType.PhysicsObjectPosAng => "PHYS",
                PacketType.MapName => "MAPN",
                PacketType.ButtonIndexStartPos => "BUTN",
                PacketType.ButtonPressIndex => "BUTN",
                PacketType.DoorIndexStartPos => "DOOR",
                PacketType.BrokenPropIndex => "BRAK",
                PacketType.TriggerIndexStartPos => "TRIG",
                PacketType.TriggerActivateIndex => "TRGD",
                PacketType.EntityRemoved => "EREM",
                PacketType.EntityFired => "FIRE",
                PacketType.NPCHealth => "NPHP",
                _ => "NONE",
            };
        }
    }
}
