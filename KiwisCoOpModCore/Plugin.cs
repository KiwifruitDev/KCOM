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
using System.Reflection;
using System.Threading;
using System.Diagnostics;

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
        Server_PostGamemode_ClientClose,
        Server_PreGamemode_Command,
        Server_PostGamemode_Command,
        Server_PreGamemode_Think,
        Server_PostGamemode_Think,
    }
    public static class PluginHandler
    {
        public static bool Handle(Type pluginType, PluginHandleType handleType, params object[]? vs)
        {
            //Debug.WriteLine($"Handling {handleType} on {(CheckForMainThread() ? "main" : "worker")} thread");
            bool handled = false;
            IPlugin? newGamemode;
            if (vs == null)
                newGamemode = (IPlugin?)Activator.CreateInstance(pluginType, handleType);
            else
                newGamemode = (IPlugin?)Activator.CreateInstance(pluginType, handleType, vs);
            if (newGamemode != null)
                handled = true;
            return handled;
        }
        public static bool Handle(List<Type> plugins, PluginHandleType handleType, params object[]? vs)
        {
            bool handled = false;
            foreach (Type pluginType in plugins)
            {
                handled = Handle(pluginType, handleType, vs);
                if(handled)
                    break;
            }
            return handled;
        }
    }
    public interface IPlugin
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
    }
}
