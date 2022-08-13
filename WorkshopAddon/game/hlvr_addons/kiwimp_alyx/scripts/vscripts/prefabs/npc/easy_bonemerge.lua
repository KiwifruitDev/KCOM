
---@param activateType "0"|"1"|"2"
function Activate(activateType)
    print("Bonemerge script activated", thisEntity:GetName())
    ListenToGameEvent("npc_ragdoll_created", NpcRagdollCreated, thisEntity)
end

function NpcRagdollCreated(_, data)
    -- Check if the npc that died is THIS npc
    if thisEntity:entindex() == data.npc_entindex then
        -- Resolve npc/ragdoll index to handles
        local npc = EntIndexToHScript(data.npc_entindex)
        local ragdoll = EntIndexToHScript(data.ragdoll_entindex)
        -- Checking for a child with name "bonemerge" to know which model to use
        for _, child in ipairs(npc:GetChildren()) do
            if vlua.find(child:GetName(), "bonemerge", 1) then
                --print("Found bonemerge", child:GetModelName())
                -- Spawning the new prop to merge with the new ragdoll
                local prop = SpawnEntityFromTableSynchronous("prop_dynamic", {
                    model = child:GetModelName(),
                    solid = "0"
                })
                prop:SetParent(ragdoll, "!bonemerge")
                ragdoll:SetRenderAlpha(0)
            end
        end
        -- Stop listening to this game event. Without this errors may occur.
        StopListeningToAllGameEvents(thisEntity)
    end
end
