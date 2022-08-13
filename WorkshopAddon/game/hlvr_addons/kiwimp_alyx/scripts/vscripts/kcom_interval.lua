require "util.utilinit"

local Player = Entities:GetLocalPlayer();

KCOM_USE_UUIDS = true;
KCOM_API_VERSION = 4; -- this value will change if breaking changes are pushed to workshop
KCOM_ACTIVE = false;
KCOM_ENTCACHE = {};

print("KCOM Enabled!");

function Precache(context)
    PrecacheModel("models/props/choreo_office/headset_prop.vmdl", context)
    PrecacheModel("models/hands/alyx_glove_left.vmdl", context)
    PrecacheModel("models/hands/alyx_glove_right.vmdl", context)
end

function KiwisCoOpMod()
    if KCOM_ACTIVE then
        if kcom_inbetween >= 5 then
            kcom_inbetween = 0;
            local playerOrigin = Player:GetOrigin();
            local playerCenter = Player:GetCenter();
            local playerAngles = Player:GetAnglesAsVector();
            local playerHealth = Player:GetHealth();
            local head = Player:GetHMDAvatar()
            
            if head then
                local leftController = head:GetVRHand(0)
                local rightController = head:GetVRHand(1)
                local leftHand = GetHandFromController(leftController)
                local rightHand = GetHandFromController(rightController)

                local playerHead = head:GetAbsOrigin();
                local playerHeadAng = head:GetAnglesAsVector();
                local playerLeftHand = leftHand:GetAbsOrigin();
                local playerRightHand = rightHand:GetAbsOrigin();
                local playerLeftHandAngles = leftHand:GetAnglesAsVector();
                local playerRightHandAngles = rightHand:GetAnglesAsVector();

                print("HEAD "..playerHead[1].." "..playerHead[2].." "..playerHead[3].." "..playerHeadAng[1].." "..playerHeadAng[2].." "..playerHeadAng[3].." KCOM");
                print("HAND "..playerLeftHand[1].." "..playerLeftHand[2].." "..playerLeftHand[3].." "..playerLeftHandAngles[1].." "..playerLeftHandAngles[2].." "..playerLeftHandAngles[3].." "..playerRightHand[1].." "..playerRightHand[2].." "..playerRightHand[3].." "..playerRightHandAngles[1].." "..playerRightHandAngles[2].." "..playerRightHandAngles[3].." KCOM");
            else
                print("HEAD "..playerCenter[1].." "..playerCenter[2].." "..(playerCenter[3]+30).." "..playerAngles[1].." "..playerAngles[2].." "..playerAngles[3].." KCOM");
            end

            print("PLYR "..playerOrigin[1].." "..playerOrigin[2].." "..playerOrigin[3].." "..playerAngles[1].." "..playerAngles[2].." "..playerAngles[3].." "..playerHealth.." KCOM");
            
            KCOM_CacheSync();
        else
            kcom_inbetween = kcom_inbetween + 1;
        end
    else
        function uuid(name, x, y, z, classname)
            -- this system ensures that entities always have a unique ID
            -- it brings the most unique attributes of an entity together
            return classname.."+"..name.."+"..math.floor(x).."+"..math.floor(y).."+"..math.floor(z);
        end

        function KCOM_CacheSync()
            for i, object in pairs(KCOM_ENTCACHE) do
                local entity = object.entity;
                if IsValidEntity(entity) then
                    if not string.find(object.class, "trigger_") and not string.find(object.class, "func_") then
                        local origin = entity:GetAbsOrigin();
                        local angles = entity:GetAnglesAsVector();
                        if object.class == "prop_door_rotating_physics" and (math.floor(angles[1]) ~= math.floor(object.angles[1])) or (math.floor(angles[2]) ~= math.floor(object.angles[2])) or (math.floor(angles[3]) ~= math.floor(object.angles[3])) then
                            print("PHYS "..object.name.." "..origin[1].." "..origin[2].." "..origin[3].." "..angles[1].." "..angles[2].." "..angles[3].." KCOM");
                            object.angles = angles;
                        elseif (math.floor(origin[1]) ~= math.floor(object.origin[1])) or (math.floor(origin[2]) ~= math.floor(object.origin[2])) or (math.floor(origin[3]) ~= math.floor(object.origin[3])) then
                            print("PHYS "..object.name.." "..origin[1].." "..origin[2].." "..origin[3].." "..angles[1].." "..angles[2].." "..angles[3].." KCOM");
                            object.origin = origin;
                            object.angles = angles;
                        end
                        if string.find(object.class, "npc_") or object.class == "generic_actor" then
                            if object.class ~= "npc_furniture" then
                                local health = entity:GetHealth();
                                if health <= 0 then
                                    print("NPHP "..object.name.." 0 KCOM");
                                elseif object.health == nil or object.health > health then
                                    print("NPHP "..object.name.." "..health.." KCOM");
                                    object.health = health;
                                end
                            end
                        end
                    end
                else
                    if object.name ~= "" then
                        if string.find(object.class, "npc_") or object.class == "generic_actor" then
                            -- bruteforce npc death
                            if object.health == nil or object.health > 0 then
                                object.health = 0
                                print("NPHP "..object.name.." 0 KCOM");
                            end
                        elseif string.find(object.class, "prop_") or string.find(object.class, "item_") then
                            print("BRAK "..object.name.." KCOM");
                        else
                            print("EREM "..object.name.." KCOM");
                        end
                    end
                    object = nil;
                end
                KCOM_ENTCACHE[i] = object;
            end
        end

        function KCOM_Teleport()
            local origin = Player:GetAbsOrigin();
            local angles = Player:GetAnglesAsVector();
            print("TELE "..origin[1].." "..origin[2].." "..origin[3].." "..angles[1].." "..angles[2].." "..angles[3].." KCOM");
        end

        function KCOM_EntitySyncSpecific(entity)
            local precached = {}

            for _, object in ipairs(KCOM_ENTCACHE) do
                if IsValidEntity(object.entity) then
                    precached[object.entity:GetEntityIndex()] = true;
                end
            end

            if precached[entity:GetEntityIndex()] then
                return -- already precached
            end

            local object = {};
            object.name = entity:GetName();
            object.origin = entity:GetAbsOrigin();
            object.angles = entity:GetAnglesAsVector();
            object.class = entity:GetClassname();
            object.model = entity:GetModelName();
            object.entity = entity; --entity:GetEntityIndex();

            
            for _, output in pairs(kcom_outputs) do
                entity:RedirectOutput(output, "KCOM_"..output, thisEntity);
            end

            if string.find(object.class, "door") then
                DoEntFireByInstanceHandle(entity, "Unlock", "", 0, nil, nil);
                DoEntFireByInstanceHandle(entity, "DisableLatch", "", 0, nil, nil);
            end

            if object.class == "trigger_teleport" then
                entity:RedirectOutput("OnStartTouch", "KCOM_Teleport", thisEntity);
            end

            if object.name == "" then
                object.name = "kcom_kcoords_"..math.floor(object.origin[1]).."_"..math.floor(object.origin[2]).."_"..math.floor(object.origin[3]);
            end

            entity:SetEntityName(object.name);
            if KCOM_USE_UUIDS then
                local uu = uuid(object.name, object.origin[1], object.origin[2], object.origin[3], object.class);
                object.name = uu;
                KCOM_ENTCACHE[uu] = object;
            else
                KCOM_ENTCACHE[#KCOM_ENTCACHE+1] = object;
            end
        end

        function KCOM_Templated(tempent, ents)
            for entkey, entity in pairs(ents) do
                local name = entity:GetName();
                local origin = entity:GetAbsOrigin();
                if name == "" then
                    name = "kcom_kcoords_"..math.floor(origin[1]).."_"..math.floor(origin[2]).."_"..math.floor(origin[3]);
                end
                SendToConsole("kcom_cache_entity_nonuuid "..name);
            end
        end

        function KCOM_EntitySync(first)
            for _, template in pairs(Entities:FindAllByClassname("point_template")) do
                template:SetSpawnCallback(KCOM_Templated, thisEntity);
            end
            for prop, _ in pairs(kcom_trackers) do
                local props = Entities:FindAllByClassname(prop);
                for _, entity in pairs(props) do
                    if entity:GetName() ~= "kcom_timer" then -- logic_timer
                        KCOM_EntitySyncSpecific(entity);
                    end
                end
            end
        end

        -- thank you Epic#4527 from the source 2 modding discord!
        -- https://discord.com/channels/692784980304330853/713548145358929990/715966997103509578
        function GetHandFromController(controller)
            for k, child in ipairs(controller:GetChildren()) do
                if (child:GetClassname() == "hlvr_prop_renderable_glove") then
                    return child
                end
            end
            return controller
        end

        kcom_trackers = {
            -- triggers
            ["trigger_multiple"] = true,
            ["trigger_once"] = true, -- special case
            ["trigger_look"] = true,
            ["trigger_proximity"] = true,
            ["trigger_teleport"] = true,
            ["trigger_detect_bullet_fire"] = true,
            ["trigger_detect_explosion"] = true,
            ["trigger_blind_zombie_crash"] = true,
            ["trigger_blind_zombie_sound_area"] = true,
            ["trigger_blind_zombie_wander_area"] = true,
            ["trigger_foliage_interaction"] = true,
            ["trigger_gravity"] = true,

            -- logic
            ["logic_relay"] = true,
            ["logic_timer"] = true,
            ["math_counter"] = true,

            -- buttons
            ["func_button"] = true,
            ["func_physical_button"] = true,

            -- props
            ["prop_physics"] = true,
            ["prop_physics_interactive"] = true,
            ["prop_physics_override"] = true,
            ["prop_dry_erase_marker"] = true,
            ["prop_animinteractable"] = true,
            ["prop_door_rotating_physics"] = true,
            ["prop_handpose"] = false, -- experimental
            ["prop_ragdoll"] = true,
            ["prop_animating_breakable"] = true,
            ["item_item_crate"] = true,

            -- gameplay
            ["item_healthvial"] = true, -- health injection pen
            ["item_hlvr_clip_energygun"] = true, -- single pistol clip
            ["item_hlvr_clip_energygun_multiple"] = true, -- four pistol clips
            ["item_hlvr_clip_rapidfire"] = true, -- single combine power cell
            ["item_hlvr_clip_shotgun_single"] = true, -- single shotgun shell
            ["item_hlvr_clip_shotgun_multiple"] = true, -- box of shotgun shells
            ["item_hlvr_health_station_vial"] = true, -- antlion grub (vial) for health station
            ["item_hlvr_prop_battery"] = true, -- combine power station battery
            ["item_hlvr_weapon_tripmine"] = true, -- tripmines
            ["info_hlvr_holo_hacking_plug"] = true, -- hologram puzzles
            ["item_hlvr_crafting_currency_large"] = true, -- large resin
            ["item_hlvr_crafting_currency_small"] = true, -- small resin
            ["item_hlvr_grenade_bomb"] = true, -- grenade bomb
            ["item_hlvr_grenade_frag"] = true, -- grenade frag
            ["item_hlvr_grenade_remote_sticky"] = true, -- sticky grenade
            ["item_hlvr_headcrab_gland"] = true, -- headcrab heart

            -- npcs
            ["npc_furniture"] = true,
            ["npc_headcrab_armored"] = true,
            ["npc_headcrab_runner"] = true,
            ["npc_headcrab_black"] = true,
            ["npc_zombie"] = true,
            ["npc_zombie_blind"] = true, -- jeff
            ["npc_headcrab"] = true,
            ["npc_combine_s"] = true,
            ["npc_antlion"] = true,
            ["npc_strider"] = true,
            ["generic_actor"] = true,
            ["npc_manhack"] = true,
            ["npc_barnacle"] = true,
        };
        kcom_toggletypes = {
            ["trigger_multiple"] = {"Disable", "Enable"},
            ["trigger_once"] = {"Disable", "Enable"},
            ["trigger_look"] = {"Disable", "Enable"},
            ["trigger_proximity"] = {"Disable", "Enable"},
            ["trigger_teleport"] = {"Disable", "Enable"},
            ["trigger_detect_bullet_fire"] = {"Disable", "Enable"},
            ["trigger_detect_explosion"] = {"Disable", "Enable"},
            ["trigger_blind_zombie_crash"] = {"Disable", "Enable"},
            ["trigger_blind_zombie_sound_area"] = {"Disable", "Enable"},
            ["trigger_blind_zombie_wander_area"] = {"Disable", "Enable"},
            ["trigger_foliage_interaction"] = {"Disable", "Enable"},
            ["trigger_gravity"] = {"Disable", "Enable"},

            ["logic_relay"] = {"Disable", "Enable"},
            ["logic_timer"] = {"Disable", "Enable"},
            ["math_counter"] = {"Disable", "Enable"},

            ["func_button"] = {"Lock", "Unlock"},
            ["func_physical_button"] = {"Lock", "Unlock"},

            ["prop_physics"] = {"DisableMotion", "EnableMotion"},
            ["prop_physics_interactive"] = {"DisableMotion", "EnableMotion"},
            ["prop_physics_override"] = {"DisableMotion", "EnableMotion"},
            ["prop_dry_erase_marker"] = {"DisableMotion", "EnableMotion"},
            ["prop_animinteractable"] = {"DisableInteraction", "EnableInteraction"},
            ["prop_door_rotating_physics"] = {"DisableMotion", "EnableMotion"},
            ["prop_animating_breakable"] = {"DisableMotion", "EnableMotion"},
            ["prop_ragdoll"] = {"DisableMotion", "EnableMotion"},
            --["prop_handpose"] = {"Disable", "Enable"},

            ["item_healthvial"] = {"DisableMotion", "EnableMotion"},
            ["item_hlvr_clip_energygun"] = {"DisableMotion", "EnableMotion"},
            ["item_hlvr_clip_energygun_multiple"] = {"DisableMotion", "EnableMotion"},
            ["item_hlvr_clip_rapidfire"] = {"DisableMotion", "EnableMotion"},
            ["item_hlvr_clip_shotgun_single"] = {"DisableMotion", "EnableMotion"},
            ["item_hlvr_clip_shotgun_multiple"] = {"DisableMotion", "EnableMotion"},
            ["item_hlvr_health_station_vial"] = {"DisableMotion", "EnableMotion"},
            ["item_hlvr_prop_battery"] = {"DisableMotion", "EnableMotion"},
            ["item_hlvr_weapon_tripmine"] = {"DisableMotion", "EnableMotion"},
            ["item_hlvr_crafting_currency_large"] = {"DisableMotion", "EnableMotion"},
            ["item_hlvr_crafting_currency_small"] = {"DisableMotion", "EnableMotion"},
            ["item_hlvr_grenade_bomb"] = {"DisableMotion", "EnableMotion"},
            ["item_hlvr_grenade_frag"] = {"DisableMotion", "EnableMotion"},
            ["item_hlvr_grenade_remote_sticky"] = {"DisableMotion", "EnableMotion"},
            ["item_hlvr_headcrab_gland"] = {"DisableMotion", "EnableMotion"},

            -- npcs are loose on purpose as they continuously move
        };

        local function KCOM_PickupSync(data)
            local handId = util.GetHandIdFromTip(data.vr_tip_attachment)
            local hand = Player.Hands[handId + 1]
            local ent_held = util.EstimateNearestEntity(data.item_name, data.item, hand:GetOrigin())
            if IsValidEntity(ent_held) then
                KCOM_EntitySyncSpecific(ent_held)
                local name = ent_held:GetName()
                local origin = ent_held:GetOrigin()
                print("SPWN " .. data.item .. " " .. name .. " " .. origin[1] .. " " .. origin[2] .. " " .. origin[3])
            end
        end
        ListenToGameEvent("item_pickup", KCOM_PickupSync, nil)

        local function KCOM_ResinSync(data)
            print("CRFT " .. Player.Items.resin_found .. " KCOM");
        end
        ListenToGameEvent("player_drop_resin_in_backpack", KCOM_ResinSync, nil)

        function fireit(params, output)
            local ent = params.caller;
            if not IsValidEntity(ent) then
                return -- the entity must have been deleted?
            end
            local name = ent:GetName();
            if not KCOM_USE_UUIDS then
                if name ~= "" then
                    print("FIRE "..name.." "..output.." KCOM");
                end
            else
                local class = ent:GetClassname();
                local origin = ent:GetOrigin();
                local uu = uuid(name, origin[1], origin[2], origin[3], class);
                print("FIRE "..uu.." "..output.." KCOM");
            end
        end

        KCOM_OnTrigger = function(params) fireit(params, "OnTrigger") end
        KCOM_OnStartTouch = function(params) fireit(params, "OnStartTouch") end
        KCOM_OnEndTouch = function(params) fireit(params, "OnEndTouch") end
        KCOM_OnStartLook = function(params) fireit(params, "OnStartLook") end
        KCOM_OnEndLook = function(params) fireit(params, "OnEndLook") end
        KCOM_OnIn = function(params) fireit(params, "OnIn") end
        KCOM_OnPressed = function(params) fireit(params, "OnPressed") end
        KCOM_OnOut = function(params) fireit(params, "OnOut") end
        KCOM_OnPlayerUse = function(params) fireit(params, "OnPlayerUse") end
        KCOM_OnPlayerPickup = function(params) fireit(params, "OnPlayerPickup") end
        KCOM_OnGlovePulled = function(params) fireit(params, "OnGlovePulled") end
        KCOM_OnAwakened = function(params) fireit(params, "OnAwakened") end
        KCOM_OnMotionEnabled = function(params) fireit(params, "OnMotionEnabled") end
        KCOM_OnAwake = function(params) fireit(params, "OnAwake") end
        KCOM_OnAsleep = function(params) fireit(params, "OnAsleep") end
        KCOM_OnOutOfWorld = function(params) fireit(params, "OnOutOfWorld") end
        KCOM_OnHealthChanged = function(params) fireit(params, "OnHealthChanged") end
        KCOM_OnBreak = function(params) fireit(params, "OnBreak") end
        KCOM_OnTakeDamage = function(params) fireit(params, "OnTakeDamage") end
        KCOM_OnIgnite = function(params) fireit(params, "OnIgnite") end
        KCOM_OnUser1 = function(params) fireit(params, "OnUser1") end
        KCOM_OnUser2 = function(params) fireit(params, "OnUser2") end
        KCOM_OnUser3 = function(params) fireit(params, "OnUser3") end
        KCOM_OnUser4 = function(params) fireit(params, "OnUser4") end
        KCOM_OnKilled = function(params) fireit(params, "OnKilled") end
        KCOM_OnInteractStart = function(params) fireit(params, "OnInteractStart") end
        KCOM_OnInteractStop = function(params) fireit(params, "OnInteractStop") end
        KCOM_OnCompletionA = function(params) fireit(params, "OnCompletionA") end
        KCOM_OnCompletionB = function(params) fireit(params, "OnCompletionB") end
        KCOM_OnCompletionC = function(params) fireit(params, "OnCompletionC") end
        KCOM_OnCompletionD = function(params) fireit(params, "OnCompletionD") end
        KCOM_OnCompletionE = function(params) fireit(params, "OnCompletionE") end
        KCOM_OnCompletionF = function(params) fireit(params, "OnCompletionF") end
        KCOM_OnCompletionA_Forward = function(params) fireit(params, "OnCompletionA_Forward") end
        KCOM_OnCompletionB_Forward = function(params) fireit(params, "OnCompletionB_Forward") end
        KCOM_OnCompletionC_Forward = function(params) fireit(params, "OnCompletionC_Forward") end
        KCOM_OnCompletionD_Forward = function(params) fireit(params, "OnCompletionD_Forward") end
        KCOM_OnCompletionE_Forward = function(params) fireit(params, "OnCompletionE_Forward") end
        KCOM_OnCompletionF_Forward = function(params) fireit(params, "OnCompletionF_Forward") end
        KCOM_OnCompletionA_Backward = function(params) fireit(params, "OnCompletionA_Forward") end
        KCOM_OnCompletionB_Backward = function(params) fireit(params, "OnCompletionB_Forward") end
        KCOM_OnCompletionC_Backward = function(params) fireit(params, "OnCompletionC_Forward") end
        KCOM_OnCompletionD_Backward = function(params) fireit(params, "OnCompletionD_Forward") end
        KCOM_OnCompletionE_Backward = function(params) fireit(params, "OnCompletionE_Forward") end
        KCOM_OnCompletionF_Backward = function(params) fireit(params, "OnCompletionF_Forward") end
        KCOM_OnCompletionExitA = function(params) fireit(params, "OnCompletionExitA") end
        KCOM_OnCompletionExitB = function(params) fireit(params, "OnCompletionExitB") end
        KCOM_OnCompletionExitC = function(params) fireit(params, "OnCompletionExitC") end
        KCOM_OnCompletionExitD = function(params) fireit(params, "OnCompletionExitD") end
        KCOM_OnCompletionExitE = function(params) fireit(params, "OnCompletionExitE") end
        KCOM_OnCompletionExitF = function(params) fireit(params, "OnCompletionExitF") end
        KCOM_OnReturnToCompletion = function(params) fireit(params, "OnReturnToCompletion") end
        KCOM_Position = function(params) fireit(params, "Position") end
        KCOM_PositionInverted = function(params) fireit(params, "PositionInverted") end
        KCOM_PositionRaw = function(params) fireit(params, "PositionRaw") end
        KCOM_Velocity = function(params) fireit(params, "Velocity") end
        KCOM_PositionInitialLimitsRemap = function(params) fireit(params, "PositionInitialLimitsRemap") end
        KCOM_OnGravityGunPull = function(params) fireit(params, "OnGravityGunPull") end
        KCOM_OnHandPosed = function(params) fireit(params, "OnHandPosed") end
        KCOM_OnHandUnPosed = function(params) fireit(params, "OnHandUnPosed") end
        KCOM_OnExplode = function(params) fireit(params, "OnExplode") end
        KCOM_OnHackStarted = function(params) fireit(params, "OnHackStarted") end
        KCOM_OnHackStopped = function(params) fireit(params, "OnHackStopped") end
        KCOM_OnHackSuccess = function(params) fireit(params, "OnHackSuccess") end
        KCOM_OnHackFailed = function(params) fireit(params, "OnHackFailed") end
        KCOM_OnHackSuccessAnimationComplete = function(params) fireit(params, "OnHackSuccessAnimationComplete") end
        KCOM_OnPuzzleCompleted = function(params) fireit(params, "OnPuzzleCompleted") end
        KCOM_OnPuzzleSuccess = function(params) fireit(params, "OnPuzzleSuccess") end
        KCOM_OnPuzzleFailed = function(params) fireit(params, "OnPuzzleFailed") end
        KCOM_OnEntitySpawned = function(params) fireit(params, "OnEntitySpawned") end
        KCOM_OnTimer = function(params) fireit(params, "OnTimer") end
        KCOM_OnTimerHigh = function(params) fireit(params, "OnTimerHigh") end
        KCOM_OnTimerLow = function(params) fireit(params, "OnTimerLow") end
        KCOM_OnTrue = function(params) fireit(params, "OnTrue") end
        KCOM_OnFalse = function(params) fireit(params, "OnFalse") end
        KCOM_OnCase01 = function(params) fireit(params, "OnCase01") end
        KCOM_OnCase02 = function(params) fireit(params, "OnCase02") end
        KCOM_OnCase03 = function(params) fireit(params, "OnCase03") end
        KCOM_OnCase04 = function(params) fireit(params, "OnCase04") end
        KCOM_OnCase05 = function(params) fireit(params, "OnCase05") end
        KCOM_OnCase06 = function(params) fireit(params, "OnCase06") end
        KCOM_OnCase07 = function(params) fireit(params, "OnCase07") end
        KCOM_OnCase08 = function(params) fireit(params, "OnCase08") end
        KCOM_OnCase09 = function(params) fireit(params, "OnCase09") end
        KCOM_OnCase10 = function(params) fireit(params, "OnCase10") end
        KCOM_OnCase11 = function(params) fireit(params, "OnCase11") end
        KCOM_OnCase12 = function(params) fireit(params, "OnCase12") end
        KCOM_OnCase13 = function(params) fireit(params, "OnCase13") end
        KCOM_OnCase14 = function(params) fireit(params, "OnCase14") end
        KCOM_OnCase15 = function(params) fireit(params, "OnCase15") end
        KCOM_OnCase16 = function(params) fireit(params, "OnCase16") end
        --KCOM_OutValue = function(params) fireit(params, "OutValue") end
        KCOM_OnHitMin = function(params) fireit(params, "OnHitMin") end
        KCOM_OnHitMax = function(params) fireit(params, "OnHitMax") end
        KCOM_OnGetValue = function(params) fireit(params, "OnGetValue") end
        KCOM_OnChangedFromMin = function(params) fireit(params, "OnChangedFromMin") end
        KCOM_OnChangedFromMax = function(params) fireit(params, "OnChangedFromMax") end
        KCOM_OnDetectedBulletFire = function(params) fireit(params, "OnDetectedBulletFire") end
        KCOM_OnDetectedExplosion = function(params) fireit(params, "OnDetectedExplosion") end
        KCOM_OnSoundGeneratedInside = function(params) fireit(params, "OnSoundGeneratedInside") end
        
        kcom_outputs = {
            "OnTrigger",
            "OnStartTouch",
            "OnEndTouch",
            "OnStartLook",
            "OnEndLook",
            "OnIn",
            "OnPressed",
            "OnOut",
            "OnPlayerUse",
            "OnPlayerPickup",
            "OnGlovePulled",
            "OnAwakened",
            "OnMotionEnabled",
            "OnAwake",
            "OnAsleep",
            "OnOutOfWorld",
            "OnHealthChanged",
            "OnBreak",
            "OnTakeDamage",
            "OnIgnite",
            "OnUser1",
            "OnUser2",
            "OnUser3",
            "OnUser4",
            "OnKilled",
            "OnInteractStart",
            "OnInteractStop",
            "OnCompletionA",
            "OnCompletionB",
            "OnCompletionC",
            "OnCompletionD",
            "OnCompletionE",
            "OnCompletionF",
            "OnCompletionA_Forward",
            "OnCompletionB_Forward",
            "OnCompletionC_Forward",
            "OnCompletionD_Forward",
            "OnCompletionE_Forward",
            "OnCompletionF_Forward",
            "OnCompletionA_Backward",
            "OnCompletionB_Backward",
            "OnCompletionC_Backward",
            "OnCompletionD_Backward",
            "OnCompletionE_Backward",
            "OnCompletionF_Backward",
            "OnCompletionExitA",
            "OnCompletionExitB",
            "OnCompletionExitC",
            "OnCompletionExitD",
            "OnCompletionExitE",
            "OnCompletionExitF",
            "OnReturnToCompletion",
            "Position",
            "PositionInverted",
            "PositionRaw",
            "Velocity",
            "PositionInitialLimitsRemap",
            "OnGravityGunPull",
            "OnHandPosed",
            "OnHandUnPosed",
            "OnExplode",
            "OnHackStarted",
            "OnHackStopped",
            "OnHackSuccess",
            "OnHackFailed",
            "OnHackSuccessAnimationComplete",
            "OnPuzzleCompleted",
            "OnPuzzleSuccess",
            "OnPuzzleFailed",
            "OnEntitySpawned",
            "OnTimer",
            "OnTimerHigh",
            "OnTimerLow",
            "OnTrue",
            "OnFalse",
            "OnCase01",
            "OnCase02",
            "OnCase03",
            "OnCase04",
            "OnCase05",
            "OnCase06",
            "OnCase07",
            "OnCase08",
            "OnCase09",
            "OnCase10",
            "OnCase11",
            "OnCase12",
            "OnCase13",
            "OnCase14",
            "OnCase15",
            "OnCase16",
            "OnHitMin",
            "OnHitMax",
            "OnGetValue",
            "OnChangedFromMin",
            "OnChangedFromMax",
        };

        KCOM_ACTIVE = true;
        kcom_heads = {};
        kcom_lefthands = {};
        kcom_righthands = {};
        kcom_text = {};
        kcom_player_count = 16;
        kcom_inbetween = 0;

        SpawnEntityFromTableSynchronous("point_worldtext", {
            origin = "16128 16128 16128",
            targetname = "kcom_hud",
            fullbright = "1",
            color = "255 255 255 255",
            world_units_per_pixel = "0.005",
            font_size = "128",
            justify_horizontal = "1",
            justify_vertical = "1",
            message = "",
            font_name = "Arial Black",
            reorient_mode = "0",
            depth_render_offset = "0.125",
            rendercolor = "255 255 255 255",
            enabled = "1",
        });

        for i = 0, kcom_player_count - 1 do
            table.insert(kcom_heads, SpawnEntityFromTableSynchronous("prop_dynamic", {
                origin = "16128 16128 16128",
                targetname = "kcom_head_" .. i,
                model = "models/props/choreo_office/headset_prop.vmdl",
                solid = "0",
            }));
            table.insert(kcom_lefthands, SpawnEntityFromTableSynchronous("prop_dynamic", {
                origin = "16128 16128 16128",
                targetname = "kcom_lefthand_" .. i,
                model = "models/hands/alyx_glove_left.vmdl",
                solid = "0",
            }));
            table.insert(kcom_righthands, SpawnEntityFromTableSynchronous("prop_dynamic", {
                origin = "16128 16128 16128",
                targetname = "kcom_righthand_" .. i,
                model = "models/hands/alyx_glove_right.vmdl",
                solid = "0",
            }));
            table.insert(kcom_text, SpawnEntityFromTableSynchronous("point_worldtext", {
                origin = "16128 16128 16128",
                targetname = "kcom_text_" .. i,
                fullbright = "1",
                color = "255 255 255 255",
                world_units_per_pixel = "0.05",
                font_size = "128",
                justify_horizontal = "1",
                justify_vertical = "1",
                message = "Placeholder",
                font_name = "Arial Black",
                reorient_mode = "0",
                depth_render_offset = "0.125",
                rendercolor = "255 255 255 255",
                enabled = "1",
            }));
        end

        Convars:RegisterCommand("kcom_teleport", function(command, x, y, z)
            if Player then
                local anchor = Player:GetHMDAnchor();
                if Player:GetHMDAvatar() then
                    anchor:SetAbsOrigin(Vector(tonumber(x), tonumber(y), tonumber(z)));
                else
                    Player:SetAbsOrigin(Vector(tonumber(x), tonumber(y), tonumber(z)));
                end
            end
        end, "Kiwi's Co-Op Mod", 0);

        Convars:RegisterCommand("kcom_teleportangles", function(command, x, y, z, pitch, yaw, roll)
            if Player then
                local anchor = Player:GetHMDAnchor();
                if Player:GetHMDAvatar() then
                    anchor:SetAbsOrigin(Vector(tonumber(x), tonumber(y), tonumber(z)));
                    anchor:SetAbsAngles(tonumber(pitch), tonumber(yaw), tonumber(roll));
                else
                    Player:SetAbsOrigin(Vector(tonumber(x), tonumber(y), tonumber(z)));
                    Player:SetAbsAngles(tonumber(pitch), tonumber(yaw), tonumber(roll));
                end
            end
        end, "Kiwi's Co-Op Mod", 0);

        Convars:RegisterCommand("kcom_setlocation", function(command, name, x, y, z, pitch, yaw, roll)
            if not KCOM_USE_UUIDS then
                local entities = Entities:FindAllByName(name);
                if entities ~= nil then
                    for _, entity in pairs(entities) do
                        local class = entity:GetClassname();
                        if kcom_toggletypes[class] ~= nil then
                            DoEntFireByInstanceHandle(entity, kcom_toggletypes[class][1], "", 0, nil, nil);
                        end
                        entity:SetAbsOrigin(Vector(tonumber(x), tonumber(y), tonumber(z)));
                        entity:SetAbsAngles(tonumber(pitch), tonumber(yaw), tonumber(roll));
                    end
                end
            else
                local object = KCOM_ENTCACHE[name];
                if not object then return end
                local entity = object.entity;
                if not IsValidEntity(entity) then return end
                if kcom_toggletypes[object.class] ~= nil then
                    DoEntFireByInstanceHandle(entity, kcom_toggletypes[object.class][1], "", 0, nil, nil);
                end
                entity:SetAbsOrigin(Vector(tonumber(x), tonumber(y), tonumber(z)));
                entity:SetAbsAngles(tonumber(pitch), tonumber(yaw), tonumber(roll));
            end
        end, "Kiwi's Co-Op Mod", 0);

        Convars:RegisterCommand("kcom_setlocation_nonuuid", function(command, name, x, y, z, pitch, yaw, roll)
            local entities = Entities:FindAllByName(name);
            if entities ~= nil then
                for _, entity in pairs(entities) do
                    local class = entity:GetClassname();
                    if kcom_toggletypes[class] ~= nil then
                        DoEntFireByInstanceHandle(entity, kcom_toggletypes[class][1], "", 0, nil, nil);
                    end
                    entity:SetAbsOrigin(Vector(tonumber(x), tonumber(y), tonumber(z)));
                    entity:SetAbsAngles(tonumber(pitch), tonumber(yaw), tonumber(roll));
                end
            end
        end, "Kiwi's Co-Op Mod", 0);

        Convars:RegisterCommand("kcom_fireoutput", function(command, name, type)
            if not KCOM_USE_UUIDS then
                local entities = Entities:FindAllByName(name);
                if entities ~= nil then
                    for _, entity in pairs(entities) do
                        local class = entity:GetClassname();
                        entity:FireOutput(type, Player, Player, {}, 0);
                        if class == "trigger_once" then
                            -- TODO: trigger_once, does it disable after firing?
                            DoEntFireByInstanceHandle(entity, "Disable", "", 0, nil, nil);
                        end
                    end
                end
            else
                local object = KCOM_ENTCACHE[name];
                if not object then return end
                local entity = object.entity;
                if not IsValidEntity(entity) then return end
                entity:FireOutput(type, Player, Player, {}, 0);
                if object.class == "trigger_once" then
                    -- TODO: trigger_once, does it disable after firing?
                    DoEntFireByInstanceHandle(entity, "Disable", "", 0, nil, nil);
                end
            end
        end, "Kiwi's Co-Op Mod", 0);

        Convars:RegisterCommand("kcom_grace", function(command, name, type)
            if not KCOM_USE_UUIDS then
                local entities = Entities:FindAllByName(name);
                if entities ~= nil then
                    for _, entity in pairs(entities) do
                        local class = entity:GetClassname();
                        if kcom_toggletypes[class] ~= nil then
                            DoEntFireByInstanceHandle(entity, kcom_toggletypes[class][2], "", 0, nil, nil);
                        end
                    end
                end
            else
                local object = KCOM_ENTCACHE[name];
                if not object then return end
                local entity = object.entity;
                if not IsValidEntity(entity) then return end
                if kcom_toggletypes[object.class] ~= nil then
                    DoEntFireByInstanceHandle(entity, kcom_toggletypes[object.class][2], "", 0, nil, nil);
                end
            end
        end, "Kiwi's Co-Op Mod", 0);

        Convars:RegisterCommand("kcom_npc_sethealth", function(command, name, health)
            if not KCOM_USE_UUIDS then
                local entities = Entities:FindAllByName(name);
                if entities ~= nil then
                    for _, entity in pairs(entities) do
                        local curhealth = entity:GetHealth();
                        if curhealth <= 0 then return end
                        if health < curhealth then
                            entity:SetHealth(tonumber(health));
                            local newhealth = entity:GetHealth();
                            if newhealth <= 0 then
                                entity:SetHealth(0);
                                StartSoundEvent("Combat.PlayerKilledNPC", Entities:GetLocalPlayer());
                                DoEntFireByInstanceHandle(entity, "BecomeRagdoll", "", 0, nil, nil);
                            end
                        end
                    end
                    --DoEntFireByInstanceHandle(entity, "StopTemporaryRagdoll", "", 0, nil, nil);
                    --DoEntFireByInstanceHandle(entity, "BecomeTemporaryRagdoll", "", 0, nil, nil);
                end
            else
                local object = KCOM_ENTCACHE[name];
                if not object then return end
                local entity = object.entity;
                if not IsValidEntity(entity) then return end
                local curhealth = entity:GetHealth();
                if curhealth <= 0 then return end
                entity:SetHealth(tonumber(health));
                local newhealth = entity:GetHealth();
                if newhealth < curhealth then
                    StartSoundEvent("DamageNPC.Bullet", Entities:GetLocalPlayer());
                end
                if newhealth <= 0 then
                    entity:SetHealth(0);
                    StartSoundEvent("Combat.PlayerKilledNPC", Entities:GetLocalPlayer());
                    DoEntFireByInstanceHandle(entity, "BecomeRagdoll", "", 0, nil, nil);
                end
            end
        end, "Kiwi's Co-Op Mod", 0);

        Convars:RegisterCommand("kcom_break", function(command, name)
            if not KCOM_USE_UUIDS then
                local entities = Entities:FindAllByName(name);
                if entities ~= nil then
                    for _, entity in pairs(entities) do
                        DoEntFireByInstanceHandle(entity, "Break", "", 0, nil, nil);
                    end
                end
            else
                local object = KCOM_ENTCACHE[name];
                if not object then return end
                local entity = object.entity;
                if not IsValidEntity(entity) then return end
                DoEntFireByInstanceHandle(entity, "Break", "", 0, nil, nil);
            end
        end, "Kiwi's Co-Op Mod", 0);

        Convars:RegisterCommand("kcom_sethealth", function(command, health)
            if Player then
                Player:SetHealth(tonumber(health));
            end
        end, "Kiwi's Co-Op Mod", 0);

        Convars:RegisterCommand("kcom_command", function(command, kcomcommand)
            print("CMND "..kcomcommand.." KCOM");
        end, "Kiwi's Co-Op Mod", 0);

        Convars:RegisterCommand("kcom_cache_entity", function(command, cacheent)
            if not KCOM_USE_UUIDS then
                local entities = Entities:FindAllByName(cacheent);
                if entities ~= nil then
                    for _, entity in pairs(entities) do
                        if IsValidEntity(entity) then
                            KCOM_EntitySyncSpecific(entity);
                        end
                    end
                end
            else
                local object = KCOM_ENTCACHE[cacheent];
                if not object then return end
                local entity = object.entity;
                if not IsValidEntity(entity) then return end
                KCOM_EntitySyncSpecific(entity);
            end
        end, "Kiwi's Co-Op Mod", 0);

        Convars:RegisterCommand("kcom_cache_entity_nonuuid", function(command, cacheent)
            print("Templated: "..cacheent);
            local entities = Entities:FindAllByName(cacheent);
            if entities ~= nil then
                for _, entity in pairs(entities) do
                    if IsValidEntity(entity) then
                        KCOM_EntitySyncSpecific(entity);
                    end
                end
            end
        end, "Kiwi's Co-Op Mod", 0);

        Convars:RegisterCommand("kcom_cache_all_entities", function(command)
            KCOM_EntitySync(false)
        end, "Kiwi's Co-Op Mod", 0);

        print("MAPN "..GetMapName().." "..KCOM_API_VERSION.." KCOM");
        KCOM_EntitySync(true);
    end
end