--[[
    v1.2.0
    https://github.com/FrostSource/hla_extravaganza

    Player script allows for more advanced player manipulation and easier
    entity access for player related entities by extending the player class.

    Include this script into the global scope using the following line:

        require "util.player"

    -

    Useful Player methods are replicated as global functions so they
    can easily be invoked in Hammer using 'CallGlobalScriptFunction'.

    -

    Common method for referencing the player and related entities is:

    local player = Entities:GetLocalPlayer()
    local hmd_avatar = player:GetHMDAvatar()
    local left_hand = hmd_avatar:GetVRHand(0)
    local right_hand = hmd_avatar:GetVRHand(1)

    This script simplifies the above code significantly by automatically
    caching player entity handles when the player spawns and introducing
    the global variable 'Player' which references the base player entity:

    local player = Player
    local hmd_avatar = Player.HMDAvatar
    local left_hand = Player.LeftHand
    local right_hand = Player.RightHand

    Since this script extends the player entity class directly you can
    mix and match your scripting style without worrying that you're
    referencing the wrong player table.

    -

    The script attempts to track which item is currently being held in each hand
    which can be accessed using the extended CPropVRHand member 'ItemHeld':

    local primary_held_name = Player.PrimaryHand.ItemHeld:GetName()

]]
require "util.util"
require "util.storage"

-----------------------------
-- Class extension members --
-----------------------------

---**The base player entity.**
---@type CBasePlayer
Player = nil
---**The entity handle of the VR headset (if in VR mode).**
---@type CPropHMDAvatar
CBasePlayer.HMDAvatar = nil
---**The entity handle of the VR anchor (if in VR mode).**
---@type CEntityInstance
CBasePlayer.HMDAnchor = nil
---**1 = Left hand, 2 = Right hand.**
---@type CPropVRHand[]
CBasePlayer.Hands = {}
---**Player's left hand.**
---@type CPropVRHand
CBasePlayer.LeftHand = nil
---**Player's right hand.**
---@type CPropVRHand
CBasePlayer.RightHand = nil
---**Player's primary hand.**
---@type CPropVRHand
CBasePlayer.PrimaryHand = nil
---**Player's secondary hand.**
---@type CPropVRHand
CBasePlayer.SecondaryHand = nil
---**If the player is left handed.**
---@type boolean
CBasePlayer.IsLeftHanded = false
---**The last entity that was dropped by the player.**
---@type EntityHandle
CBasePlayer.LastItemDropped = nil
---**The classname of the last entity dropped by the player. In case the entity no longer exists.**
---@type string
CBasePlayer.LastClassDropped = ""
---**The last entity that was grabbed by the player.**
---@type EntityHandle
CBasePlayer.LastItemGrabbed = nil
---**The classname of the last entity grabbed by the player. In case the entity no longer exists.**
---@type string
CBasePlayer.LastClassGrabbed = ""

PLAYER_WEAPON_HAND           = "hand"
PLAYER_WEAPON_ENERGYGUN      = "energygun"
PLAYER_WEAPON_RAPIDFIRE      = "rapidfire"
PLAYER_WEAPON_SHOTGUN        = "shotgun"
PLAYER_WEAPON_MULTITOOL      = "multitool"
PLAYER_WEAPON_GENERIC_PISTOL = "generic_pistol"

---**The classname of the weapon/item attached to hand.
---@type string|"PLAYER_WEAPON_HAND"|"PLAYER_WEAPON_ENERGYGUN"|"PLAYER_WEAPON_RAPIDFIRE"|"PLAYER_WEAPON_SHOTGUN"|"PLAYER_WEAPON_MULTITOOL"|"PLAYER_WEAPON_GENERIC_PISTOL"
CBasePlayer.CurrentlyEquipped = PLAYER_WEAPON_HAND
---**The classname of the weapon/item previously attached to hand.
---@type string|"PLAYER_WEAPON_HAND"|"PLAYER_WEAPON_ENERGYGUN"|"PLAYER_WEAPON_RAPIDFIRE"|"PLAYER_WEAPON_SHOTGUN"|"PLAYER_WEAPON_MULTITOOL"|"PLAYER_WEAPON_GENERIC_PISTOL"
CBasePlayer.PreviouslyEquipped = PLAYER_WEAPON_HAND

---**Table of items player currently has possession of.**
CBasePlayer.Items = {
    grenades = {
        ---Frag grenades.
        frag = 0,
        ---Xen grenades.
        xen = 0,
    },
    ---Healthpen syringes.
    healthpen = 0,
    ammo = {
        ---Ammo for the main pistol. This is number of magazines, not bullets. Multiply by 10 to get bullets.
        energygun = 0,
        ---Ammo for the rapidfire pistol. This is number of magazines, not bullets. Multiply by 30 to get bullets.
        rapidfire = 0,
        ---Ammo for the shotgun. This is number of shells.
        shotgun = 0,
        ---Ammo for the generic pistol. This is number of magazines, not bullets.
        generic_pistol = 0,
    },
    ---Crafting currency the player has.
    ---@type integer
    resin = nil,
    ---Total number of resin player has had in inventory, regardless of upgrades.
    ---@type integer
    resin_found = nil,
}

