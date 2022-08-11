if kcom_active then
    --if kcom_inbetween >= 5 then
        --kcom_inbetween = 0;

        local player = Entities:GetLocalPlayer();
        local playerOrigin = player:GetOrigin();
        local playerCenter = player:GetCenter();
        local playerAngles = player:GetAnglesAsVector();
        local playerHealth = player:GetHealth();
        local head = player:GetHMDAvatar()
        
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
    --else
        --kcom_inbetween = kcom_inbetween + 1;
    --end
else
    kcom_api_version = 4; -- this value will change if breaking changes are pushed to workshop

    function KCOM_CacheSync()
        for name, object in pairs(entcache) do
            if object.entity ~= nil and IsValidEntity(object.entity) then
                if not string.find(object.class, "trigger_") and not string.find(object.class, "func_") then
                    local origin = object.entity:GetAbsOrigin();
                    local angles = object.entity:GetAnglesAsVector();
                    if object.class == "prop_door_rotating_physics" and (math.floor(angles[1]) ~= math.floor(object.angles[1])) or (math.floor(angles[2]) ~= math.floor(object.angles[2])) or (math.floor(angles[3]) ~= math.floor(object.angles[3])) then
                        print("PHYS "..name.." "..origin[1].." "..origin[2].." "..origin[3].." "..angles[1].." "..angles[2].." "..angles[3].." KCOM");
                        entcache[name].angles = angles;
                    elseif (math.floor(origin[1]) ~= math.floor(object.origin[1])) or (math.floor(origin[2]) ~= math.floor(object.origin[2])) or (math.floor(origin[3]) ~= math.floor(object.origin[3])) then
                        print("PHYS "..name.." "..origin[1].." "..origin[2].." "..origin[3].." "..angles[1].." "..angles[2].." "..angles[3].." KCOM");
                        entcache[name].origin = origin;
                        entcache[name].angles = angles;
                    end
                    entcache[name] = object;
                    if string.find(object.class, "npc_") or object.class == "generic_actor" then
                        if not object.entity:IsAlive() then
                            npccache[name] = nil;
                            print("NPHP "..name.." 0 KCOM");
                        else
                            local health = object.entity:GetHealth();
                            local oldhealth = kcom_npc_damage_cache[name]
                            if oldhealth == nil then
                                oldhealth = health;
                            end
							if oldhealth ~= health then
                                print("NPHP "..name.." "..health.." KCOM");
							end
                            kcom_npc_damage_cache[name] = health;
                        end
                    end
                end
            else
                if name ~= "" then
                    if string.find(object.class, "prop_") or string.find(object.class, "item_") then
                        print("BRAK "..name.." KCOM");
                    else
                        print("EREM "..name.." KCOM");
                    end
                end
                entcache[name] = nil;
            end
        end
    end

    function KCOM_Teleport()
        local player = Entities:GetLocalPlayer();
        local origin = player:GetAbsOrigin();
	    local angles = player:GetAnglesAsVector();
        print("TELE "..origin[1].." "..origin[2].." "..origin[3].." "..angles[1].." "..angles[2].." "..angles[3].." KCOM");
    end

    function KCOM_EntitySyncSpecific(entity)
        local index = entity:GetName();
        local object = {};
        object.origin = entity:GetAbsOrigin();
        object.angles = entity:GetAnglesAsVector();
        object.class = entity:GetClassname();
        object.entity = entity;
        if string.find(object.class, "trigger_") or string.find(object.class, "func_") then
            if entcache[index] == nil then
                if index == "" then
                    index = "kcom_trigger_"..math.floor(object.origin[1]).."_kcoords_"..math.floor(object.origin[2]).."_"..math.floor(object.origin[3]);
                    entity:SetEntityName(index);
                end
                for _, output in pairs(kcom_outputs) do
                    entity:RedirectOutput(output, "KCOM_"..output, thisEntity);
                end
                if object.class == "trigger_teleport" then
                    entity:RedirectOutput("OnTrigger", "KCOM_Teleport", thisEntity);
                end
            end
        elseif string.find(object.class, "door") then
            DoEntFireByInstanceHandle(entity, "Unlock", "", 0, nil, nil);
            DoEntFireByInstanceHandle(entity, "DisableLatch", "", 0, nil, nil);
        elseif entcache[index] ~= nil and not string.find(index, "_kcoords_") then
            if index == "" then
                entcache[index] = nil;
                index = "kcom_object_kcoords_"..math.floor(object.origin[1]).."_"..math.floor(object.origin[2]).."_"..math.floor(object.origin[3]);
                entity:SetEntityName(index);
            else
                entcache[index] = nil;
                index = index .. "_kcoords_"..math.floor(object.origin[1]).."_"..math.floor(object.origin[2]).."_"..math.floor(object.origin[3]);
                entity:SetEntityName(index);
            end
        end
        entcache[index] = object;
    end

    function KCOM_EntitySync()
        entcache = {};
        for prop, _  in pairs(kcom_trackers) do
            local props = Entities:FindAllByClassname(prop);
            for _, entity in pairs(props) do
                KCOM_EntitySyncSpecific(entity)
            end
        end
    end
    
    for _, entity in pairs(Entities:FindAllByClassname("point_template")) do
        entity:RedirectOutput("OnEntitySpawned", "KCOM_EntitySync", thisEntity);
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
        ["prop_handpose"] = true,
        ["prop_animating_breakable"] = true,

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
        ["npc_headcrab_armored"] = true,
        ["npc_headcrab_runner"] = true,
        ["npc_headcrab_black"] = true,
        ["npc_zombie"] = true,
        ["npc_zombie_blind"] = true,
        ["npc_headcrab"] = true,
        ["npc_combine_s"] = true,
        ["npc_antlion"] = true,
        ["npc_strider"] = true,
        ["generic_actor"] = true,
        ["npc_manhack"] = true,
    };
    kcom_toggletypes = {
        ["trigger_multiple"] = {"Disable", "Enable"},
        ["trigger_once"] = {"Disable", "Enable"},
        ["trigger_look"] = {"Disable", "Enable"},
        ["trigger_proximity"] = {"Disable", "Enable"},
        ["trigger_teleport"] = {"Disable", "Enable"},

        ["func_button"] = {"Lock", "Unlock"},
        ["func_physical_button"] = {"Lock", "Unlock"},

        ["prop_physics"] = {"DisableMotion", "EnableMotion"},
        ["prop_physics_interactive"] = {"DisableMotion", "EnableMotion"},
        ["prop_physics_override"] = {"DisableMotion", "EnableMotion"},
        ["prop_dry_erase_marker"] = {"DisableMotion", "EnableMotion"},
        ["prop_animinteractable"] = {"DisableInteraction", "EnableInteraction"},
        ["prop_door_rotating_physics"] = {"DisableMotion", "EnableMotion"},
        ["prop_animating_breakable"] = {"DisableMotion", "EnableMotion"},
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

    function fireit(params, output)
        local ent = params.caller;
        local name = ent:GetName();
        if name ~= "" then
            print("FIRE "..name.." "..output.." KCOM");
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
    };

    kcom_active = true;
    kcom_heads = {};
    kcom_lefthands = {};
    kcom_righthands = {};
    kcom_text = {};
    kcom_player_count = 16;
    kcom_npc_damage_cache = {};
    kcom_inbetween = 0;
    entcache = {};

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
        local player = Entities:GetLocalPlayer();
        if player then
            local anchor = player:GetHMDAnchor();
            if player:GetHMDAvatar() then
                anchor:SetAbsOrigin(Vector(tonumber(x), tonumber(y), tonumber(z)));
            else
                player:SetAbsOrigin(Vector(tonumber(x), tonumber(y), tonumber(z)));
            end
        end
    end, "Kiwi's Co-Op Mod", 0);

	Convars:RegisterCommand("kcom_teleportangles", function(command, x, y, z, pitch, yaw, roll)
        local player = Entities:GetLocalPlayer();
        if player then
            local anchor = player:GetHMDAnchor();
            if player:GetHMDAvatar() then
                anchor:SetAbsOrigin(Vector(tonumber(x), tonumber(y), tonumber(z)));
				anchor:SetAbsAngles(Angle(tonumber(pitch), tonumber(yaw), tonumber(roll)));
            else
                player:SetAbsOrigin(Vector(tonumber(x), tonumber(y), tonumber(z)));
				player:SetAbsAngles(Angle(tonumber(pitch), tonumber(yaw), tonumber(roll)));
            end
        end
    end, "Kiwi's Co-Op Mod", 0);

    Convars:RegisterCommand("kcom_setlocation", function(command, name, x, y, z, pitch, yaw, roll)
        local entity = Entities:FindByName(nil, name);
        if entity ~= nil then
            local class = entity:GetClassname();
            if kcom_toggletypes[class] ~= nil then -- TODO: what does this code do? why do you need to re-enable entities?
                DoEntFireByInstanceHandle(entity, kcom_toggletypes[entity:GetClassname()][1], "", 0, nil, nil);
            end
            entity:SetAbsOrigin(Vector(tonumber(x), tonumber(y), tonumber(z)));
            entity:SetAbsAngles(tonumber(pitch), tonumber(yaw), tonumber(roll));
        end
    end, "Kiwi's Co-Op Mod", 0);

    Convars:RegisterCommand("kcom_fireoutput", function(command, name, type)
        local entity = Entities:FindByName(nil, name);
        if entity ~= nil then
            entity:FireOutput(type, player, player, {}, 0);
            -- TODO: trigger_once, does it disable after firing?
            --DoEntFireByInstanceHandle(entity, "Disable", "", 0, nil, nil);
        end
    end, "Kiwi's Co-Op Mod", 0);

    Convars:RegisterCommand("kcom_grace", function(command, name, type)
        local entity = Entities:FindByName(nil, name);
        if entity ~= nil then
            local class = entity:GetClassname();
            if kcom_toggletypes[class] ~= nil then
                DoEntFireByInstanceHandle(entity, kcom_toggletypes[entity:GetClassname()][2], "", 0, nil, nil);
            end
        end
    end, "Kiwi's Co-Op Mod", 0);

    Convars:RegisterCommand("kcom_npc_sethealth", function(command, name, health)
        local entity = Entities:FindByName(nil, name);
        if entity ~= nil then
            --DoEntFireByInstanceHandle(entity, "StopTemporaryRagdoll", "", 0, nil, nil);
            entity:SetHealth(tonumber(health));
            --DoEntFireByInstanceHandle(entity, "BecomeTemporaryRagdoll", "", 0, nil, nil);
        end
    end, "Kiwi's Co-Op Mod", 0);

    Convars:RegisterCommand("kcom_command", function(command, kcomcommand)
        print("CMND "..kcomcommand.." KCOM");
    end, "Kiwi's Co-Op Mod", 0);

    print("MAPN "..GetMapName().." "..kcom_api_version.." KCOM");
    
    KCOM_EntitySync();
end
