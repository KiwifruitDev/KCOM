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

namespace AlyxDeathmatch
{
    public enum DeathmatchPacketType
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
        PlayerDeath,
    }
    public class DeathmatchPacket
    {
        public DeathmatchPacketType type = DeathmatchPacketType.None;
        public string[] args = Array.Empty<string>();
        public DeathmatchPacket(string type)
        {
            this.type = ParseType(type);
        }
        public DeathmatchPacket(string type, string args)
        {
            this.type = ParseType(type);
            this.args = args.Split(" ");
        }
        public DeathmatchPacket(string type, string[] args)
        {
            this.type = ParseType(type);
            this.args = args;
        }
        public DeathmatchPacket(DeathmatchPacketType type)
        {
            this.type = type;
        }
        public DeathmatchPacket(DeathmatchPacketType type, string args)
        {
            this.type = type;
            this.args = args.Split(" ");
        }
        public DeathmatchPacket(DeathmatchPacketType type, string[] args)
        {
            this.type = type;
            this.args = args;
        }
        public bool IsValid()
        {
            return type switch
            {
                DeathmatchPacketType.None => false,
                _ => true,
            };
        }
        private static DeathmatchPacketType ParseType(string type)
        {
            return type.ToUpper() switch
            {
                "PLYR" => DeathmatchPacketType.PlayerPosAng,
                "HEAD" => DeathmatchPacketType.HeadPosAng,
                "HAND" => DeathmatchPacketType.HandPosAng,
                "INIT" => DeathmatchPacketType.Initialization,
                "IENT" => DeathmatchPacketType.InitializedEntities,
                "RHND" => DeathmatchPacketType.RightHandIndexes,
                "LHND" => DeathmatchPacketType.LeftHandIndexes,
                "HSET" => DeathmatchPacketType.HeadsetIndexes,
                "TAGS" => DeathmatchPacketType.TextIndexes,
                "NPCS" => DeathmatchPacketType.ColliderIndexes,
                "PRFX" => DeathmatchPacketType.Prefix,
                "ALIV" => DeathmatchPacketType.Alive,
                "DMGE" => DeathmatchPacketType.ColliderDamage,
                "PROP" => DeathmatchPacketType.PhysicsObjectIndexStartPos,
                "PHYS" => DeathmatchPacketType.PhysicsObjectPosAng,
                "MAPN" => DeathmatchPacketType.MapName,
                "BUTN" => DeathmatchPacketType.ButtonIndexStartPos,
                "BPRS" => DeathmatchPacketType.ButtonPressIndex,
                "DOOR" => DeathmatchPacketType.DoorIndexStartPos,
                "BRAK" => DeathmatchPacketType.BrokenPropIndex,
                "TRIG" => DeathmatchPacketType.TriggerIndexStartPos,
                "TRGD" => DeathmatchPacketType.TriggerActivateIndex,
                "EREM" => DeathmatchPacketType.EntityRemoved,
                "FIRE" => DeathmatchPacketType.EntityFired,
                "DEAD" => DeathmatchPacketType.PlayerDeath,
                _ => DeathmatchPacketType.None,
            };
        }
        public override string ToString()
        {
            return type switch
            {
                DeathmatchPacketType.PlayerPosAng => "PLYR",
                DeathmatchPacketType.HeadPosAng => "HEAD",
                DeathmatchPacketType.HandPosAng => "HAND",
                DeathmatchPacketType.Initialization => "INIT",
                DeathmatchPacketType.InitializedEntities => "IENT",
                DeathmatchPacketType.RightHandIndexes => "RHND",
                DeathmatchPacketType.LeftHandIndexes => "LHND",
                DeathmatchPacketType.HeadsetIndexes => "HSET",
                DeathmatchPacketType.TextIndexes => "TAGS",
                DeathmatchPacketType.ColliderIndexes => "NPCS",
                DeathmatchPacketType.Prefix => "PRFX",
                DeathmatchPacketType.Alive => "ALIV",
                DeathmatchPacketType.ColliderDamage => "DMGE",
                DeathmatchPacketType.PhysicsObjectIndexStartPos => "PROP",
                DeathmatchPacketType.PhysicsObjectPosAng => "PHYS",
                DeathmatchPacketType.MapName => "MAPN",
                DeathmatchPacketType.ButtonIndexStartPos => "BUTN",
                DeathmatchPacketType.ButtonPressIndex => "BUTN",
                DeathmatchPacketType.DoorIndexStartPos => "DOOR",
                DeathmatchPacketType.BrokenPropIndex => "BRAK",
                DeathmatchPacketType.TriggerIndexStartPos => "TRIG",
                DeathmatchPacketType.TriggerActivateIndex => "TRGD",
                DeathmatchPacketType.EntityRemoved => "EREM",
                DeathmatchPacketType.EntityFired => "FIRE",
                DeathmatchPacketType.PlayerDeath => "DEAD",
                _ => "NONE",
            };
        }
    }
}
