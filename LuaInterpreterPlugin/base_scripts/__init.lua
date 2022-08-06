--[[
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
]]--

-- Internal functionality synonomous with C# code

import('KiwisCoOpModCore')
import('KiwisCoOpMod')
import('LuaInterpreterPlugin')
import('System.Drawing, Version=4.0.0.0, Culture=neutral')
import('System.Drawing')
import('System.Runtime, Version=4.1.0.0, Culture=neutral')
import('System.Diagnostics')
import('System')
import('Newtonsoft.Json, Version=13.0.1`, Culture=neutral')
import('Newtonsoft.Json')
print = function(...)
    local channel = Channel('LUA', 'Lua Scripting', Color.CornflowerBlue)
    local varargTable = {...}
    if Program.userInterface then
        Program.userInterface:Invoke(function()
            Program.userInterface:LogToOutput(channel, table.unpack(varargTable))
        end)
    end
end
lua_env = {}
lua_env.persistence = {}
function lua_env.persistence.set(_, k, v)
    LuaEnvironment.instance:SetPersistent(k, v)
end
function lua_env.persistence.remove(_, k)
    LuaEnvironment.instance:RemovePersistent(k)
end
function lua_env.persistence.get(_, k)
    return LuaEnvironment.instance:GetPersistent(k)
end
function lua_env.persistence.getall()
    return LuaEnvironment.instance:GetPersistentAll()
end
function lua_env.persistence.clear()
    LuaEnvironment.instance:ClearPersistent()
end
lua_env.cachedscripts = {}
function lua_env.cachedscripts.set(k, v)
    LuaEnvironment.instance:SetCachedScript(k, v)
end
function lua_env.cachedscripts.remove(k)
    LuaEnvironment.instance:RemoveCachedScript(k)
end
function lua_env.cachedscripts.get(k)
    return LuaEnvironment.instance:GetCachedScript(k)
end
function lua_env.cachedscripts.getall()
    return LuaEnvironment.instance:GetCachedScriptAll()
end
function lua_env.cachedscripts.clear()
    LuaEnvironment.instance:ClearCachedScript()
end
function lua_env.cachedscripts.refresh()
    LuaEnvironment.instance:LoadFiles()
end