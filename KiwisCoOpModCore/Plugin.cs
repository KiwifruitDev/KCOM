using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace KiwisCoOpModCore
{
    public enum PluginHandleType
    {
        None = 0,
        UserInterface_Initialized,
        UserInterface_Exit,
        UserInterface_PreStart,
        UserInterface_PostStart,
        UserInterface_PreClose,
        UserInterface_PostClose,
        Client_PreStart,
        Client_PostStart,
        Client_PreClose,
        Client_PostClose,
        Client_PreResponse,
        Client_PostResponse,
        Server_PreGamemode_PreStart,
        Server_PostGamemode_PreStart,
        Server_PreGamemode_PostStart,
        Server_PostGamemode_PostStart,
        Server_PreGamemode_PreClose,
        Server_PostGamemode_PreClose,
        Server_PreGamemode_PostClose,
        Server_PostGamemode_PostClose,
        Server_PreGamemode_PreResponse,
        Server_PostGamemode_PreResponse,
        Server_PreGamemode_PostResponse,
        Server_PostGamemode_PostResponse,
        Server_PreGamemode_ClientOpen,
        Server_PostGamemode_ClientOpen,
        Server_PreGamemode_ClientClose,
        Server_PostGamemode_ClientClose
    }
    public static class PluginHandler
    {
        public static bool Handle(List<Type> plugins, PluginHandleType handleType, params object[]? vs)
        {
            bool handled = false;
            foreach(Type pluginType in plugins)
            {
                Plugin? newGamemode;
                if (vs == null)
                    newGamemode = (Plugin?)Activator.CreateInstance(pluginType, handleType);
                else
                    newGamemode = (Plugin?)Activator.CreateInstance(pluginType, handleType, vs);
                if (newGamemode != null)
                    handled = true;
            }
            return handled;
        }
        public static bool Handle(Type pluginType, PluginHandleType handleType, params object[]? vs)
        {
            bool handled = false;
            Plugin? newGamemode;
            if (vs == null)
                newGamemode = (Plugin?)Activator.CreateInstance(pluginType, handleType);
            else
                newGamemode = (Plugin?)Activator.CreateInstance(pluginType, handleType, vs);
            if (newGamemode != null)
                handled = true;
            return handled;
        }
        public static Plugin? Instance(Type pluginType)
        {
            return (Plugin?)Activator.CreateInstance(pluginType, PluginHandleType.None);
        }
    }
    public interface Plugin
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public bool Default { get; set; }
    }
}