---**The entity handle of the item held by this hand.**
---@type EntityHandle
CPropVRHand.ItemHeld = nil
---**The last entity that was dropped by this hand.**
---@type EntityHandle
CPropVRHand.LastItemDropped = nil
---**The classname of the last entity dropped by this hand. In case the entity no longer exists.**
---@type string
CPropVRHand.LastClassDropped = ""
---**The last entity that was grabbed by this hand.**
---@type EntityHandle
CPropVRHand.LastItemGrabbed = nil
---**The classname of the last entity grabbed by this hand. In case the entity no longer exists.**
---@type string
CPropVRHand.LastClassGrabbed = ""
---**The literal type of this hand.**
---@type integer|"0"|"1"
CPropVRHand.Literal = nil


-------------------------------
-- Class extension functions --
-------------------------------

---Force the player to drop an entity if held.
---@param handle EntityHandle # Handle of the entity to drop
function CBasePlayer:DropByHandle(handle)
    if IsValidEntity(handle) then
        local dmg = CreateDamageInfo(handle, handle, Vector(), Vector(), 0, 2)
        handle:TakeDamage(dmg)
        DestroyDamageInfo(dmg)
    end
end

---Force the player to drop any item held in their left hand.
function CBasePlayer:DropLeftHand()
    self:DropByHandle(self.LeftHand.ItemHeld)
end
util.SanitizeFunctionForHammer(CBasePlayer.DropLeftHand, "DropLeftHand", CBasePlayer)

---Force the player to drop any item held in their right hand.
function CBasePlayer:DropRightHand()
    self:DropByHandle(self.RightHand.ItemHeld)
end
util.SanitizeFunctionForHammer(CBasePlayer.DropRightHand, "DropRightHand", CBasePlayer)

---Force the player to drop any item held in their primary hand.
function CBasePlayer:DropPrimaryHand()
    self:DropByHandle(self.PrimaryHand.ItemHeld)
end
util.SanitizeFunctionForHammer(CBasePlayer.DropPrimaryHand, "DropPrimaryHand", CBasePlayer)

---Force the player to drop any item held in their secondary/off hand.
function CBasePlayer:DropSecondaryHand()
    self:DropByHandle(self.SecondaryHand.ItemHeld)
end
util.SanitizeFunctionForHammer(CBasePlayer.DropSecondaryHand, "DropSecondaryHand", CBasePlayer)

---Force the player to drop the caller entity if held.
---@param data TypeIOInvoke
function CBasePlayer:DropCaller(data)
    self:DropByHandle(data.caller)
end
util.SanitizeFunctionForHammer(CBasePlayer.DropCaller, "DropCaller", CBasePlayer)

---Force the player to drop the activator entity if held.
---@param data TypeIOInvoke
function CBasePlayer:DropActivator(data)
    self:DropByHandle(data.activator)
end
util.SanitizeFunctionForHammer(CBasePlayer.DropActivator, "DropActivator", CBasePlayer)

---Force the player to grab `handle` with `hand`.
---@param handle EntityHandle
---@param hand? CPropVRHand|0|1
function CBasePlayer:GrabByHandle(handle, hand)
    if IsEntity(handle, true) then
        -- If no hand provided, find nearest
        if hand == nil then
            local pos = handle:GetOrigin()
            if VectorDistanceSq(self.Hands[1]:GetOrigin(),pos) < VectorDistanceSq(self.Hands[2]:GetOrigin(),pos) then
                hand = 0
            else
                hand = 1
            end
        elseif IsEntity(hand) then
            hand = hand:GetHandID()
        end
        DoEntFireByInstanceHandle(handle, "Use", tostring(hand), 0, self, self)
    end
end

---Force the player to grab the caller entity.
---@param data TypeIOInvoke
function CBasePlayer:GrabCaller(data)
    self:GrabByHandle(data.caller)
end
util.SanitizeFunctionForHammer(CBasePlayer.GrabCaller, "GrabCaller", CBasePlayer)

---Force the player to grab the activator entity.
---@param data TypeIOInvoke
function CBasePlayer:GrabActivator(data)
    self:GrabByHandle(data.activator)
end
util.SanitizeFunctionForHammer(CBasePlayer.GrabActivator, "GrabActivator", CBasePlayer)

---@enum PLAYER_MOVETYPE
PLAYER_MOVETYPE = {
    TELEPORT_BLINK = 0,
    TELEPORT_SHIFT = 1,
    CONTINUOUS_HEAD = 2,
    CONTINUOUS_HAND = 3,
}

---Get VR movement type.
---@return PLAYER_MOVETYPE
function CBasePlayer:GetMoveType()
    return Convars:GetInt('hlvr_movetype_default') --[[@as PLAYER_MOVETYPE]]
end

