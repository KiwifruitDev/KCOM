--[[
    Entity script for maps/prefabs/logic/weighted_random/weighted_random.vmap
]]
DoIncludeScript("util/weighted_random", thisEntity:GetPrivateScriptScope())

---@type WeightedRandomObject
WR = WR or nil

---@param spawnkeys CScriptKeyValues
function Spawn(spawnkeys)
    local weights = {}
    for i = 1, 16 do
        local case = "Case" .. (i < 10 and "0" or "") .. i
        local weight = spawnkeys:GetValue(case)
        weight = tonumber(weight)
        if weight ~= nil and weight ~= 0 then
            weights[#weights+1] = { weight = weight, case = case}
            thisEntity:Attribute_SetFloatValue(case, weight)
            print(case, weight)
        end
    end
    WR = WeightedRandom(weights)
end

---@param activateType "0"|"1"|"2"
function Activate(activateType)
    if activateType == 2 then
        local weights = {}
        for i = 1, 16 do
            local case = "Case" .. (i < 10 and "0" or "") .. i
            local weight = thisEntity:Attribute_GetFloatValue(case, 0)
            if weight ~= 0 then
                weights[#weights+1] = { weight = weight, case = case}
            end
        end
        WR = WeightedRandom(weights)
    end
end

function PickRandomWeight(data)
    thisEntity:FireOutput("On"..WR:Random().case, data.activator, data.caller, {}, 0)
end
