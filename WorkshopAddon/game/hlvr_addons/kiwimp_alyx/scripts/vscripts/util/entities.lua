--[[
    v0.1.0
    https://github.com/FrostSource/hla_extravaganza

    Provides generic function extensions for base entities.
]]
require "util.util"

---Get the top level entities parented to this entity. Not children of children.
---@return table
function CBaseEntity:GetTopChildren()
    local children = {}
    for _, child in ipairs(self:GetChildren()) do
        if child:GetMoveParent() == self then
            children[#children+1] = child
        end
    end
    return children
end

CBaseEntity.AddOutput = AddOutput

---Send an input to this entity.
---@param action string # Input name.
---@param value? string # Parameter override for the input.
---@param delay? number # Delay in seconds.
---@param activator? EntityHandle
---@param caller? EntityHandle
function CBaseEntity:EntFire(action, value, delay, activator, caller)
    DoEntFireByInstanceHandle(self, action, value or "", delay or 0, activator or nil, caller or nil)
end
