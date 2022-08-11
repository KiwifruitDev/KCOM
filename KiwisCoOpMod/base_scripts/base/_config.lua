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

-- Server configurable variables

lua_config = {}

-- An introduction message to be sent to a player when they join the server
lua_config.client_introduction_message = {
    "-----------------------------",
    "Welcome to my server!",
    "This is a server for Half-Life: Alyx.",
    "Type /help for a list of commands.",
    "There ~areis ~playercount player~plural on ~map (~gamemode/~subgamemode)",
    "Enjoy your stay here!",
    "-----------------------------",
}

-- Gamemode types
lua_config.gamemodes = {
    ["AlyxGamemode"] = "Half-Life: Alyx",
    ["CampaignGamemode"] = "Campaign Gamemode",
    ["CoreGamemode"] = "Core Gamemode",
}

lua_config.sub_gamemodes = {}

-- The output of the /help command
lua_config.client_helptable = {
    "- Command help: -",
    "/echo <message> - Echo a message.",
    "/ping - Check your ping.",
    "/help - This help menu.",
    "/list - List all players on the server.",
    "/vc - Enter a VConsole command.",
    "--------------------",
}

-- The output of the "help" internal server command
lua_config.server_helptable = {
    "- Command help: -",
    "echo <message> - Echo a message.",
    "persistent_set <key> <value> - Set a persistent Lua value.",
    "persistent_get <key> - Get a persistent Lua value.",
    "persistent_remove <key> - Remove a persistent Lua value.",
    "persistent_get_all - List all persistent Lua values.",
    "persistent_clear - Clear all persistent Lua values.",
    "script_refresh <script> - Refresh a Lua script.",
    "script_refresh_all - Refresh all Lua scripts.",
    "kick <username> - Kick a player from the server.",
    "ban <username> - Ban a player from the server.",
    "ipban <username> - Ban a player from the server by IP.",
    "unban <username> - Remove a player's ban from the server.",
    "lua <code> - Run Lua code.",
    "tp <username> (<username>/<x> <y> <z>) - Teleport a player to a location.",
    "tpall (<username>/<x> <y> <z>) - Teleport all players to a location.",
    "--------------------",
}