---Returns the entity the player is looking at directly.
---@param maxDistance? number # Max distance the trace can search.
---@return EntityHandle
function CBasePlayer:GetLookingAt(maxDistance)
    maxDistance = maxDistance or 2048
    ---@type TraceTableLine
    local traceTable = {
        startpos = self:EyePosition(),
        endpos = self:EyePosition() + AnglesToVector(self:EyeAngles()) * maxDistance,
        ignore = self,
    }
    if TraceLine(traceTable) then
        return traceTable.enthit
    end
    return nil
end

---Disables fall damage for the player.
function CBasePlayer:DisableFallDamage()
    local name = Storage.LoadString(self, "FallDamageFilterName", DoUniqueString("__player_fall_damage_filter"))
    Storage.SaveString(self, "FallDamageFilterName", name)
    local filter = Entities:FindByName(nil, name) or SpawnEntityFromTableSynchronous("filter_damage_type",{
        targetname = name,
        damagetype = "32"
    })
    DoEntFireByInstanceHandle(self, "SetDamageFilter", name, 0, self, self)
end
util.SanitizeFunctionForHammer(CBasePlayer.DisableFallDamage, "DisableFallDamage", CBasePlayer)

---Enables fall damage for the player.
function CBasePlayer:EnableFallDamage()
    --Killing the filter is not necessary but may be helpful.
    local name = Storage.LoadString(self, "FallDamageFilterName", "")
    if name ~= "" then
        local filter = Entities:FindByName(nil, name)
        if filter then filter:Kill() end
    end
    DoEntFireByInstanceHandle(self, "SetDamageFilter", "", 0, self, self)
end
util.SanitizeFunctionForHammer(CBasePlayer.EnableFallDamage, "EnableFallDamage", CBasePlayer)

---Adds resources to the player.
---@param pistol_ammo? number
---@param rapidfire_ammo? number
---@param shotgun_ammo? number
---@param resin? number
function CBasePlayer:AddResources(pistol_ammo, rapidfire_ammo, shotgun_ammo, resin)
    SendToServerConsole("hlvr_addresources "..(pistol_ammo or 0).." "..(rapidfire_ammo or 0).." "..(shotgun_ammo or 0).." "..(resin or 0))

    -- For setting resources if ever find a way to track resin
    -- SendToServerConsole("hlvr_setresources "..
    --     (pistol_ammo or self.Items.ammo.energygun*10).." "..
    --     (rapidfire_ammo or self.Items.ammo.rapidfire*30).." "..
    --     (shotgun_ammo or self.Items.ammo.shotgun).." "..
    --     (resin or self.Items.resin)
    -- )
end

---Add pistol ammo to the player.
---@param amount number
function CBasePlayer:AddPistolAmmo(amount)
    self:AddResources(amount, nil, nil, nil)
end
---Add shotgun ammo to the player.
---@param amount number
function CBasePlayer:AddShotgunAmmo(amount)
    self:AddResources(nil, nil, amount, nil)
end
---Add rapidfire ammo to the player.
---@param amount number
function CBasePlayer:AddRapidfireAmmo(amount)
    self:AddResources(nil, amount, nil, nil)
end
---Add resin to the player.
---@param amount number
function CBasePlayer:AddResin(amount)
    self:AddResources(nil, nil, nil, amount)
end

---Marges an existing prop with a given hand.
---@param hand CPropVRHand|"0"|"1" # The hand handle or index.
---@param prop EntityHandle|string # The prop handle or targetname.
---@param hide_hand boolean # If the hand should turn invisible after merging.
function CBasePlayer:MergePropWithHand(hand, prop, hide_hand)
    if type(hand) == "number" then
        hand = self.Hands[hand+1]
    end
    hand:MergeProp(prop, hide_hand)
end

---Return if the player has a gun equipped.
---@return boolean
function CBasePlayer:HasWeaponEquipped()
    return self.CurrentlyEquipped == PLAYER_WEAPON_ENERGYGUN
        or self.CurrentlyEquipped == PLAYER_WEAPON_SHOTGUN
        or self.CurrentlyEquipped == PLAYER_WEAPON_RAPIDFIRE
        or self.CurrentlyEquipped == PLAYER_WEAPON_GENERIC_PISTOL
end

---Get the amount of ammo stored in the backpack for the currently equipped weapon.
---@return number # The amount of ammo, or 0 if no weapon equipped.
function CBasePlayer:GetCurrentWeaponReserves()
    return self.Items.ammo[self.CurrentlyEquipped] or 0
end

---Player has item holder equipped.
---@return boolean
function CBasePlayer:HasItemHolder()
    for _, hand in ipairs(self.Hands) do
        if hand:GetFirstChildWithClassname("hlvr_hand_item_holder") then
            return true
        end
    end
    -- For one handed players.
    if self.HMDAvatar:GetFirstChildWithClassname("hlvr_hand_item_holder") then
        return true
    end
    return false
end

---Player has grabbity gloves equipped.
---@return boolean
function CBasePlayer:HasGrabbityGloves()
    return self.PrimaryHand:GetGrabbityGlove() ~= nil
end

