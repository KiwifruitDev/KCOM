---@diagnostic disable: lowercase-global, deprecated, undefined-doc-name

--[[
    Version 2.0.0

    This file helps intellisense in editors like Visual Studio Code by
    introducing definitions of all known VLua functions into the global scope.
    It is purely for helping with the coding process and does not introduce
    or modify any functionality in-game.

    I recommend using Visual Studio Code with this Lua extension:
    https://marketplace.visualstudio.com/items?itemName=sumneko.lua

    Place this file in the top level of your scripting folder, e.g.
        game\hlvr_addons\my_addon\scripts\
        or
        game\hlvr_addons\my_addon\scripts\vscripts\

    As long as the editor knows about this file it should assume its contents
    belong in the global scope, even though this file will never be executed.


    Since functions that return an entity handle can't know what type of
    entity is returned, these functions return a type that includes all
    entity classes (EntityHandle).
    You can specify more precise typing using EmmyLua notation:

    ---@type CPhysicsProp
    local box = Entities:FindByName("box")
    -- Code completion is now more accurate, only showing methods that belong
    -- to that specific class
    box:DisableMotion()


    Hook snippets are included in the .vscode folder to help speed up code writing.
    Start typing "hook" or the name of the function for the options to appear (e.g. Activate).


    If code completion isn't working you may need to increase the max file size
    limit for pre-processing.
]]

--#region Aliases/Types

---Combined entity handle type.
---@alias EntityHandle CBaseEntity|CEntityInstance|CBaseModelEntity|CBasePlayer|CHL2_Player|CBaseAnimating|CBaseFlex|CBaseCombatCharacter|CBodyComponent|CAI_BaseNPC|CBaseTrigger|CEnvEntityMaker|CInfoWorldLayer|CLogicRelay|CMarkupVolumeTagged|CEnvProjectedTexture|CPhysicsProp|CSceneEntity|CPointClientUIWorldPanel|CPointTemplate|CPointWorldText|CPropHMDAvatar|CPropVRHand

---@class EHANDLE
---@alias ScriptScope table
---@alias COMMON_OPVAR_NAMES "skylight_proximity_array"|"skylight_invert_scalar_array"|"hotel_sub_basement_proximity_array"|"large_room_addin_array"|"small_room_invert_scalar_array"|"hotel_small_room_proximity_array"|"xen_proximity_array"

---@type EntityHandle
thisEntity = nil

---@class TypeLocalTimeTable
---@field Hours number
---@field Minutes number
---@field Seconds number

---@alias SHAKE_COMMAND
---| 0 # SHAKE_START
---| 1 # SHAKE_STOP

---@class TypeIOInvoke
---@field activator EntityHandle
---@field caller EntityHandle

---@class TypeDamageTable
---@field inflictor EntityHandle
---@field attacker EntityHandle
---@field damage_direction Vector
---@field damage_position Vector
---@field damage_force Vector
---@field damage integer

---@class TraceTableBase
---@field startpos Vector # Global vector where to start the trace.
---@field endpos Vector # Global vector where to end the trace.
---@field pos Vector # Global vector where the trace hit.
---@field fraction number # Fraction from the start to end where the trace hit.
---@field hit boolean # Whether the trace hit something. Always present.
---@field startsolid boolean # Whether the trace started inside the entity. This parameter is set to nil if it is false.
---@field normal Vector # Global normal vector of the surface hit.

---@class TraceTableCollideable : TraceTableBase
---@field ent EntityHandle # Entity to trace against.
---@field mins Vector # (Optional) Minimum coordinates of the bounding box. Local to the entity.
---@field maxs Vector # (Optional) Maximum coordinates of the bounding box. Local to the entity.

---@class TraceTableHull : TraceTableBase
---@field min Vector # Minimum extents of the bounding box.
---@field max Vector # Maximum extents of the bounding box.
---@field mask integer # Collision type bitmask.
---@field ignore EntityHandle # Entity to ignore when tracing.
---@field enthit EntityHandle # Handle of the entity the trace hit.

---@class TraceTableLine : TraceTableBase
---@field mask integer # Collision type bitmask.
---@field ignore EntityHandle # Entity to ignore when tracing.
---@field enthit EntityHandle # Handle of the entity the trace hit.

--#region Game Events

-- A case can be made more removing many of these events that have no use in Alyx.
---@alias GAME_EVENTS_HLVR
---**item (string)** *Item classname.*
---
---**item_name (string)** *Item targetname.*
---
---**wasparentedto (string)** *Unknown.*
---
---**vr_tip_attachment (number)** *Hand that grabbed, 1 = left, 2 = right (reversed if left handed).*
---
---**otherhand_vr_tip_attachment (number)** *Other hand that grabbed.*
---
---**controller_type (number)** *Type of controller used (see ENUM_CONTROLLER_TYPES).*
---| "\"item_pickup\"" # Player grabs an object with hand.
---**item (string)** *Item classname.*
---
---**item_name (string)** *Item targetname.*
---
---**vr_tip_attachment (number)** *Hand that grabbed, 1 = left, 2 = right (reversed if left handed).*
---| "\"item_released\"" # Player drops an object from hand.
---| "\"item_attachments\"" # Unknown.
---Player switches weapon.
---
---**item (string)** *Weapon class 'hand_use_controller', 'hlvr_weapon_energygun', 'hlvr_weapon_rapidfire', 'hlvr_weapon_shotgun', 'hlvr_multitool'.*
---| "\"weapon_switch\""
---| "\"grabbity_glove_pull\"" # Player pulls object with glove.
---| "\"grabbity_glove_catch\"" # Player grabs an object after pulling it with glove.
---| "\"grabbity_glove_highlight_start\"" # Grabbity glove starts highlighting an object.
---| "\"grabbity_glove_highlight_stop\"" # Grabbity glove stops highlighting an object.
---| "\"grabbity_glove_locked_on_start\"" # Player locks onto object with glove.
---| "\"grabbity_glove_locked_on_stop\"" # Player stops locking onto object with glove.
---| "\"player_gestured\"" # Player gestures with hand.
---| "\"player_shoot_weapon\"" # Player shot any weapon.
---| "\"player_teleport_start\"" # Player starts the teleporting process (when button is held or released?).
---| "\"player_teleport_finish\"" # Player finishes the teleporting process.
---| "\"player_picked_up_weapon_off_hand\"" # Player grabs usable weapon with non-dominant hand.
---| "\"player_picked_up_weapon_off_hand_crafting\"" # Player grabs weapon with non-dominant hand from crafting cradle.
---| "\"player_eject_clip\"" # Player ejects the magazine from the pistol.
---| "\"player_armed_grenade\"" # Player arms any(?) grenade.
---| "\"player_health_pen_prepare\"" # Player reveals a health pen needle.
---| "\"player_health_pen_retract\"" # Player retracts a health pen needle.
---| "\"player_health_pen_used\"" # Player injects a health pen.
---| "\"player_pistol_empty_clip\"" #
---| "\"player_pistol_clip_inserted\"" #
---| "\"player_pistol_empty_chamber\"" #
---| "\"player_pistol_chambered_round\"" #
---| "\"player_pistol_slide_lock\"" #
---| "\"player_pistol_bought_lasersight\"" #
---| "\"player_pistol_toggle_lasersight\"" #
---| "\"player_pistol_bought_burstfire\"" #
---| "\"player_pistol_toggle_burstfire\"" #
---| "\"player_pistol_pickedup_charged_clip\"" #
---| "\"player_pistol_armed_charged_clip\"" #
---| "\"player_pistol_clip_charge_ended\"" #
---Player grabs weapon ammo from backpack. This does not have any key to determine ammo type.
---Use weapon_switch event to track held weapon to determine ammo type retrieved.
---Fires *before* item_pickup event.
---| "\"player_retrieved_backpack_clip\""
---Player stores any ammo type in backpack.
---
---**ammotype (string)** *Name of ammo type 'Pistol', 'SMG1', 'Buckshot', 'AlyxGun'.*
---Sometimes for some reason the key is `ammoType` (capital T), seems to happen when shotgun shell is taken from backpack and put back.
---| "\"player_drop_ammo_in_backpack\""
---| "\"player_drop_resin_in_backpack\"" # Player stores resin in backpack.
---| "\"player_using_healthstation\"" #
---| "\"health_station_open\"" #
---| "\"player_looking_at_wristhud\"" #
---| "\"player_shotgun_shell_loaded\"" #
---| "\"player_shotgun_state_changed\"" #
---| "\"player_shotgun_upgrade_grenade_launcher_state\"" #
---| "\"player_shotgun_autoloader_state\"" #
---| "\"player_shotgun_autoloader_shells_added\"" #
---| "\"player_shotgun_upgrade_quickfire\"" #
---| "\"player_shotgun_is_ready\"" #
---| "\"player_shotgun_open\"" #
---| "\"player_shotgun_loaded_shells\"" #
---| "\"player_shotgun_upgrade_grenade_long\"" #
---| "\"player_rapidfire_capsule_chamber_empty\"" #
---| "\"player_rapidfire_cycled_capsule\"" #
---| "\"player_rapidfire_magazine_empty\"" #
---| "\"player_rapidfire_opened_casing\"" #
---| "\"player_rapidfire_closed_casing\"" #
---| "\"player_rapidfire_inserted_capsule_in_chamber\"" #
---| "\"player_rapidfire_inserted_capsule_in_magazine\"" #
---| "\"player_rapidfire_upgrade_selector_can_use\"" #
---| "\"player_rapidfire_upgrade_selector_used\"" #
---| "\"player_rapidfire_upgrade_can_charge\"" #
---| "\"player_rapidfire_upgrade_can_not_charge\"" #
---| "\"player_rapidfire_upgrade_fully_charged\"" #
---| "\"player_rapidfire_upgrade_not_fully_charged\"" #
---| "\"player_rapidfire_upgrade_fired\"" #
---| "\"player_rapidfire_energy_ball_can_charge\"" #
---| "\"player_rapidfire_energy_ball_fully_charged\"" #
---| "\"player_rapidfire_energy_ball_not_fully_charged\"" #
---| "\"player_rapidfire_energy_ball_can_pick_up\"" #
---| "\"player_rapidfire_energy_balls_picked_up\"" #
---| "\"player_rapidfire_stun_grenade_ready\"" #
---| "\"player_rapidfire_stun_grenade_not_ready\"" #
---| "\"player_rapidfire_stun_grenade_picked_up\"" #
---| "\"player_rapidfire_explode_button_ready\"" #
---| "\"player_rapidfire_explode_button_not_ready\"" #
---| "\"player_rapidfire_explode_button_pressed\"" #
---| "\"game_saved\"" # Game saves (does this trigger on autosaves?)
---| "\"player_attempted_invalid_storage\"" # Player tried to store arbitrary prop in backpack (does this fire for clips too?)
---| "\"player_attempted_invalid_pistol_clip_storage\"" # Player tried to store pistol magazine in backpack.
---| "\"opened_weapon_switch\"" # Player opened the weapon switch menu.
---| "\"player_started_2h_levitate\"" # is this ladders?
---Player put item in wrist pocket. Fires just after item_released.
---
---**item (string)** *Item classname.*
---
---**item_name (string)** *Item targetname.*
---| "\"player_stored_item_in_itemholder\""
---Player took item from wrist pocket. Fires just after item_pickup event.
---
---**item (string)** *Item classname.*
---
---**item_name (string)** *Item targetname.*
---
---**vr_tip_attachment (number)** *Hand that grabbed, 1 = left, 2 = right (reversed if left handed).*
---| "\"player_removed_item_from_itemholder\""
---| "\"player_picked_up_flashlight\"" #
---| "\"player_picked_up_flashlight_single_controller\"" #
---| "\"player_attached_flashlight\"" #
---| "\"two_hand_pistol_grab_start\"" #
---| "\"two_hand_pistol_grab_end\"" #
---| "\"two_hand_rapidfire_grab_start\"" #
---| "\"two_hand_rapidfire_grab_end\"" #
---| "\"two_hand_shotgun_grab_start\"" #
---| "\"two_hand_shotgun_grab_end\"" #
---| "\"health_pen_teach_storage\"" # what happens if you trigger these?
---| "\"health_vial_teach_storage\"" #
---| "\"player_opened_game_menu\"" # does this fire for client or server?
---| "\"player_closed_game_menu\"" #
---| "\"player_pickedup_storable_clip\"" #
---| "\"player_pickedup_insertable_clip\"" #
---| "\"player_covered_mouth\"" #
---| "\"player_upgrade_weapon\"" #
---| "\"soldier_killed_by_gastank_explosion\"" #
---| "\"charger_killed_while_shield_up\"" #
---| "\"steal_xen_grenade\"" #
---| "\"tripmine_hack_started\"" #
---| "\"tripmine_hacked\"" #
---is_primary_left (number)
---| "\"primary_hand_changed\""
---| "\"close_to_blindzombie\"" #
---| "\"player_grabbed_by_barnacle\"" #
---| "\"player_released_by_barnacle\"" #
---| "\"single_controller_mode_changed\"" #
---| "\"movement_hand_changed\"" #
---| "\"npc_ragdoll_created\"" # Fired when an npc is killed and transitioning to ragdoll.
---| "\"friendly_npc_spawned\"" #
---| "\"combine_tank_moved_by_player\"" #
---| "\"change_level_activated\"" #
---| "\"save_game_loaded\"" #
---| "\"player_quick_turned\"" #
---| "\"game_option_changed\"" #
---| "\"barnacle_grabbed_zombie\"" # test headcrab and combine
---| "\"barnacle_grabbed_grenade\"" #
---| "\"barnacle_killed_by_grenade\"" #
---| "\"zombie_killed_by_grenade\"" #
---| "\"player_continuous_jump_finish\"" #
---| "\"player_continuous_mantle_finish\"" #
---| "\"player_crouch_toggle_finish\"" #
---| "\"player_stand_toggle_finish\"" #
---| "\"player_grabbed_ladder\"" #
---| "\"commentary_started\"" #
---| "\"commentary_stopped\"" #
---| "\"vr_controller_hint_create\"" # can create hints?

---@alias GAME_EVENTS_CORE
---| "\"server_spawn\"" # As soon as a server starts.
---| "\"server_pre_shutdown\"" # Server is about to be shut down.
---| "\"server_shutdown\"" # Server shut down.
---| "\"server_message\"" # A generic server message.
---| "\"server_cvar\"" # A server console var has changed.
---| "\"server_addban\"" #
---| "\"server_removeban\"" #
---| "\"player_activate\"" #
---| "\"player_connect_full\"" # Player has sent final message in the connection sequence.
---| "\"player_say\"" #
---| "\"player_full_update\"" #
---| "\"player_connect\"" # A new client connected.
---| "\"player_disconnect\"" # A client was disconnected.
---| "\"player_info\"" # A player changed his name.
---| "\"player_spawn\"" # Player spawned in game.
---| "\"player_team\"" # Player change his team.
---| "\"local_player_team\"" # Sent only on the clientfor the local player - happens only after the local players team variable has been updated.
---| "\"player_changename\"" #
---| "\"player_class\"" # A player changed his class.
---| "\"player_score\"" # Players score changed.
---| "\"player_hurt\"" #
---| "\"player_shoot\"" # Player shoot his weapon.
---| "\"player_chat\"" # A public player chat.
---| "\"teamplay_broadcast_audio\"" # Emits a sound to everyone on a team.
---| "\"finale_start\"" #
---| "\"player_stats_updated\"" #
---| "\"user_data_downloaded\"" # Fired when achievements/stats are downloaded from Steam or XBox Live.
---| "\"ragdoll_dissolved\"" #
---| "\"team_info\"" # Info about team.
---| "\"team_score\"" # Team score changed.
---| "\"hltv_cameraman\"" # A spectator/player is a cameraman.
---| "\"hltv_chase\"" # Shot of a single entity.
---| "\"hltv_rank_camera\"" # A camera ranking.
---| "\"hltv_rank_entity\"" # An entity ranking.
---| "\"hltv_fixed\"" # Show from fixed view.
---| "\"hltv_message\"" # A HLTV message send by moderators.
---| "\"hltv_statis\"" # General HLTV status.
---| "\"hltv_title\"" #
---| "\"hltv_chat\"" # A HLTV chat msg sent by spectators.
---| "\"hltv_versioninfo\"" #
---| "\"demo_start\"" #
---| "\"demo_stop\"" #
---| "\"demo_skip\"" #
---| "\"map_shutdown\"" #
---| "\"map_transition\"" #
---| "\"hostname_changed\"" #
---| "\"difficulty_changed\"" #
---| "\"game_message\"" # A message send by game logic to everyone.
---| "\"game_newmap\"" # Send when new map is completely loaded.
---| "\"round_start\"" #
---| "\"round_end\"" #
---| "\"round_start_pre_entity\"" #
---| "\"round_start_post_nav\"" #
---| "\"round_freeze_end\"" #
---| "\"teamplay_round_start\"" # Round restart.
---| "\"player_death\"" #
---| "\"player_footstep\"" #
---| "\"player_hintmessage\"" #
---| "\"beak_breakable\"" #
---| "\"break_prop\"" #
---| "\"entity_killed\"" #
---| "\"door_open\"" #
---| "\"door_close\"" #
---| "\"door_unlocked\"" #
---| "\"vote_started\"" #
---| "\"vote_failed\"" #
---| "\"vote_passed\"" #
---| "\"vote_changed\"" #
---| "\"vote_cast_yes\"" #
---| "\"vote_cast_no\"" #
---| "\"achievement_event\"" #
---| "\"achievement_earned\"" #
---| "\"achievement_write_failed\"" # Used for a notification message when an achievement fails to write.
---| "\"bonus_updated\"" #
---| "\"spec_target_updated\"" #
---| "\"entity_visible\"" #
---| "\"player_use_miss\"" # The player pressed use but a use entity wasn't found.
---| "\"gameinstructor_draw\"" #
---| "\"gameinstructor_nodraw\"" #
---| "\"flare_ignite_npc\"" #
---| "\"helicopter_grenade_punt_miss\"" #
---| "\"physgun_pickup\"" #
---| "\"inventory_updated\"" #
---| "\"cart_updated\"" #
---| "\"store_pricesheet_updated\"" #
---| "\"item_schema_initialized\"" #
---| "\"drop_rate_modified\"" #
---| "\"event_ticket_modified\"" #
---| "\"gc_connected\"" #
---| "\"instructor_start_lesson\"" # see if this can create better entity hints
---| "\"instructor_close_lesson\"" #
---| "\"instructor_server_hint_create\"" # Create a hint using data supplied by the server/map. Intended for hints to smooth playtests before content is ready to make the hint unneccessary. NOT INTENDED AS A SHIPPABLE CRUTCH.
---| "\"instructor_server_hint_stop\"" # Destroys a server/map created hint.
---| "\"set_instructor_group_enabled\"" #
---| "\"clientside_lesson_closed\"" #
---| "\"dynamic_shadow_light_changed\"" #

