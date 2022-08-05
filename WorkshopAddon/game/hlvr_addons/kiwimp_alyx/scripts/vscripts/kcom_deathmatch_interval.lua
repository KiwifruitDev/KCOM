if kcom_active then
    local player = Entities:GetLocalPlayer();
    local playerOrigin = player:GetOrigin();
    local playerCenter = player:GetCenter();
    local playerAngles = player:GetAnglesAsVector();
    local playerHealth = player:GetHealth();

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

    local physicsprops = {
        "prop_physics",
        "prop_physics_interactive",
        "prop_physics_override",
        "prop_dry_erase_marker",
        "prop_animinteractable",
        "prop_door_rotating_physics",
        "npc_headcrab_armored",
        "npc_headcrab_runner",
        "npc_zombie",
        "npc_zombie_blind",
        "npc_headcrab",
        "npc_combine_s",
        "npc_antlion",
        "npc_strider",
    }

    local found = {}

    for _, prop in pairs(physicsprops) do
        local props = Entities:FindAllByClassname(prop);
        for _, entity in pairs(props) do
            local index = entity:GetName();
            local object = {};
            object.origin = entity:GetOrigin();
            object.angles = entity:GetAnglesAsVector();
            object.class = entity:GetClassname();
            object.entity = entity;
            if entcache[index] ~= nil then
                object.init = entcache[index].init;
                if index == "" then
                    entcache[index] = nil;
                    index = "kcom_object_"..math.floor(object.origin[1]).."_"..math.floor(object.origin[2]).."_"..math.floor(object.origin[3]);
                    entity:SetEntityName(index);
                    object.init = true;
                    entcache[index] = object;
                elseif not entcache[index].init then
                    entcache[index] = nil;
                    index = index .. "_"..math.floor(object.origin[1]).."_"..math.floor(object.origin[2]).."_"..math.floor(object.origin[3]);
                    entity:SetEntityName(index);
                    object.init = true;
                    entcache[index] = object;
                end
                if object.class == "prop_door_rotating_physics" and ((math.floor(entcache[index].angles[1]) ~= math.floor(object.angles[1])) or (math.floor(entcache[index].angles[2]) ~= math.floor(object.angles[2])) or (math.floor(entcache[index].angles[3]) ~= math.floor(object.angles[3]))) then
                    print("PHYS "..index.." "..object.origin[1].." "..object.origin[2].." "..object.origin[3].." "..object.angles[1].." "..object.angles[2].." "..object.angles[3].." KCOM");
                elseif (math.floor(entcache[index].origin[1]) ~= math.floor(object.origin[1])) or (math.floor(entcache[index].origin[2]) ~= math.floor(object.origin[2])) or (math.floor(entcache[index].origin[3]) ~= math.floor(object.origin[3])) then
                    print("PHYS "..index.." "..object.origin[1].." "..object.origin[2].." "..object.origin[3].." "..object.angles[1].." "..object.angles[2].." "..object.angles[3].." KCOM");
                end
            else
                object.init = false;
            end
            entcachenames[index] = true;
            found[index] = true;
            entcache[index] = object;
        end
    end

    local outputs = {
        "OnPressIn",
        "OnPressed",
        "OnPressOut",
        "OnStartTouch",
        "OnStartLook",
        "OnEndTouch",
        "OnEndLook",
        "OnTrigger",
    }

    local trackers = {
        "trigger_multiple",
        "trigger_once", -- special case
        "trigger_look",
        "trigger_proximity",
        "func_button",
        "func_physical_button",
    }

    for _, track in pairs(trackers) do
        for _, entity in pairs(Entities:FindAllByClassname(track)) do
            local index = entity:GetName();
            if entcache[index] == nil then
                local object = {};
                object.origin = entity:GetOrigin();
                object.angles = entity:GetAnglesAsVector();
                object.entity = entity;
                if index == "" then
                    index = "kcom_trigger_"..math.floor(object.origin[1]).."_"..math.floor(object.origin[2]).."_"..math.floor(object.origin[3]);
                    entity:SetEntityName(index);
                end
                for _, output in pairs(outputs) do
                    entity:RedirectOutput(output, "KCOM_"..output, thisEntity);
                end
                entcache[index] = object;
                entcachenames[index] = true;
            end
            found[index] = true;
        end
    end

    --[[
    for entname, _ in pairs(entcachenames) do
        if not found[entname] then
            if entname ~= "" then
                print("EREM "..entname.." KCOM");
            end
            entcache[entname] = nil;
            entcachenames[entname] = nil;
            found[entname] = nil;
        end
    end
    ]]--
    
    for _, v in pairs(kcom_npcs) do
        local health = v:GetHealth()
        if health < 100 and health > 8 then
            print("DMGE "..v:GetName().." "..100-health.." ".." KCOM");
            print(v:GetName().." "..100-health.." ");
        end
        v:SetHealth(100);
    end
