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
using NLua;
using KiwisCoOpModCore;
using Fleck;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Reflection;

namespace KiwisCoOpMod
{
    public sealed class LuaEnvironment
    {
        public static readonly LuaEnvironment instance = new();
        Lua lua;
        public LuaEnvironment()
        {
            lua = new();
            lua.LoadCLRPackage();
            // Just in case the front-end Lua scripts don't implement __init.lua
            lua.DoString(@"
                printerr = function(...)
                    local channel = Channel('ERR', 'Lua Errors', Color.Red)
                    local varargTable = {...}
                    if Program.userInterface then
                        Program.userInterface:Invoke(function()
                            Program.userInterface:LogToOutput(channel, table.unpack(varargTable))
                        end)
                    end
                end
            ");
            LoadFiles();
        }
        public void Dispose()
        {
            lua.Dispose();
        }
        public void LoadFiles()
        {
            // Create "scripts" directory if it doesn't exist
            if (!Directory.Exists("scripts"))
                Directory.CreateDirectory("scripts");
            // Load scripts (.lua files) in "scripts" directory
            List<string> foundFiles = Directory.GetFiles("scripts", "*.lua", SearchOption.AllDirectories).ToList();
            foundFiles.Sort(); // Sort alphabetically
            foreach (string file in foundFiles)
            {
                lua.DoFile(file);
            }
        }
        public void RunFile(string file)
        {
            try
            {
                lua.DoFile(file);
            }
            catch(Exception e)
            {
                LuaError(e);
            }
        }
        public void RunLua(string luaCode)
        {
            try
            {
                lua.DoString(luaCode);
            }
            catch (Exception e)
            {
                LuaError(e);
            }
        }

        // Lua does not implement splitting
        public string StringSplit(string str, string delimiter)
        {
            return JsonConvert.SerializeObject(str.Split(delimiter));
        }
        public void LuaError(Exception e)
        {
            LuaFunction? printerror = lua["printerr"] as LuaFunction;
            if (printerror != null)
            {
                try
                {
                    Debug.WriteLine(e);
                    printerror.Call(e.Message);
                }
                catch { }
            }
        }
        public static bool CheckForMainThread()
        {
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA &&
                !Thread.CurrentThread.IsBackground && !Thread.CurrentThread.IsThreadPoolThread && Thread.CurrentThread.IsAlive)
            {
                MethodInfo correctEntryMethod = Assembly.GetEntryAssembly().EntryPoint;
                StackTrace trace = new StackTrace();
                StackFrame[] frames = trace.GetFrames();
                for (int i = frames.Length - 1; i >= 0; i--)
                {
                    MethodBase method = frames[i].GetMethod();
                    if (correctEntryMethod == method)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public void Handle(PluginHandleType handleType, params object[] vs)
        {
            try
            {
                // If on a different thread than main, invoke on main thread
                if (!CheckForMainThread())
                {
                    Program.userInterface.Invoke(() =>
                    {
                        HandleInternal(handleType, vs);
                    });
                }
                else
                    HandleInternal(handleType, vs);
            }
            catch { } // Give up
        }
        public void HandleInternal(PluginHandleType handleType, params object[] vs)
        {
            // Load cached scripts
            try
            {
                LuaFunction? scriptFunc = lua["handle"] as LuaFunction;
                if (scriptFunc != null && vs != null)
                {
                    switch (handleType)
                    {
                        case PluginHandleType.Client_PreResponse:
                        case PluginHandleType.Client_PostResponse:
                            scriptFunc.Call(handleType.ToString(), (Response)vs[0]);
                            break;
                        case PluginHandleType.Server_PostGamemode_PreStart:
                        case PluginHandleType.Server_PreGamemode_PostStart:
                        case PluginHandleType.Server_PostGamemode_PostStart:
                        case PluginHandleType.Server_PreGamemode_PreClose:
                        case PluginHandleType.Server_PostGamemode_PreClose:
                        case PluginHandleType.Server_PreGamemode_PostClose:
                        case PluginHandleType.Server_PostGamemode_PostClose:
                            scriptFunc.Call(handleType.ToString(), (Type)vs[0], (List<Type>)vs[1], (string)vs[2]);
                            break;
                        case PluginHandleType.Server_PreGamemode_PreResponse:
                        case PluginHandleType.Server_PostGamemode_PreResponse:
                        case PluginHandleType.Server_PreGamemode_PostResponse:
                        case PluginHandleType.Server_PostGamemode_PostResponse:
                            scriptFunc.Call(handleType.ToString(), (Response)vs[0], (List<IndexedClient>)vs[1], (IWebSocketConnection)vs[2], (string)vs[3]);
                            break;
                        case PluginHandleType.Server_PreGamemode_ClientOpen:
                        case PluginHandleType.Server_PostGamemode_ClientOpen:
                        case PluginHandleType.Server_PreGamemode_ClientClose:
                        case PluginHandleType.Server_PostGamemode_ClientClose:
                            scriptFunc.Call(handleType.ToString(), (List<IndexedClient>)vs[0], (IWebSocketConnection)vs[1], (string)vs[2]);
                            break;
                        case PluginHandleType.Server_PreGamemode_Command:
                        case PluginHandleType.Server_PostGamemode_Command:
                            scriptFunc.Call(handleType.ToString(), (List<string>)vs[0], (List<IndexedClient>)vs[1], (Type)vs[2]);
                            break;
                        case PluginHandleType.Server_PreGamemode_Think:
                        case PluginHandleType.Server_PostGamemode_Think:
                            scriptFunc.Call(handleType.ToString(), (int)vs[0], (List<IndexedClient>)vs[1], (string)vs[2]);
                            break;
                        default:
                            scriptFunc.Call(handleType.ToString());
                            break;
                    }
                }
                else if (scriptFunc != null)
                {
                    scriptFunc.Call(handleType);
                }
            }
            catch (Exception e)
            {
                LuaError(e);
            }
        }
    }
}