---@alias GAME_EVENTS_ALL GAME_EVENTS_HLVR | GAME_EVENTS_CORE

--#endregion

--#region Game event tables

---@class GAME_EVENT_BASE
---@field game_event_name string
---@field game_event_listener integer
---@field splitscreenplayer integer

---@class GAME_EVENT_ITEM_PICKUP : GAME_EVENT_BASE
    ---@field item string # Item classname.
    ---@field item_name string # Item targetname.
    ---@field wasparentedto string # Unknown if class or targetname.
    ---@field vr_tip_attachment 1|2 # Hand that grabbed, 1 = left, 2 = right (reversed if left handed).
    ---@field otherhand_vr_tip_attachment 1|2 # Other hand that grabbed.
    ---@field controller_type number # Unknown
---@class GAME_EVENT_ITEM_RELEASED : GAME_EVENT_BASE
    ---@field item string # Item classname.
    ---@field item_name string # Item targetname.
    ---@field vr_tip_attachment 1|2 # Hand that grabbed, 1 = left, 2 = right (reversed if left handed).
---@class GAME_EVENT_ITEM_ATTACHMENT : GAME_EVENT_BASE
    ---@field item string
---@class GAME_EVENT_WEAPON_SWITCH : GAME_EVENT_BASE
    ---@field item "hand_use_controller"|"hlvr_weapon_energygun"|"hlvr_weapon_rapidfire"|"hlvr_weapon_shotgun"|"hlvr_multitool"|"hlvr_weapon_generic_pistol"
---@class GAME_EVENT_GRABBITY_GLOVE_PULL : GAME_EVENT_BASE
    ---@field item string
    ---@field item_name string
    ---@field entindex integer
    ---@field hand_is_primary boolean
    ---@field vr_tip_attachment integer
    ---@field wasparentedto string
---@class GAME_EVENT_GRABBITY_GLOVE_CATCH : GAME_EVENT_BASE
    ---@field entindex integer
    ---@field item string
    ---@field hand_is_primary boolean
    ---@field vr_tip_attachment integer
---@class GAME_EVENT_GRABBITY_HIGHLIGHT_START : GAME_EVENT_BASE
    ---@field entindex integer
    ---@field hand_is_primary boolean
    ---@field vr_tip_attachment integer
---@class GAME_EVENT_GRABBITY_HIGHLIGHT_STOP : GAME_EVENT_GRABBITY_HIGHLIGHT_START, GAME_EVENT_BASE
---@class GAME_EVENT_GRABBITY_LOCKED_ON_START : GAME_EVENT_GRABBITY_HIGHLIGHT_START, GAME_EVENT_BASE
---@class GAME_EVENT_GRABBITY_LOCKED_ON_STOP : GAME_EVENT_GRABBITY_HIGHLIGHT_START, GAME_EVENT_BASE
    ---@field highlight_active boolean
---@class GAME_EVENT_PLAYER_GESTURED : GAME_EVENT_BASE
    ---@field item string
---@class GAME_EVENT_PLAYER_SHOOT_WEAPON : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_TELEPORT_START : GAME_EVENT_BASE
    ---@field positionX number
    ---@field positionY number
    ---@field positionZ number
    ---@field map_name string
---@class GAME_EVENT_PLAYER_TELEPORT_FINISH : GAME_EVENT_PLAYER_TELEPORT_START, GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_PICKED_UP_WEAPON_OFF_HAND : GAME_EVENT_BASE
    ---@field picked_up integer
---@class GAME_EVENT_PLAYER_PICKED_UP_WEAPON_OFF_HAND_CRAFTING : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_EJECT_CLIP
    ---@field holding_ammo integer
---@class GAME_EVENT_PLAYER_ARMED_GRENADE : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_HEALTH_PEN_PREPARE : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_HEALTH_PEN_RETRACT : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_HEALTH_PEN_USED : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_PISTOL_EMPTY_CLIP : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_PISTOL_CLIP_INSERTED : GAME_EVENT_BASE
    ---@field bullet_count integer
---@class GAME_EVENT_PLAYER_PISTOL_EMPTY_CHAMBER : GAME_EVENT_PLAYER_PISTOL_CLIP_INSERTED, GAME_EVENT_BASE
    ---@field controller_type integer
---@class GAME_EVENT_PLAYER_PISTOL_CHAMBERED_ROUND : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_PISTOL_SLIDE_LOCK : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_PISTOL_BOUGHT_LASERSIGHT : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_PISTOL_TOGGLE_LASERSIGHT : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_PISTOL_BOUGHT_BURSTFIRE : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_PISTOL_TOGGLE_BURSTFIRE : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_PISTOL_PICKEDUP_CHARGED_CLIP : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_PISTOL_ARMED_CHARGED_CLIP : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_PISTOL_CLIP_CHARGE_ENDED : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RETRIEVED_BACKPACK_CLIP : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_DROP_AMMO_IN_BACKPACK : GAME_EVENT_BASE
    ---@field ammotype "Pistol"|"SMG1"|"Buckshot"|"AlyxGun"
    ---@field ammoType "Pistol"|"SMG1"|"Buckshot"|"AlyxGun" # Sometimes for some reason the key is `ammoType` (capital T), seems to happen when shotgun shell is taken from backpack and put back.
---@class GAME_EVENT_PLAYER_DROP_RESIN_IN_BACKPACK : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_USING_HEALTHSTATION : GAME_EVENT_BASE
---@class GAME_EVENT_HEALTH_STATION_OPEN : GAME_EVENT_BASE -- doesn't have userid (if adding it)
---@class GAME_EVENT_PLAYER_LOOKING_AT_WRISTHUD : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_SHOTGUN_SHELL_LOADED : GAME_EVENT_BASE
    ---@field shellcount integer
    ---@field hint_target integer # Entity ID that the hint should display at.
---@class GAME_EVENT_PLAYER_SHOTGUN_STATE_CHANGED : GAME_EVENT_BASE
    ---@field shotgun_state integer
    ---@field ammo_count integer
    ---@field hint_target integer # Entity ID that the hint should display at.
---@class GAME_EVENT_PLAYER_SHOTGUN_UPGRADE_GRENADE_LAUNCHER_STATE : GAME_EVENT_BASE
    ---@field state integer
    ---@field hint_target integer # Entity ID that the hint should display at.
---@class GAME_EVENT_PLAYER_SHOTGUN_AUTOLOADER_STATE : GAME_EVENT_BASE
    ---@field state integer
    ---@field hint_target integer # Entity ID that the hint should display at.
---@class GAME_EVENT_PLAYER_SHOTGUN_AUTOLOADER_SHELLS_ADDED : GAME_EVENT_BASE
    ---@field shellcount integer
    ---@field hint_target integer # Entity ID that the hint should display at.
---@class GAME_EVENT_PLAYER_SHOTGUN_UPGRADE_QUICKFIRE : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_SHOTGUN_IS_READY : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_SHOTGUN_OPEN : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_SHOTGUN_LOADED_SHELLS : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_SHOTGUN_UPGRADE_GRENADE_LONG : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_CAPSULE_CHAMBER_EMPTY : GAME_EVENT_BASE
    ---@field hint_target integer
---@class GAME_EVENT_PLAYER_RAPIDFIRE_CYCLED_CAPSULE : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_MAGAZINE_EMPTY : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_OPENED_CASING : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_CLOSED_CASING : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_INSERTED_CAPSULE_IN_CHAMBER : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_INSERTED_CAPSULE_IN_MAGAZINE : GAME_EVENT_BASE
    ---@field num_capsules_in_magazine integer
---@class GAME_EVENT_PLAYER_RAPIDFIRE_UPGRADE_SELECTOR_CAN_USE : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_UPGRADE_SELECTOR_USED : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_UPGRADE_CAN_CHARGE : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_UPGRADE_CAN_NOT_CHARGE : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_UPGRADE_FULLY_CHARGED : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_UPGRADE_NOT_FULLY_CHARGED : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_UPGRADE_FIRED : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_ENERGY_BALL_CAN_CHARGE : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_ENERGY_BALL_FULLY_CHARGED : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_ENERGY_BALL_NOT_FULLY_CHARGED : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_ENERGY_BALL_CAN_PICK_UP : GAME_EVENT_BASE
    ---@field hint_target integer
---@class GAME_EVENT_PLAYER_RAPIDFIRE_ENERGY_BALL_PICKED_UP : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_STUN_GRENADE_READY : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_STUN_GRENADE_NOT_READY : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_STUN_GRENADE_PICKED_UP : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_EXPLODE_BUTTON_READY : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_EXPLODE_BUTTON_NOT_READY : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RAPIDFIRE_EXPLODE_BUTTON_PRESSED : GAME_EVENT_BASE
---@class GAME_EVENT_GAME_SAVED : GAME_EVENT_BASE -- doesn't have userid (if adding it)
---@class GAME_EVENT_PLAYER_ATTEMPTED_INVALID_STORAGE : GAME_EVENT_BASE
    ---@field vr_tip_attachment integer
---@class GAME_EVENT_PLAYER_ATTEMPTED_INVALID_PISTOL_CLIP_STORAGE : GAME_EVENT_BASE
    ---@field vr_tip_attachment integer
---@class GAME_EVENT_OPENED_WEAPON_SWITCH : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_STARTED_2H_LEVITATE : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_STORED_ITEM_IN_ITEMHOLDER : GAME_EVENT_BASE
    ---@field item string
    ---@field item_name string
---@class GAME_EVENT_PLAYER_REMOVED_ITEM_FROM_ITEMHOLDER : GAME_EVENT_BASE
    ---@field item string
    ---@field vr_tip_attachment integer
---@class GAME_EVENT_PLAYER_PICKED_UP_FLASHLIGHT : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_PICKED_UP_FLASHLIGHT_SINGLE_CONTROLLER : GAME_EVENT_BASE
    ---@field entindex integer
---@class GAME_EVENT_PLAYER_ATTACHED_FLASHLIGHT : GAME_EVENT_BASE
---@class GAME_EVENT_TWO_HAND_PISTOL_GRAB_START : GAME_EVENT_BASE
---@class GAME_EVENT_TWO_HAND_PISTOL_GRAB_END : GAME_EVENT_BASE
---@class GAME_EVENT_TWO_HAND_RAPIDFIRE_GRAB_START : GAME_EVENT_BASE
---@class GAME_EVENT_TWO_HAND_RAPIDFIRE_GRAB_END : GAME_EVENT_BASE
---@class GAME_EVENT_TWO_HAND_SHOTGUN_GRAB_START : GAME_EVENT_BASE
---@class GAME_EVENT_TWO_HAND_SHOTGUN_GRAB_END : GAME_EVENT_BASE
---@class GAME_EVENT_HEALTH_PEN_TEACH_STORAGE : GAME_EVENT_BASE
    ---@field vr_tip_attachment integer
    ---@field hint_target integer # Entity ID that the hint should display at for single controller mode.
---@class GAME_EVENT_HEALTH_VIAL_TEACH_STORAGE : GAME_EVENT_BASE
    ---@field vr_tip_attachment integer
    ---@field hint_target integer # Entity ID that the hint should display at for single controller mode.
---@class GAME_EVENT_PLAYER_OPENED_GAME_MENU : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_CLOSED_GAME_MENU : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_PICKEDUP_STORABLE_CLIP : GAME_EVENT_BASE
    ---@field vr_tip_attachment integer
    ---@field otherhand_vr_tip_attachment integer
---@class GAME_EVENT_PLAYER_PICKEDUP_INSERTABLE_CLIP : GAME_EVENT_BASE
    ---@field vr_tip_attachment integer
    ---@field otherhand_vr_tip_attachment integer
---@class GAME_EVENT_PLAYER_COVERED_MOUTH : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_UPGRADE_WEAPON : GAME_EVENT_BASE
    ---@field num_upgrades integer
---@class GAME_EVENT_SOLDIER_KILLED_BY_GASTANK_EXPLOSION : GAME_EVENT_BASE -- doesn't have userid (if adding it)
---@class GAME_EVENT_CHARGER_KILLED_WHILE_SHIELD_UP : GAME_EVENT_BASE -- doesn't have userid (if adding it)
---@class GAME_EVENT_STEAL_XEN_GRENADE : GAME_EVENT_BASE -- doesn't have userid (if adding it)
---@class GAME_EVENT_TRIPMINE_HACK_STARTED : GAME_EVENT_BASE
---@class GAME_EVENT_TRIPMINE_HACKED : GAME_EVENT_BASE
---@class GAME_EVENT_PRIMARY_HAND_CHANGED : GAME_EVENT_BASE
    ---@field is_primary_left boolean
---@class GAME_EVENT_CLOSE_TO_BLINDZOMBIE : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_GRABBED_BY_BARNACLE : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_RELEASED_BY_BARNACLE : GAME_EVENT_BASE
---@class GAME_EVENT_SINGLE_CONTROLLER_MODE_CHANGED : GAME_EVENT_BASE
    ---@field is_single_controller_mode boolean
    ---@field is_primary_left boolean
---@class GAME_EVENT_MOVEMENT_HAND_CHANGED : GAME_EVENT_BASE
    ---@field movement_is_primary_hand boolean
---@class GAME_EVENT_NPC_RAGDOLL_CREATED : GAME_EVENT_BASE -- doesn't have userid (if adding it)
    ---@field npc_entindex integer
    ---@field ragdoll_entindex integer
---@class GAME_EVENT_FRIENDLY_NPC_SPAWNED : GAME_EVENT_BASE -- doesn't have userid (if adding it)
    ---@field npc_entindex integer
    ---@field npc_is_friendly boolean
---@class GAME_EVENT_COMBINE_TANK_MOVED_BY_PLAYER : GAME_EVENT_BASE
    ---@field entindex integer
---@class GAME_EVENT_CHANGE_LEVEL_ACTIVATED : GAME_EVENT_BASE -- doesn't have userid (if adding it)
    ---@field map_name string
    ---@field landmark_name string
    ---@field landmark_height number
    ---@field landmark_yaw number
---@class GAME_EVENT_SAVE_GAME_LOADED : GAME_EVENT_BASE -- doesn't have userid (if adding it)
    ---@field sub_directory string
---@class GAME_EVENT_PLAYER_QUICK_TURNED : GAME_EVENT_BASE
    ---@field map_name string
---@class GAME_EVENT_GAME_OPTION_CHANGED : GAME_EVENT_BASE
    ---@field game_option_name string
    ---@field game_option_value string
    ---@field map_name string
---@class GAME_EVENT_BARNACLE_GRABBED_ZOMBIE : GAME_EVENT_BASE -- doesn't have userid (if adding it)
---@class GAME_EVENT_BARNACLE_GRABBED_GRENADE : GAME_EVENT_BASE -- doesn't have userid (if adding it)
---@class GAME_EVENT_BARNACLE_KILLED_BY_GRENADE : GAME_EVENT_BASE -- doesn't have userid (if adding it)
---@class GAME_EVENT_ZOMBIE_KILLED_BY_GRENADE : GAME_EVENT_BASE -- doesn't have userid (if adding it)
---@class GAME_EVENT_PLAYER_CONTINUOUS_JUMP_FINISH : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_CONTINUOUS_MANTLE_FINISH : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_CROUCH_TOGGLE_FINISH : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_STAND_TOGGLE_FINISH : GAME_EVENT_BASE
---@class GAME_EVENT_PLAYER_GRABBED_LADDER : GAME_EVENT_BASE
---@class GAME_EVENT_COMMENTARY_STARTED : GAME_EVENT_BASE
    ---@field title string
    ---@field speaker string
    ---@field nodeid integer
    ---@field nodeidmax integer
    ---@field start_time number
    ---@field end_time number
    ---@field transitioned integer
---@class GAME_EVENT_COMMENTARY_STOPPED : GAME_EVENT_BASE
    ---@field nodeid integer
---@class GAME_EVENT_VR_CONTROLLER_HINT_CREATE : GAME_EVENT_BASE
    ---@field hint_name string # What to name the hint. Referenced against instructor for the proper lesson.
    ---@field hint_dominant_hand boolean # True for dominant hand, false for off hand.



--[[
    Example of using the event table class:

    ---Callback for an item_pickup game event.
    ---@param data GAME_EVENT_ITEM_PICKUP # Table containing event related data.
    function ItemPickupCallback(data)
    end
]]

---Global criteria table for CBaseEntity:GatherCriteria.
---Not all fields will exist for all entities.
---@class CriteriaTable
---@field playerhealth number # Health of the player.
---@field map string # Name of the map file.
---@field in_combat 0|1 # If the NPC is in combat.
---@field current_crafting_currency number # Amount of resin player has.
---@field playeractivity string # Unknown if this is applicable in Alyx, returns `"ACT_RESET"`.
---@field skill.cfg number # Unsure if this changes. Appears to always be `1`.
---@field playerhealthfrac number # The health fraction (health/maxhealth).
---@field episodic number # Always `1`.
---@field playerweapon string|"none" # Name of the weapon used in non-vr.
---@field gordon_precriminal number # Always `0`.
---@field health number # Health of the entity, can be negative.
---@field playerspeed number # In VR this appears to be `0` while moving/teleporting, and quickly climbs to ~continous_speed*2 while stationary.
---@field primaryhand_active_attachment "hand_use_controller"|"hlvr_weapon_energygun"|"hlvr_weapon_shotgun"|"hlvr_weapon_rapidfire"|"hlvr_multitool" # Classname of the weapon in the player's hand.
---@field time_since_combat number # Seconds since last in combat.
---@field name string # Same as CEntityInstance:GetName().
---@field healthfrac number # The health fraction (health/maxhealth).
---@field classname string # Same as CBaseEntity:GetClassname().
---@field randomnum integer # Random integer [0-100].
---@field npcstate "[NPCState::None]"|"[NPCState::Idle]"|"[NPCState::Alert]"|"[NPCState::Combat]" # Current NPC state.
---@field speed number # Current speed of the entity.
---@field num_squad_members number # Number of living NPCs in the squad.
---@field activity string # Unknown if this changes. Returns `"ACT_IDLE"`.
---@field weapon string # Name of the weapon the NPC is holding.
---@field lost_squad_members number # Squad members which have been killed while this entity is alive.
---@field distancetoplayer number # Distance to player in inches (NPC head to player head?).
---@field has_officer 0|1 # If this squad has at least one officer.
---@field timesincecombat number|999999|-1 # Seconds since the NPC was last in combat. 999999=never, -1=currently in combat.
---@field distancetoenemy number|16384 # Distance to the current enemy that can be seen. 16384 if can't be seen.
---@field combine_class "default"|"officer"|"charger"|"suppressor" # Combine class name (`default` == grunt).
---@field seenbyplayer 0|1 #
---@field seeplayer 0|1 #
---@field timesinceseenplayer number|-1 # Seconds since the NPC last saw the player. This is never 0 while the player is in view. `-1` if never seen.
---@field enemy string|nil # Classname of the current enemy, nil if no enemy.


