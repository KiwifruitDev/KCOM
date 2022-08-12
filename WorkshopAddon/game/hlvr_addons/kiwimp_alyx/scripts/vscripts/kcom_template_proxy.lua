kcom_templates = kcom_templates or {}

function TemplateProxy(params)
    local templated = params.caller
    if not IsValidEntity(templated) then return end
    local name = templated:GetName()
    local enttable = kcom_templates[name]
    if not enttable then return end
    if #enttable <= 0 and not kcom_active then return end
    for _, entname in pairs(enttable) do
        local tempent = Entities:FindByName(nil, entname)
        if not IsValidEntity(tempent) then return end
        print("TENT " .. tempent:GetName() .. " KCOM")
        SendToServerConsole("kcom_cache_entity "..entname)
    end
end

function Spawn(spawnkeys)
    local name = spawnkeys:GetValue("Group00") -- Template entity targetname
    kcom_templates[name] = kcom_templates[name] or {}
    local tempents = Entities:FindAllByName(name)
    if #tempents < 1 then return end
    for _, entity in pairs(tempents) do
        if entity:GetClassname() == "point_template" then
            entity:RedirectOutput("OnEntitySpawned", "TemplateProxy", thisEntity);
        end
    end
    for i = 1, 15 do
        local num = tostring(i)
        if i < 10 then
            num = "0" .. num
        end
        kcom_templates[name][#kcom_templates[name] + 1] = spawnkeys:GetValue("Group" .. num) -- Templated entities
    end
end