function CBasePlayer:GetFlashlight()
    return self.SecondaryHand:GetFirstChildWithClassname("hlvr_flashlight_attachment")
end

---Get the first entity the flashlight is pointed at (if the flashlight exists).
---If flashlight does not exist, both returns will be `nil`.
---@param maxDistance number # Max tracing distance, default is 2048.
---@return EntityHandle|nil # The entity that was hit, or nil.
---@return Vector|nil # The position the trace hit, regardless of entity found.
function CBasePlayer:GetFlashlightPointedAt(maxDistance)
    local flashlight = self:GetFlashlight()
    if flashlight then
        local attach = flashlight:ScriptLookupAttachment("light_attach")
        local origin = flashlight:GetAttachmentOrigin(attach)
        local endpoint = origin + flashlight:GetAttachmentForward(attach) * (maxDistance or 2048)
        ---@type TraceTableLine
        local traceTable = {
            startpos = origin,
            endpos = endpoint,
            ignore = flashlight,
        }
        TraceLine(traceTable)
        return traceTable.enthit, traceTable.pos
    end
end

---Gets the current resin from the player.
---This is can be more accurate than `Player.Items.resin`
---Calling this will update `Player.Items.resin`
---@return number
function CBasePlayer:GetResin()
    ---@type CriteriaTable
    local t = {}
    self:GatherCriteria(t)
    local r = t.current_crafting_currency
    if Player.Items.resin ~= r then
        Player.Items.resin = r
    end
    return r
end

---@type table<function,table|boolean>[]
local registered_event_callbacks = {
    player_activate = {},
    vr_player_ready = {},
    item_pickup = {},
    item_released = {},
    primary_hand_changed = {},
    player_drop_ammo_in_backpack = {},
    player_retrieved_backpack_clip = {},
    player_stored_item_in_itemholder = {},
    player_removed_item_from_itemholder = {},
    weapon_switch = {},
}

---Register a callback function with for a player event.
---@param event "player_activate"|"vr_player_ready"|"item_pickup"|"item_released"|"primary_hand_changed"|"player_drop_ammo_in_backpack"|"player_retrieved_backpack_clip"|"player_stored_item_in_itemholder"|"player_removed_item_from_itemholder"|"weapon_switch"
---@param callback function
---@param context? table # Optional: The context to pass to the function as `self`. If omitted the context will not passed to the callback.
function RegisterPlayerEventCallback(event, callback, context)
    print("Registering player callback", event, callback)
    registered_event_callbacks[event][callback] = context or true
end

---Unregisters a callback with a name.
---@param callback function
function UnregisterPlayerEventCallback(callback)
    for _, event in pairs(registered_event_callbacks) do
        event[callback] = nil
    end
end


---Merge an existing prop with this hand.
---@param prop EntityHandle|string # The prop handle or targetname.
---@param hide_hand boolean # If the hand should turn invisible after merging.
function CPropVRHand:MergeProp(prop, hide_hand)
    if type(prop) == "string" then
        prop = Entities:FindByName(nil, prop)
    end
    if IsValidEntity(prop) then
        local glove = util.GetFirstChildWithClassname(self, "hlvr_prop_renderable_glove")
        -- don't use FollowEntity
        prop:SetParent(glove, "!bonemerge")
        if hide_hand then glove:SetRenderAlpha(0) end
    else
        Warning("Could not find prop '"..tostring(prop).."' to merge with hand.\n")
    end
end

---Return true if this hand is currently holding a prop.
---@return boolean
function CPropVRHand:IsHoldingItem()
    return IsValidEntity(self.ItemHeld)
end

---Get the entity for this hands grabbity glove.
---@return EntityHandle
function CPropVRHand:GetGrabbityGlove()
    return self:GetFirstChildWithClassname("prop_grabbity_gloves")
end


---Forces the player to drop this entity if held.
---@param self CBaseEntity
function CBaseEntity:Drop()
    Player:DropByHandle(self)
end
util.SanitizeFunctionForHammer(CBaseEntity.Drop, "Drop", CBaseEntity)

---Force the player to grab this entity with the nearest hand.
function CBaseEntity:Grab()
    Player:GrabByHandle(self, nil)
end
util.SanitizeFunctionForHammer(CBaseEntity.Grab, "Grab", CBaseEntity)


-----------------
-- Local logic --
-----------------

local shellgroup_cache = 0

local player_weapon_to_ammotype =
{
    [PLAYER_WEAPON_HAND] = "Pistol",
    [PLAYER_WEAPON_MULTITOOL] = "Pistol",
    [PLAYER_WEAPON_ENERGYGUN] = "Pistol",
    [PLAYER_WEAPON_RAPIDFIRE] = "SMG1",
    [PLAYER_WEAPON_SHOTGUN] = "Buckshot",
    [PLAYER_WEAPON_GENERIC_PISTOL] = "AlyxGun",
}

local function savePlayerData()
    Storage.SaveTable(Player, "PlayerItems", Player.Items)
end

local function loadPlayerData()
    Player.Items = Storage.LoadTable(Player, "PlayerItems", Player.Items)
end