--#endregion

--#endregion

----------
--- Global
--- Global functions. These can be called without any class.
--#region

---Prints any table keys/values with deep recursion.
---@param tbl table
function DeepPrintTable(tbl) end

--#region Math

---Returns the number of degrees difference between two yaw angles
---@param angle1 number
---@param angle2 number
---@return number
function AngleDiff(angle1, angle2) end
---Generate a vector given a QAngle
---@param angle QAngle
---@return Vector
function AnglesToVector(angle) end
---Constructs a quaternion representing a rotation by angle around the specified vector axis.
---Bug: The Quaternion class is non-functional
---@param axis Vector
---@param angle number
---@return Quaternion
---@deprecated
function AxisAngleToQuaternion(axis, angle) end
---Compute the closest point relative to a vector on the OBB of an entity.
---@param entity EntityHandle
---@param position Vector
---@return Vector
function CalcClosestPointOnEntityOBB(entity, position) end
---Compute the distance between two entity OBB. A negative return value indicates an input error. A return value of zero indicates that the OBBs are overlapping.
---@param entity1 EntityHandle
---@param entity2 EntityHandle
---@return number
function CalcDistanceBetweenEntityOBB(entity1, entity2) end
---Calculate the cross product between two vectors (also available as a Vector class method).
---@param vector1 Vector
---@param vector2 Vector
---@return Vector
function CrossVectors(vector1, vector2) end
---Get the closest point from P to the (infinite) line through lineA and lineB and calculate the shortest distance from P to the line.
---@param P Vector
---@param lineA Vector
---@param lineB Vector
---@return number
function CalcDistanceToLineSegment2D(P, lineA, lineB) end
---Smooth curve decreasing slower as it approaches zero.
---@param decayTo number
---@param decayTime number
---@param dt number
---@return number
function ExponentialDecay(decayTo, decayTime, dt) end
---Linear interpolation of vector values over [0,1].
---@param vector1 Vector
---@param vector2 Vector
---@param t number
---@return Vector
function LerpVectors(vector1, vector2, t) end
---Get a random float within a range.
---@param min number
---@param max number
---@return number
function RandomFloat(min, max) end
---Get a random int within a range, inclusive.
---@param min integer
---@param max integer
---@return integer
function RandomInt(min, max) end
---Rotate a QAngle by another QAngle.
---@param angle1 QAngle
---@param angle2 QAngle
---@return QAngle
function RotateOrientation(angle1, angle2) end
---Rotate a Vector around a point.
---@param rotationOrigin Vector
---@param rotationAngle QAngle
---@param vectorToRotate Vector
---@return Vector
function RotatePosition(rotationOrigin, rotationAngle, vectorToRotate) end
---Rotates a quaternion by the specified angle around the specified vector axis.
---Bug: The Quaternion class is non-functional
---@param quat Quaternion
---@param axis Vector
---@param angle number
---@return Quaternion
---@deprecated
function RotateQuaternionByAxisAngle(quat, axis, angle) end
---Find the delta between two QAngles.
---@param src QAngle
---@param dest QAngle
---@return QAngle
function RotationDelta(src, dest) end
---Converts delta QAngle to an angular velocity Vector.
---@param angle1 QAngle
---@param angle2 QAngle
---@return Vector
function RotationDeltaAsAngularVelocity(angle1, angle2) end
---Very basic interpolation of quaternions q0 to q1 over time 't' on [0,1].
---Bug: The Quaternion class is non-functional
---@param q0 Quaternion
---@param q1 Quaternion
---@param t number
---@return Quaternion
---@deprecated
function SplineQuaternions(q0, q1, t) end
---Very basic interpolation of two vectors over time t on [0,1].
---@param vector1 Vector
---@param vector2 Vector
---@param t number
---@return Vector
function SplineVectors(vector1, vector2, t) end
---Get QAngles for a Vector.
---@param input Vector The Vector to convert to QAngle.
---@return QAngle #Vector as QAngle.
function VectorToAngles(input) end

--#endregion

--#region utilsinit.lua

-- Functions automatically included from the utilsinit.lua core library.

---Return value as an absolute value, i.e. Non-negative.
---@param value number
---@return number
function abs(value) end
---Clamp the value between the min and max.
---@param value number
---@param min number
---@param max number
---@return number
function Clamp(value, min, max) end
---Convert degrees to radians.
---@param deg number
---@return number
function Deg2Rad(deg) end
---Convert radians to degrees.
---@param rad number
---@return number
function Deg2Rad(rad) end
---Linear interpolation of float values a and b over t [0,1].
---@param t number
---@param a number
---@param b number
---@return number
function Lerp(t, a, b) end
---Returns the largest value of the inputs.
---@param x number
---@param y number
---@return number
function max(x, y) end
---Returns the smallest value of the inputs.
---@param x number
---@param y number
---@return number
function min(x, y) end
---Returns a new table with `table2` merged into `table1`, with `table1` overwriting any keys in `table2`.
---Use vlua.tableadd to concatenate tables.
---@param table1 table
---@param table2 table
---@return table
function Merge(table1, table2) end
---Remap a value in the range [a,b] to [c,d].
---@param value number
---@param a number
---@param b number
---@param c number
---@param d number
---@return number
function RemapVal(value, a, b, c, d) end
---Remap a value in the range [a,b] to [c,d], clamping the output to the range.
---@param value number
---@param a number
---@param b number
---@param c number
---@param d number
---@return number
function RemapValClamped(value, a, b, c, d) end
---Distance between two vectors squared (faster than calculating the plain distance).
---@param vector1 Vector
---@param vector2 Vector
---@return number
function VectorDistanceSq(vector1, vector2) end
---Distance between two vectors.
---@param vector1 Vector
---@param vector2 Vector
---@return number
function VectorDistance(vector1, vector2) end
---Linear interpolation of vector values over [0,1]. The native function LerpVectors performs the same task.
---@param t number
---@param vector1 Vector
---@param vector2 Vector
---@return Vector
function VectorLerp(t, vector1, vector2) end
---Check if the vector is a zero vector.
---@param vector Vector
---@return boolean
function VectorIsZero(vector) end

--#endregion

--#region Printing & Drawing

---Appends a string to a log file on the server
---Warning: Deprecated
---@param string_1 string
---@param string_2 string
---@deprecated
function AppendToLogFile(string_1, string_2) end
---Draw a debug overlay box
---@param origin Vector
---@param mins Vector
---@param maxs Vector
---@param red number
---@param green number
---@param blue number
---@param alpha number
---@param seconds number
function DebugDrawBox(origin, mins, maxs, red, green, blue, alpha, seconds) end
---Draw box oriented to a Vector direction
---@param origin Vector
---@param mins Vector
---@param maxs Vector
---@param orientation Vector
---@param rgb Vector
---@param alpha number
---@param seconds number
function DebugDrawBoxDirection(origin, mins, maxs, orientation, rgb, alpha, seconds) end
---Draw a debug circle
---@param origin Vector
---@param rgb Vector
---@param alpha number
---@param radius number
---@param noDepthTest boolean
---@param seconds number
function DebugDrawCircle(origin, rgb, alpha, radius, noDepthTest, seconds) end
---Try to clear all the debug overlay info
function DebugDrawClear() end
---Draw a debug overlay line
---@param startPos Vector
---@param endPos Vector
---@param red number
---@param green number
---@param blue number
---@param noDepthTest boolean
---@param seconds number
function DebugDrawLine(startPos, endPos, red, green, blue, noDepthTest, seconds) end
---Draw a debug line using color vec.
---@param startPos Vector
---@param endPos Vector
---@param rgb Vector
---@param noDepthTest boolean
---@param seconds number
function DebugDrawLine_vCol(startPos, endPos, rgb, noDepthTest, seconds) end
---Draw text to the screen with a line offset. Use \n to break to a new line.
-- Coordinates are in absolute screen pixels.
---@param x number Horizontal screen position.
---@param y number Vertical screen position.
---@param lineOffset integer Number of lines to start offset by.
---@param text string
---@param red number
---@param green number
---@param blue number
---@param alpha number
---@param seconds number
function DebugDrawScreenTextLine(x, y, lineOffset, text, red, green, blue, alpha, seconds) end
---Draw a debug sphere.
---@param center Vector
---@param rgb Vector
---@param alpha number
---@param radius number
---@param noDepthTest boolean
---@param seconds number
function DebugDrawSphere(center, rgb, alpha, radius, noDepthTest, seconds) end
---Draw screen oriented text at an absolute world position.
---@param origin Vector
---@param text string
---@param viewCheck boolean What does this do?
---@param seconds number
function DebugDrawText(origin, text, viewCheck, seconds) end
---Draw pretty debug text.
---@param x number Horizontal screen position.
---@param y number Vertical screen position.
---@param lineOffset integer Number of lines to start offset by.
---@param text string
---@param red number
---@param green number
---@param blue number
---@param alpha number
---@param seconds number
---@param font string Name of a system font.
---@param size integer
---@param bold boolean
function DebugScreenTextPretty(x, y, lineOffset, text, red, green, blue, alpha, seconds, font, size, bold) end
---Print a message to the console. Will show up in the 'General' channel.
---@param message string
function Msg(message) end
---Print a console message with a linked console command. Will show up in the 'VScriptScripts' channel.
---@param message string
---@param command string What does this do? Appears to just be a linked message.
function PrintLinkedConsoleMessage(message, command) end
---Have entity say message. Will appear in console in 'Client' channel.
---For some reason will trim word "say" from beginning of message.
---@param entity EntityHandle
---@param message string
---@param teamOnly boolean If true message will only be sent to clients on same team.
function Say(entity, message, teamOnly) end
---Print a hud message on all clients.
---Doesn't appear to work. Let me know otherwise.
---@param message string
---@deprecated
function ShowMessage(message) end
---Displays a message for a specific player.
---@param playerId integer
---@param message string
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
function UTIL_MessageText(playerId, message, red, green, blue, alpha) end
---Sends a message to a specific player in the message box with a context table.
---@param playerId integer
---@param message string
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param context table
function UTIL_MessageText_WithContext(playerId, message, red, green, blue, alpha, context) end
---Sends a message to everyone in the message box.
---@param messsage string
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
function UTIL_MessageTextAll(messsage, red, green, blue, alpha) end
---Sends a message to everyone in the message box with a context table.
---@param messsage string
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param context table
function UTIL_MessageTextAll(messsage, red, green, blue, alpha, context) end
---Resets the message text for the player.
---@param playerId integer
function UTIL_ResetMessageText(playerId) end
---Resets the message text for all players.
function UTIL_ResetMessageTextAll() end
---Print a yellow warning message in the console. Will show in 'General' channel.
---@param msg string
function Warning(msg) end

--#endregion

--#region Entity Manipulation

---Cancel all I/O events for a particular entity.
---Does not seem to work often if at all.
---@param EHANDLE EHANDLE
function CancelEntityIOEvents(EHANDLE) end
---Connects all output script functions of the passed entity script scope to the entity outputs.
---Function name regex format: "^On.*Output$"
---Doesn't work because ConnectOutput is broken.
---@param outputs table
---@deprecated
function ConnectOutputs(outputs)
    local function OnPlayerPickupOutput() end
    local function OnUser1Output() end
    ConnectOutputs({
        OnPlayerPickupOutput = OnPlayerPickupOutput,
        OnUser1Output = OnUser1Output
    })
end
---Allocate a CTakeDamageInfo object, used as an argument to CBaseEntity::TakeDamage(). Call DestroyDamageInfo( hInfo ) to free the object.
---@param inflictor EntityHandle
---@param attacker EntityHandle
---@param force Vector
---@param hitPos Vector
---@param damage number
---@param damageTypes ENUM_DAMAGE_TYPES
---@return CTakeDamageInfo
function CreateDamageInfo(inflictor, attacker, force, hitPos, damage, damageTypes) end
---Pass table - Inputs: entity, effect
---@param keys table
---@return boolean
function CreateEffect(keys) end
---Create a scene entity to play the specified scene.
---@param sceneName string Path to the vcd resource.
---@return CSceneEntity
function CreateSceneEntity(sceneName) end
---Creates and returns an enabled AABB trigger.
---@param origin Vector
---@param mins Vector
---@param maxs Vector
---@return CBaseTrigger
function CreateTrigger(origin, mins, maxs) end
---Creates and returns an AABB trigger thats bigger than the radius provided.
---@param origin Vector
---@param radius number
---@return CBaseTrigger
function CreateTriggerRadiusApproximate(origin, radius) end
---Free a CTakeDamageInfo object that was created with CreateDamageInfo().
---@param info CTakeDamageInfo
function DestroyDamageInfo(info) end
---Internal native function for EntFire().
---@param target string
---@param action string
---@param value string
---@param delay number
---@param activator EntityHandle
---@param caller EntityHandle
function DoEntFire(target, action, value, delay, activator, caller) end
---Internal native function for EntFireByHandle().
---@param target EntityHandle
---@param action string
---@param value string
---@param delay number
---@param activator EntityHandle
---@param caller EntityHandle
function DoEntFireByInstanceHandle(target, action, value, delay, activator, caller) end
---Generate an entity I/O event on all entities matching the specified target name. The script scope of the calling entity should be passed to the first parameter.
---@param scope ScriptScope
---@param target string
---@param action string
---@param value? string Default = ""
---@param delay? number Default = 0.0
---@param activator? EntityHandle Default = thisEntity
function EntFire(scope, target, action, value, delay, activator) end
---Generate an entity I/O event on the specified entity. The calling entity should be passed to the first parameter.
---@param self EntityHandle
---@param target EntityHandle
---@param action string
---@param value? string # Default = ""
---@param delay? number # Default = 0.0
---@param activator? EntityHandle # Default = self
function EntFireByHandle(self, target, action, value, delay, activator) end
---Turn an entity index integer to an HScript (entity handle) representing that entity's script instance.
---@param entindex integer
---@return EntityHandle?
function EntIndexToHScript(entindex) end
---Fire Entity's Action Input w/no data.
---@param EHANDLE EHANDLE
---@param inputName string
function FireEntityIOInputNameOnly(EHANDLE, inputName) end
---Fire Entity's Action Input with passed String - you own the memory.
---@param EHANDLE EHANDLE
---@param inputName string
---@param value string
function FireEntityIOInputString(EHANDLE, inputName, value) end
---Fire Entity's Action Input with passed Vector.
---@param EHANDLE EHANDLE
---@param inputName string
---@param value Vector
function FireEntityIOInputVec(EHANDLE, inputName, value) end
---Get the longest delay for all events attached to an output.
---@param EHANDLE EHANDLE
---@param outputName string
---@return number
function GetMaxOutputDelay(EHANDLE, outputName) end
---Get Angular Velocity for VPHYS or normal object. Returns a vector of the axis of rotation, multiplied by the rotation in radians per second.
---@param entity EntityHandle
---@return Vector
function GetPhysAngularVelocity(entity) end
---Get Velocity for VPHYS or normal object.
---@param entity EntityHandle
---@return Vector
function GetPhysVelocity(entity) end
---Returns the validity of the entity. Opposite of CBaseEntity:IsNull()
---@param entity EntityHandle
---@return boolean
function IsValidEntity(entity) end
---Get a script instance of a player by player ID.
---What is a player ID?
---@param playerID unknown|integer
---@return unknown
function PlayerInstanceFromIndex(playerID) end
---Precache an entity from KeyValues in table.
---@param classname string
---@param spawnKeys table
---@param context CScriptPrecacheContext
function PrecacheEntityFromTable(classname, spawnKeys, context) end
---Precache a list of entity KeyValues tables.
---@param groupSpawnTables table
---@param context CScriptPrecacheContext
function PrecacheEntityListFromTable(groupSpawnTables, context) end
---Manually precache a single model.
---@param modelName string
---@param context CScriptPrecacheContext
function PrecacheModel(modelName, context) end
---model_folder|sound|soundfile|particle|particle_folder"
---@param resourceType string|"model_folder"|"sound"|"soundfile"|"particle"|"particle_folder"
---@param resourcePath string
---@param context CScriptPrecacheContext
function PrecacheResource(resourceType, resourcePath, context) end
---Set Angular Velocity for VPHYS or normal object, from a vector of the axis of rotation, multiplied by the rotation in radians per second.
---@param entity EntityHandle
---@param velocity Vector
function SetPhysAngularVelocity(entity, velocity) end
---Set rendering on/off for an EHANDLE.
---Bug:	Unstable in Half-Life: Alyx. May cause hard crash.
---@param EHANDLE EHANDLE
---@param enabled boolean
function SetRenderingEnabled(EHANDLE, enabled) end
---Asynchronously spawns a single entity from a table. A callback will be triggered when the spawning is complete, passing the handle of the entity as a parameter.
---@param classname string
---@param spawnKeys table
---@param callback function
---@param unknown unknown
function SpawnEntityFromTableAsynchronous(classname, spawnKeys, callback, unknown) end
---Synchronously spawns a single entity from a table
---@param classname string
---@param spawnKeys table
---@return EntityHandle
function SpawnEntityFromTableSynchronous(classname, spawnKeys) end
---Hierarchically spawn an entity group from a set of spawn tables.
---How does this work?
---@param groupSpawnTables table
---@param async boolean
---@param callback function
---@return boolean
function SpawnEntityGroupFromTable(groupSpawnTables, async, callback) end
---Asynchronously spawn an entity group from a list of spawn tables. A callback will be triggered when the spawning is complete, passing a list of spawned entities.
---Each table must include key 'classname'.
---@param spawnKeysList table
---@param callback function
---@return integer
function SpawnEntityListFromTableAsynchronous(spawnKeysList, callback) end
---Synchronously spawn an entity group from a list of spawn tables.
---Each table must include key 'classname'.
---@param spawnKeysList table
---@return table # List of spawned entity handles.
function SpawnEntityListFromTableSynchronous(spawnKeysList) end
---
---@param entity unknown
---@param effectName string
function StopEffect(entity, effectName) end
---Deletes the entity
---@param entity EntityHandle
function UTIL_Remove(entity) end
---Deletes the entity with no delay.
---Warning:	Incorrect usage may cause crashes
---@param entity EntityHandle
function UTIL_RemoveImmediate(entity) end

--#endregion

--#region Tracing

---Does a raytrace against a single entity. Input and output parameters are stored in the specified table.
---@param parameters table
---@return boolean
function TraceCollideable(parameters) end
---Traces a axis aligned bounding box along a line. Input and output parameters are stored in the specified table.
---@param parameters table
---@return boolean
function TraceHull(parameters) end
---Does a raytrace along a line. Input and output parameters are stored in the specified table.
---@param parameters table
---@return boolean
function TraceLine(parameters) end

--#endregion

--#region Sound

