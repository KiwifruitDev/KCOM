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

campaign_config = {}

campaign_config.gamemodetype = "campaign"
campaign_config.pregame_timer = 60 -- Time in seconds before the game starts
campaign_config.checkpoints = true -- Enable checkpoints
campaign_config.respawn = true -- Enable respawning

campaign_config.sharing = {}
campaign_config.sharing.resin = false -- Share resin with other players
campaign_config.sharing.ammo = false -- Share ammo with other players
campaign_config.sharing.health = false -- Share health with other players

lua_config.sub_gamemodes[campaign_config.gamemodetype] = "Story Mode"