---@class PLAYER_EVENT_PLAYER_ACTIVATE : GAME_EVENT_BASE
---@field player CBasePlayer # The entity handle of the player.
---@field game_loaded boolean # If the player was activated from a game save being loaded.
---@class PLAYER_EVENT_VR_PLAYER_READY : PLAYER_EVENT_PLAYER_ACTIVATE
---@field hmd_avatar CPropHMDAvatar # The hmd avatar entity handle.

-- Setting up player values.
local listenEventPlayerActivateID
local function listenEventPlayerActivate(_, data)
    Player = GetListenServerHost()
    loadPlayerData()
    local player_previously_activated = Storage.LoadBoolean(Player, "PlayerPreviouslyActivated", false)
    Storage.SaveBoolean(Player, "PlayerPreviouslyActivated", true)
    Player:SetContextThink("global_player_setup_delay", function()
        Player.HMDAvatar = Player:GetHMDAvatar() --[[@as CPropHMDAvatar]]
        if Player.HMDAvatar then
            Player.Hands[1] = Player.HMDAvatar:GetVRHand(0)
            Player.Hands[2] = Player.HMDAvatar:GetVRHand(1)
            Player.Hands[1].Literal = Player.Hands[1]:GetLiteralHandType()
            Player.Hands[2].Literal = Player.Hands[2]:GetLiteralHandType()
            Player.Hands[1]:SetEntityName("player_hand_left")
            Player.Hands[1]:SetEntityName("player_hand_right")
            Player.LeftHand = Player.Hands[1]
            Player.RightHand = Player.Hands[2]
            Player.IsLeftHanded = Convars:GetBool("hlvr_left_hand_primary") --[[@as boolean]]
            if Player.IsLeftHanded then
                Player.PrimaryHand = Player.LeftHand
                Player.SecondaryHand = Player.RightHand
            else
                Player.PrimaryHand = Player.RightHand
                Player.SecondaryHand = Player.LeftHand
            end
            Player.HMDAnchor = Player:GetHMDAnchor() --[[@as CEntityInstance]]
            -- Registered callback
            data.player = Player
            data.game_loaded = player_previously_activated
            data.hmd_avatar = Player.HMDAvatar
            -- Get resin only if it wasn't loaded
            if Player.Items.resin == nil or Player.Items.resin_found == nil then
                -- For some reason resin isn't in criteria until 0.6 seconds
                Player:SetContextThink("__resin_update", function()
                    Player:GetResin()
                    Player.Items.resin_found = Player.Items.resin
                end, 0.5)
            end
            for callback, context in pairs(registered_event_callbacks["vr_player_ready"]) do
                if context ~= true then
                    callback(context, data)
                else
                    callback(data)
                end
            end
        end
    end, 0)
    -- Registered callback
    data.player = Player
    data.game_loaded = player_previously_activated
    for callback, context in pairs(registered_event_callbacks[data.game_event_name]) do
        if context ~= true then
            callback(context, data)
        else
            callback(data)
        end
    end
    StopListeningToGameEvent(listenEventPlayerActivateID)
end
listenEventPlayerActivateID = ListenToGameEvent("player_activate", listenEventPlayerActivate, _G)

---@class PLAYER_EVENT_ITEM_PICKUP : GAME_EVENT_ITEM_PICKUP
---@field item EntityHandle # The entity handle of the item that was picked up.
---@field item_class string # Classname of the entity that was picked up.
---@field hand CPropVRHand # The entity handle of the hand that picked up the item.
---@field hand_opposite CPropVRHand # The entity handle of the opposite hand.

---Tracking player held items.
---@param data GAME_EVENT_ITEM_PICKUP
local function listenEventItemPickup(data)
    -- print("\nITEM PICKUP:")
    -- util.PrintTable(data)
    -- print("\n")
    if data.vr_tip_attachment == nil then return end
    -- 1=primary,2=secondary converted to 0=left,1=right
    local handId = util.GetHandIdFromTip(data.vr_tip_attachment)
    local hand = Player.Hands[handId + 1]
    local hand_opposite = Player.Hands[(1 - handId) + 1]
    local ent_held = util.EstimateNearestEntity(data.item_name, data.item, hand:GetOrigin())

    hand.ItemHeld = ent_held
    hand.LastItemGrabbed = ent_held
    hand.LastClassGrabbed = data.item
    Player.LastItemGrabbed = ent_held
    Player.LastClassGrabbed = data.item

    if data.item == "item_hlvr_crafting_currency_small" or data.item == "item_hlvr_crafting_currency_large" then
        local resin = Player.Items.resin
        local resin_removed = resin - Player:GetResin()
        -- Determine if player has actually removed resin from backpack
        if resin_removed > 0 then
            -- Give the resin a flag so it's not counted towards found resin if stored again
            ent_held:SetContextNum("resin_taken_from_backpack", 1, 0)
        end
    end

    -- If the item being dropped was in the opposite hand we can assume it isn't anymore.
    -- This might not be needed because item_released is fired when taken by other hand.
    if hand_opposite.ItemHeld == hand.ItemHeld then
        hand_opposite.ItemHeld = nil
    end
    -- Registered callback
