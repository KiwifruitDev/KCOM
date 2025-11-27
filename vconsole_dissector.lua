-- VConsole and KCOM dissector for Wireshark

vconsole_proto = Proto("vconsole", "VConsole Protocol")
kcom_proto = Proto("kcom", "KCOM Protocol")

operation_F = ProtoField.string("vconsole.operation", "Operation")
version_F = ProtoField.uint8("vconsole.version", "Protocol Version")
parameterlength_F = ProtoField.uint16("vconsole.parameters", "Parameter Length")
parameters_F = ProtoField.none("vconsole.parameters", "Parameters")
commandlength_F = ProtoField.uint16("vconsole.commandlength", "Command Length")
command_F = ProtoField.string("vconsole.command", "Command")
messagelength_F = ProtoField.uint16("vconsole.messagelength", "Message Length")
message_F = ProtoField.string("vconsole.message", "Message")
messagelength_F = ProtoField.uint16("vconsole.messagelength", "Message Length")
focus_F = ProtoField.bool("vconsole.focus", "Focusing")

kcomlength_F = ProtoField.uint16("kcom.length", "Length")
kcomoperation_F = ProtoField.string("kcom.operation", "Operation")
kcomparameters_F = ProtoField.string("kcom.parameters", "Parameters")

vconsole_proto.fields = {operation_F, version_F, parameterlength_F, parameters_F, commandlength_F, command_F, messagelength_F, message_F, focus_F, kcomlength_F, kcomoperation_F, kcomparameters_F}

vconsole_port = 29000

function vconsole_proto.dissector(buffer, pinfo, tree)
    local data_len = buffer:len()
    if data_len >= 13 then
        pinfo.cols.protocol = vconsole_proto.name
        local subtree = tree:add(vconsole_proto, "VConsole")
        local operation = buffer(0, 4)
        local param_len = data_len - 5 -- 5 bytes of header
        subtree:add_le(operation_F, operation)
        subtree:add_le(version_F, buffer(5, 1))
        if param_len > 0 then
            if operation:string() == "PRNT" then
                -- Print message
                -- KCOM is embedded here
                local message_len = param_len - 36
                local message = buffer(40, message_len)
                local message_str = message:string()
                subtree:add(messagelength_F, message_len)
                subtree:add_le(message_F, message)
                -- if message starts with K> and ends with <KCOM\n then it's a KCOM message
                if message_len >= 8 and message_str:sub(1, 2) == "K>" and message_str:sub(-6) == "<KCOM\n" then
                    pinfo.cols.protocol = kcom_proto.name
                    local kcomtree = tree:add(vconsole_proto, "KCOM")
                    local kcom_len = message_len - 8
                    local kcom_command = buffer(42, 4)
                    local kcom_arguments = buffer(47, kcom_len - 5)
                    kcomtree:add(kcomlength_F, kcom_len)
                    kcomtree:add_le(kcomoperation_F, kcom_command)
                    kcomtree:add_le(kcomparameters_F, kcom_arguments)
                end
            elseif operation:string() == "CMND" then
                -- Commands (from VConsole to game)
                local command_len = param_len - 8
                local command = buffer(12, command_len)
                subtree:add(commandlength_F, command_len)
                subtree:add_le(command_F, command)
            elseif operation:string() == "VFCS" then
                -- Window focus on VConsole
                -- Used to ensure that the game is running at full speed
                -- Normally when tabbed out, the game lags to save resources
                local focus = buffer(data_len-1, 1)
                subtree:add_le(focus_F, focus)
            else
                -- Generic parameters
                subtree:add_le(parameterlength_F, param_len)
                local parameters = buffer(5, param_len)
                subtree:add_le(parameters_F, parameters)
            end
        end
    end
end

local tcp_port_table = DissectorTable.get("tcp.port")
tcp_port_table:add(vconsole_port, vconsole_proto)
