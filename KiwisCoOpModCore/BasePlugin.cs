using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace KiwisCoOpModCore
{
    public class BasePlugin : Plugin
    {
        public BasePlugin() : base()
        {
            Author = "KiwifruitDev";
            Name = "Base Plugin";
            Description = "The absolute minimum.";
            Default = false;
        }
        public BasePlugin(PluginHandleType type, params object[]? vs)
        { }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public bool Default { get; set; }
    }
}
