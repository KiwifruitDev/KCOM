/*
    Kiwi's Co-Op Mod for Half-Life: Alyx
    Copyright (c) 2024 KiwifruitDev. All rights reserved.
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
using System.Threading;

namespace KiwisCoOpMod
{
    public enum WeaponType
    {
        None = -1,
        Pistol,
        Shotgun,
        SMG,
        Count
    }
    public enum ItemType
    {
        None = -1,
        Grenade,
        GrubVial,
        HealthPen,
        KeycardGreen,
        KeycardYellow,
        KeycardBlue,
        Count
    }
    [Flags]
    public enum WeaponUpgradeFlags
    {
        None = 0,
        PistolLaserSight = 1,
        PistolBurstFire = 2,
        ShotgunAutoLoader = 4,
    }
    public class Inventory
    {
        public int Resin { get; set; } = 0;
        public int[] ReserveAmmo { get; set; } = new int[(int)WeaponType.Count];
        public int[] Ammo { get; set; } = new int[(int)WeaponType.Count];
        public WeaponType Weapon { get; set; } = WeaponType.None;
        public WeaponUpgradeFlags WeaponUpgrades { get; set; } = WeaponUpgradeFlags.None;
        public ItemType[] Pockets { get; set; } = new ItemType[2] { ItemType.None, ItemType.None };
        public ItemType[] HeldItems { get; set; } = new ItemType[2] { ItemType.None, ItemType.None };
        public bool GravityGloves = false;
        public bool ChargedGravityGloves = false;
    }
}