---@diagnostic disable-next-line: assign-type-mismatch
    data.item = ent_held
    data.item_class = data.item
    data.hand = hand
    data.hand_opposite = hand_opposite
    for callback, context in pairs(registered_event_callbacks[data.game_event_name]) do
        if context ~= true then
            callback(context, data)
        else
            callback(data)
        end
    end
end
ListenToGameEvent("item_pickup", listenEventItemPickup, nil)

---@class PLAYER_EVENT_ITEM_RELEASED : GAME_EVENT_ITEM_RELEASED
---@field item EntityHandle # The entity handle of the item that was dropped.
---@field item_class string # Classname of the entity that was dropped.
---@field hand CPropVRHand # The entity handle of the hand that dropped the item.
---@field hand_opposite CPropVRHand # The entity handle of the opposite hand.

-- ---@type "item_hlvr_crafting_currency_small"|"item_hlvr_crafting_currency_large"|nil
---@type EntityHandle|nil
local last_resin_dropped

---Item released event
---@param data GAME_EVENT_ITEM_RELEASED
local function listenEventItemReleased(data)
    -- print("\nITEM RELEASED:")
    -- util.PrintTable(data)
    -- print("\n")
    if data.vr_tip_attachment == nil then return end
    -- 1=primary,2=secondary converted to 0=left,1=right
    local handId = util.GetHandIdFromTip(data.vr_tip_attachment)
    local hand = Player.Hands[handId + 1]
    local hand_opposite = Player.Hands[(1 - handId) + 1]
    -- Hack to get the number of shells dropped
    if data.item == "item_hlvr_clip_shotgun_shellgroup" then
        shellgroup_cache = #hand.ItemHeld:GetChildren()
    elseif data.item == "item_hlvr_crafting_currency_small" or data.item == "item_hlvr_crafting_currency_large" then
        last_resin_dropped = hand.ItemHeld
    end
    Player.LastItemDropped = hand.ItemHeld
    Player.LastClassDropped = data.item
    hand.LastItemDropped = hand.ItemHeld
    hand.LastClassDropped = data.item
    hand.ItemHeld = nil
    -- Registered callback
    data.item_class = data.item
---@diagnostic disable-next-line: assign-type-mismatch
    data.item = Player.LastItemDropped
    data.hand = hand
    data.hand_opposite = hand_opposite
    for callback, context in pairs(registered_event_callbacks[data.game_event_name]) do
        if context ~= true then
            callback(context, data)
        else
            callback(data)
        end
    end
end
ListenToGameEvent("item_released", listenEventItemReleased, nil)

---@class PLAYER_EVENT_PRIMARY_HAND_CHANGED : GAME_EVENT_PRIMARY_HAND_CHANGED

---Tracking handedness.
---@param data GAME_EVENT_PRIMARY_HAND_CHANGED
local function listenEventPrimaryHandChanged(data)
    if data.is_primary_left then
        Player.PrimaryHand = Player.LeftHand
        Player.SecondaryHand = Player.RightHand
    else
        Player.PrimaryHand = Player.RightHand
        Player.SecondaryHand = Player.LeftHand
    end
    -- Registered callback
    for callback, context in pairs(registered_event_callbacks[data.game_event_name]) do
        if context ~= true then
            callback(context, data)
        else
            callback(data)
        end
    end
end
ListenToGameEvent("primary_hand_changed", listenEventPrimaryHandChanged, nil)

-- Inherit from base instead of event to remove 'ammoType'
---@class PLAYER_EVENT_PLAYER_DROP_AMMO_IN_BACKPACK : GAME_EVENT_BASE
---@field ammotype "Pistol"|"SMG1"|"Buckshot"|"AlyxGun" # Type of ammo that was stored.
---@field ammo_amount 0|1|2|3|4 # Amount of ammo stored for the given type (1 clip, 2 shells).

