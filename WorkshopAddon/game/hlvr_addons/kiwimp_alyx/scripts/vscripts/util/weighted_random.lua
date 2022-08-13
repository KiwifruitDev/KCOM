--[[
    v1.1.1
    Weighted random allows you to assign chances to tables.

    Should be loaded into global scope for using the following line:

        require "util.weighted_random"

    -

    The WeightedRandom() function takes a list of tables where each one must at least
    contain a key with the name "weight" as a number value. The table may contain any
    other keys/values you wish and will be returned if this weight is chosen.

    Weights do not need to be [0-1] range.

    Usage example:

    local wr = WeightedRandom({
        { weight = 1, name = "Common" },
        { weight = 0.75, name = "Semi-common" },
        { weight = 0.5, name = "Uncommon" },
        { weight = 0.25, name = "Rare" },
        { weight = 0.1, name = "Extremely rare" },
    })

    for i = 1, 20 do
        print(wr:Random().name)
    end
]]

---@class WeightedRandom
local WeightedRandomBaseClass = {
    ---Root list containing all weighted tables.
    ---@type table[]
    ItemPool = {},
    ---If true this weighted random will use math.randomseed().
    ---Otherwise it uses Valve's RandomFloat().
    UseRandomSeed = false,
}
WeightedRandomBaseClass.__index = WeightedRandomBaseClass

---Add a table value with an associated weight.
---
---Note: The table `tbl` is not cloned, the given reference is used.
---@param tbl table # Table of values that will be returned.
---@param weight number # Weight for this table.
function WeightedRandomBaseClass:Add(tbl, weight)
    tbl.weight = weight
    self.ItemPool[#self.ItemPool+1] = tbl
end

---Get the total weight of this weighted random object.
---@return number
function WeightedRandomBaseClass:TotalWeight()
    local weight_sum = 0
    for _,item in ipairs(self.ItemPool) do
        weight_sum = weight_sum + item.weight
    end
    return weight_sum
end

---Get a random table from the list of weighted tables.
---@return table
function WeightedRandomBaseClass:Random()
    local weight_sum = self:TotalWeight()
    local weight_remaining
    if self.UseRandomSeed then
        weight_remaining = math.random(0, weight_sum)
    else
        weight_remaining = RandomFloat(0, weight_sum)
    end
    for _,item in ipairs(self.ItemPool) do
        weight_remaining = weight_remaining - item.weight
        if weight_remaining < 0 then
            return item
        end
    end
    -- Return to last item just in case (should never reach here.)
    return self.ItemPool[#self.ItemPool]
end

---Create a new WeightedRandom object with given weights.
---@param weights table[]|"{\n\t{ weight = 1 },\n}"
---@return WeightedRandom
function WeightedRandom(weights)
    return setmetatable({ItemPool = weights or {}}, WeightedRandomBaseClass)
end
