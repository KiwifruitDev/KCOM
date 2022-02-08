using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwisCoOpModCore
{
    public enum AddonType
    {
        Gamemode,
        Plugin
    }
    public class AddonInitializer
    {
        public string Name;
        public Type Type;
        public AddonType AddonType;
        public AddonInitializer(string name, Type type, AddonType addonType)
        {
            Name = name;
            Type = type;
            AddonType = addonType;
        }
    }
}
