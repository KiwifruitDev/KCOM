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
    "Welcome to my server!",
    "This is a server for Half-Life: Alyx.",
    "Type /help for a list of commands.",
    "Enjoy your stay!"
}

-- The output of the /help command
lua_config.client_helptable = {
    "- Command help: -",
    "/ping - Check your ping.",
    "/help - This help menu.",
    "/list - List all players on the server.",
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
    "kick <username> - Force a player to reconnect to the server.",
    "ban <username> - Temporarily ban a player from the server.",
    "--------------------",
}