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

lua_env.persistence["checkpoint_using_hud"] = lua_env.persistence["checkpoint_using_hud"] or false
lua_env.persistence["checkpoints"] = lua_env.persistence["checkpoints"] or {}
lua_env.persistence["checkpoint_map"] = ""
lua_env.persistence["checkpoint_index"] = 1
lua_env.persistence["checkpoint_start_time"] = lua_env.persistence["checkpoint_start_time"] or 0

if not lua_env.persistence["script_checkpoints"] then
    lua_env.persistence["script_checkpoints"] = #lua_env.handlers + 1
end

lua_env.handlers[lua_env.persistence["script_checkpoints"]] = function(handleType, arg1, arg2, arg3, arg4)
    if lua_config.sub_gamemode ~= campaign_config.gamemodetype then
        return -- Not the right gamemode
    end
    if handleType == "Server_PreGamemode_PreStart" then
        lua_env.persistence["checkpoint_map"] = ""
        lua_env.persistence["checkpoint_index"] = 1
        -- Load all checkpoints form ./checkpoints/*.json (windows)
        local files = io.popen("dir /b .\\checkpoints")
        for file in files:lines() do
            local f = io.open("checkpoints/" .. file, "r")
            local data = f:read("*a")
            f:close()
            local json = json.decode(data)
            for mapname, checkpointmap in pairs(json) do
                lua_env.persistence["checkpoints"][mapname] = json[mapname]
            end
        end
    elseif handleType == "Server_PreGamemode_Think" then
        if not campaign_config.checkpoints then
            return -- Checkpoints are disabled
        end
        local tickrate = arg1
        local allPlayers = arg2
        local map = arg3
        if not allPlayers or not map then return end
        if map ~= lua_env.persistence["checkpoint_map"] then
            lua_env.persistence["checkpoint_map"] = map
            lua_env.persistence["checkpoint_index"] = 1
        end
        local playerindex = -1
        local newhealth = -1
        for i = 0, allPlayers.Count - 1 do
            local player = allPlayers[i]
            if not player then break end
            local playerTable = lua_env.persistence["players"][player.Username]
            if not playerTable then break end
            if not lua_env.persistence["checkpoints"][map] then break end
            local lastcheckpoint = lua_env.persistence["checkpoints"][map][lua_env.persistence["checkpoint_index"] - 1]
            -- Teleport player to last checkpoint if dead
            local health = playerTable.health
            if health ~= newhealth then
                newhealth = health
            end
            if health <= 1 and campaign_config.respawn then -- Buddha mode
                if lastcheckpoint then -- Teleport to last checkpoint
                    local teleportPlayer = Response("command", "kcom_teleport " .. lastcheckpoint.x .. " " .. lastcheckpoint.y .. " " .. lastcheckpoint.z)
                    local healPlayer = Response("command", "kcom_sethealth 100;ent_fire kcom_hud setmessage \"Respawned at last checkpoint!\"")
                    player.Session:Send(teleportPlayer:ToString())
                    player.Session:Send(healPlayer:ToString())
                    lua_env.persistence["checkpoint_start_time"] = os.time()
                else -- Teleport to random player
                    local randomPlayer = allPlayers[math.random(0, allPlayers.Count - 1)]
                    local randomPlayerTable = lua_env.persistence["players"][randomPlayer.Username]
                    local teleportPlayer = Response("command", "kcom_teleport " .. randomPlayerTable.origin.x .. " " .. randomPlayerTable.origin.y .. " " .. randomPlayerTable.origin.z)
                    local healPlayer = Response("command", "kcom_sethealth 100;ent_fire kcom_hud setmessage \"Respawned at " .. randomPlayer.Username .. "!\"")
                    player.Session:Send(teleportPlayer:ToString())
                    player.Session:Send(healPlayer:ToString())
                end
            end
            local checkpoint = lua_env.persistence["checkpoints"][map][lua_env.persistence["checkpoint_index"]]
            if not checkpoint then break end
            -- Check if player is in checkpoint using x, y, z, r(adius)
            local x, y, z = playerTable.origin.x, playerTable.origin.y, playerTable.origin.z
            local cx, cy, cz = checkpoint.x, checkpoint.y, checkpoint.z
            local radius = checkpoint.r
            local player_distance = math.sqrt((x - cx) ^ 2 + (y - cy) ^ 2 + (z - cz) ^ 2)
            if player_distance <= radius then
                -- Player reached checkpoint!
                playerindex = i
                break
            end
        end
        if playerindex >= 0 then
            -- Player reached checkpoint!
            local checkpoint = lua_env.persistence["checkpoints"][map][lua_env.persistence["checkpoint_index"]]
            if not checkpoint then return end
            lua_env.persistence["checkpoint_start_time"] = os.time()
            for i = 0, allPlayers.Count - 1 do
                local player = allPlayers[i]
                if not player then break end
                local teleportPlayer = Response("command", "kcom_cache_all_entities;kcom_teleport " .. checkpoint.x .. " " .. checkpoint.y .. " " .. checkpoint.z..";ent_fire kcom_hud setmessage \"Checkpoint " .. lua_env.persistence["checkpoint_index"] .. "/" .. #lua_env.persistence["checkpoints"][map] .. " reached!\"")
                local checkpointStatus = Response("status", "Checkpoint " .. lua_env.persistence["checkpoint_index"] .. "/" .. #lua_env.persistence["checkpoints"][map] .. " reached!")
                player.Session:Send(teleportPlayer:ToString())
                player.Session:Send(checkpointStatus:ToString())
            end
            lua_env.persistence["checkpoint_using_hud"] = true
            lua_env.persistence["checkpoint_index"] = lua_env.persistence["checkpoint_index"] + 1
        end
        -- Display text for 5 seconds
        if os.time() - lua_env.persistence["checkpoint_start_time"] > 5 and lua_env.persistence["checkpoint_checkpoint_using_hud"] then
            for i = 0, allPlayers.Count - 1 do
                local player = allPlayers[i]
                if not player then break end
                local teleportHud = Response("command", "ent_fire kcom_hud setmessage \"\"")
                player.Session:Send(teleportHud:ToString())
            end
            lua_env.persistence["checkpoint_using_hud"] = false
        end
        -- Sync health if enabled
        if campaign_config.sharing.health then
            for i = 0, allPlayers.Count - 1 do
                local player = allPlayers[i]
                if not player then break end
                local playerTable = lua_env.persistence["players"][player.Username]
                if not playerTable then break end
                if playerTable.health ~= newhealth then
                    local setHealth = Response("command", "kcom_sethealth " .. playerTable.health)
                    player.Session:Send(setHealth:ToString())
                end
            end
        end
    end
end