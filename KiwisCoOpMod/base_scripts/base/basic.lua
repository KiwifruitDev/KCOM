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

if not lua_env.persistence["script_basic"] then
    lua_env.persistence["script_basic"] = #lua_env.handlers + 1
end

lua_env.persistence["bans"] = lua_env.persistence["bans"] or {}
lua_env.persistence["ipbans"] = lua_env.persistence["ipbans"] or {}
lua_env.persistence["players"] = lua_env.persistence["players"] or {}

lua_env.handlers[lua_env.persistence["script_basic"]] = function(handleType, arg1, arg2, arg3, arg4)
    if handleType == "Server_PreGamemode_PreStart" then
        -- Load bans from file
        local file = io.open("bans.json", "r")
        if file then
            local data = file:read("*all")
            file:close()
            lua_env.persistence["bans"] = json.decode(data)
        end
        local file2 = io.open("ipbans.json", "r")
        if file2 then
            local data = file2:read("*all")
            file2:close()
            lua_env.persistence["ipbans"] = json.decode(data)
        end
    elseif handleType == "Server_PostGamemode_PreStart" then
        -- Set gamemode type
        if not arg1 then return end
        lua_env.persistence["gamemode"] = arg1.Name
    elseif handleType == "Server_PreGamemode_PreResponse" then
        if not arg1 or not arg2 or not arg3 or not arg4 then return end
        lua_env.persistence["map"] = arg4
        if not arg1.type then return end
        if arg1.type == "chat" then
            if not arg1.data then return end
            -- Echo command
            if string.find(arg1.data, "^/echo") then -- Message starts with "/echo"
                local message = string.sub(arg1.data, 7) -- Remove "/echo " from the message
                local respMsg = Response("status", message) -- Create response
                arg3:Send(respMsg:ToString()) -- Send response to websocket
                arg1.type = "lua_chat_handled" -- Let the server know that we handled it
            -- Ping command
            elseif arg1.data == "/ping" then
                if not arg1.timestamp then return end
                -- Getting ping time (ms)
                local ping = (DateTime.UtcNow:Subtract(DateTime.UnixEpoch).TotalMilliseconds - arg1.timestamp) 
                local pingMsg = Response("status", "Pong! " .. math.floor(ping+0.5) .. "ms")
                arg3:Send(pingMsg:ToString())
                arg1.type = "lua_chat_handled"
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
        elseif arg1.type == "print" then
            if not arg1.data then return end
            -- Test print function
            --[[
            if string.find(arg1.data, "joined the game") then
                print(arg1.data)
            end
            ]]--
            -- Get player's coordinates
            for i = 0, arg2.Count - 1 do
                if arg2[i].Session.ConnectionInfo.Id:ToString() == arg3.ConnectionInfo.Id:ToString() then
                    if string.find(arg1.data, "KCOM") then
                        local packet = split(arg1.data, " ")
                        if packet ~= nil then
                            if #packet <= 1 then return end
                            if packet[1] == "PLYR" then
                                if #packet < 9 then return end
                                lua_env.persistence["players"][arg2[i].Username] = {
                                    ["health"] = tonumber(packet[8]),
                                    ["origin"] = {
                                        ["x"] = tonumber(packet[2]),
                                        ["y"] = tonumber(packet[3]),
                                        ["z"] = tonumber(packet[4])
                                    },
                                    ["angles"] = {
                                        ["pitch"] = tonumber(packet[5]),
                                        ["yaw"] = tonumber(packet[6]),
                                        ["roll"] = tonumber(packet[7])
                                    },
                                }
                            end
                        end
                    end
                    break
                end
            end
        end
    elseif handleType == "Server_PreGamemode_Command" then
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
            lua_env.persistence[arg1[1]] = arg1[2]
            print("Set " .. arg1[1] .. " to " .. arg1[2])
        -- Get a persistent variable
        elseif arg1[0] == "pget"
            or arg1[0] == "persistent_get" then
            if arg1.Count < 2 then
                print("Usage: 'pget <key>'")
                return
            end
            local persistentVar = lua_env.persistence[arg1[1]]
            if persistentVar then
                print(arg1[1] .. " = " .. persistentVar)
            else
                print("Key not found")
            end
        -- Get persistent keys
        elseif arg1[0] == "pgetall"
            or arg1[0] == "persistent_get_all" then
            print("- Persistent variables: -")
            for k, v in pairs(lua_env.persistence) do
                if type(v) == "table" then
                    print(k .. " = *table*")
                else
                    print(k .. " = " .. v)
                end
            end
            print("------------------------")
        -- Remove a persistent variable
        elseif arg1[0] == "premove"
            or arg1[0] == "persistent_remove" then
            if not arg1[1] then
                print("Usage: 'persistent_remove <key>'")
                return
            end
            lua_env.persistence[arg1[1]] = nil
            print("Removed " .. arg1[1])
        -- Clear all persistent variables
        elseif arg1[0] == "pclear"
            or arg1[0] == "persistent_clear" then
            lua_env.persistence = {}
            print("Cleared persistent variables")
        -- Script refresh
        elseif arg1[0] == "srefresh"
            or arg1[0] == "script_refresh" then
            if arg1.Count < 2 then
                print("Usage: 'script_refresh <script>'")
                return
            end
            refresh("scripts/" .. arg1[1] .. ".lua")
            print("Refreshed " .. arg1[1])
        -- Script refresh all
        elseif arg1[0] == "srefreshall"
            or arg1[0] == "script_refresh_all" then
            refresh_all()
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
            -- Check if player is already banned
            for k, v in pairs(lua_env.persistence["bans"]) do
                if v == arg1[1] then
                    print("Player is already banned")
                    return
                end
            end
            for i = 0, arg2.Count - 1 do
                if arg2[i].Username == arg1[1] then
                    arg2[i].Session:Close()
                    lua_env.persistence["bans"][#lua_env.persistence["bans"] + 1] = arg2[i].Username
                    -- Save bans
                    local file = io.open("bans.json", "w")
                    file:write(json.encode(lua_env.persistence["bans"]))
                    file:close()
                    print("Banned " .. arg1[1])
                    return
                end
            end
            print("Player not found")
        -- IP ban a player by username
        elseif arg1[0] == "ipban" then
            if not arg2 then return end
            if arg1.Count < 2 then
                print("Usage: 'ipban <username>'")
                return
            end
            for i = 0, arg2.Count - 1 do
                if arg2[i].Username == arg1[1] then
                    -- Check if player is already banned
                    for k, v in pairs(lua_env.persistence["ipbans"]) do
                        if v == arg2[i].Session.ConnectionInfo.ClientIpAddress then
                            print("Player is already banned by IP address")
                            return
                        end
                    end
                    arg2[i].Session:Close()
                    lua_env.persistence["ipbans"][#lua_env.persistence["ipbans"] + 1] = arg2[i].Session.ConnectionInfo.ClientIpAddress
                    -- Save bans
                    local file = io.open("ipbans.json", "w")
                    file:write(json.encode(lua_env.persistence["ipbans"]))
                    file:close()
                    print("Banned " .. arg1[1] .. " by IP address")
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
            local done = false
            for k, v in pairs(lua_env.persistence["bans"]) do
                if v == arg1[1] then
                    lua_env.persistence["bans"][k] = nil
                    -- Shift bans
                    local newBans = {}
                    for k, v in pairs(lua_env.persistence["bans"]) do
                        if v then
                            newBans[#newBans + 1] = v
                        end
                    end
                    lua_env.persistence["bans"] = newBans
                    -- Save bans
                    local file = io.open("bans.json", "w")
                    file:write(json.encode(lua_env.persistence["bans"]))
                    file:close()
                    print("Unbanned " .. arg1[1])
                    break
                end
            end
            for k, v in pairs(lua_env.persistence["ipbans"]) do
                if v == arg1[1] then
                    lua_env.persistence["ipbans"][k] = nil
                    -- Shift bans
                    local newBans = {}
                    for k, v in pairs(lua_env.persistence["ipbans"]) do
                        if v then
                            newBans[#newBans + 1] = v
                        end
                    end
                    lua_env.persistence["ipbans"] = newBans
                    -- Save bans
                    local file = io.open("ipbans.json", "w")
                    file:write(json.encode(lua_env.persistence["ipbans"]))
                    file:close()
                    print("Unbanned " .. arg1[1] .. " by IP address")
                    break
                end
            end
            if not done then
                print("Player not found")
            end
        -- Execute lua code
        elseif arg1[0] == "lua" then
            if arg1.Count < 2 then
                print("Usage: 'lua <code>'")
                return
            end
            local code = arg1[1]
            for i = 2, arg1.Count - 1 do
                code = code .. " " .. arg1[i]
            end
            luarun(code)
        -- Teleport a player to a location
        elseif arg1[0] == "tp" then
            if not arg2 then return end
            if arg1.Count < 2 then
                print("Usage: 'tp <username> (<username>/<x> <y> <z>)'")
                return
            end
            if arg1.Count < 5 then
                for i = 0, arg2.Count - 1 do
                    if arg2[i].Username == arg1[1] then
                        for j = 0, arg2.Count - 1 do
                            if arg2[j].Username == arg1[2] then
                                if not lua_env.persistence["players"][arg1[2]] then
                                    print("Invalid player")
                                    return
                                end
                                local teleresp = Response("command", "ent_setpos 1 " .. lua_env.persistence["players"][arg1[2]].origin.x .. " " .. lua_env.persistence["players"][arg1[2]].origin.y .. " " .. lua_env.persistence["players"][arg1[2]].origin.z)
                                arg2[i].Session:Send(teleresp:ToString())
                                print("Teleported " .. arg1[1] .. " to " .. arg1[2])
                                return
                            end
                        end
                        break
                    end
                end
            else
                local teleresp = Response("command", "ent_setpos 1 " .. arg1[2] .. " " .. arg1[3] .. " " .. arg1[4])
                for i = 0, arg2.Count - 1 do
                    if arg2[i].Username == arg1[1] then
                        arg2[i].Session:Send(teleresp:ToString())
                        print("Teleported " .. arg1[1] .. " to " .. arg1[2] .. " " .. arg1[3] .. " " .. arg1[4])
                        return
                    end
                end
            end
            print("Player not found")
        -- Teleport all players to a location
        elseif arg1[0] == "tpall" then
            if not arg2 then return end
            if arg1.Count < 2 then
                print("Usage: 'tpall (<username>/<x> <y> <z>)'")
                return
            end
            if arg1.Count < 4 then
                for i = 0, arg2.Count - 1 do
                    if arg2[i].Username == arg1[1] then
                        local teleresp = Response("command", "ent_setpos 1 " .. lua_env.persistence["players"][arg1[1]].origin.x .. " " .. lua_env.persistence["players"][arg1[1]].origin.y .. " " .. lua_env.persistence["players"][arg1[1]].origin.z)
                        for j = 0, arg2.Count - 1 do
                            arg2[j].Session:Send(teleresp:ToString())
                        end
                        print("Teleported all players to " .. arg1[1])
                        return
                    end
                end
                print("Player not found")
            else
                local teleresp = Response("command", "ent_setpos 1 " .. arg1[2] .. " " .. arg1[3] .. " " .. arg1[4])
                for i = 0, arg2.Count - 1 do
                    arg2[i].Session:Send(teleresp:ToString())
                end
                print("Teleported all players to " .. arg1[2] .. " " .. arg1[3] .. " " .. arg1[4])
            end
        end
    elseif handleType == "Server_PreGamemode_ClientOpen" then
        if not arg2 or not arg3 then return end
        -- Username ban check
        for k, v in pairs(lua_env.persistence["bans"]) do
            if v == arg3 then
                local bannedMsg = Response("status", "You are banned")
                arg2:Send(bannedMsg:ToString())
                arg2:Close()
                return
            end
        end
        -- IP ban check
        for k, v in pairs(lua_env.persistence["ipbans"]) do
            if v == arg2.ConnectionInfo.ClientIpAddress then
                local bannedMsg = Response("status", "You are banned by IP address")
                arg2:Send(bannedMsg:ToString())
                arg2:Close()
                return
            end
        end
        -- Add username to table
        lua_env.persistence["players"][arg3] = {
            ["health"] = 100,
            ["origin"] = {
                ["x"] = 0,
                ["y"] = 0,
                ["z"] = 0,
            },
            ["angles"] = {
                ["pitch"] = 0,
                ["yaw"] = 0,
                ["roll"] = 0,
            },
        }
        -- Send an introductory message
        for k, v in pairs(lua_config.client_introduction_message) do
            local help = Response("status", v)
            local str = help:ToString()
            str = str:gsub("~map", lua_env.persistence["map"])
            str = str:gsub("~areis", arg1.Count > 1 and "are" or "is")
            str = str:gsub("~playercount", arg1.Count)
            str = str:gsub("~plural", arg1.Count > 1 and "s" or "")
            local gamemodename = lua_env.persistence["gamemode"]
            if lua_config.gamemodes[lua_env.persistence["gamemode"]] then
                gamemodename = lua_config.gamemodes[lua_env.persistence["gamemode"]]
            end
            str = str:gsub("~gamemode", gamemodename)
            arg2:Send(str)
        end
    elseif handleType == "Server_PreGamemode_ClientClose" then
        if not arg3 then return end
        -- Remove username from table
        lua_env.persistence["players"][arg3] = nil
    end
end