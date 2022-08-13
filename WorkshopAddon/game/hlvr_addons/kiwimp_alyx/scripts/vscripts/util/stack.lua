--[[
    v1.1.1
    https://github.com/FrostSource/hla_extravaganza

    Adds stack behaviour for tables with index 1 as the top of the stack.

    ======================================== Basic Usage ==========================================

    ```lua
    -- Create a stack with 3 initial values.
    -- 1 is the top of the stack.
    local stack = Stack(1, 2, 3)

    -- Prints "1"
    print(stack:Pop())

    -- Pop multiple values
    local a,b = stack:Pop(2)

    -- Prints "2   3"
    print(a,b)

    -- Push any values
    stack:Push('a','b','c')

    -- To loop over the items, reference stack.items directly:
    for index, value in ipairs(stack.items) do
        print(index, value)
    end

    -- Or use the `pairs` helper function:
    for index, value in stack:pairs() do
        print(index, value)
    end
    ```

    =========================================== Notes =============================================

    This class supports `util.storage` with `Storage.Save(stack)` or if encountered when saving
    a table.

    ```lua
    Storage:Save('stack', stack)
    stack = Storage:Load('stack')
    ```
]]

---@class Stack
local StackClass =
{
    ---@type any[]
    items = {}
}
StackClass.__index = StackClass

if pcall(require, "util.storage") then
    Storage.RegisterType("util.Stack", StackClass)
    ---**Static Function**
    ---
    ---Helper function for saving the `stack`.
    ---@param handle EntityHandle # The entity to save on.
    ---@param name string # The name to save as.
    ---@param stack Stack # The stack to save.
    ---@return boolean # If the save was successful.
    function StackClass.__save(handle, name, stack)
        Storage.SaveTable(handle, Storage.Join(name, "items"), stack.items)
        Storage.SaveType(handle, name, "util.Stack")
        return true
    end

    ---**Static Function**
    ---
    ---Helper function for loading the `stack`.
    ---@param handle EntityHandle # Entity to load from.
    ---@param name string # Name to load.
    ---@return Stack|nil
    function StackClass.__load(handle, name)
        local items = Storage.LoadTable(handle, Storage.Join(name, "items"))
        if items ~= nil then
            local _stack = Stack()
            _stack.items = items
            return _stack
        end
        return nil
    end
end


---Push values to the stack.
---@param ... any
function StackClass:Push(...)
    for _, value in ipairs({...}) do
        table.insert(self.items, 1, value)
    end
end

---Pop a number of items from the stack.
---@param count? number # Default is 1
---@return any
function StackClass:Pop(count)
    count = min(count or 1, #self.items)
    local tbl = {}
    for i = 1, count do
        tbl[#tbl+1] = table.remove(self.items, 1)
    end
    return unpack(tbl)
end

---Peek at a number of items at the top of the stack without removing them.
---@param count? number # Default is 1
---@return any
function StackClass:Peek(count)
    count = min(count or 1, #self.items)
    local tbl = {}
    for i = 1, count do
        tbl[#tbl+1] = self.items[i]
    end
    return unpack(tbl)
end

---Remove a value from the stack regardless of its position.
---@param value any
function StackClass:Remove(value)
    for index, val in ipairs(self.items) do
        if value == val then
            table.remove(self.items, index)
            return
        end
    end
end

---Move an existing value to the top of the stack.
---Only the first occurance will be moved.
---@param value any # The value to move.
---@return boolean # True if value was found and moved.
function StackClass:MoveToTop(value)
    for index, val in ipairs(self.items) do
        if value == val then
            table.remove(self.items, index)
            table.insert(self.items, 1, value)
            return true
        end
    end
    return false
end

---Move an existing value to the bottom of the stack.
---Only the first occurance will be moved.
---@param value any # The value to move.
---@return boolean # True if value was found and moved.
function StackClass:MoveToBottom(value)
    for index, val in ipairs(self.items) do
        if value == val then
            table.remove(self.items, index)
            table.insert(self.items, value)
            return true
        end
    end
    return false
end

---Get if this stack contains a value.
---@param value any
---@return boolean
function StackClass:Contains(value)
    return vlua.find(self.items, value) ~= nil
end

---Return the number of items in the stack.
---@return integer
function StackClass:Length()
    return #self.items
end

---Get if the stack is empty.
function StackClass:IsEmpty()
    return #self.items == 0
end

---Helper method for looping.
---@return fun(table: any[], i: integer):integer, any
---@return any[]
---@return number i
function StackClass:pairs()
    return ipairs(self.items)
end

function StackClass:__tostring()
    return "Stack ("..#self.items.." items)"
end




---Create a new `Stack` object.
---First value is at the top.
---@param ... any
---@return Stack
function Stack(...)
    return setmetatable({
        items = {...}
    },
    StackClass)
end