---Play named sound for all players.
---Function does not appear to exist.
---@param sound string
---@deprecated
function EmitGlobalSound(sound) end
---Play named sound on Entity.
---@param sound string
---@param entity EntityHandle
function EmitSoundOn(sound, entity) end
---Play named sound only on the client for the passed in player.
---@param sound string
---@param player CHL2_Player
function EmitSoundOnClient(sound, player) end
---Sets an opvar value for all players.
---@param stackName string|"hlvr_global_opvars"
---@param operatorName string|"opvars"
---@param opvarName string|COMMON_OPVAR_NAMES
---@param opvarValue number
function SetOpvarFloatAll(stackName, operatorName, opvarName, opvarValue) end
---Sets an opvar value for a single player ( szStackName, szOperatorName, szOpvarName, flOpvarValue, hEnt )
---@param stackName string|"hlvr_global_opvars"
---@param operatorName string|"opvars"
---@param opvarName string|COMMON_OPVAR_NAMES
---@param opvarValue number
---@param player CHL2_Player
function SetOpvarFloatPlayer(stackName, operatorName, opvarName, opvarValue, player) end
---Start a sound event. Appears to always emit from the player.
---@param sound string
---@param unknown EntityHandle
function StartSoundEvent(sound, unknown) end
---Start a sound event from position
---@param sound string
---@param position Vector
function StartSoundEventFromPosition(sound, position) end
---Start a sound event from position with reliable delivery
---@param sound string
---@param position Vector
function StartSoundEventFromPositionReliable(sound, position) end
---Start a sound event from position with optional delivery
---@param sound string
---@param position Vector
function StartSoundEventFromPositionUnreliable(sound, position) end
---Start a sound event with reliable delivery
---@param sound string
---@param unknown EntityHandle
function StartSoundEventReliable(sound, unknown) end
---Start a sound event with optional delivery
---@param sound string
---@param unknown EntityHandle
function StartSoundEventUnreliable(sound, unknown) end
---Stops a sound event. Seems to be exactly the same as StopSoundOn.
---@param sound string
---@param entity EntityHandle # The entity playing the sound.
function StopSoundEvent(sound, entity) end
---Stop named sound on Entity.
---@param sound string
---@param playingEntity EntityHandle
function StopSoundOn(sound, playingEntity) end

--#endregion

--#region Miscellaneous

---Gets the value of the given Convar, as a float.
---@param convar string
---@return number
function cvar_getf(convar) end
---Sets the value of the given Convar, to a float.
---@param convar string
---@param value number
---@return boolean # Returns true if successful.
function cvar_setf(convar, value) end
---	Breaks in the debugger.
--- How does this work?
function DebugBreak() end
---Internal native function for IncludeScript().
---@param scriptFileName string
---@param scope ScriptScope
---@return boolean
function DoIncludeScript(scriptFileName, scope) end
---Internal native function for ScriptAssert().
---How does this work?
---@param assertion boolean
---@param message string
function DoScriptAssert(assertion, message) end
---Internal native function for UniqueString().
---@param root string|"" # String that will be added to the end.
---@return string
function DoUniqueString(root) end
---Fire a pre-defined event, which can be found in resource\game.gameevents & resource\core.gameevents
---@param eventName GAME_EVENTS_ALL
---@param parameterTable table # Arbitrary key/value pairs are allowed.
function FireGameEvent(eventName, parameterTable) end
---Fire a game event without broadcasting to the client.
---@param eventName GAME_EVENTS_ALL
---@param parameterTable table # Arbitrary key/value pairs are allowed.
function FireGameEventLocal(eventName, parameterTable) end
---Get the time spent on the server in the last frame.
---@return number
function FrameTime() end
---Returns the currently active spawn group handle.
---@return integer
function GetActiveSpawnGroupHandle() end
---Returns the engines current frame count.
---@return integer
function GetFrameCount() end
---Get the local player on a listen server. Functionally the same as Entities:GetLocalPlayer()?
---@return CHL2_Player
function GetListenServerHost() end
---Get the name of the map.
---@return string
function GetMapName() end
---Execute a script file. Included in the current scope by default.
---Doesn't appear to exist, use DoIncludeScript instead.
---@param scriptFileName string
---@param scope ScriptScope|nil
---@return boolean
---@deprecated
function IncludeScript(scriptFileName, scope) end
---If the given file doesn't exist, creates it with the given contents; does nothing if it exists
---Warning: Deprecated
---@param string_1 string
---@param string_2 string
---@deprecated
function InitLogFile(string_1, string_2) end
---Returns true if this is lua running from the client.dll.
---@return boolean
function IsClient() end
---Returns true if this server is a dedicated server.
---@return boolean
function IsDedicatedServer() end
---Returns true if the entity is valid and marked for deletion.
---@param entity EntityHandle
---@return boolean
function IsMarkedForDeletion(entity) end
---Returns true if this is lua running from the server.dll.
---@return boolean
function IsServer() end
---Returns true if this is lua running from the workshop tools.
---@return boolean
function IsInToolsMode() end
---Register as a listener for a game event from script.
---@param eventname GAME_EVENTS_ALL
---@param callback function
---@param context table|nil # Context to pass as the first parameter of `callback`.
---@return integer # ID used to cancel with StopListeningToGameEvent().
function ListenToGameEvent(eventname, callback, context) end
---Creates a table from the specified keyvalues text file.
---Does this work? What path is valid?
---@param path string
---@return table
function LoadKeyValues(path) end
---Creates a table from the specified keyvalues string.
---Does this work? What is the format?
---@param keyvals string
---@return table
function LoadKeyValuesFromString(keyvals) end
---Returns the local system time as a table with the format {Hours = int; Minutes = int; Seconds = int}
---@return TypeLocalTimeTable
function LocalTime() end
---Convert a string to a non-reversable (hashed?) integer.
---@param str string
---@return integer
function MakeStringToken(str) end
---Triggers the creation of entities in a manually-completed spawn group.
---@param int integer
function ManuallyTriggerSpawnGroupCompletion(int) end
---Create a C proxy for a script-based spawn group filter.
---@param str string
function RegisterSpawnGroupFilterProxy(str) end
---Reloads the MotD file.
function ReloadMOTD() end
---Remove the C proxy for a script-based spawn group filter
---@param str string
function RemoveSpawnGroupFilterProxy(str) end
---Add a rule to the decision database.
---@param rule Decider|unknown
---@return boolean
function rr_AddDecisionRule(rule) end
---Commit the result of QueryBestResponse back to the given entity to play.
---@param entity EntityHandle|unknown
---@param airesponse unknown
---@return boolean
function rr_CommitAIResponse(entity, airesponse) end
---Retrieve a table of all available expresser targets, in the form { name : handle, name: handle }.
---In Alyx appears to return table with one value of player.
---@return table
function rr_GetResponseTargets() end
---Static : tests 'query' against entity's response system and returns the best response found (or nil if none found).
---@param ent unknown
---@param query unknown
---@param result unknown
function rr_QueryBestResponse(ent, query, result) end
---Start a screenshake.
---@param center Vector
---@param amplitude number
---@param frequency number
---@param duration number
---@param radius number
---@param command SHAKE_COMMAND
---@param airShake boolean
function ScreenShake(center, amplitude, frequency, duration, radius, command, airShake) end
---Asserts the passed in value. Prints out a message and brings up the assert dialog.
---Appears to do nothing.
---@param assertion boolean
---@param message string|""
function ScriptAssert(assertion, message) end
---Send a string to the console as a client command.
---@param command string # Can send multiple commands with ;
function SendToConsole(command) end
---Send a string to the console as a server command.
---@param command string # Can send multiple commands with ;
function SendToServerConsole(command) end
---Set the current quest name.
---@param name string
function SetQuestName(name) end
---Set the current quest phase.
---@param phase integer
function SetQuestPhase(phase) end
---Stop listening to all game events within a specific context.
---@param context table
function StopListeningToAllGameEvents(context) end
---Stop listening to a particular game event.
---@param eventlistener integer
---@return boolean
function StopListeningToGameEvent(eventlistener) end
---Get the current server time.
---@return number
function Time() end
---Generate a string guaranteed to be unique across the life of the script VM, with an optional root string. Useful for adding data to table's when not sure what keys are already in use in that table.
---@param root? string # String that will be added to the end.
---@return string
function UniqueString(root) end
---Unload a spawn group by name
---@param groupName string
function UnloadSpawnGroup(groupName) end
---Unload a spawn group by handle
---@param groupHandle integer
function UnloadSpawnGroupByHandle(groupHandle) end
---No Description Set
---@param handle_1 unknown
function UpdateEventPoints(handle_1) end

--#endregion

--#region VLua

---Functions automatically included from the library.lua core library. Located in the vlua table rather than directly in the global scope.
---Library functions to support Lua code generated by Sq2Lua.exe
---@class vlua
vlua = {}
---Implements Squirrel clear table method.
---@param t table
---@return table
function vlua.clear(t) end
---Implements Squirrel three way compare operator ( <=> ).
--- < -1
--- == 0
--- >  1
---@param a number
---@param b number
---@return integer
function vlua.compare(a, b) end
---Implements Squirrel in operator.
---@param t table
---@param key any
---@return boolean
function vlua.contains(t, key) end
---Implements Squirrel slot delete operator.
---Will also delete numbered slots.
---@param t table
---@param key any
---@return integer
function vlua.delete(t, key) end
---Implements Squirrel clone operator.
---@param t table
---@return table
function vlua.clone(t) end
---Implements Squirrel rawdelete library function.
---@param t table
---@param key any
---@return integer
function vlua.rawdelete(t, key) end
---Implements Squirrel rawin library function.
---@param t table
---@param key any
---@return integer
function vlua.rawin(t, key) end
---Implements Squirrel find method for tables and strings. (o, substr, [startidx]) for strings, (o, value) for tables
---@param tbl table
---@param value any|string
---@return any
---@overload fun(str: string, substr: string, startIndex: integer): string|nil
function vlua.find(tbl, value) end
---Implements Squirrel slice method for tables and strings.
---@param tbl table
---@param startIndex integer
---@param endIndex integer
---@return any
---@overload fun(str: string, startIndex: integer, endIndex: integer): string
function vlua.slice(tbl, startIndex, endIndex) end
---Implements Squirrel reverse method for tables.
---@param t table
---@return table
function vlua.reverse(t) end
---Implements Squirrel resize method for tables.
---Unordered tables will have size number of fill added.
---@param t table
---@param size integer
---@param fill any
---@return table
function vlua.resize(t, size, fill) end
---Implements Squirrel extend method for tables.
---Appears to append array onto o. array must be an ordered table.
---@param o table
---@param array table
---@return table?
function vlua.extend(o, array) end
---Implements Squirrel map method for tables.
---Passes values one at a time to function first param. Return a value to be added to resulting table.
---@param o table
---@param mapFunc function
---@return table
function vlua.map(o, mapFunc) end
---Implements Squirrel reduce method for tables.
---Two values are passed each time. Func must return a value to be passed in next call.
---@param o table
---@param reduceFunc function
---@return any?
function vlua.reduce(o, reduceFunc) end
---Implements tableadd function to support += paradigm used in Squirrel.
---Adds `t2` into `t1` overwriting any existing keys.
---@param t1 table
---@param t2 table
---@return table
function vlua.tableadd(t1, t2) end
---	Implements Squirrel split function for strings.
---@param input string
---@param separator string
---@return table
function vlua.split(input, separator) end
---Safe Ternary operator. The Lua version will return the wrong value if the value if true is nil.
---@param conditional boolean
---@param valueIfTrue any
---@param valueIfFalse any
---@return any
function vlua.select(conditional, valueIfTrue, valueIfFalse) end

--#endregion

--#endregion

----------
--- Classes
--#region

--#region CBaseEntity

