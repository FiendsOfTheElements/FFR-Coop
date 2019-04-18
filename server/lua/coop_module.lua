local M = {}

M.ep_version = function()
    local cjson = require "cjson.safe"
    ngx.print(cjson.encode({"0.08;0.07b"}))
end

M.ep_init = function()
    local cjson = require "cjson.safe"
    local ffr_coop = ngx.shared.ffr_coop
    local args, err = ngx.req.get_uri_args()
    
    if err == "truncated" then
        --ngx.print(err)
        return
    end
    math.randomseed(os.clock())
    local x = math.floor(math.random() * 10000)
    local s = tostring(x)
    local defdata = "000000000000000000000000000"
    local emptyUserItems = {"","","","","","","","","","","","","","","","","","","","","","","","","","",""};
    local gameobject = {team=x, players = {args["player"]}, data = defdata, messages = {}, useritems = emptyUserItems}
    
    ffr_coop:set(gameobject["team"], cjson.encode(gameobject), 216000)
    ngx.print(x)

end

M.ep_join = function()
    local cjson = require "cjson.safe"
    local ffr_coop = ngx.shared.ffr_coop
    local args, err = ngx.req.get_uri_args()
    
    if err == "truncated" then
        --ngx.print(err)
        return
    end
    
    local stored = ffr_coop:get(args["team"])
    if not stored then
        return
    end
    local defdata = "000000000000000000000000000"
    local gameobject = cjson.decode(stored)
    
    if gameobject["data"] == defdata then
        table.insert(gameobject["players"], args["player"])
        ffr_coop:set(gameobject["team"], cjson.encode(gameobject))
        ngx.print(defdata)
        return
    end
end

M.ep_teamcheck = function()
    local cjson = require "cjson.safe"
    local ffr_coop = ngx.shared.ffr_coop
    local args, err = ngx.req.get_uri_args()
    if err == "truncated" then
        --ngx.print(err)
        return
    end
    local stored = ffr_coop:get(args["team"])
    if not stored then
        return
    end
    local gobj = cjson.decode(stored)
    local plist = table.concat(gobj["players"], "{")
    ngx.print(plist)
end

M.ep_coop = function()
    local cjson = require "cjson.safe"
    local args, err = ngx.req.get_uri_args()
    
    if err == "truncated" then
        ngx.print(err)
        return
    end
    
    local keyItemNames = {"Lute", "Crown", "Crystal", "Herb", "Key", "TNT", "Adamant", "Slab", "Ruby", "Rod", "Floater", 
                        "Chime", "Tail", "Cube", "Bottle", "Oxyale", "Ship", "Canoe", "Airship", "Bridge", "Canal", 
                        "SlabTranslation", "EarthOrb", "FireOrb", "WaterOrb", "AirOrb", "EndGame"}
    
    local ffr_coop = ngx.shared.ffr_coop
    local data = args["data"]
    local stored = ffr_coop:get(args["team"])
    local gameobj = cjson.decode(stored)
    local old_data = gameobj["data"]
    local concat_t = {}
    local concat_s = ""

    local new_t = {}
    for c in data:gmatch"." do
        table.insert(new_t, c)
    end
    
    local old_t = {}
    for c in old_data:gmatch"." do
        table.insert(old_t, c)
    end

    for k, v in pairs(new_t) do
        if v == "1" or old_t[k] == "1" then
            table.insert(concat_t, "1")
            if (old_t[k] == "0") and (v == "1") then
                gameobj["useritems"][k] = args["player"]
            end
        else
            table.insert(concat_t, "0")
        end
    end
    concat_s = table.concat(concat_t)
    gameobj["data"] = concat_s
    ffr_coop:set(gameobj["team"], cjson.encode(gameobj), 216000)
    concat_s = concat_s .. "{" .. table.concat(gameobj["useritems"], "{")

    ngx.print(concat_s)
end

return M