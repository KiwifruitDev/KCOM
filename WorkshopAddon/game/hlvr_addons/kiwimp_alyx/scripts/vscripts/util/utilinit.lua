--[[
    v1.1.1
    https://github.com/FrostSource/hla_extravaganza

    Initializing script for all useful utility scripts.
    Can be set as an entity script or required from another script:

    require "util.utilinit"

    Note: Use a consistent require naming style otherwise a script
    may be executed into global scope multiple times:

    require "util.util"
    require "util/util"
    require "util\\util"

    Although all 3 above styles are valid, they are considered unqiue
    and will be executed into global scope 3 times with unique function signatures.
    By using a consistent style you can require as many times as you like without this worry:

    require "util.util"
    require "util.util"
    require "util.util"

    "util.util" will be executed into global scope once and the other two are ignored.
    This behaviour is the same across multiple scripts.
]]
require "util.enums"
require "util.debug"
require "util.util"
require "util.storage"
require "util.stack"
require "util.queue"
require "util.inventory"
require "util.weighted_random"
require "util.player"