---The base class for entities.
---@class CBaseEntity
CBaseEntity = {}
---Adds the render effect flag.
---@param flags integer
function CBaseEntity:AddEffects(flags) end
---Apply a Velocity Impulse.
---@param impulse Vector
function CBaseEntity:ApplyAbsVelocityImpulse(impulse) end
---Apply an Angular Velocity Impulse.
---@param angImpulse Vector
function CBaseEntity:ApplyLocalAngularVelocityImpulse(angImpulse) end
---Get float value for an entity attribute.
---@param name string
---@param default number
---@return number
function CBaseEntity:Attribute_GetFloatValue(name, default) end
---Get int value for an entity attribute.
---@param name string
---@param default integer
---@return number
function CBaseEntity:Attribute_GetIntValue(name, default) end
---Set float value for an entity attribute.
---@param name string
---@param value number
function CBaseEntity:Attribute_SetFloatValue(name, value) end
---Set int value for an entity attribute.
---@param name string
---@param value integer
function CBaseEntity:Attribute_SetIntValue(name, value) end
---Delete an entity attribute.
---@param name string
function CBaseEntity:DeleteAttribute(name) end
---
---@param soundName string
function CBaseEntity:EmitSound(soundName) end
---Plays/modifies a sound from this entity. changes sound if Pitch and/or Volume or SoundTime is > 0.
---@param soundName string
---@param pitch integer
---@param volume number
---@param soundTime number
function CBaseEntity:EmitSoundParams(soundName, pitch, volume, soundTime) end
---Get the QAngles that this entity is looking at.
---@return QAngle
function CBaseEntity:EyeAngles() end
---Get vector to eye position - absolute coords
---@return Vector
function CBaseEntity:EyePosition() end
---If in hierarchy, get the first move child.
---@return EntityHandle
function CBaseEntity:FirstMoveChild() end
---Parents calling entity to the passed entity matching origin and angle, with optional bone merging.
---Pass nil to unfollow.
---@param entity EntityHandle|nil
---@param boneMerge boolean
function CBaseEntity:FollowEntity(entity, boneMerge) end
---Gathers into a table, the criteria that would be used for response queries on this entity. This is the same as the table that is passed to response rule script function callbacks.
---@param result CriteriaTable # The table to gather criteria into.
function CBaseEntity:GatherCriteria(result) end
---Returns the world space origin of the entity.
---@return Vector
function CBaseEntity:GetAbsOrigin() end
---Get the absolute entity scale.
---To do: How to access non-uniform scale?
---@return number
function CBaseEntity:GetAbsScale() end
---Get the entity pitch, yaw, roll as QAngle
---@return QAngle
function CBaseEntity:GetAngles() end
---Get entity pitch, yaw, roll as a vector
---@return Vector
function CBaseEntity:GetAnglesAsVector() end
---Get the local angular velocity - returns a vector of pitch,yaw,roll
---@return Vector
function CBaseEntity:GetAngularVelocity() end
---Get Base velocity. Only functional on prop_dynamic entities with the Scripted Movement property set.
---@return Vector
function CBaseEntity:GetBaseVelocity() end
---Get a vector containing max bounds, centered on object
---@return Vector
function CBaseEntity:GetBoundingMaxs() end
---Get a vector containing min bounds, centered on object
---@return Vector
function CBaseEntity:GetBoundingMins() end
---Get a table containing the 'Mins' & 'Maxs' vector bounds, centered on object
---@return table
function CBaseEntity:GetBounds() end
---Get vector to center of object - absolute coords
---@return Vector
function CBaseEntity:GetCenter() end
---Get the entities parented to this entity. Including children of children.
---@return EntityHandle[]
function CBaseEntity:GetChildren() end
---Looks up a context and returns it if available. May return string, float, or nil (if the context isn't found)
---@param name string
---@return string|number?
function CBaseEntity:GetContext(name) end
---Get the forward Vector of the entity
---@return Vector
function CBaseEntity:GetForwardVector() end
---No Description Set
---@return integer
function CBaseEntity:GetHealth() end
---Get entity relative angular velocity. Only functional on prop_dynamic entities with the Scripted Movement property set.
---@return QAngle
function CBaseEntity:GetLocalAngularVelocity() end
---Get entity pitch, yaw, roll as a QAngle, in the space of the entity's parent or attachment point
---@return QAngle
function CBaseEntity:GetLocalAngles() end
---Get entity origin as a Vector, in the space of the entity's parent or attachment point.
---@return Vector
function CBaseEntity:GetLocalOrigin() end
---	Get the entity scale relative to that of its parent.
---@return number
function CBaseEntity:GetLocalScale() end
---Get Entity relative velocity. Only functional on prop_dynamic entities with the Scripted Movement property set.
---@return Vector
function CBaseEntity:GetLocalVelocity() end
---Get the mass of an entity. (returns 0 if it doesn't have a physics object)
---@return number
function CBaseEntity:GetMass() end
---No Description Set
---@return integer
function CBaseEntity:GetMaxHealth() end
---Returns the asset path name of the model.
---@return string
function CBaseEntity:GetModelName() end
---If in hierarchy, retrieves the entity's parent
---@return EntityHandle
function CBaseEntity:GetMoveParent() end
---	Returns the origin of the entity, either in world space ot in its parents space if parented.
---@return Vector
function CBaseEntity:GetOrigin() end
---Gets this entity's owner.
---Not the same as parent.
---@return EntityHandle
function CBaseEntity:GetOwner() end
---Get the owner entity, if there is one.
---Not the same as parent.
---@return EntityHandle
function CBaseEntity:GetOwnerEntity() end
---Get the right vector of the entity
---@return Vector
function CBaseEntity:GetRightVector() end
---If in hierarchy, walks up the hierarchy to find the root parent
---@return EntityHandle
function CBaseEntity:GetRootMoveParent() end
---Returns float duration of the sound.
---Returns 2 for all sounds.
---@param soundName string
---@param actormodelname string|""
---@return number
---@deprecated
function CBaseEntity:GetSoundDuration(soundName, actormodelname) end
---Returns the spawn group handle of this entity.
---@return integer
function CBaseEntity:GetSpawnGroupHandle() end
---No Description Set
---@return integer
function CBaseEntity:GetTeam() end
---Get the team number of this entity.
---@return integer
function CBaseEntity:GetTeamNumber() end
---Get the up vector of the entity.
---@return Vector
function CBaseEntity:GetUpVector() end
---World space velocity of the entity. Only functional on prop_dynamic entities with the Scripted Movement property set.
---@return Vector
function CBaseEntity:GetVelocity() end
---See if an entity has a particular attribute.
---@param name string
---@return boolean
function CBaseEntity:HasAttribute(name) end
---Is the entity alive
---@return boolean
function CBaseEntity:IsAlive() end
---Is this entity an CAI_BaseNPC?
---@return boolean
function CBaseEntity:IsNPC() end
---Returns the invalidity of the entity. Opposite of IsValidEntity()
---@return boolean
function CBaseEntity:IsNull() end
---Is the entity a player
---@return boolean
function CBaseEntity:IsPlayer() end
---Deletes the entity (UTIL_Remove())
function CBaseEntity:Kill() end
---	Return the next entity in the same movement hierarchy.
---@return EntityHandle
function CBaseEntity:NextMovePeer() end
---Takes duration, value for a temporary override.
---Doesn't seem to work.
---@param duration number
---@param friction number
function CBaseEntity:OverrideFriction(duration, friction) end
---Precache a sound for later playing.
---@param soundname string
function CBaseEntity:PrecacheScriptSound(soundname) end
---Removes the render effect flag.
---@param flags integer
function CBaseEntity:RemoveEffects(flags) end
---Set entity world space pitch, yaw, roll by component.
---@param pitch number
---@param yaw number
---@param roll number
function CBaseEntity:SetAbsAngles(pitch, yaw, roll) end
---	Sets the world space entity origin.
---@param origin Vector
function CBaseEntity:SetAbsOrigin(origin) end
---Set the absolute scale of the entity.
---@param scale number
function CBaseEntity:SetAbsScale(scale) end
---Set entity pitch, yaw, roll by component. If parented, this is set relative to the parents local space.
---@param pitch number
---@param yaw number
---@param roll number
function CBaseEntity:SetAngles(pitch, yaw, roll) end
---Set the local angular velocity - takes float pitch, yaw, roll velocities. Only functional on prop_dynamic entities with the Scripted Movement property set.
---@param pitch number
---@param yaw number
---@param roll number
function CBaseEntity:SetAngularVelocity(pitch, yaw, roll) end
---Set the position of the constraint.
---@param pos Vector
function CBaseEntity:SetConstraint(pos) end
---Store any key/value pair in this entity's dialog contexts. Value must be a string. Will last for duration (set 0 to mean 'forever').
---@param name string
---@param value string
---@param duration number
function CBaseEntity:SetContext(name, value, duration) end
---Store any key/value pair in this entity's dialog contexts. Value must be a number (int or float). Will last for duration (set 0 to mean 'forever').
---@param name string
---@param value number|integer
---@param duration number
function CBaseEntity:SetContextNum(name, value, duration) end
---Set a context think function on this entity.
---@param thinkName string
---@param thinkFunction function
---@param initialDelay number
function CBaseEntity:SetContextThink(thinkName, thinkFunction, initialDelay) end
---Set entity targetname
---@param name string
function CBaseEntity:SetEntityName(name) end
---Set the orientation of the entity to have this forward forwardVec
---@param forwardVec Vector
function CBaseEntity:SetForwardVector(forwardVec) end
---Set PLAYER friction, ignored for objects
---@param friction number
function CBaseEntity:SetFriction(friction) end
---Set PLAYER gravity, ignored for objects
---@param gravity number
function CBaseEntity:SetGravity(gravity) end
---Set entity health. Does not respect max health.
---@param hp integer
function CBaseEntity:SetHealth(hp) end
---Set the entity pitch, yaw, roll by component, relative to the local space of the entity's parent or attachment point.
---@param pitch number
---@param yaw number
---@param roll number
function CBaseEntity:SetLocalAngles(pitch, yaw, roll) end
---Set entity local origin. Relative to the local space of the entity's parent or attachment point.
---@param origin Vector
function CBaseEntity:SetLocalOrigin(origin) end
---Set the entity scale relative to the entity's parent.
---@param scale number
function CBaseEntity:SetLocalScale(scale) end
---Set the mass of an entity. (does nothing if it doesn't have a physics object)
---@param mass number
function CBaseEntity:SetMass(mass) end
---Set entity max health
---@param maxHP integer
function CBaseEntity:SetMaxHealth(maxHP) end
---	Set entity absolute origin
---@param origin Vector
function CBaseEntity:SetOrigin(origin) end
---	Sets this entity's owner.
---@param owningEntity EntityHandle
function CBaseEntity:SetOwner(owningEntity) end
---Set the parent for this entity. The attachment is optional, pass an empty string to not use it.
---@param parent EntityHandle
---@param attachmentName string|""
function CBaseEntity:SetParent(parent, attachmentName) end
---Set entity team.
---@param team integer
function CBaseEntity:SetTeam(team) end
---Sets a thinker function to be called periodically.
---Uses SetContextThink under the hood.
---
---If using a string function name, function must exist in private script scope.
---@param thinkFunction function|string # If string, will look up in the calling instance or given `context` to find the function. If binding a local function this must be a direct reference, not a string.
---@param thinkName string # Name of the think, used for stopping.
---@param initialDelay number # Initial delay before the function is first called.
---@param context? EntityHandle # If `thinkFunction` is a string, use this context to find the function, otherwise ignored.
function CBaseEntity:SetThink(thinkFunction, thinkName, initialDelay, context) end
---Sets the world space velocity of the entity. Only functional on prop_dynamic entities with the Scripted Movement property set.
---@param velocity Vector
function CBaseEntity:SetVelocity(velocity) end
---Stops the named sound playing from this entity.
---@param soundName string
function CBaseEntity:StopSound(soundName) end
---Stops the named thinker function.
---@param thinkName string
function CBaseEntity:StopThink(thinkName) end
---Apply damage to this entity. Use CreateDamageInfo() to create a damageinfo object.
---@param damageInfo CTakeDamageInfo
---@return integer
function CBaseEntity:TakeDamage(damageInfo) end
---Returns the input Vector transformed from entity to world space.
---To do: May not respect entity scale
---@param point Vector
---@return Vector
function CBaseEntity:TransformPointEntityToWorld(point) end
---Returns the input Vector transformed from world to entity space.
---To do: May not respect entity scale
---@param point Vector
---@return Vector
function CBaseEntity:TransformPointWorldToEntity(point) end
---	Fires off this entity's OnTrigger responses.
function CBaseEntity:Trigger() end
---Validates the private script scope and creates it if one doesn't exist.
function CBaseEntity:ValidatePrivateScriptScope() end

---Returns true if the entity is instance of given class.
---@param classOrClassName string|table
---@return boolean
function CBaseEntity:IsInstance(classOrClassName) end

--#endregion

--#region CEntityInstance

---All entities inherit from this.
---@class CEntityInstance : CBaseEntity
CEntityInstance = {}
---Adds an I/O connection that will call the named function on this entity when the specified output fires.
---Bug: This doesn't appear to work in Half-Life: Alyx scripts. Use RedirectOutput with thisEntity as the third parameter instead.
---@param output string
---@param functionName string
---@deprecated
function CEntityInstance:ConnectOutput(output, functionName) end
---Deletes the entity (UTIL_Remove())
function CEntityInstance:Destroy() end
---Removes a connected script function from an I/O event on this entity.
---@param output string
---@param functionName string
---@deprecated
function CEntityInstance:DisconnectOutput(output, functionName) end
---	Removes a connected script function from an I/O event on the passed entity.
---@param output string
---@param functionName string
---@param entity EntityHandle
function CEntityInstance:DisconnectRedirectedOutput(output, functionName, entity) end
---Get the entity index of this entity. Will be different every time the map loads.
---Use EntIndexToHScript() to convert back to handle.
---Functionally the same as GetEntityIndex().
---@return integer
function CEntityInstance:entindex() end
---Fire an entity output.
---@param outputName string
---@param activator EntityHandle
---@param caller EntityHandle
---@param parameter string|nil # The parameter override to send with the output.
---@param delay number
function CEntityInstance:FireOutput(outputName, activator, caller, parameter, delay) end
---Get the entity classname as a string.
---@return string classname
function CEntityInstance:GetClassname() end
---Get the entity name w/help if not defined (i.e. classname/etc)
---@return string
function CEntityInstance:GetDebugName() end
---Get the entity as an EHANDLE
---@return EHANDLE
function CEntityInstance:GetEntityHandle() end
---Get the entity index of this entity. Will be different every time the map loads.
---Use EntIndexToHScript() to convert back to handle.
---@return integer
function CEntityInstance:GetEntityIndex() end
---Get Integer Attribute. Functionally the same as Attribute_GetIntValue().
---@param key string
---@return integer
function CEntityInstance:GetIntAttr(key) end
---Returns the targetname of the entity.
---@return string targetname
function CEntityInstance:GetName() end
---Retrieve, creating if necessary, the private per-instance script-side data associated with an entity
---@return ScriptScope
function CEntityInstance:GetOrCreatePrivateScriptScope() end
---Retrieve, creating if necessary, the public script-side data associated with an entity
---@return ScriptScope
function CEntityInstance:GetOrCreatePublicScriptScope() end
---Retrieve the private per-instance script-side data associated with an entity
---@return ScriptScope
function CEntityInstance:GetPrivateScriptScope() end
---Retrieve the public script-side data associated with an entity
---@return ScriptScope
function CEntityInstance:GetPublicScriptScope() end
---Adds an I/O connection that will call the named function on the passed entity when the specified output fires.
---This means the redirection is persistent after game loads.
---@param output string
---@param functionName string
---@param entity EntityHandle
function CEntityInstance:RedirectOutput(output, functionName, entity) end
---Deletes the entity (UTIL_Remove())
function CEntityInstance:RemoveSelf() end
---Set Integer Attribute. Functionally the same as Attribute_SetIntValue().
---@param key string
---@param value integer
function CEntityInstance:SetIntAttr(key, value) end

--#endregion

--#region CBaseModelEntity

---Entities with models inherit from this.
---As far as I can tell CEntityInstance always exists with CBaseModelEntity.
---@class CBaseModelEntity : CEntityInstance
CBaseModelEntity = {}
---Get the material group hash of this entity.
---@return integer
function CBaseModelEntity:GetMaterialGroupHash() end
---Get the mesh group mask of this entity.
---@return Uint64
function CBaseModelEntity:GetMaterialGroupMask() end
---Get the alpha modulation of this entity.
---@return integer
function CBaseModelEntity:GetRenderAlpha() end
---Get the render color of the entity.
---@return Vector
function CBaseModelEntity:GetRenderColor() end
---Sets a bodygroup by index.
---@param group integer
---@param value integer
function CBaseModelEntity:SetBodygroup(group, value) end
---Sets a bodygroup by name.
---@param group string # Case-insensitive.
---@param value integer
function CBaseModelEntity:SetBodygroupByName(group, value) end
---Sets the light group of the entity.
---@param lightGroup string
function CBaseModelEntity: SetLightGroup(lightGroup) end
---Set the material group of this entity.
---@param name string # Case-insensitive.
function CBaseModelEntity:SetMaterialGroup(name) end
---	Set the material group hash of this entity.
---@param hash integer
function CBaseModelEntity:SetMaterialGroupHash(hash) end
---	Set the mesh group mask of this entity.
---@param mask Uint64
function CBaseModelEntity:SetMaterialGroupMask(mask) end
---Changes the model of the entity. Make sure the new model is precached before using.
---@param modelName string
function CBaseModelEntity:SetModel(modelName) end
---	Set the alpha modulation of this entity.
---@param alpha integer
function CBaseModelEntity:SetRenderAlpha(alpha) end
---Sets the render color of the entity.
---@param red integer
---@param green integer
---@param blue integer
function CBaseModelEntity:SetRenderColor(red, green, blue) end
---Sets the render mode of the entity.
---@param mode integer
function CBaseModelEntity:SetRenderMode(mode) end
---Set a single mesh group for this entity.
---@param meshGroupName string
function CBaseModelEntity:SetSingleMeshGroup(meshGroupName) end
---Unknown how this works.
---@param mins Vector
---@param maxs Vector
function CBaseModelEntity:SetSize(mins, maxs) end
---	Set skin by number.
---@param skin integer
function CBaseModelEntity:SetSkin(skin) end

--#endregion

--#region CBasePlayer

---Entity class for players.
---@class CBasePlayer : CBaseCombatCharacter
CBasePlayer = {}
---Returns whether this player's chaperone bounds are visible.
---@return boolean
function CBasePlayer:AreChaperoneBoundsVisible() end
---Returns the value of the analog action for the given hand. See Analog Input Actions for action index values and return types.
---Note: Only reports input when headset is awake. Will still transmit input when controllers lose tracking.
---@param nLiteralHandType integer
---@param nAnalogAction integer
---@return Vector
function CBasePlayer:GetAnalogActionPositionForHand(nLiteralHandType, nAnalogAction) end
---Returns the HMD anchor entity for this player if it exists.
---@return CEntityInstance|nil
function CBasePlayer:GetHMDAnchor() end
---Returns the HMD Avatar entity for this player if it exists.
---@return CPropHMDAvatar|nil
function CBasePlayer:GetHMDAvatar() end
---Returns the Vector position of the point you ask for. Pass 0-3 to get the four points.
---@param point 0|1|2|3
---@return Vector
function CBasePlayer:GetPlayArea(point) end
---Returns the player's user ID.
---@return integer
function CBasePlayer:GetUserID() end
---Returns the type of controller being used while in VR.
---@return ENUM_CONTROLLER_TYPES
function CBasePlayer:GetVRControllerType() end
---Returns true if the digital action is on for the given hand. See Digital Input Actions for action index values.
---Note: Only reports input when headset is awake. Will still transmit input when controllers lose tracking.
---@param literalHandType integer
---@param digitalAction ENUM_DIGITAL_INPUT_ACTIONS
---@return boolean
function CBasePlayer:IsDigitalActionOnForHand(literalHandType, digitalAction) end
---Returns true if the player is in noclip mode.
---@return boolean
function CBasePlayer:IsNoclipping() end
---	Returns true if the use key is pressed.
---@return boolean
function CBasePlayer:IsUsePressed() end
---Returns true if the controller button is pressed.
---Non-functional for motion controller buttons in Half-Life: Alyx. Works with key bindings when VR is turned off.
---@param nButton integer
---@return boolean
function CBasePlayer:IsVRControllerButtonPressed(nButton) end
---Returns true if the SteamVR dashboard is showing for this player.
---@return boolean
function CBasePlayer:IsVRDashboardShowing() end

--#endregion

--#region CHL2_Player

---Half-life player subclass SHOULD RETURN THIS INSTEAD OF CBasePlayer ??
---@class CHL2_Player : CBasePlayer
CHL2_Player = {}
---
---@param name string
---@param delta integer
---@return boolean
function CHL2_Player:PlayerCounter_CanModifyValue(name, delta) end
---
---@param name string
---@param max integer
---@return integer
function CHL2_Player:PlayerCounter_SetMax(name, max) end
---
---@param name string
---@param delta integer
---@return integer
function CHL2_Player:PlayerCounter_ModifyValue(name, delta) end
---
---@param name string
---@param min integer
---@return integer
function CHL2_Player:PlayerCounter_SetMin(name, min) end
---
---@param name string
---@param min integer
---@param max integer
---@return integer
function CHL2_Player:PlayerCounter_SetMinMax(name, min, max) end
---
---@param name string
---@param value integer
---@return integer
function CHL2_Player:PlayerCounter_SetValue(name, value) end
---
---@param name string
---@return integer
function CHL2_Player:PlayerCounter_GetValue(name) end

--#endregion

--#region CBaseAnimating

---A class containing methods involved in animations. Most model based entities inherit this.
---As far as I can tell CBaseAnimating and CBaseModelEntity always exist together.
---@class CBaseAnimating : CBaseModelEntity
CBaseAnimating = {}
---Returns the duration in seconds of the active sequence.
---@return number
function CBaseAnimating:ActiveSequenceDuration() end
---Get the attachment id's angles as a p,y,r vector
---@param iAttachment integer
---@return Vector
function CBaseAnimating:GetAttachmentAngles(iAttachment) end
---Get the attachment id's forward vector.
---@param iAttachment integer
---@return Vector
function CBaseAnimating:GetAttachmentForward(iAttachment) end
---Get the attachment id's origin vector
---@param iAttachment integer
---@return Vector
function CBaseAnimating:GetAttachmentOrigin(iAttachment) end
---Get the cycle of the animation, a [0-1] range.
---@return number
function CBaseAnimating:GetCycle() end
---Get the value of the given animGraph parameter.
---@param pszParam string
---@return table
function CBaseAnimating:GetGraphParameter(pszParam) end
---Get scale of entity's model.
---@return number
function CBaseAnimating:GetModelScale() end
---Returns the name of the active sequence.
---@return string
function CBaseAnimating:GetSequence() end
---Ask whether the main sequence is done playing
---@return boolean
function CBaseAnimating:IsSequenceFinished() end
---	Registers a listener for string AnimTags, replaces existing script listener if any.
---@param animTagListenerFunc function
function CBaseAnimating:RegisterAnimTagListener(animTagListenerFunc) end
---Sets the active sequence by name, resetting the current cycle
---@param pSequenceName string
function CBaseAnimating:ResetSequence(pSequenceName) end
---Get the named attachment id
---@param pAttachmentName string
---@return integer
function CBaseAnimating:ScriptLookupAttachment(pAttachmentName) end
---Returns the duration in seconds of the given sequence name.
---@param pSequenceName string
---@return number
function CBaseAnimating:SequenceDuration(pSequenceName) end
---Pass the desired look target in world space to the graph.
---@param vValue Vector
function CBaseAnimating:SetGraphLookTarget(vValue) end
---Set the specific param value, type is inferred from the type in script.
---@param pszParam string
---@param svArg table
function CBaseAnimating:SetGraphParameter(pszParam, svArg) end
---Set the specific boolean parameter on or off.
---@param szName string
---@param bValue boolean
function CBaseAnimating:SetGraphParameterBool(szName, bValue) end
---Pass the enum (int) value to the specified param.
---@param szName string
---@param nValue integer
function CBaseAnimating:SetGraphParameterEnum(szName, nValue) end
---Pass the float value to the specified parameter.
---@param szName string
---@param flValue number
function CBaseAnimating:SetGraphParameterFloat(szName, flValue) end
---Pass the int value to the specified param.
---@param szName string
---@param nValue integer
function CBaseAnimating:SetGraphParameterInt(szName, nValue) end
---Pass the vector value to the specified param in the graph.
---@param szName string
---@param vValue Vector
function CBaseAnimating:SetGraphParameterVector(szName, vValue) end
---Sets the model's scale to scale, so if a unit had its model scale at 1, and you use SetModelScale(10.0), it would set the scale to 10.0.
---@param scale number
function CBaseAnimating:SetModelScale(scale) end
---Set the specified pose parameter to the specified value.
---@param szName string
---@param fValue number
---@return number
function CBaseAnimating:SetPoseParameter(szName, fValue) end
---Sets the active sequence by name, keeping the current cycle.
---@param pSequenceName string
function CBaseAnimating:SetSequence(pSequenceName) end
---Stop the current animation by setting playback rate to 0.0.
function CBaseAnimating:StopAnimation() end
---Unregisters the current string AnimTag listener, if any
---@param hScript table
function CBaseAnimating:UnregisterAnimTagListener(hScript) end

--#endregion

--#region CBaseFlex

---Animated entities that have vertex flex capability.
---@class CBaseFlex : CBaseAnimating
CBaseFlex = {}
---Finds a flex controller by name, returns the index, -1 if not found
---@param flexControllerName string
---@return integer
function CBaseFlex:FindFlexController(flexControllerName) end
---	Returns the instance of the oldest active scene entity (if any).
---@return CSceneEntity|nil
function CBaseFlex:GetCurrentScene() end
---Gets the weight of a flex controller specified by index, use FindFlexController to get the index of a flex controller by name.
---@param flexControllerIndex integer
---@return unknown|number
function CBaseFlex:GetFlexWeight(flexControllerIndex) end
---	Play the specified .vcd file.
---@param sceneFile string
---@param delay number
---@return number
function CBaseFlex:ScriptPlayScene(sceneFile, delay) end
---Sets the weight of a flex controller specified by index, use FindFlexController to get the index of a flex controller by name.
---@param flexControllerIndex integer
---@param weight number
function CBaseFlex:SetFlexWeight(flexControllerIndex, weight) end

--#endregion

--#region CBaseCombatCharacter

---No Description Set (inherits from what? exists? is combine? is player?)
---@class CBaseCombatCharacter : CBaseFlex
CBaseCombatCharacter = {}
---Returns an array of all the equipped weapons
---@return table
function CBaseCombatCharacter:GetEquippedWeapons() end
---Get the combat character faction.
---@return integer
function CBaseCombatCharacter:GetFaction() end
---Gets the number of weapons currently equipped
---@return integer
function CBaseCombatCharacter:GetWeaponCount() end
---Returns the shoot position eyes (or hand in VR).
---Can pass 0,0.
---@param hand integer
---@param unknown unknown
---@return Vector
function CBaseCombatCharacter:ShootPosition(hand, unknown) end

--#endregion

--#region CBodyComponent

---No Description Set (inherits from what?)
---@class CBodyComponent
CBodyComponent = {}
---Apply an impulse at a worldspace position to the physics
---@param Vector_1 Vector
---@param Vector_2 Vector
---@deprecated
function CBodyComponent:AddImpulseAtPosition(Vector_1, Vector_2) end
---Add linear and angular velocity to the physics object
---@param Vector_1 Vector
---@param Vector_2 Vector
---@deprecated
function CBodyComponent:AddVelocity(Vector_1, Vector_2) end
---Detach from its parent
---@deprecated
function CBodyComponent:DetachFromParent() end
---Returns the active sequence
---@return unknown
---@deprecated
function CBodyComponent:GetSequence() end
---Is attached to parent
---@return boolean
---@deprecated
function CBodyComponent:IsAttachedToParent() end
---Returns a sequence id given a name
---@param string_1 string
---@return unknown
---@deprecated
function CBodyComponent:LookupSequence(string_1) end
---Returns the duration in seconds of the specified sequence
---@param string_1 string
---@return number
---@deprecated
function CBodyComponent:SequenceDuration(string_1) end
---No Description Set
---@param Vector_1 Vector
---@deprecated
function CBodyComponent:SetAngularVelocity(Vector_1) end
---Pass string for the animation to play on this model
---@param string_1 string
---@deprecated
function CBodyComponent:SetAnimation(string_1) end
---No Description Set
---@param string_1 string
---@deprecated
function CBodyComponent:SetBodyGroup(string_1) end
---No Description Set
---@param utlstringtoken_1 unknown
---@deprecated
function CBodyComponent:SetMaterialGroup(utlstringtoken_1) end
---No Description Set
---@param velocity Vector
---@deprecated
function CBodyComponent:SetVelocity(velocity) end

--#endregion

--#region CEntities

---Provides methods to enumerate all server-side entities.
---Global accessor variable: Entities
---@class CEntities
Entities = {}
---Creates an entity by class name.
---Warning: This does not initialize the created entity. Use the Spawn prefixed global functions instead.
---@param className string
---@return EntityHandle
function Entities:CreateByClassname(className) end
---Finds all entities by class name. Returns an array containing all the found entities.
---@param className string
---@return EntityHandle[]
function Entities:FindAllByClassname(className) end
---Find entities by class name within a radius. Returns an array containing all the found entities.
---@param className string
---@param origin Vector
---@param maxRadius number
---@return EntityHandle[]
function Entities:FindAllByClassnameWithin(className, origin, maxRadius) end
---Find entities by model name. Returns an array containing all the found entities.
---@param modelName string
---@return EntityHandle[]
function Entities:FindAllByModel(modelName) end
---Find all entities by name. Returns an array containing all the found entities in it.
---@param name string
---@return EntityHandle[]
function Entities:FindAllByName(name) end
---Find all entities by name within a radius. Returns an array containing all the found entities.
---@param name string
---@param origin Vector
---@param maxRadius number
---@return EntityHandle[]
function Entities:FindAllByNameWithin(name, origin, maxRadius) end
---Find all entities with this target set. Returns an array containing all the found entities.
-- How does this work?
---@param targetSet string
---@return EntityHandle[]
function Entities:FindAllByTarget(targetSet) end
---Find all entities within a radius. Returns an array containing all the found entities.
---@param origin Vector
---@param maxRadius number
---@return EntityHandle[]
function Entities:FindAllInSphere(origin, maxRadius) end
---Find entities by class name. Pass nil to start an iteration, or reference to a previously found entity to continue a search.
---@param startFrom EntityHandle|nil
---@param className string
---@return EntityHandle
function Entities:FindByClassname(startFrom, className) end
---Find the entity by class name nearest to a point.
---@param className string
---@param origin Vector
---@param maxRadius number
---@return EntityHandle
function Entities:FindByClassnameNearest(className, origin, maxRadius) end
---Find entities by class name within a radius. Pass nil to start an iteration, or reference to a previously found entity to continue a search
---@param startFrom EntityHandle|nil
---@param className string
---@param origin Vector
---@param maxRadius number
---@return EntityHandle
function Entities:FindByClassnameWithin(startFrom, className, origin, maxRadius) end
---Find entities by model name. Pass nil to start an iteration, or reference to a previously found entity to continue a search
---@param startFrom EntityHandle|nil
---@param modelName string
---@return EntityHandle
function Entities:FindByModel(startFrom, modelName) end
---Find entities by model name within a radius. Pass nil to start an iteration, or reference to a previously found entity to continue a search
---@param startFrom EntityHandle|nil
---@param modelName string
---@param origin Vector
---@param maxRadius number
---@return EntityHandle
function Entities:FindByModelWithin(startFrom, modelName, origin, maxRadius) end
---Find entities by name. Pass nil to start an iteration, or reference to a previously found entity to continue a search
---@param lastEnt EntityHandle|nil
---@param searchString string
---@return EntityHandle
function Entities:FindByName(lastEnt, searchString) end
---Find entity by name nearest to a point.
---@param name string
---@param origin Vector
---@param maxRadius number
---@return EntityHandle
function Entities:FindByNameNearest(name, origin, maxRadius) end
---Find entities by name within a radius. Pass nil to start an iteration, or reference to a previously found entity to continue a search
---@param startFrom EntityHandle|nil
---@param name string
---@param origin Vector
---@param maxRadius number
---@return EntityHandle
function Entities:FindByNameWithin(startFrom, name, origin, maxRadius) end
---Find entities by targetname. Pass nil to start an iteration, or reference to a previously found entity to continue a search
---How does this work?
---@param startFrom EntityHandle|nil
---@param targetName string
---@return EntityHandle
function Entities:FindByTarget(startFrom, targetName) end
---Find entities within a radius. Pass nil to start an iteration, or reference to a previously found entity to continue a search
---@param startFrom EntityHandle|nil
---@param origin Vector
---@param maxRadius number
---@return EntityHandle
function Entities:FindInSphere(startFrom, origin, maxRadius) end
---Begin an iteration over the list of entities
---@return EntityHandle
function Entities:First() end
---Get the local player.
---@return CHL2_Player
function Entities:GetLocalPlayer() end
---Continue an iteration over the list of entities, providing reference to a previously found entity
---@param startFrom EntityHandle What happens if starting from nil?
---@return EntityHandle
function Entities:Next(startFrom) end

--#endregion

--#region CAI_BaseNPC

---No Description Set
---@class CAI_BaseNPC : CBaseCombatCharacter
CAI_BaseNPC = {}
---Get the squad to which this NPC belongs.
---Unsure if functionality exists for this.
---@return table
function CAI_BaseNPC:GetSquad() end
---Set a position goal and start moving.
---@param vPos Vector
---@param bRun boolean
---@param flSuccessTolerance number
function CAI_BaseNPC:NpcForceGoPosition(vPos, bRun, flSuccessTolerance) end
---Removes the NPC's current goal.
function CAI_BaseNPC:NpcNavClearGoal() end
---Get the position of the current goal.
---@return Vector
function CAI_BaseNPC:NpcNavGetGoalPosition() end
---Returns true if NPC has a goal and path
---@return boolean
function CAI_BaseNPC:NpcNavGoalActive() end

--#endregion

--#region CBaseTrigger

---Entity class for triggers.
---@class CBaseTrigger : CEntityInstance
CBaseTrigger = {}
---Disable the trigger
function CBaseTrigger:Disable() end
---Enable the trigger
function CBaseTrigger:Enable() end
---Checks whether the passed entity is touching the trigger.
---@param entity EntityHandle
---@return boolean
function CBaseTrigger:IsTouching(entity) end

--#endregion

--#region CEnvTimeOfDay2

---No Description Set
---@class CEnvTimeOfDay2
CEnvTimeOfDay2 = {}
---Lookup dynamic time-of-day float value.
---@param unknown string
---@param unknown2 number
---@return number
function CEnvTimeOfDay2:GetFloat(unknown, unknown2) end
---Lookup dynamic time-of-day vector value.
---@param unknown string
---@param unknown2 number
---@return Vector
function CEnvTimeOfDay2:GetVector(unknown, unknown2) end

--#endregion

--#region CEnvEntityMaker

---Entity class for env_entity_maker.
---@class CEnvEntityMaker : CEntityInstance
CEnvEntityMaker = {}
---Create an entity at the location of the maker
function CEnvEntityMaker:SpawnEntity() end
---Create an entity at the location of a specified entity instance
---@param entity EntityHandle
function CEnvEntityMaker:SpawnEntityAtEntityOrigin(entity) end
---Create an entity at a specified location and orientaton, orientation is Euler angle in degrees (pitch, yaw, roll)
---@param origin Vector
---@param angles Vector
function CEnvEntityMaker:SpawnEntityAtLocation(origin, angles) end
---Create an entity at the location of a named entity
---@param pszName string
function CEnvEntityMaker:SpawnEntityAtNamedEntityOrigin(pszName) end

--#endregion

--#region CEntityScriptFramework

---Interface to the C++-side of entity framework
---Global accessor variable: EntityFramework
---@class CEntityScriptFramework
-- No methods available.
EntityFramework = {}
--TEST HOOKS

--#endregion

--#region CInfoWorldLayer

---Entity class for info_world_layer.
---@class CInfoWorldLayer : CEntityInstance
CInfoWorldLayer = {}
---Hides this layer.
function CInfoWorldLayer:HideWorldLayer() end
---Shows this layer.
function CInfoWorldLayer:ShowWorldLayer() end

--#endregion

--#region CLogicRelay

---Entity class for logic_relay.
---@class CLogicRelay : CEntityInstance
CLogicRelay = {}
---Trigger(hActivator, hCaller) : Triggers the logic_relay
---@param hActivator EntityHandle
---@param hCaller EntityHandle
function CLogicRelay:Trigger(hActivator, hCaller) end

--#endregion

--#region CMarkupVolumeTagged

---No Description Set
---@class CMarkupVolumeTagged : CEntityInstance
CMarkupVolumeTagged = {}
---Does this volume have the given tag.
---@param tagName string
---@return boolean
function CMarkupVolumeTagged:HasTag(tagName) end

--#endregion

--#region CScriptPrecacheContext

---Container to hold context published to precache functions in script
---@class CScriptPrecacheContext
CScriptPrecacheContext = {}
---Precaches a specific resource
---@param resource string
function CScriptPrecacheContext:AddResource(resource) end
---Reads a spawn key.
---@param key string
---@return any
function CScriptPrecacheContext:GetValue(key) end

--#endregion

--#region CScriptKeyValues

---Container holding keyvalues published to the Spawn() hook function.
---@class CScriptKeyValues
CScriptKeyValues = {}
---Reads a spawn key.
---@param key string
---@return any
function CScriptKeyValues:GetValue(key) end

--#endregion

--#region CNativeOutputs

---Container for holding outputs published by scripted entity classes to the game code.
---Does this have functionality?
---@class CNativeOutputs
CNativeOutputs = {}
---Creates a new CNativeOutputs object.
---@return CNativeOutputs
function CNativeOutputs() end
---Add an output.
---@param outputName string
---@param description string
function CNativeOutputs:AddOutput(outputName, description) end
---Initialize with specified number of outputs.
---@param numOutputs integer
function CNativeOutputs:Init(numOutputs) end

--#endregion

--#region CEnvProjectedTexture

---Entity class for env_projectedtexture
---@class CEnvProjectedTexture : CBaseEntity
CEnvProjectedTexture = {}
---Set light maximum range
---@param range number
function CEnvProjectedTexture:SetFarRange(range) end
---Set light linear attenuation value
---@param atten number
function CEnvProjectedTexture:SetLinearAttenuation(atten) end
---Set light minimum range
---@param range number
function CEnvProjectedTexture:SetNearRange(range) end
---Set light quadratic attenuation value
---@param atten number
function CEnvProjectedTexture:SetQuadraticAttenuation(atten) end
---Turn on/off light volumetrics.
---@param on boolean
---@param intensity number
---@param noise number
---@param planes integer
---@param planeOffset number
function CEnvProjectedTexture:SetVolumetrics(on, intensity, noise, planes, planeOffset) end

--#endregion

--#region CInfoData

---No Description Set what does this belong to?
---@class CInfoData
CInfoData = {}
---Query color data for this key
---@param tok string
---@param default Vector
---@return Vector
function CInfoData:QueryColor(tok, default) end
---Query float data for this key
---@param tok string
---@param default number
---@return number
function CInfoData:QueryFloat(tok, default) end
---Query int data for this key
---@param tok string
---@param default integer
---@return integer
function CInfoData:QueryInt(tok, default) end
---Query number data for this key
---@param tok string
---@param default number
---@return number
function CInfoData:QueryNumber(tok, default) end
---Query string data for this key
---@param tok string
---@param default string
---@return string
function CInfoData:QueryString(tok, default) end
---Query vector data for this key
---@param tok string
---@param default Vector
---@return Vector
function CInfoData:QueryColor(tok, default) end

--#endregion

--#region CPhysicsProp

---Entity class for prop_physics and related classes.
---@class CPhysicsProp : CBaseAnimating
CPhysicsProp = {}
---Enable/disable dynamic vs dynamic continuous collision traces.
---@param dynamicVsDynamicContinuousEnabled boolean
function CPhysicsProp:SetDynamicVsDynamicContinuous(dynamicVsDynamicContinuousEnabled) end
---Disable motion for the prop.
function CPhysicsProp:DisableMotion() end
---Enable motion for the prop.
function CPhysicsProp:EnableMotion() end

--#endregion

--#region CDebugOverlayScriptHelper

---No Description Set
---@class CDebugOverlayScriptHelper
debugoverlay = {}
---Draws an axis. Specify origin + orientation in world space.
---@param Vector_1 Vector
---@param Quaternion_2 Quaternion
---@param float_3 number
---@param bool_4 boolean
---@param float_5 number
---@deprecated
function debugoverlay:Axis(Vector_1, Quaternion_2, float_3, bool_4, float_5) end
---Draws a world-space axis-aligned wireframe box. Specify bounds in world space.
---@param min Vector
---@param max Vector
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param noDepthTest boolean
---@param seconds number
function debugoverlay:Box(min, max, red, green, blue, alpha, noDepthTest, seconds) end
---Draws an oriented box at the origin. Specify bounds in local space.
---@param Vector_1 Vector
---@param Vector_2 Vector
---@param Vector_3 Vector
---@param Quaternion_4 Quaternion
---@param int_5 integer
---@param int_6 integer
---@param int_7 integer
---@param int_8 integer
---@param bool_9 boolean
---@param float_10 number
---@deprecated
function debugoverlay:BoxAngles(Vector_1, Vector_2, Vector_3, Quaternion_4, int_5, int_6, int_7, int_8, bool_9, float_10) end
---Draws a capsule. Specify base in world space.
---@param Vector_1 Vector
---@param Quaternion_2 Quaternion
---@param float_3 number
---@param float_4 number
---@param int_5 integer
---@param int_6 integer
---@param int_7 integer
---@param int_8 integer
---@param bool_9 integer
---@param float_10 number
---@deprecated
function debugoverlay:Capsule(Vector_1, Quaternion_2, float_3, float_4, int_5, int_6, int_7, int_8, bool_9, float_10) end
---Draws a circle. Specify center in world space.
---@param Vector_1 Vector
---@param Quaternion_2 Quaternion
---@param float_3 number
---@param int_4 integer
---@param int_5 integer
---@param int_6 integer
---@param int_7 integer
---@param bool_8 boolean
---@param float_9 number
---@deprecated
function debugoverlay:Circle(Vector_1, Quaternion_2, float_3, int_4, int_5, int_6, int_7, bool_8, float_9) end
---Draws a circle oriented to the screen. Specify center in world space.
---@param origin Vector
---@param radius number
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param noDepthTest boolean
---@param seconds number
function debugoverlay:CircleScreenOriented(origin, radius, red, green, blue, alpha, noDepthTest, seconds) end
---Draws a wireframe cone.
---@param pos Vector # Starting tip for the cone.
---@param axis Vector # Normalized direction the cone faces.
---@param radius number # Radius of the cone.
---@param distance number # How far the cone will draw.
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param noDepthTest boolean
---@param seconds number
function debugoverlay:Cone(pos, axis, radius, distance, red, green, blue, alpha, noDepthTest, seconds) end
---Draws a screen-aligned cross. Specify origin in world space.
---@param origin Vector
---@param radius number
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param noDepthTest boolean
---@param seconds number
function debugoverlay:Cross(origin, radius, red, green, blue, alpha, noDepthTest, seconds) end
---Draws a world-aligned cross. Specify origin in world space.
---@param origin Vector
---@param radius number
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param noDepthTest boolean
---@param seconds number
function debugoverlay:Cross3D(origin, radius, red, green, blue, alpha, noDepthTest, seconds) end
---Draws an oriented cross. Specify origin in world space.
---@param Vector_1 Vector
---@param Quaternion_2 Quaternion
---@param float_3 number
---@param int_4 integer
---@param int_5 integer
---@param int_6 integer
---@param int_7 integer
---@param bool_8 boolean
---@param float_9 number
---@deprecated
function debugoverlay:Cross3DOriented(Vector_1, Quaternion_2, float_3, int_4, int_5, int_6, int_7, bool_8, float_9) end
---Draws a dashed line. Specify endpoint's in world space.
---@param startPos Vector
---@param endPos Vector
---@param distanceBetweenTicks number
---@param tickHighlightOffset integer
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param noDepthTest boolean
---@param seconds number
function debugoverlay:DrawTickMarkedLine(startPos, endPos, distanceBetweenTicks, tickHighlightOffset, red, green, blue, alpha, noDepthTest, seconds) end
---Draws the attachments of the entity
---@param ehandle EHANDLE
---@param size number # Seems to be size? But only works [2-19].
---@param seconds number
function debugoverlay:EntityAttachments(ehandle, size, seconds) end
---Draws the axis of the entity origin
---@param ehandle EHANDLE
---@param size number
---@param unknown boolean # False made it draw
---@param seconds number
function debugoverlay:EntityAxis(ehandle, size, unknown, seconds) end
---Draws bounds of an entity.
---How does this work?
---@param ehandle_1 EHANDLE
---@param int_2 integer
---@param int_3 integer
---@param int_4 integer
---@param int_5 integer
---@param bool_6 boolean
---@param float_7 number
function debugoverlay:EntityBounds(ehandle_1, int_2, int_3, int_4, int_5, bool_6, float_7) end
---Draws the skeleton of the entity
---@param ehandle EHANDLE
---@param seconds number
function debugoverlay:EntitySkeleton(ehandle, seconds) end
---Draws text on an entity
---@param ehandle EHANDLE
---@param heightOffset integer # Is this correct?
---@param text string
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param seconds number
function debugoverlay:EntityText(ehandle, heightOffset, text, red, green, blue, alpha, seconds) end
---Draws a screen-space filled 2D rectangle. Coordinates are in pixels.
---Vector2D class doesn't exist?
---@param Vector2D_1 Vector2D
---@param Vector2D_2 Vector2D
---@param int_3 integer
---@param int_4 integer
---@param int_5 integer
---@param int_6 integer
---@param float_7 number
---@deprecated
function debugoverlay:FilledRect2D(Vector2D_1, Vector2D_2, int_3, int_4, int_5, int_6, float_7) end
---Draws a horizontal arrow. Specify endpoint's in world space.
---@param startPos Vector
---@param endPos Vector
---@param width number
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param noDepthTest boolean
---@param seconds number
function debugoverlay:HorzArrow(startPos, endPos, width, red, green, blue, alpha, noDepthTest, seconds) end
---Draws a line between two points
---@param startPos Vector
---@param endPos Vector
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param noDepthTest boolean
---@param seconds number
function debugoverlay:Line(startPos, endPos, red, green, blue, alpha, noDepthTest, seconds) end
---Draws a line between two point's in screenspace
---Vector2D class doesn't exist?
---@param Vector2D_1 Vector2D
---@param Vector2D_2 Vector2D
---@param int_3 integer
---@param int_4 integer
---@param int_5 integer
---@param int_6 integer
---@param float_7 number
---@deprecated
function debugoverlay:Line2D(Vector2D_1, Vector2D_2, int_3, int_4, int_5, int_6, float_7) end
---Pops the identifier used to group overlays. Overlays marked with this identifier can be deleted in a big batch.
function debugoverlay:PopDebugOverlayScope() end
---Pushes an identifier used to group overlays. Deletes all existing overlays using this overlay id.
---@param utlstringtoken_1 string
function debugoverlay:PushAndClearDebugOverlayScope(utlstringtoken_1) end
---Pushes an identifier used to group overlays. Overlays marked with this identifier can be deleted in a big batch.
---@param utlstringtoken_1 string
function debugoverlay:PushDebugOverlayScope(utlstringtoken_1) end
---Removes all overlays marked with a specific identifier, regardless of their lifetime.
---@param utlstringtoken_1 string
function debugoverlay:RemoveAllInScope(utlstringtoken_1) end
---Draws a solid cone. Specify endpoint and direction in world space.
---@param startPos Vector
---@param endPos Vector
---@param unknown_1 number
---@param unknown_2 number
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param noDepthTest boolean
---@param seconds number
function debugoverlay:SolidCone(startPos, endPos, unknown_1, unknown_2, red, green, blue, alpha, noDepthTest, seconds) end
---Draws a wireframe sphere. Specify center in world space.
---@param position Vector
---@param radius number
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param noDepthTest boolean
---@param seconds number
function debugoverlay:Sphere(position, radius, red, green, blue, alpha, noDepthTest, seconds) end
---Draws a swept box. Specify endpoint's in world space and the bounds in local space.
---@param Vector_1 Vector
---@param Vector_2 Vector
---@param Vector_3 Vector
---@param Vector_4 Vector
---@param Quaternion_5 Quaternion
---@param int_6 integer
---@param int_7 integer
---@param int_8 integer
---@param int_9 integer
---@param float_10 number
---@deprecated
function debugoverlay:SweptBox(Vector_1, Vector_2, Vector_3, Vector_4, Quaternion_5, int_6, int_7, int_8, int_9, float_10) end
---Draws 2D text. Specify origin in world space.
---@param position Vector
---@param heightOffset integer
---@param text string
---@param unknown number
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param seconds number
function debugoverlay:Text(position, heightOffset, text, unknown, red, green, blue, alpha, seconds) end
---Draws a screen-space texture. Coordinates are in pixels.
---Vector2D class doesn't exist?
---@param string_1 string
---@param Vector2D_2 Vector2D
---@param Vector2D_3 Vector2D
---@param int_4 integer
---@param int_5 integer
---@param int_6 integer
---@param int_7 integer
---@param Vector2D_8 Vector2D
---@param Vector2D_9 Vector2D
---@param float_10 number
---@deprecated
function debugoverlay:Texture(string_1, Vector2D_2, Vector2D_3, int_4, int_5, int_6, int_7, Vector2D_8, Vector2D_9, float_10) end
---Draws a filled triangle in world space for the specific amount of seconds (-1 means forever).
---@param point1 Vector
---@param point2 Vector
---@param point3 Vector
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param noDepthTest boolean
---@param seconds number
function debugoverlay:Triangle(point1, point2, point3, red, green, blue, alpha, noDepthTest, seconds) end
---Toggles the overlay render type, for unit tests
function debugoverlay:UnitTestCycleOverlayRenderType() end
---Draws 3D text. Specify origin + orientation in world space.
---@param Vector_1 Vector
---@param Quaternion_2 Quaternion
---@param string_3 string
---@param int_4 integer
---@param int_5 integer
---@param int_6 integer
---@param int_7 integer
---@param bool_8 boolean
---@param float_9 number
---@deprecated
function debugoverlay:VectorText3D(Vector_1, Quaternion_2, string_3, int_4, int_5, int_6, int_7, bool_8, float_9) end
---Draws a vertical arrow. Specify endpoint's in world space.
---@param startPos Vector
---@param endPos Vector
---@param width number
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param noDepthTest boolean
---@param seconds number
function debugoverlay:VertArrow(startPos, endPos, width, red, green, blue, alpha, noDepthTest, seconds) end
---Draws a arrow associated with a specific yaw. Specify endpoint's in world space.
---@param startPos Vector
---@param yaw number
---@param length number
---@param width number
---@param red integer
---@param green integer
---@param blue integer
---@param alpha integer
---@param noDepthTest boolean
---@param seconds number
function debugoverlay:YawArrow(startPos, yaw, length, width, red, green, blue, alpha, noDepthTest, seconds) end

--#endregion

--#region CSceneEntity

---Choreographed scene which controls animation and/or dialog on one or more actors.
---@class CSceneEntity : CEntityInstance
CSceneEntity = {}
---Adds a team (by index) to the broadcast list
---@param int_1 integer
function CSceneEntity:AddBroadcastTeamTarget(int_1) end
---Cancel scene playback
function CSceneEntity:Cancel() end
---Returns length of this scene in seconds.
---@return number
function CSceneEntity:EstimateLength() end
---Get the camera
---@return handle
function CSceneEntity:FindCamera() end
---given an entity reference, such as !target, get actual entity from scene object
---@param string_1 string
---@return handle
function CSceneEntity:FindNamedEntity(string_1) end
---If this scene is currently paused.
---@return boolean
function CSceneEntity:IsPaused() end
---If this scene is currently playing.
---@return boolean
function CSceneEntity:IsPlayingBack() end
---given a dummy scene name and a vcd string, load the scene
---@param string_1 string
---@param string_2 string
---@return boolean
function CSceneEntity:LoadSceneFromString(string_1, string_2) end
---Removes a team (by index) from the broadcast list
---@param int_1 integer
function CSceneEntity:RemoveBroadcastTeamTarget(int_1) end
---Start scene playback, takes activatorEntity as param
---@param handle_1 handle
function CSceneEntity:Start(handle_1) end

--#endregion

--#region CCustomGameEventManager

---No Description Set
---@class CCustomGameEventManager
CustomGameEventManager = {}
---( string EventName, func CallbackFunction ) - Register a callback to be called when a particular custom event arrives. Returns a listener ID that can be used to unregister later.
---@param string_1 string
---@param handle_2 handle
---@return integer
function CustomGameEventManager:RegisterListener(string_1, handle_2) end
---( string EventName, table EventData )
---@param string_1 string
---@param handle_2 handle
function CustomGameEventManager:Send_ServerToAllClients(string_1, handle_2) end
---( Entity Player, string EventName, table EventData )
---@param handle_1 handle
---@param string_2 string
---@param handle_3 handle
function CustomGameEventManager:Send_ServerToPlayer(handle_1, string_2, handle_3) end
---( int TeamNumber, string EventName, table EventData )
---@param int_1 integer
---@param string_2 string
---@param handle_3 handle
function CustomGameEventManager:Send_ServerToTeam(int_1, string_2, handle_3) end
---( int ListnerID ) - Unregister a specific listener
---@param int_1 integer
function CustomGameEventManager:UnregisterListener(int_1) end

--#endregion

--#region CParticleSystem

---Entity class for particle systems? No methods avaialable.

--#endregion

--#region CPointClientUIWorldPanel

---Entity class for point_clientui_world_panel
---A 2D Panorama panel projected at a set position in the world.
---@class CPointClientUIWorldPanel : CBaseModelEntity
CPointClientUIWorldPanel = {}
---Tells the panel to accept user input.
function CPointClientUIWorldPanel:AcceptUserInput() end
---Adds CSS class(es) to the panel.
---@param classes string
function CPointClientUIWorldPanel:AddCSSClasses(classes) end
---Tells the panel to ignore user input.
function CPointClientUIWorldPanel:IgnoreUserInput() end
---Returns whether this entity is grabbable.
function CPointClientUIWorldPanel:IsGrabbable() end
---Remove CSS class(es) from the panel.
---@param classes string
function CPointClientUIWorldPanel:RemoveCSSClasses(classes) end

--#endregion

--#region CPointTemplate

---Entity class for point_template
---@class CPointTemplate : CEntityInstance
CPointTemplate = {}
---DeleteCreatedSpawnGroups() : Deletes any spawn groups that this point_template has spawned. Note: The point_template will not be deleted by this.
function CPointTemplate:DeleteCreatedSpawnGroups() end
---ForceSpawn() : Spawns all of the entities the point_template is pointing at.
function CPointTemplate:ForceSpawn() end
---GetSpawnedEntities() : Get the list of the most recent spawned entities
function CPointTemplate:GetSpawnedEntities() end
---SetSpawnCallback( hCallbackFunc, hCallbackScope, hCallbackData ) : Set a callback for when the template spawns entities. The spawned entities will be passed in as an array.
---@param hCallbackFunc handle
---@param hCallbackScope handle
function CPointTemplate:SetSpawnCallback(hCallbackFunc, hCallbackScope) end

--#endregion

--#region CPointWorldText

---Entity class for point_worldtext
---@class CPointWorldText : CBaseModelEntity
CPointWorldText = {}
---Set the message on this entity.
---@param pMessage string
function CPointWorldText:SetMessage(pMessage) end

--#endregion

--#region CPropHMDAvatar

---Entity class for prop_hmd_avatar
---@class CPropHMDAvatar : CBaseAnimating
CPropHMDAvatar = {}
---Get VR hand by ID (0 and 1).
---@param nHandID 0|1
---@return CPropVRHand
function CPropHMDAvatar:GetVRHand(nHandID) end

--#endregion

--#region CPropVRHand

---Entity class for prop_vr_hand. Represents a VR motion controller. The controllers can be enumerated for each player using the CPropHMDAvatar::GetVRHand() method.
---@class CPropVRHand : CBaseAnimating
CPropVRHand = {}
---Add the attachment to this hand.
---@param attachment EntityHandle
function CPropVRHand:AddHandAttachment(attachment) end
---Add a model override for this hand.
---@param modelName string
function CPropVRHand:AddHandModelOverride(modelName) end
---Find a specific model override for this hand.
---@param modelName string
---@return EntityHandle
function CPropVRHand:FindHandModelOverride(modelName) end
---Fire a haptic pulse on this hand. Integer range [0, 1, 2] for strength.
---@param strength 0|1|2
function CPropVRHand:FireHapticPulse(strength) end
---Fire a haptic pulse on this hand. Specify the duration in micro seconds.
---@param pulseDuration integer
function CPropVRHand:FireHapticPulsePrecise(pulseDuration) end
---Get the attachment on this hand.
---@return EntityHandle
function CPropVRHand:GetHandAttachment() end
---Get the players hand ID for this hand.
---@return 0|1 # 0 - Left hand, 1 - Right hand
function CPropVRHand:GetHandID() end
---Get literal type for this hand.
---@return 0|1 # 0 - Right hand, 1 - Left hand
function CPropVRHand:GetLiteralHandType() end
---Get the player for this hand.
---@return CHL2_Player
function CPropVRHand:GetPlayer() end
---Get the filtered controller velocity.
---@return Vector
function CPropVRHand:GetVelocity() end
---Remove all model overrides for this hand.
function CPropVRHand:RemoveAllHandModelOverrides() end
---Remove hand attachment by handle.
---@param hAttachment EntityHandle
function CPropVRHand:RemoveHandAttachmentByHandle(hAttachment) end
---Remove a model override for this hand.
---@param pModelName string
function CPropVRHand:RemoveHandModelOverride(pModelName) end
---Set the attachment for this hand.
---@param hAttachment EntityHandle
function CPropVRHand:SetHandAttachment(hAttachment) end

--#endregion

--#region CScriptParticleManager

---Allows the creation and manipulation of particle systems.
---Global accessor variable: ParticleManager
---@class ParticleManager
ParticleManager = {}
---Creates a new particle effect. Returns the index of the created effect.
---@param particleName string
---@param particleAttach ENUM_PATTACH
---@param owningEntity EntityHandle
---@return integer particleID
function ParticleManager:CreateParticle(particleName, particleAttach, owningEntity) end
---Creates a new particle effect that only plays for the specified player. Returns the index of the created effect.
---@param particleName string
---@param particleAttach ENUM_PATTACH
---@param owningEntity EntityHandle
---@param owningPlayer CBasePlayer
---@return integer particleID
function ParticleManager:CreateParticleForPlayer(particleName, particleAttach, owningEntity, owningPlayer) end
---Destroys particle.
---@param particleID integer
---@param immediately boolean
function ParticleManager:DestroyParticle(particleID, immediately) end
---No Description Set
---@param string_1 string
---@param handle_2 handle
---@return string
function ParticleManager:GetParticleReplacement(string_1, handle_2) end
---Frees the specified particle index
---@param particleID integer
function ParticleManager:ReleaseParticleIndex(particleID) end
---No Description Set
---@param alwaysSimulate integer
function ParticleManager:SetParticleAlwaysSimulate(alwaysSimulate) end
---Set the control point data for a control on a particle effect
---@param particleID integer
---@param controlIndex integer
---@param controlData Vector
function ParticleManager:SetParticleControl(particleID, controlIndex, controlData) end
---Attaches the control point to an entity.
---@param particleID integer
---@param controlIndex integer
---@param entity EntityHandle
---@param attachType ENUM_PATTACH
---@param attachment string
---@param origin Vector
---@param unknown boolean
function ParticleManager:SetParticleControlEnt(particleID, controlIndex, entity, attachType, attachment, origin, unknown) end
---Set the forward direction for a control point on a particle effect.
---@param particleID integer
---@param controlIndex integer
---@param forward Vector
function ParticleManager:SetParticleControlForward(particleID, controlIndex, forward) end
---Set the linear offset for a control on a particle effect.
---@param particleID integer
---@param controlIndex integer
---@param offset Vector
function ParticleManager:SetParticleControlOffset(particleID, controlIndex, offset) end
---Set the orientation for a control point on a particle effect.
---Note: This is left handed -- bad!!
---@param particleID integer
---@param controlIndex integer
---@param forward Vector
---@param right Vector
---@param up Vector
function ParticleManager:SetParticleControlOrientation(particleID, controlIndex, forward, right, up) end
---Set the orientation for a control point on a particle effect.
---@param particleID integer
---@param controlIndex integer
---@param forward Vector
---@param left Vector
---@param up Vector
function ParticleManager:SetParticleControlOrientationFLU(particleID, controlIndex, forward, left, up) end

--#endregion

--#region SteamInfo

-- Is this global??
---Is the script connected to the public Steam universe.

---@deprecated
---@return boolean
function IsPublicUniverse() end

--#endregion

--#region CTakeDamageInfo

---DamageInfo handle returned by CreateDamageInfo()
---@class CTakeDamageInfo
CTakeDamageInfo = {}
---Adds to the damage value.
---@param addAmount number
function CTakeDamageInfo:AddDamage(addAmount) end
---Adds damage type bit flags.
---@param bitsDamageType ENUM_DAMAGE_TYPES
function CTakeDamageInfo:AddDamageType(bitsDamageType) end
---
---@return boolean
function CTakeDamageInfo:AllowFriendlyFire() end
---
---@return boolean
function CTakeDamageInfo:BaseDamageIsValid() end
---
---@return boolean
function CTakeDamageInfo:CanBeBlocked() end
---
---@return integer
function CTakeDamageInfo:GetAmmoType() end
---Returns the attacker entity.
---@return EntityHandle
function CTakeDamageInfo:GetAttacker() end
---
---@return number
function CTakeDamageInfo:GetBaseDamage() end
---Returns the damage value.
---@return number
function CTakeDamageInfo:GetDamage() end
---
---@return integer
function CTakeDamageInfo:GetDamageCustom() end
---Returns the damage force.
---@return Vector
function CTakeDamageInfo:GetDamageForce() end
---Returns the damage position.
---@return integer
function CTakeDamageInfo:GetDamagePosition() end
---
---@return number
function CTakeDamageInfo:GetDamageTaken() end
---Returns the damage type bitfield.
---@return integer
function CTakeDamageInfo:GetDamageType() end
---Returns the inflictor entity (usually the weapon).
---@return EntityHandle
function CTakeDamageInfo:GetInflictor() end
---
---@return number
function CTakeDamageInfo:GetMaxDamage() end
---
---@return number
function CTakeDamageInfo:GetOriginalDamage() end
---
---@return number
function CTakeDamageInfo:GetRadius() end
---
---@return Vector
function CTakeDamageInfo:GetReportedPosition() end
---
---@return number
function CTakeDamageInfo:GetStabilityDamage() end
---
---@param bitsToTest ENUM_DAMAGE_TYPES
---@return boolean
function CTakeDamageInfo:HasDamageType(bitsToTest) end
---
---@param scaleAmount number
function CTakeDamageInfo:ScaleDamage(scaleAmount) end
---
---@param allow boolean
function CTakeDamageInfo:SetAllowFriendlyFire(allow) end
---
---@param ammoType integer what is ammo type?
function CTakeDamageInfo:SetAmmoType(ammoType) end
---
---@param attacker EntityHandle
function CTakeDamageInfo:SetAttacker(attacker) end
---
---@param block boolean
function CTakeDamageInfo:SetCanBeBlocked(block) end
---Set new damage value.
---@param damage number
function CTakeDamageInfo:SetDamage(damage) end
---
---@param damageCustom integer
function CTakeDamageInfo:SetDamageCustom(damageCustom) end
---Sets the damage force vector.
---@param damageForce Vector
function CTakeDamageInfo:SetDamageForce(damageForce) end
---Sets the global space damage position.
---@param damagePosition Vector
function CTakeDamageInfo:SetDamagePosition(damagePosition) end
---
---@param damageTaken integer
function CTakeDamageInfo:SetDamageTaken(damageTaken) end
---Set the damage type bitfield.
---@param bitsDamageType ENUM_DAMAGE_TYPES
function CTakeDamageInfo:SetDamageType(bitsDamageType) end
---
---@param maxDamage number
function CTakeDamageInfo:SetMaxDamage(maxDamage) end
---
---@param originalDamage number
function CTakeDamageInfo:SetOriginalDamage(originalDamage) end
---
---@param radius number
function CTakeDamageInfo:SetRadius(radius) end
---
---@param reportedPosition Vector
function CTakeDamageInfo:SetReportedPosition(reportedPosition) end
---
---@param stabilityDamage number
function CTakeDamageInfo:SetStabilityDamage(stabilityDamage) end

--#endregion

--#region Convars

---Allows access to read and modify console variables.
---Global accessor variable: Convars
---@class Convars
Convars = {}
---GetBool(name) : returns the convar as a boolean flag.
---@param name string
---@return boolean|nil
function Convars:GetBool(name) end
---GetCommandClient() : returns the player who issued this console command.
---@return CHL2_Player
function Convars:GetCommandClient() end
---GetFloat(name) : returns the convar as a float. May return nil if no such convar.
---@param name string
---@return float|nil
function Convars:GetFloat(name) end
---GetInt(name) : returns the convar as an int. May return nil if no such convar.
---@param name string
---@return integer|nil
function Convars:GetInt(name) end
---GetStr(name) : returns the convar as a string. May return nil if no such convar.
---@param name string
---@return string|nil
function Convars:GetStr(name) end
---RegisterCommand(name, fn, helpString, flags) : register a console command.
---@param name string
---@param callback function
---@param helpText string
---@param flags integer
function Convars:RegisterCommand(name, callback, helpText, flags) end
---RegisterConvar(name, defaultValue, helpString, flags): register a new console variable.
---@param name string
---@param defaultValue string make sure has to be string
---@param helpText string
---@param flags integer
function Convars:RegisterConvar(name, defaultValue, helpText, flags) end
---SetBool(name, val) : sets the value of the convar to the bool.
---@param name string
---@param value boolean
function Convars:SetBool(name, value) end
---SetFloat(name, val) : sets the value of the convar to the float.
---@param name string
---@param value number
function Convars:SetFloat(name, value) end
---SetInt(name, val) : sets the value of the convar to the int.
---@param name string
---@param value integer
function Convars:SetInt(name, value) end
---SetStr(name, val) : sets the value of the convar to the string.
---@param name string
---@param value string
function Convars:SetStr(name, value) end

--#endregion

--#region Decider

---No Description Set
---@class Decider
Decider = {}
---Add a CRule object (defined in rulescript_base.nut)
---@param rule CRule
---@return boolean
function Decider:AddRule(rule) end
---Returns an array of all matching responses. If leeway is nonzero, all results scoring within 'leeway' of the best score return.
---@param query handle
---@param leeway number
---@return handle
function Decider:FindAllMatches(query, leeway) end
---Query the database and return the best result found. If multiple of equal score found, an arbitrary one returns.
---@param query handle
---@return handle
function Decider:FindBestMatch(query) end

--#endregion

--#region GlobalSys

---Used to read the command line parameters the game was started with.
---Global accessor variable: GlobalSys
---@class GlobalSys
GlobalSys = {}
---Returns true if the command line param was used, otherwise false.
---@param name string
---@return boolean
function GlobalSys:CommandLineCheck(name) end
---Returns the command line param as a float.
---@param name string
---@param default number
---@return number
function GlobalSys:CommandLineFloat(name, default) end
---Returns the command line param as an int.
---@param name string
---@param default integer
---@return integer
function GlobalSys:CommandLineInt(name, default) end
---Returns the command line param as a string.
---@param name string
---@param default string
---@return string
function GlobalSys:CommandLineStr(name, default) end

--#endregion

--#region Uint64

---Integer with binary operations. Used for motion controller button masks.
---DOES LUA HAVE SUPPORT FOR THESE OPERATORS?
---How do you initialize this int?
---@class Uint64
Uint64 = {}
---Performs bitwise AND between two integers.
---@param operand Uint64
---@return integer
function Uint64:BitwiseAnd(operand) end
---Performs bitwise OR between two integers.
---@param operand Uint64
---@return integer
function Uint64:BitwiseOr(operand) end
---Performs bitwise XOR between two integers.
---@param operand Uint64
---@return integer
function Uint64:BitwiseXor(operand) end
---Performs bitwise NOT between two integers.
---@param operand Uint64
---@return integer
function Uint64:BitwiseNot(operand) end
---Clears the specified bit.
---@param bitValue integer
---@return integer
function Uint64:ClearBit(bitValue) end
---Checks if a bit is set.
---@param bitValue integer
---@return boolean
function Uint64:IsBitSet(bitValue) end
---Sets the specified bit.
---@param bitValue integer
---@return integer
function Uint64:SetBit(bitValue) end
---Toggles the specified bit.
---@param bitValue integer
---@return integer
function Uint64:ToggleBit(bitValue) end
---Returns a hexadecimal string representation of the integer.
---@return string
function Uint64:ToHexString() end

--#endregion

--#region QAngle

---Class for angles.
---@class QAngle
---@field x number Pitch angle.
---@field y number Yaw angle.
---@field z number Roll angle.
---@field __index string
QAngleClass = {}
---Creates a new QAngle.
---@param pitch? number
---@param yaw? number
---@param roll? number
---@return QAngle
function QAngle(pitch, yaw, roll) end
---Overloaded +. Adds angles together.
---Note: Use RotateOrientation() instead to properly rotate angles.
---@param qangle QAngle
---@return QAngle
function QAngleClass:__add(qangle) end
---Overloaded ==. Tests for Equality
---@param qangle QAngle
---@return QAngle
function QAngleClass:__eq(qangle) end
---Overloaded .. Converts the QAngle to a human readable string.
---@return string
function QAngleClass:__tostring() end
---Returns the forward vector.
---@return Vector
function QAngleClass:Forward() end
---Returns the left vector.
---@return Vector
function QAngleClass:Left() end
---Returns the up vector.
---@return Vector
function QAngleClass:Up() end

--#endregion

--#region Quarternion (misspelled on wiki)

---Class for quaterinions.
---Global accessor variable: None available
---
---**Bug: This class is broken and cannot be instantiated.**
---@class Quaternion
---@deprecated
Quaternion = {}

--#endregion

--#region Vector

---3D vector class.
---@class Vector
---@field x number X-axis
---@field y number Y-axis
---@field z number Z-axis
---@field __index string
---@operator add(Vector|number): Vector Overloaded +. Adds vectors together.
---@operator div(Vector|number): Vector Overloaded /. Divides vectors.
---@operator len(Vector): number Overloaded # returns the length of the vector.
---@operator mul(Vector|number): Vector Overloaded * returns the vectors multiplied together. can also be used to multiply with scalars.
---@operator sub(Vector|number): Vector
---@operator unm: Vector
VectorClass = {}
---Creates a new vector with the specified Cartesian coordinates.
---Can pass zero arguments for a zeroed Vector.
---@param x? number
---@param y? number
---@param z? number
---@return Vector
function Vector(x, y, z) end
---Overloaded ==. Tests for Equality.
---@param vector Vector
---@return boolean
function VectorClass:__eq(vector) end
---Overloaded .. Converts vectors to strings
---Does not appear to work.
---@return string
---@deprecated
function VectorClass:__tostring() end
---Cross product of two vectors.
---@param vector Vector
---@return Vector
function VectorClass:Cross(vector) end
---Dot product of two vectors.
---@param vector Vector
---@return number
function VectorClass:Dot(vector) end
---Length of the Vector.
---@return number
function VectorClass:Length() end
---Length of the Vector in the XY plane.
---@return number
function VectorClass:Length2D() end
---Linear interpolation between the vector and the passed in target over t = [0,1].
---@param target Vector
---@param t number
---@return Vector
function VectorClass:Lerp(target, t) end
---Returns the vector normalized.
---@return Vector
function VectorClass:Normalized() end

--#endregion

--#endregion

----------
--- Enumerations
--#region

--#region Analog Input Actions

-- Actions for CBasePlayer:GetAnalogActionPositionForHand. These map to the actions in the SteamVR binding menu.
---@alias ENUM_ANALOG_INPUT_ACTIONS
---| "0" # Hand | Hand Curl | X Axis
---| "1" # Hand | Trigger Pull | X Axis
---| "2" # Interact | Squeeze Xen Grenade | X Axis
---| "3" # Move | Teleport Turn | Required X, Y Axis
---| "4" # Move | Continuous Turn | X, Y Axis

--#endregion

--#region Controller types

-- Player VR controller types returned by CBasePlayer::GetVRControllerType()
-- Warning: The enumerations are missing from the scripting environment.
---@alias ENUM_CONTROLLER_TYPES
---| "0" # VR_CONTROLLER_TYPE_UNKNOWN
---| "1" # VR_CONTROLLER_TYPE_X360
---| "2" # VR_CONTROLLER_TYPE_VIVE
---| "3" # VR_CONTROLLER_TYPE_TOUCH
---| "4" # VR_CONTROLLER_TYPE_RIFT_S
---| "5" # UNKNOWN
---| "6" # VR_CONTROLLER_TYPE_KNUCKLES
---| "7" # VR_CONTROLLER_TYPE_WINDOWSMR
---| "8" # VR_CONTROLLER_TYPE_WINDOWSMR_SAMSUNG
---| "9" # VR_CONTROLLER_TYPE_GENERIC_TRACKED
---| "10" # VR_CONTROLLER_TYPE_COSMOS

--#endregion

--#region Digital Input Actions

-- Actions for CBasePlayer:IsDigitalActionOnForHand. These map to the actions in the SteamVR binding menu.
-- Note: No enumerations exist in the game for these yet.
---@alias ENUM_DIGITAL_INPUT_ACTIONS
---| "0" # Menu > Toggle Menu
---| "1" # Menu > Menu Interact
---| "2" # Menu > Menu Dismiss
---| "3" # Interact > Use
---| "4" # Interact > Use Grip
---| "5" # Weapon > Show inventory
---| "6" # Interact > Grav Glove Lock
---| "7" # Weapon > Fire
---| "8" # Weapon > Alt Fire
---| "9" # Weapon > Reload
---| "10" # Weapon > Eject Magazine
---| "11" # Weapon > Slide Release
---| "12" # Weapon > Open Chamber
---| "13" # Weapon > Toggle Laser Sight
---| "14" # Weapon > Toggle Burst Fire
---| "15" # Interact > Toggle Health Pen
---| "16" # Interact > Arm Grenade
---| "17" # Interact > Arm Xen Grenade
---| "18" # Move > Teleport
---| "19" # Move > Turn Left
---| "20" # Move > Turn Right
---| "21" # Move > Move Back
---| "22" # Move > Walk
---| "23" # Move > Jump
---| "24" # Move > Mantle
---| "25" # Move > Crouch Toggle
---| "26" # Move > Stand toggle
---| "27" # Move > Adjust Height

--#endregion

--#region Activation types

-- Passed to the Activate() hook function.
ACTIVATE_TYPE_INITIAL_CREATION    = 0
ACTIVATE_TYPE_DATAUPDATE_CREATION = 1
ACTIVATE_TYPE_ONRESTORE           = 2
---@alias ENUM_ACTIVATION_TYPES
---| "0" # When the function is called after entity creation.
---| "1" # Unknown.
---| "2" # When the function is called after the entity has been restored from a saved game.

--#endregion

--#region Damage types

DMG_GENERIC                 =	0
DMG_CRUSH                   =	1
DMG_BULLET                  =	2
DMG_SLASH                   =	4
DMG_BURN                    =   8
DMG_VEHICLE                 =	16
DMG_FALL                    =	32
DMG_BLAST                   =	64
DMG_CLUB                    =	128
DMG_SHOCK                   =	256
DMG_SONIC                   =	512
DMG_ENERGYBEAM              =	1024
DMG_PREVENT_PHYSICS_FORCE   =	2048
DMG_NEVERGIB                =	4096
DMG_ALWAYSGIB               =	8192
DMG_DROWN                   =	16384
DMG_PARALYZE                =	32768
DMG_NERVEGAS                =	65536
DMG_POISON                  =	131072
DMG_RADIATION               =	262144
DMG_DROWNRECOVER            =	524288
DMG_ACID                    =	1048576
DMG_SLOWBURN                =	2097152
DMG_REMOVENORAGDOLL         =	4194304
DMG_PHYSGUN                 =	8388608
DMG_PLASMA                  =	16777216
DMG_AIRBOAT                 =	33554432
DMG_DISSOLVE                =	67108864
DMG_BLAST_SURFACE           =	134217728
DMG_DIRECT                  =   268435456
DMG_BUCKSHOT                =   536870912 -- Shotgun damage. Gibs headcrabs.
---@alias ENUM_DAMAGE_TYPES
---| "0" # DMG_GENERIC
---| "1" # DMG_CRUSH
---| "2" # DMG_BULLET
---| "4" # DMG_SLASH
---| "8" # DMG_BURN
---| "16" # DMG_VEHICLE
---| "32" # DMG_FALL
---| "64" # DMG_BLAST
---| "128" # DMG_CLUB
---| "256" # DMG_SHOCK
---| "512" # DMG_SONIC
---| "1024" # DMG_ENERGYBEAM
---| "2048" # DMG_PREVENT_PHYSICS_FORCE
---| "4096" # DMG_NEVERGIB
---| "8192" # DMG_ALWAYSGIB
---| "16384" # DMG_DROWN
---| "32768" # DMG_PARALYZE
---| "65536" # DMG_NERVEGAS
---| "131072" # DMG_POISON
---| "262144" # DMG_RADIATION
---| "524288" # DMG_DROWNRECOVER
---| "1048576" # DMG_ACID
---| "2097152" # DMG_SLOWBURN
---| "4194304" # DMG_REMOVENORAGDOLL
---| "8388608" # DMG_PHYSGUN
---| "16777216" # DMG_PLASMA
---| "33554432" # DMG_AIRBOAT
---| "67108864" # DMG_DISSOLVE
---| "134217728" # DMG_BLAST_SURFACE
---| "268435456" # DMG_DIRECT
---| "536870912" # DMG_BUCKSHOT

--#endregion

--#region ParticleAttachment_t

-- Commented out names don't exist
--PATTACH_INVALID             = -1
PATTACH_ABSORIGIN           = 0
PATTACH_ABSORIGIN_FOLLOW    = 1
PATTACH_CUSTOMORIGIN        = 2
PATTACH_CUSTOMORIGIN_FOLLOW = 3
PATTACH_POINT               = 4
PATTACH_POINT_FOLLOW        = 5
PATTACH_EYES_FOLLOW         = 6
PATTACH_OVERHEAD_FOLLOW     = 7
PATTACH_WORLDORIGIN         = 8
PATTACH_ROOTBONE_FOLLOW     = 9
PATTACH_RENDERORIGIN_FOLLOW = 10
--PATTACH_MAIN_VIEW           = 11
--PATTACH_WATERWAKE           = 12
--PATTACH_CENTER_FOLLOW       = 13
--PATTACH_CUSTOM_GAME_STATE_1 = 14
MAX_PATTACH_TYPES           = 15
---@alias ENUM_PATTACH
---| "-1" # PATTACH_INVALID
---| "0" # PATTACH_ABSORIGIN
---| "1" # PATTACH_ABSORIGIN_FOLLOW
---| "2" # PATTACH_CUSTOMORIGIN
---| "3" # PATTACH_CUSTOMORIGIN_FOLLOW
---| "4" # PATTACH_POINT
---| "5" # PATTACH_POINT_FOLLOW
---| "6" # PATTACH_EYES_FOLLOW
---| "7" # PATTACH_OVERHEAD_FOLLOW
---| "8" # PATTACH_WORLDORIGIN
---| "9" # PATTACH_ROOTBONE_FOLLOW
---| "10" # PATTACH_RENDERORIGIN_FOLLOW
---| "11" # PATTACH_MAIN_VIEW
---| "12" # PATTACH_WATERWAKE
---| "13" # PATTACH_CENTER_FOLLOW
---| "14" # PATTACH_CUSTOM_GAME_STATE_1
---| "15" # MAX_PATTACH_TYPES

--#endregion

--#region Effect flags

-- Enumerations used by Entity:AddEffects, Entity:RemoveEffects and Entity:IsEffectActive.
-- Names don't exist.
---@alias ENUM_EFFECT_FLAGS
---| "1" # EF_BONEMERGE Performs bone merge on client side
---| "2" # EF_BRIGHTLIGHT DLIGHT centered at entity origin
---| "4" # EF_DIMLIGHT Player flashlight
---| "8" # EF_NOINTERP Don't interpolate the next frame
---| "16" # EF_NOSHADOW Disables shadow
---| "32" # EF_NODRAW Prevents the entity from drawing and networking.
---| "64" # EF_NORECEIVESHADOW Don't receive shadows
---| "128" # EF_BONEMERGE_FASTCULL For use with EF_BONEMERGE. If this is set, then it places this ents origin at its parent and uses the parent's bbox + the max extents of the aiment. Otherwise, it sets up the parent's bones every frame to figure out where to place the aiment, which is inefficient because it'll setup the parent's bones even if the parent is not in the PVS.
---| "256" # EF_ITEM_BLINK Makes the entity blink
---| "512" # EF_PARENT_ANIMATES Always assume that the parent entity is animating
---| "1024" # EF_FOLLOWBONE Internal flag that is set by Entity:FollowBone

--#endregion

--#region Unknown globals

-- Found through brute force.
BURST   = 5
EMPTY   = 0
TAUNT   = 14
PRESIM  = 0
RELOAD  = 6
ACT_ARM = 71
ACT_FLY = 25
ACT_HOP = 30
ACT_RUN = 10
ACT_USE = 47

--#endregion

--#endregion