else
    kcom_models = {
        "models/characters/combine_soldier_captain/combine_captain.vmdl",
        "models/characters/alyx/alyx.vmdl",
        "models/characters/eli/eli_end.vmdl",
        "models/characters/eli/eli.vmdl",
        "models/characters/combine_grunt/combine_grunt.vmdl",
        "models/creatures/zombie_classic/zombie_classic.vmdl",
        "models/creatures/zombie_blind/zombie_blind.vmdl",
        "models/characters/citizens/citizens_male.vmdl",
        "models/characters/citizens/citizen_female_02.vmdl",
        "models/player.vmdl",
        "models/editor/playerstart.vmdl",
        "models/characters/gordon/gordon.vmdl",
    }
    function fireit(params, output)
        local ent = params.caller;
        if ent ~= nil then
            local name = ent:GetName();
            if name ~= "" then
                print("FIRE "..name.." "..output.." KCOM");
            end
        end
    end
    KCOM_OnTrigger = function(params) fireit(params, "OnTrigger") end
    KCOM_OnStartTouch = function(params) fireit(params, "OnStartTouch") end
    KCOM_OnEndTouch = function(params) fireit(params, "OnEndTouch") end
    KCOM_OnStartLook = function(params) fireit(params, "OnStartLook") end
    KCOM_OnEndLook = function(params) fireit(params, "OnEndLook") end
    KCOM_OnPressIn = function(params) fireit(params, "OnPressIn") end
    KCOM_OnPressed = function(params) fireit(params, "OnPressed") end
    KCOM_OnPressOut = function(params) fireit(params, "OnPressOut") end
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
    Position = function(params) fireit(params, "Position") end
    PositionInverted = function(params) fireit(params, "PositionInverted") end
    PositionRaw = function(params) fireit(params, "PositionRaw") end
    Velocity = function(params) fireit(params, "Velocity") end
    PositionInitialLimitsRemap = function(params) fireit(params, "PositionInitialLimitsRemap") end
    OnGravityGunPull = function(params) fireit(params, "OnGravityGunPull") end
    kcom_toggletypes = {
        ["trigger_multiple"] = {"Disable", "Enable"},
        ["trigger_once"] = {"Disable", "Enable"},
        ["trigger_look"] = {"Disable", "Enable"},
        ["trigger_proximity"] = {"Disable", "Enable"},
        ["func_button"] = {"Lock", "Unlock"},
        ["func_physical_button"] = {"Lock", "Unlock"},
        ["prop_physics"] = {"DisableMotion", "EnableMotion"},
        ["prop_physics_interactive"] = {"DisableMotion", "EnableMotion"},
        ["prop_physics_override"] = {"DisableMotion", "EnableMotion"},
        ["prop_dry_erase_marker"] = {"DisableMotion", "EnableMotion"},
        ["prop_animinteractable"] = {"DisableInteraction", "DisableInteraction"},
        ["prop_door_rotating_physics"] = {"DisableMotion", "EnableMotion"},
    };
    kcom_active = true;
    kcom_heads = {};
    kcom_lefthands = {};
    kcom_righthands = {};
    kcom_text = {};
    kcom_npcs = {};
    kcom_player_count = 16;
    entcache = {};
    entcachenames = {};
    kcom_precache = SpawnEntityFromTableSynchronous("logic_script", {
        targetname = "kcom_precache",
        origin = "-16128 0 16128",
        vscripts = "kcom_precache"
    });
    for i = 0, kcom_player_count do
        local modelindex = i
        if modelindex > 11 then
            modelindex = modelindex - 12
        end
        table.insert(kcom_heads, SpawnEntityFromTableSynchronous("prop_dynamic", {
            origin = "-16128 0 16128",
            targetname = "kcom_head_" .. i,
            model = "models/props/choreo_office/headset_prop.vmdl",
            solid = "0",
        }));
        table.insert(kcom_lefthands, SpawnEntityFromTableSynchronous("prop_dynamic", {
            origin = "-16128 0 16128",
            targetname = "kcom_lefthand_" .. i,
            model = "models/hands/alyx_glove_left.vmdl",
            solid = "0",
            bodygroups = "{glove_type = 1}"
        }));
        table.insert(kcom_righthands, SpawnEntityFromTableSynchronous("prop_dynamic", {
            origin = "-16128 0 16128",
            targetname = "kcom_righthand_" .. i,
            model = "models/hands/alyx_glove_right.vmdl",
            solid = "0",
            bodygroups = "{glove_type = 1}"
        }));
        table.insert(kcom_righthands, SpawnEntityFromTableSynchronous("point_worldtext", {
            origin = "-16128 0 16128",
            targetname = "kcom_text_" .. i,
            fullbright = "1",
            color = "255 255 255 255",
            world_units_per_pixel = "0.05",
            font_size = "128",
            justify_horizontal = "1",
            justify_vertical = "1",
        }));
        table.insert(kcom_npcs, SpawnEntityFromTableSynchronous("generic_actor", {
            origin = "-16128 0 16128",
            targetname = "kcom_npc_" .. i,
            model = kcom_models[modelindex],
            --rendermode = "kRenderNone",
            act_as_flyer = "1",
            max_health = "100",
            health = "100",
        }));
    end
    Convars:RegisterCommand("kcom_setlocation", function(command, name, x, y, z, pitch, yaw, roll)
        local entity = Entities:FindByName(nil, name);
        if entity ~= nil then
            if kcom_toggletypes[entity:GetClassname()] ~= nil then
                DoEntFireByInstanceHandle(entity, kcom_toggletypes[entity:GetClassname()][1], "", 0, nil, nil);
            end
            if string.find(entity:GetClassname(), "door") then
                DoEntFireByInstanceHandle(entity, "DisableLatch", "", 0, nil, nil);
            end
            entity:SetOrigin(Vector(tonumber(x), tonumber(y), tonumber(z)));
            entity:SetAngles(tonumber(pitch), tonumber(yaw), tonumber(roll));
            --DoEntFireByInstanceHandle(entity, kcom_toggletypes[entity:GetClassname()][2], "", 0, nil, nil);
        end
    end, "Kiwi's Co-Op Mod", 0);
    Convars:RegisterCommand("kcom_fireoutput", function(command, name, type)
        local entity = Entities:FindByName(nil, name);
        if entity ~= nil then
            --[[
            if kcom_toggletypes[entity:GetClassname()] ~= nil then
                DoEntFireByInstanceHandle(entity, kcom_toggletypes[entity:GetClassname()][1], "", 0, nil, nil);
            end
            ]]--
            local player = Entities:GetLocalPlayer();
            entity:FireOutput(type, player, player, {}, 0);
            --[[
            if entity:GetClassname() == "trigger_once" then
                entity:RemoveSelf(); -- remove the trigger because that's what trigger_once does
            end
            ]]--
            --DoEntFireByInstanceHandle(entity, kcom_toggletypes[entity:GetClassname()][2], "", 0, nil, nil);
        end
    end, "Kiwi's Co-Op Mod", 0);
    Convars:RegisterCommand("kcom_grace", function(command, name, type)
        local entity = Entities:FindByName(nil, name);
        if entity ~= nil then
            DoEntFireByInstanceHandle(entity, kcom_toggletypes[entity:GetClassname()][2], "", 0, nil, nil);
        end
    end, "Kiwi's Co-Op Mod", 0);
    Convars:RegisterCommand("kcom_hurt", function(command, index, damage)
        local player = Entities:GetLocalPlayer();
        local dmg = tonumber(damage);
        if player:GetHealth() - dmg <= 0 then
            player:SetHealth(100);
            print("DEAD "..index.." KCOM");
        else
            player:SetHealth(player:GetHealth() - dmg);
        end
    end, "Kiwi's Co-Op Mod", 0);
    DoEntFire("player", "IgnoreFallDamage", "0.0", 0.0, self, self);
    print("MAPN "..GetMapName().." KCOM");
end