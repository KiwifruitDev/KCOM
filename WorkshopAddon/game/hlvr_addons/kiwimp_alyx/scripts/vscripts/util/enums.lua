
-- Missing enumerations for CBasePlayer:GetAnalogActionPositionForHand.

---X axis only.
---@type number
ANALOG_INPUT_HAND_CURL           = 0
---X axis only.
---@type number
ANALOG_INPUT_TRIGGER_PULL        = 1
---X axis only.
---@type number
ANALOG_INPUT_SQUEEZE_XEN_GRENADE = 2
---X and Y axis.
---@type number
ANALOG_INPUT_TELEPORT_TURN       = 3
---X and Y axis.
---@type number
ANALOG_INPUT_CONTINOUS_TURN      = 4

--Missing enumerations for CBasePlayer:GetVRControllerType()

---@type number
VR_CONTROLLER_TYPE_UNKNOWN           = 0
---@type number
VR_CONTROLLER_TYPE_X360              = 1
---@type number
VR_CONTROLLER_TYPE_VIVE              = 2
---@type number
VR_CONTROLLER_TYPE_TOUCH             = 3
---@type number
VR_CONTROLLER_TYPE_RIFT_S            = 4
---@type number
VR_CONTROLLER_TYPE_KNUCKLES          = 6
---@type number
VR_CONTROLLER_TYPE_WINDOWSMR         = 7
---@type number
VR_CONTROLLER_TYPE_WINDOWSMR_SAMSUNG = 8
---@type number
VR_CONTROLLER_TYPE_GENERIC_TRACKED   = 9
---@type number
VR_CONTROLLER_TYPE_COSMOS            = 10

-- Missing enumerations for CBasePlayer:IsDigitalActionOnForHand.

---@type number
DIGITAL_INPUT_TOGGLE_MENU        = 0
---@type number
DIGITAL_INPUT_MENU_INTERACT      = 1
---@type number
DIGITAL_INPUT_MENU_DISMISS       = 2
---@type number
DIGITAL_INPUT_USE                = 3
---@type number
DIGITAL_INPUT_USE_GRIP           = 4
---@type number
DIGITAL_INPUT_SHOW_INVENTORY     = 5
---@type number
DIGITAL_INPUT_GRAV_GLOVE_LOCK    = 6
---@type number
DIGITAL_INPUT_FIRE               = 7
---@type number
DIGITAL_INPUT_ALT_FIRE           = 8
---@type number
DIGITAL_INPUT_RELOAD             = 9
---@type number
DIGITAL_INPUT_EJECT_MAGAZINE     = 10
---@type number
DIGITAL_INPUT_SLIDE_RELEASE      = 11
---@type number
DIGITAL_INPUT_OPEN_CHAMBER       = 12
---@type number
DIGITAL_INPUT_TOGGLE_LASER_SIGHT = 13
---@type number
DIGITAL_INPUT_TOGGLE_BURST_FIRE  = 14
---@type number
DIGITAL_INPUT_TOGGLE_HEALTH_PEN  = 15
---@type number
DIGITAL_INPUT_ARM_GRENADE        = 16
---@type number
DIGITAL_INPUT_ARM_XEN_GRENADE    = 17
---@type number
DIGITAL_INPUT_TELEPORT           = 18
---@type number
DIGITAL_INPUT_TURN_LEFT          = 19
---@type number
DIGITAL_INPUT_TURN_RIGHT         = 20
---@type number
DIGITAL_INPUT_MOVE_BACK          = 21
---@type number
DIGITAL_INPUT_WALK               = 22
---@type number
DIGITAL_INPUT_JUMP               = 23
---@type number
DIGITAL_INPUT_MANTLE             = 24
---@type number
DIGITAL_INPUT_CROUCH_TOGGLE      = 25
---@type number
DIGITAL_INPUT_STAND_TOGGLE       = 26
---@type number
DIGITAL_INPUT_ADJUST_HEIGHT      = 27

-- Missing particle attachment enumerations.

---@type number
PATTACH_INVALID             = -1
---@type number
PATTACH_MAIN_VIEW           = 11
---@type number
PATTACH_WATERWAKE           = 12
---@type number
PATTACH_CENTER_FOLLOW       = 13
---@type number
PATTACH_CUSTOM_GAME_STATE_1 = 14

-- Missing enumerations used by Entity:AddEffects, Entity:RemoveEffects and Entity:IsEffectActive.

---@type number
EF_BONEMERGE          = 1
---@type number
EF_BRIGHTLIGHT        = 2
---@type number
EF_DIMLIGHT           = 4
---@type number
EF_NOINTERP           = 8
---@type number
EF_NOSHADOW           = 16
---@type number
EF_NODRAW             = 32
---@type number
EF_NORECEIVESHADOW    = 64
---@type number
EF_BONEMERGE_FASTCULL = 128
---@type number
EF_ITEM_BLINK         = 256
---@type number
EF_PARENT_ANIMATES    = 512
---@type number
EF_FOLLOWBONE         = 1024
