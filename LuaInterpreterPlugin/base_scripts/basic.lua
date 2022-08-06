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

function Plugin(handleType, arg1, arg2, arg3, arg4)
    if handleType == "ServerPreGamemodePreResponse" then
        if not arg1.type then return end
        if arg1.type == "chat" then
            if not arg1.data then return end
            -- Ping command
            if arg1.data == "/ping" then
                if not arg1.timestamp then return end
                -- Getting ping time (ms)
                local ping = (DateTime.UtcNow:Subtract(DateTime.UnixEpoch).TotalMilliseconds - arg1.timestamp) 
                local pingMsg = Response("status", "Pong! " .. math.floor(ping+0.5) .. "ms")
                arg3:Send(pingMsg:ToString())
                arg1.type = "lua_chat_handled" -- Let the server know that we handled it
            -- Help command
            elseif arg1.data == "/help" then
                if not arg3 then return end
                for k, v in pairs(lua_config.client_helptable) do
                    local help = Response("status", v)
                    arg3:Send(help:ToString())
                end
                arg1.type = "lua_chat_handled"
            -- List command
            elseif arg1.data == "/list" then
                if not arg2 or not arg3 then return end
                local list = Response("status", "- Players: -")
                arg3:Send(list:ToString())
                for i = 0, arg2.Count - 1 do
                    local player = Response("status", arg2[i].Username)
                    arg3:Send(player:ToString())
                end
                local listfooter = Response("status", "-----------")
                arg3:Send(listfooter:ToString())
                arg1.type = "lua_chat_handled"
            end
        -- Test print function
        elseif arg1.type == "print" then
            if not arg1.data then return end
            if string.find(arg1.data, "joined the game") then
                print(arg1.data)
            end
        end
    elseif handleType == "ServerPreGamemodeCommand" then
        -- Echo command
        if arg1[0] == "echo" then
            if arg1.Count < 2 then
                print("Usage: 'echo <message>'")
                return
            end
            print(arg1[1])
        elseif arg1[0] == "help" then
            for k, v in pairs(lua_config.server_helptable) do
                print(v)
            end
        -- Set a persistent variable
        elseif arg1[0] == "pset"
            or arg1[0] == "persistent_set" then
            if arg1.Count < 3 then
                print("Usage: 'pset <key> <value>'")
                return
            end
            lua_env.persistence:set(arg1[1], arg1[2])
            print("Set " .. arg1[1] .. " to " .. arg1[2])
        -- Get a persistent variable
        elseif arg1[0] == "pget"
            or arg1[0] == "persistent_get" then
            if arg1.Count < 2 then
                print("Usage: 'pget <key>'")
                return
            end
            local persistentVar = lua_env.persistence:get(arg1[1])
            if persistentVar then
                print(arg1[1] .. " = " .. persistentVar)
            else
                print("Key not found")
            end
        -- Get persistent keys
        elseif arg1[0] == "pgetall"
            or arg1[0] == "persistent_get_all" then
            print("- Persistent variables: -")
            local persistentKeys = lua_env.persistence:getall()
            for i = 0, persistentKeys.Count - 1 do
                print(persistentKeys[i].Key .. " = " .. persistentKeys[i].Value)
            end
            print("------------------------")
        -- Remove a persistent variable
        elseif arg1[0] == "premove"
            or arg1[0] == "persistent_remove" then
            if not arg1[1] then
                print("Usage: 'persistent_remove <key>'")
                return
            end
            lua_env.persistence:remove(arg1[1])
            print("Removed " .. arg1[1])
        -- Clear all persistent variables
        elseif arg1[0] == "pclear"
            or arg1[0] == "persistent_clear" then
            lua_env.persistence:clear()
            print("Cleared persistent variables")
        -- Script refresh
        elseif arg1[0] == "srefresh"
            or arg1[0] == "script_refresh" then
            if arg1.Count < 2 then
                print("Usage: 'script_refresh <script>'")
                return
            end
            local file = io.open("scripts/" .. arg1[1] .. ".lua", "r")
            if not file then
                print("File not found")
                return
            end
            local script = file:read("*all")
            file:close()
            lua_env.cachedscripts.set(arg1[1], script)
            print("Refreshed " .. arg1[1])
        -- Script refresh all
        elseif arg1[0] == "srefreshall"
            or arg1[0] == "script_refresh_all" then
            lua_env.cachedscripts.refresh()
            print("Refreshed all scripts")
        -- Kick a player
        elseif arg1[0] == "kick" then
            if not arg2 then return end
            if arg1.Count < 2 then
                print("Usage: 'kick <username>'")
                return
            end
            for i = 0, arg2.Count - 1 do
                if arg2[i].Username == arg1[1] then
                    arg2[i].Session:Close()
                    print("Kicked " .. arg1[1])
                    return
                end
            end
            print("Player not found")
        -- Ban a player
        elseif arg1[0] == "ban" then
            if not arg2 then return end
            if arg1.Count < 2 then
                print("Usage: 'ban <username>'")
                return
            end
            for i = 0, arg2.Count - 1 do
                if arg2[i].Username == arg1[1] then
                    arg2[i].Session:Close()
                    local bancount = lua_env.persistence:get("bancount")
                    if not bancount then
                        bancount = 0
                    end
                    bancount = bancount + 1
                    lua_env.persistence:set("bancount", bancount.."")
                    lua_env.persistence:set("ban" .. bancount, arg1[1])
                    print("Banned " .. arg1[1])
                    return
                end
            end
            print("Player not found")
        -- Unban a player
        elseif arg1[0] == "unban" then
            if arg1.Count < 2 then
                print("Usage: 'unban <username>'")
                return
            end
            local bancount = lua_env.persistence:get("bancount")
            if not bancount then
                bancount = 0
            end
            for i = 0, bancount do
                if lua_env.persistence:get("ban" .. i) == arg1[1] then
                    lua_env.persistence:remove("ban" .. i)
                    print("Unbanned " .. arg1[1])
                    return
                end
            end
            -- Shift bans
            for i = 0, bancount do
                local ban = lua_env.persistence:get("ban" .. i)
                if ban then
                    lua_env.persistence:set("ban" .. i, ban)
                end
            end
            -- Remove last ban
            lua_env.persistence:remove("ban" .. bancount)
            -- Shift ban count
            bancount = bancount - 1
            lua_env.persistence:set("bancount", bancount.."")
            print("Player not found")
        end
    elseif handleType == "ServerPreGamemodeClientOpen" then
        if not arg2 or not arg3 then return end
        -- Check if player is banned
        local bancount = lua_env.persistence:get("bancount")
        if not bancount then
            bancount = 0
        end
        for i = 0, bancount do
            if lua_env.persistence:get("ban" .. i) == arg3 then
                local bannedMsg = Response("status", "You are banned")
                arg2:Send(bannedMsg:ToString())
                arg2:Close()
                break
            end
        end
        -- Send an introductory message
        for k, v in pairs(lua_config.client_introduction_message) do
            local help = Response("status", v)
            arg2:Send(help:ToString())
        end
    end
end