---Ammo tracking
---@param data GAME_EVENT_PLAYER_DROP_AMMO_IN_BACKPACK
local function listenEventPlayerDropAmmoInBackpack(data)
    -- print("\nSTORE AMMO:")
    -- util.PrintTable(data)
    -- print("\n")

    --Pistol (energygun)
    --SMG1 (rapidfire)
    --Buckshot (shotgun)
    --AlyxGun (generic pistol)

    -- Sometimes for some reason the key is `ammoType` (capital T), seems to happen when shotgun shell is taken from backpack and put back.
    local ammotype = data.ammotype or data.ammoType
    local ammo_amount = 0
    -- Energygun
    if ammotype == "Pistol" then
        if Player.LastClassDropped == "item_hlvr_clip_energygun_multiple" then
            Player.Items.ammo.energygun = Player.Items.ammo.energygun + 4
            ammo_amount = 4
            -- print("Player stored 4 energygun clips")
        else
            Player.Items.ammo.energygun = Player.Items.ammo.energygun + 1
            ammo_amount = 1
            -- print("Player stored 1 energygun clip")
        end
    -- Rapidfire
    elseif ammotype == "SMG1" then
        Player.Items.ammo.rapidfire = Player.Items.ammo.rapidfire + 1
        ammo_amount = 1
        -- print("Player stored 1 rapidfire clip")
    -- Shotgun
    elseif ammotype == "Buckshot" then
        if Player.LastClassDropped == "item_hlvr_clip_shotgun_multiple" then
            Player.Items.ammo.shotgun = Player.Items.ammo.shotgun + 4
            ammo_amount = 4
            -- print("Player stored 4 shotgun shells")
        elseif Player.LastClassDropped == "item_hlvr_clip_shotgun_shellgroup" then
            -- this can be 2 or 3.. how to figure out??
            Player.Items.ammo.shotgun = Player.Items.ammo.shotgun + shellgroup_cache--2--3
            ammo_amount = shellgroup_cache
            -- print("Player stored "..shellgroup_cache .." shotgun shells")
        else
            Player.Items.ammo.shotgun = Player.Items.ammo.shotgun + 1
            ammo_amount = 1
            -- print("Player stored 1 shotgun shell")
        end
    -- Generic pistol
    elseif ammotype == "AlyxGun" then
        if Player.LastClassDropped == "item_hlvr_clip_generic_pistol_multiple" then
            Player.Items.ammo.generic_pistol = Player.Items.ammo.generic_pistol + 4
            ammo_amount = 4
            -- print("Player stored 4 generic pistol clips")
        else
            Player.Items.ammo.generic_pistol = Player.Items.ammo.generic_pistol + 1
            ammo_amount = 1
            -- print("Player stored 1 generic pistol clip")
        end
    else
        print("Couldn't figure out ammo for "..tostring(ammotype))
    end
    savePlayerData()
    -- Registered callback
    data.ammotype = ammotype
    data.ammo_amount = ammo_amount
    for callback, context in pairs(registered_event_callbacks[data.game_event_name]) do
        if context ~= true then
            callback(context, data)
        else
            callback(data)
        end
    end
end
ListenToGameEvent("player_drop_ammo_in_backpack", listenEventPlayerDropAmmoInBackpack, nil)

-- Inherit from base instead of event to remove 'ammoType'
---@class PLAYER_EVENT_PLAYER_RETRIEVED_BACKPACK_CLIP : GAME_EVENT_BASE
---@field ammotype "Pistol"|"SMG1"|"Buckshot"|"AlyxGun" # Type of ammo that was retrieved.
---@field ammo_amount integer # Amount of ammo retrieved for the given type (1 clip, 2 shells).

---Ammo tracking
---@param data GAME_EVENT_PLAYER_RETRIEVED_BACKPACK_CLIP
local function listenEventPlayerRetrievedBackpackClip(data)
    -- print("\nRETRIEVE AMMO:")
    -- util.PrintTable(data)
    -- print("\n")

    local do_callback = true
    local ammotype = player_weapon_to_ammotype[Player.CurrentlyEquipped]
    local ammo_amount = 0
    if Player.CurrentlyEquipped == PLAYER_WEAPON_ENERGYGUN
    or Player.CurrentlyEquipped == PLAYER_WEAPON_HAND
    or Player.CurrentlyEquipped == PLAYER_WEAPON_MULTITOOL then
        Player.Items.ammo.energygun = Player.Items.ammo.energygun - 1
        ammo_amount = 1
    elseif Player.CurrentlyEquipped == PLAYER_WEAPON_RAPIDFIRE then
        Player.Items.ammo.rapidfire = Player.Items.ammo.rapidfire - 1
        ammo_amount = 1
    elseif Player.CurrentlyEquipped == PLAYER_WEAPON_SHOTGUN then
        do_callback = false
        -- Delayed think is used because item_pickup is fired after this event
        Player:SetContextThink("delay_shotgun_shellgroup", function()
            -- Player always retrieves a shellgroup even if no autoloader and single shell
            -- checking just in case
            ammo_amount = #Player.LastItemGrabbed:GetChildren()
            if Player.LastClassGrabbed == "item_hlvr_clip_shotgun_shellgroup" then
                Player.Items.ammo.shotgun = Player.Items.ammo.shotgun - ammo_amount
            end
            -- Registered callback
            data.ammotype = ammotype
            data.ammo_amount = ammo_amount
            for callback, context in pairs(registered_event_callbacks[data.game_event_name]) do
                if context ~= true then
                    callback(context, data)
                else
                    callback(data)
                end
            end
        end, 0)
    elseif Player.CurrentlyEquipped == PLAYER_WEAPON_GENERIC_PISTOL then
        Player.Items.ammo.generic_pistol = Player.Items.ammo.generic_pistol - 1
        ammo_amount = 1
    end
    savePlayerData()
    -- Registered callback
    if do_callback then
        data.ammotype = ammotype
        data.ammo_amount = ammo_amount
        for callback, context in pairs(registered_event_callbacks[data.game_event_name]) do
            if context ~= true then
                callback(context, data)
            else
                callback(data)
            end
        end
    end
