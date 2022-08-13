
MaxTraceDistance = MaxTraceDistance or 1024
IgnoredEntity = IgnoredEntity or nil
TargetHandle = TargetHandle or nil
local colorFromScales = nil

---Enables the laser to think and starts the particle.
function Enable()
    thisEntity:SetContextNum("IsEnabled", 1, 0)
    thisEntity:SetThink(UpdateLaser, "UpdateLaser", 0)
    DoEntFireByInstanceHandle(thisEntity, "Start", "", 0, nil, nil)
end
---Disables the laser from thinking and stops the particle.
function Disable()
    thisEntity:SetContextNum("IsEnabled", 0, 0)
    thisEntity:StopThink("UpdateLaser")
    DoEntFireByInstanceHandle(thisEntity, "StopPlayEndCap", "", 0, nil, nil)
end
---Sets the color by moving the color entity.
---@param red integer
---@param green integer
---@param blue integer
function SetColor(red, green, blue)
    print("color")
    local c = Entities:FindByName(nil, thisEntity:GetName().."_color")
    if c then
        c:SetOrigin(Vector(red, green, blue))
    end
end

---@param spawnkeys CScriptKeyValues
function Spawn(spawnkeys)
    local traceDist = tonumber(spawnkeys:GetValue("cpoint7_parent"))
    thisEntity:SetContextNum("MaxTraceDistance", type(traceDist) == "number" and traceDist or 0, 0)
    local ignoredName = spawnkeys:GetValue("cpoint62")
    if ignoredName and ignoredName ~= "" then
        thisEntity:SetContext("IgnoredEntityName", ignoredName, 0)
    end
    local startActive = spawnkeys:GetValue("start_active")
    if startActive then
        thisEntity:SetContextNum("IsEnabled", startActive and 1 or 0, 0)
    end
    -- Set color entity position
    colorFromScales = spawnkeys:GetValue("local.scales")
    thisEntity:SetContextThink("color", function()
        SetColor(colorFromScales.x,colorFromScales.y,colorFromScales.z)
    end, 0.01)
end

---@param activateType "0"|"1"|"2"
function Activate(activateType)
    -- Load values
    IgnoredEntity = thisEntity:GetContext("IgnoredEntityName")
    if IgnoredEntity then
        IgnoredEntity = Entities:FindByName(nil, IgnoredEntity)
    end
    MaxTraceDistance = thisEntity:GetContext("MaxTraceDistance")
    -- Find target entity
    TargetHandle = Entities:FindByName(nil, thisEntity:GetName().."_target")
    if thisEntity:GetContext("IsEnabled") == 1 then
        Enable()
    end
    print("traced_laser: ignored("..tostring(IgnoredEntity)..") distance("..tostring(MaxTraceDistance)..") target("..tostring(TargetHandle)..")")
end

function UpdateLaser()
    local traceTable = {
        startpos = thisEntity:GetOrigin(),
        endpos = thisEntity:GetOrigin() + thisEntity:GetForwardVector() * MaxTraceDistance,
        ignore = IgnoredEntity
    }
    TraceLine(traceTable)
    TargetHandle:SetOrigin(traceTable.pos)
    if traceTable.hit then
        TargetHandle:SetForwardVector(traceTable.normal)
    else
        TargetHandle:SetForwardVector(thisEntity:GetForwardVector())
    end
    return 0
end
