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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace LuaInterpreterPlugin
{
    public sealed class LuaEnvironment
    {
        public static readonly LuaEnvironment instance = new();
        // Cached scripts are loaded each time.
        // Scripts can manage that here.
        private readonly Dictionary<string, string> cachedScripts = new();
        public void SetCachedScript(string key, string value)
        {
            cachedScripts[key] = value;
        }
        public void RemoveCachedScript(string key)
        {
            cachedScripts.Remove(key);
        }
        public string? GetCachedScript(string key)
        {
            return cachedScripts[key];
        }
        public List<KeyValuePair<string, string>> GetCachedScriptAll()
        {
            List<KeyValuePair<string, string>> list = new();
            foreach (KeyValuePair<string, string> kvp in cachedScripts)
            {
                list.Add(kvp);
            }
            return list;
        }
        public void ClearCachedScript()
        {
            cachedScripts.Clear();
        }
        // Scripts can add persistent strings to this list.
        // They will be available to all scripts.
        private readonly Dictionary<string, string> persistent = new();
        public void SetPersistent(string key, string value)
        {
            persistent[key] = value;
        }
        public void RemovePersistent(string key)
        {
            persistent.Remove(key);
        }
        public string? GetPersistent(string key)
        {
            if(persistent.ContainsKey(key))
                return persistent[key];
            return null;
        }
        public List<KeyValuePair<string, string>> GetPersistentAll()
        {
            List<KeyValuePair<string, string>> list = new();
            foreach (KeyValuePair<string, string> kvp in persistent)
            {
                list.Add(kvp);
            }
            return list;
        }
        public void ClearPersistent()
        {
            persistent.Clear();
        }
        public void LoadFiles()
        {
            cachedScripts.Clear();
            // Create "scripts" directory if it doesn't exist
            if (!Directory.Exists("scripts"))
                Directory.CreateDirectory("scripts");
            // Load scripts (.lua files) in "scripts" directory
            List<string> foundFiles = Directory.GetFiles("scripts", "*.lua").ToList();
            foundFiles.Sort();
            foreach (string file in foundFiles)
            {
                cachedScripts[Path.GetFileNameWithoutExtension(file)] = File.ReadAllText(file);
            }
        }
        public void Initialize(string handleType, params object[] vs) //string vs)
        {
            Lua lua = new();
            lua.LoadCLRPackage();
            // Load persistent strings as global variables
            foreach (KeyValuePair<string, string> kvp in persistent)
            {
                lua.DoString($"{kvp.Key} = '{kvp.Value}'");
            }
            // Load cached scripts
            try
            {
                foreach (KeyValuePair<string, string> kvp in cachedScripts)
                {
                    try
                    {
                        lua.DoString(kvp.Value);
                        LuaFunction? scriptFunc = lua["Plugin"] as LuaFunction;
                        if (scriptFunc != null && vs != null)
                        {
                            switch (handleType)
                            {
                                case "ClientPreResponse":
                                case "ClientPostResponse":
                                    scriptFunc.Call(handleType, (Response)vs[0]);
                                    break;
                                case "ServerPostGamemodePreStart":
                                case "ServerPreGamemodePostStart":
                                case "ServerPostGamemodePostStart":
                                case "ServerPreGamemodePreClose":
                                case "ServerPostGamemodePreClose":
                                case "ServerPreGamemodePostClose":
                                case "ServerPostGamemodePostClose":
                                    scriptFunc.Call(handleType, (Type)vs[0], (List<Type>)vs[1]);
                                    break;
                                case "ServerPreGamemodePreResponse":
                                case "ServerPostGamemodePreResponse":
                                case "ServerPreGamemodePostResponse":
                                case "ServerPostGamemodePostResponse":
                                    scriptFunc.Call(handleType, (Response)vs[0], (List<IndexedClient>)vs[1], (IWebSocketConnection)vs[2], (string)vs[3]);
                                    break;
                                case "ServerPreGamemodeClientOpen":
                                case "ServerPostGamemodeClientOpen":
                                case "ServerPreGamemodeClientClose":
                                case "ServerPostGamemodeClientClose":
                                    scriptFunc.Call(handleType, (List<IndexedClient>)vs[0], (IWebSocketConnection)vs[1], (string)vs[2]);
                                    break;
                                case "ServerPreGamemodeCommand":
                                case "ServerPostGamemodeCommand":
                                    scriptFunc.Call(handleType, (List<string>)vs[0], (List<IndexedClient>)vs[1], (Type)vs[2]);
                                    break;
                                default:
                                    scriptFunc.Call(handleType);
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
                        LuaFunction? printerror = lua["printerr"] as LuaFunction;
                        if (printerror != null)
                        {
                            try
                            {
                                printerror.Call(e.Message);
                            }
                            catch { }
                        }
                    }
                }
                lua.Dispose();
            }
            catch { }
        }
    }
}
