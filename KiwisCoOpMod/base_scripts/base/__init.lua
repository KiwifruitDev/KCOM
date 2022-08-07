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
json = require('scripts/include/json')
lua_env = {}
lua_env.persistence = {}
lua_env.handlers = {}
print = function(...)
    local channel = Channel('LUA', 'Lua Scripting', Color.CornflowerBlue)
    local varargTable = {...}
    if Program.userInterface then
        Program.userInterface:Invoke(function()
            Program.userInterface:LogToOutput(channel, table.unpack(varargTable))
        end)
    end
end
printdebug = function(...)
    Debug.WriteLine(...)
end
printerr = function(...)
    local channel = Channel('ERR', 'Lua Errors', Color.Red)
    local varargTable = {...}
    if Program.userInterface then
        Program.userInterface:Invoke(function()
            Program.userInterface:LogToOutput(channel, table.unpack(varargTable))
        end)
    end
end
string.split = function(str, delimiter)
    LuaEnvironment.instance:SplitString(str, delimiter)
end
refresh = function(script)
    LuaEnvironment.instance:RunFile(script)
end
refresh_all = function()
    LuaEnvironment.instance:LoadFiles()
end
handle = function(handleType, arg1, arg2, arg3, arg4)
    for _, func in pairs(lua_env.handlers) do
        func(handleType, arg1, arg2, arg3, arg4)
    end
end
luarun = function(code)
    LuaEnvironment.instance:RunLua(code)
end