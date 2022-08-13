--[[
    v1.0.1
    https://github.com/FrostSource/hla_extravaganza

    Debug utility functions.

    Load this file at game start using the following line:

        require "util.debug"

]]
Debug = {}

---comment
---@param list EntityHandle[]
function Debug.PrintEntityList(list)
    print(string.format("\n%-12s %-40s %-40s %-40s %-60s","Handle", "Classname:", "Name:", "Model Name:", "Parent Class"))
    print(string.format("%-12s %-40s %-40s %-40s %-60s",  "------", "----------", "-----", "-----------", "------------"))
    for _, ent in ipairs(list) do
        print(string.format("%-12s %-40s %-40s %-40s %-60s", ent, ent:GetClassname(), ent:GetName(), ent:GetModelName(), ent:GetMoveParent() and ent:GetMoveParent():GetClassname() or "" ))
    end
    print()
end

function Debug.PrintAllEntities()
    local list = {}
    local e = Entities:First()
    while e ~= nil do
        list[#list+1] = e
        e = Entities:Next(e)
    end
    Debug.PrintEntityList(list)
end

function Debug.PrintAllEntitiesInSphere(origin, radius)
    Debug.PrintEntityList(Entities:FindAllInSphere(origin, radius))
end
