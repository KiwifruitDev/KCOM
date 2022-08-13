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

if not lua_env.persistence["gamemode_campaign"] then
    lua_env.persistence["gamemode_campaign"] = #lua_env.handlers + 1
end

local gamemodetype = "campaign"

lua_env.persistence["frozen_players"] = lua_env.persistence["frozen_players"] or {}
lua_env.persistence["pregame_map"] = lua_env.persistence["pregame_map"] or ""
lua_env.persistence["pregame_timer"] = lua_env.persistence["pregame_timer"] or 60
lua_env.persistence["pregame_timer_display"] = lua_env.persistence["pregame_timer_display"] or 30
lua_env.persistence["pregame_timer_start"] = lua_env.persistence["pregame_timer_start"] or 0
lua_env.persistence["pregame_timer_active"] = lua_env.persistence["pregame_timer_active"] or false
lua_env.persistence["pregame_timer_count"] = lua_env.persistence["pregame_timer_count"] or 0

lua_env.handlers[lua_env.persistence["gamemode_campaign"]] = function(handleType, arg1, arg2, arg3, arg4)
    if not lua_env.persistence["configjson"].sub_gamemode == gamemodetype then
        return -- Not the right gamemode
    end
    if handleType == "Server_PreGamemode_PreStart" then
        -- Gamemode initialized
        lua_env.persistence["pregame_map"] = ""
        lua_env.persistence["pregame_timer"] = 60
        lua_env.persistence["pregame_timer_start"] = os.time()
        lua_env.persistence["pregame_timer_active"] = true
        lua_env.persistence["pregame_timer_count"] = 0
    elseif handleType == "Server_PreGamemode_Think" then
        local tickrate = arg1
        local allPlayers = arg2
        local map = arg3
        if not allPlayers or not map then return end
        if map ~= lua_env.persistence["pregame_map"] then
            lua_env.persistence["pregame_map"] = map
            lua_env.persistence["pregame_timer"] = 60
            lua_env.persistence["pregame_timer_start"] = os.time()
            lua_env.persistence["pregame_timer_active"] = true
            lua_env.persistence["pregame_timer_count"] = 0
        end
        -- Check if the pregame timer is still running (in seconds)
        if lua_env.persistence["pregame_timer_active"] then
            for i = 0, allPlayers.Count - 1 do
                local player = allPlayers[i]
                if os.time() - lua_env.persistence["pregame_timer_start"] < lua_env.persistence["pregame_timer"] then
                    lua_env.persistence["frozen_players"][player.Username] = true
                    -- Print pregame timer if different from last time
                    if lua_env.persistence["pregame_timer_display"] ~= lua_env.persistence["pregame_timer"] - (os.time() - lua_env.persistence["pregame_timer_start"]) then
                        lua_env.persistence["pregame_timer_display"] = lua_env.persistence["pregame_timer"] - (os.time() - lua_env.persistence["pregame_timer_start"])
                        local hudDisplay = Response("command", "ent_fire kcom_hud setmessage \"\\nWaiting for players: " .. lua_env.persistence["pregame_timer_display"] .. "\"")
                        local timescale = Response("command", "host_timescale 0")
                        player.Session:Send(hudDisplay:ToString())
                        player.Session:Send(timescale:ToString())
                    end
                else
                    -- Remove player from frozen_players
                    lua_env.persistence["frozen_players"][player.Username] = nil
                    local hudDisplay = Response("command", "ent_fire kcom_hud setmessage \"\"")
					local timescale = Response("command", "host_timescale 1")
                    player.Session:Send(hudDisplay:ToString())
					player.Session:Send(timescale:ToString())
                    lua_env.persistence["pregame_timer_active"] = false
                end
            end
        end
    end
end
