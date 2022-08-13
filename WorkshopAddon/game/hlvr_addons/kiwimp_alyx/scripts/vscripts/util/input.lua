--[[
    v0.0.1
    https://github.com/FrostSource/hla_extravaganza

    This needs to be tested in a heavily scripted game to see the efficiency
    of checking every button on both hands every frame.
]]
require "util.player"

Input =
{

    ANALOG =
    {
        HAND_CURL           = 0,
        TRIGGER_PULL        = 1,
        SQUEEZE_XEN_GRENADE = 2,
        TELEPORT_TURN       = 3,
        CONTINUOUS_TURN     = 4
    },

    DIGITAL =
    {
        TOGGLE_MENU         = 0,
        MENU_INTERACT       = 1,
        MENU_DISMISS        = 2,
        USE                 = 3,
        USE_GRIP            = 4,
        SHOW_INVENTORY      = 5,
        GRAV_GLOVE_LOCK     = 6,
        FIRE                = 7,
        ALT_FIRE            = 8,
        RELOAD              = 9,
        EJECT_MAGAZINE      = 10,
        SLIDE_RELEASE       = 11,
        OPEN_CHAMBER        = 12,
        TOGGLE_LASER_SIGHT  = 13,
        TOGGLE_BURST_FIRE   = 14,
        TOGGLE_HEALTH_PEN   = 15,
        ARM_GRENADE         = 16,
        ARM_XEN_GRENADE     = 17,
        TELEPORT            = 18,
        TURN_LEFT           = 19,
        TURN_RIGHT          = 20,
        MOVE_BACK           = 21,
        WALK                = 22,
        JUMP                = 23,
        MANTLE              = 24,
        CROUCH_TOGGLE       = 25,
        STAND_TOGGLE        = 26,
        ADJUST_HEIGHT       = 27
    },

    LastPressed = -1,

    LastReleased = -1,
}

local UPDATE_INTERVAL = 0
local ANALOG_ITER = pairs(Input.ANALOG)
local DIGITAL_ITER = pairs(Input.DIGITAL)

---@type boolean[][]
local pressed_flags = {}
---@type number[][]
local pressed_time = {}
---@type table<any,boolean>[][]
local pressed_contexts = {}
local frame_forgiveness = 0.01

---@type function[][][]
local callbacks = {}

local input_data =
{
    --0 literal
    {
        --0 digital action
        {
            time = 0,
            pressed_callbacks = {},
            release_callbacks = {},
        },
        --...
    }
    --...
}

-- Populate tables
for l = 0, 1 do
    -- pressed_flags[l] = {}
    -- pressed_time[l] = {}
    -- pressed_contexts[l] = {}
    -- callbacks[l] = {}
    -- for _, value in DIGITAL_ITER,Input.DIGITAL do
    --     pressed_flags[value] = false
    --     pressed_time[value] = 0
    -- end
    for _, value in DIGITAL_ITER,Input.DIGITAL do
        input_data[l][value].time = 0
        input_data[l][value].pressed_callbacks = {}
        input_data[l][value].release_callbacks = {}
    end
end

---Resolve a value into a hand literal.
---@param hand integer|EntityHandle|nil
---@return "0"|"1"
local function resolveHand(hand)
    if not hand then
        return Player.PrimaryHand.Literal
    elseif not type(hand)=="number" then
        return hand.Literal
    end
    return hand
end

function Input.RegisterPressCallback(digital_button, hand, callback)
end

function Input.RegisterReleaseCallback(digital_button, hand, callback)
end

---Set the amount of seconds after which button is longer considered "just pressed".
---Higher values are better if your think functions are not returned every frame.
---Should be at least as high as your highest think return where you are checking buttons.
---@param forgiveness number # Seconds, default is `0.01`
function Input.SetFrameForgiveness(forgiveness)
    -- frame_forgiveness = math.max(1, math.floor(forgiveness))
    frame_forgiveness = math.max(0, forgiveness)
end

---Get the amount of time in seconds a button has been pressed for.
---@param digital_button integer|"Input.DIGITAL."
---@param hand? integer|EntityHandle|nil # Can be hand literal, entity, or nil to use primary.
function Input.GetPressTime(digital_button, hand)
    hand = resolveHand(hand)
    -- return Time() - pressed_time[hand][digital_button]
    if input_data[hand][digital_button].time > -1 then
        return Time() - input_data[hand][digital_button].time
    else
        return 0
    end
end

---Get if a digital button was pressed.
---This will not return true again until the button is released.
---@param digital_button integer|"Input.DIGITAL."
---@param hand? integer|EntityHandle|nil # Can be hand literal, entity, or nil to use primary.
---@param context? any|"thisEntity" # Used to track pressed state for specific context, pass nil to use player.
---@return boolean
function Input.GetPressed(digital_button, hand, context)
    hand = resolveHand(hand)
    context = context or Player
    if (Time() - pressed_time[hand][digital_button]) <= frame_forgiveness
    and not pressed_contexts[hand][digital_button][context] then
        pressed_contexts[hand][digital_button][context] = true
        return true
    end
    return false
    -- return pressed_flags[digital_button] > 0 and pressed_flags[digital_button] < frame_forgiveness
end

---comment
---@param digital_button integer|"Input.DIGITAL."
---@param hand? integer|EntityHandle|nil # Can be hand literal, entity, or nil to use primary.
function Input.GetHeld(digital_button, hand)
    hand = resolveHand(hand)
    return pressed_flags[digital_button] > 0
end

local function InputThink()

    for i = 0, 1 do
        for _, value in DIGITAL_ITER,Input.DIGITAL do
            local id = input_data[i][value]
            if Player:IsDigitalActionOnForHand(i, value) then
                -- if pressed_flags[value] < frame_forgiveness then
                --     pressed_flags[value] = pressed_flags[value] + 1
                --     -- do callback here
                -- end
                if not pressed_flags[i][value] then
                    Input.LastPressed = value
                    -- pressed_time[l][value] = Time()
                    -- pressed_flags[l][value] = true
                    -- for _, callback in ipairs(callbacks[i][value]) do
                    --     callback(i, value)
                    -- end
                    id.time = Time()
                    for _, callback in ipairs(id.pressed_callbacks) do
                        callback(i, value)
                    end
                end
            else
                -- pressed_flags[value] = 0
                -- -- do callback here
                if pressed_flags[i][value] then
                    Input.LastReleased = value
                    -- pressed_time[i][value] = 0
                    -- pressed_flags[i][value] = false
                    -- pressed_contexts[i][value] = {}
                    -- for _, callback in ipairs(callbacks[i][value]) do
                    --     callback(i, value)
                    -- end
                    id.time = Time()
                    for _, callback in ipairs(id.release_callbacks) do
                        callback(i, value)
                    end
                end
            end
        end
    end

    return UPDATE_INTERVAL
end


--[[ Activating the think ]]

if Player then
    Player:SetThink(InputThink, "InputThink", 0)
else
    local eventId
    local function playerActivate()
        print("player actiavet input", eventId)
        Player:SetThink(InputThink, "InputThink", 0)
    end
    eventId = ListenToGameEvent("player_activate", playerActivate, _G)
end