end
ListenToGameEvent("player_retrieved_backpack_clip", listenEventPlayerRetrievedBackpackClip, nil)

---@class PLAYER_EVENT_PLAYER_STORED_ITEM_IN_ITEMHOLDER : GAME_EVENT_PLAYER_STORED_ITEM_IN_ITEMHOLDER

---Wrist tracking
---@param data GAME_EVENT_PLAYER_STORED_ITEM_IN_ITEMHOLDER
local function listenEventPlayerStoredItemInItemholder(data)
    -- print("\nSTORE WRIST:")
    -- util.PrintTable(data)
    -- print("\n")

    if data.item == "item_hlvr_grenade_frag" then
        Player.Items.grenades.frag = Player.Items.grenades.frag + 1
    elseif data.item == "item_hlvr_grenade_xen" then
        Player.Items.grenades.xen = Player.Items.grenades.xen + 1
    elseif data.item == "item_healthvial" then
        Player.Items.healthpen = Player.Items.healthpen + 1
    end
    savePlayerData()
    -- Registered callback
    for callback, context in pairs(registered_event_callbacks[data.game_event_name]) do
        if context ~= true then
            callback(context, data)
        else
            callback(data)
        end
    end
end
ListenToGameEvent("player_stored_item_in_itemholder", listenEventPlayerStoredItemInItemholder, nil)

---@class PLAYER_EVENT_PLAYER_REMOVED_ITEM_FROM_ITEMHOLDER : GAME_EVENT_PLAYER_REMOVED_ITEM_FROM_ITEMHOLDER

---Tracking wrist items
---@param data GAME_EVENT_PLAYER_REMOVED_ITEM_FROM_ITEMHOLDER
local function listenEventPlayerRemovedItemFromItemholder(data)
    -- print("\nREMOVE FROM WRIST:")
    -- util.PrintTable(data)
    -- print("\n")

    if data.item == "item_hlvr_grenade_frag" then
        Player.Items.grenades.frag = Player.Items.grenades.frag - 1
    elseif data.item == "item_hlvr_grenade_xen" then
        Player.Items.grenades.xen = Player.Items.grenades.xen - 1
    elseif data.item == "item_healthvial" then
        Player.Items.healthpen = Player.Items.healthpen - 1
    end
    savePlayerData()
    -- Registered callback
    for callback, context in pairs(registered_event_callbacks[data.game_event_name]) do
        if context ~= true then
            callback(context, data)
        else
            callback(data)
        end
    end
end
ListenToGameEvent("player_removed_item_from_itemholder", listenEventPlayerRemovedItemFromItemholder, nil)

-- No known way to track resin being taken out reliably.

---Track resin
---@param data GAME_EVENT_PLAYER_DROP_RESIN_IN_BACKPACK
local function listenEventPlayerDropResinInBackpack(data)
    -- print("\nSTORE RESIN:")
    -- util.PrintTable(data)
    -- print("\n")

    local resin = Player.Items.resin
                        -- this updates
    local resin_added = Player:GetResin() - resin
    -- This makes sure only newly found resin is counted
    if last_resin_dropped
    and IsEntity(last_resin_dropped, true)
    and not last_resin_dropped:GetContext("resin_taken_from_backpack")
    then
        Player.Items.resin_found = Player.Items.resin_found + resin_added
    end
    last_resin_dropped = nil
end
ListenToGameEvent("player_drop_resin_in_backpack", listenEventPlayerDropResinInBackpack, nil)

---@class PLAYER_EVENT_WEAPON_SWITCH : GAME_EVENT_WEAPON_SWITCH

---Track weapon equipped
---@param data GAME_EVENT_WEAPON_SWITCH
local function listenEventWeaponSwitch(data)
    -- print("\nWEAPON SWITCH:")
    -- util.PrintTable(data)
    -- print("\n")

    Player.PreviouslyEquipped = Player.CurrentlyEquipped
    if data.item == "hand_use_controller" then Player.CurrentlyEquipped = PLAYER_WEAPON_HAND
    elseif data.item == "hlvr_weapon_energygun" then Player.CurrentlyEquipped = PLAYER_WEAPON_ENERGYGUN
    elseif data.item == "hlvr_weapon_rapidfire" then Player.CurrentlyEquipped = PLAYER_WEAPON_RAPIDFIRE
    elseif data.item == "hlvr_weapon_shotgun" then Player.CurrentlyEquipped = PLAYER_WEAPON_SHOTGUN
    elseif data.item == "hlvr_multitool" then Player.CurrentlyEquipped = PLAYER_WEAPON_MULTITOOL
    elseif data.item == "hlvr_weapon_generic_pistol" then Player.CurrentlyEquipped = PLAYER_WEAPON_GENERIC_PISTOL
    end

    -- Registered callback
    for callback, context in pairs(registered_event_callbacks[data.game_event_name]) do
        if context ~= true then
            callback(context, data)
        else
            callback(data)
        end
    end
end
ListenToGameEvent("weapon_switch", listenEventWeaponSwitch, nil)
