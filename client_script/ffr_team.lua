luanet.load_assembly("LibFFRNetwork")
cooptype = luanet.import_type("LibFFRNetwork.CoopHelper")
coop = cooptype()


------------------------------------------
--      FFR CO-OP MODE VERSION 0.10     --
--          MANUAL CONFIGURATION        --
------------------------------------------
SCRIPT_VERSION = "0.10"
SERVER_IP = "142.166.18.108"
DEBUG = true
LOCAL = false

local u8 = nil
local w_u8 = nil


--Sets correct memory access functions based on whether NesHawk or QuickNES is loaded
local function defineMemoryFunctions()
	local mem_domain = {}
	local domains = memory.getmemorydomainlist();
	if domains[1] == "System Bus" then
		--NesHawk
		mem_domain["systembus"] = function() memory.usememorydomain("System Bus") end
		mem_domain["saveram"]   = function() memory.usememorydomain("Battery RAM") end
		mem_domain["rom"]       = function() memory.usememorydomain("PRG ROM") end
	elseif domains[1] == "WRAM" then
		--QuickNES
		mem_domain["systembus"] = function() memory.usememorydomain("System Bus") end
		mem_domain["saveram"]   = function() memory.usememorydomain("WRAM") end
		mem_domain["rom"]       = function() memory.usememorydomain("PRG ROM") end
	end
	return mem_domain
end

local mem_domain = defineMemoryFunctions()
u8 = memory.read_u8;
w_u8 = memory.write_u8;

local shardhuntmode = false;
mem_domain["rom"]()
if u8(0x37761) == 0x1C then
	shardhuntmode = true;
	console.log("Detected shard hunt rom");
end

if not (LOCAL == true) then
	coop:SetServer(SERVER_IP)
end

mem_domain["systembus"]()
coop:ReportScriptVersion(SCRIPT_VERSION)
coop:ReportRomName(gameinfo.getromname())

function debug_log(s)
	if DEBUG == true then
		f = io.open("ffrteam_metrics.txt", "a")
		f:write(string.format("%d - %s\n", emu.framecount(), s))
		f:close()
		console.log(s)
	end
end

item_messages = {};

local function createKeyItemsTable()
	local k = {	Lute=false, 	Crown=false, 	Crystal=false, 	Herb=false, 			Key=false, 		TNT=false, 
				Adamant=false, 	Slab=false, 	Ruby=false, 	Rod=false, 				Floater=false, 	Chime=false, 
				Tail=false, 	Cube=false, 	Bottle=false, 	Oxyale=false, 			Ship=false, 	Canoe=false, 
				Airship=false, 	Bridge=false, 	Canal=false,	SlabTranslation=false, 	EarthOrb=false,	FireOrb=false,
				WaterOrb=false,	AirOrb=false,	EndGame=false};
	
	return k;
end
local giveItemFunctions = {
	Lute= 				function() w_u8(0x0021, 0x01) end;
	Crown= 				function() w_u8(0x0022, 0x01) end;
	Key= 				function() w_u8(0x0025, 0x01) end;
	Rod= 				function() w_u8(0x002A, 0x01) end;
	Floater= 			function() w_u8(0x002B, 0x01) end;
	Chime= 				function() w_u8(0x002C, 0x01) end;
	Cube= 				function() w_u8(0x002E, 0x01) end;
	Oxyale= 			function() w_u8(0x0030, 0x01) end;
	Ship= 				function() w_u8(0x0000, 0x01) end;
	Canoe= 				function() w_u8(0x0012, 0x01) end;
	Bridge=				function() w_u8(0x0008, 0x01) end;
	Canal= 				function() w_u8(0x000C, 0x00) end;
	Crystal=			function() w_u8(0x0023, 0x01) end;
	Herb=				function() w_u8(0x0024, 0x01) end;
	TNT=				function() w_u8(0x0026, 0x01) end;
	Adamant=			function() w_u8(0x0027, 0x01) end;
	Slab=				function() w_u8(0x0028, 0x01) end;
	Ruby=				function() w_u8(0x0029, 0x01) end;
	Tail=				function() w_u8(0x002D, 0x01) end;
	Bottle=				function() w_u8(0x002F, 0x01) end;
	Airship=			function() w_u8(0x0004, 0x01) end;
	SlabTranslation= 	function() w_u8(0x020B, bit.bor(u8(0x020B), 0x02)) end;
	EarthOrb=			function() w_u8(0x0031, 0x01) end;
	FireOrb=			function() w_u8(0x0032, 0x01) end;
	WaterOrb=			function() w_u8(0x0033, 0x01) end;
	AirOrb=				function() w_u8(0x0034, 0x01) end;
	EndGame=			function() end;
};

local function giveItem(item)
	debug_log("giveItem called with item "..item)
	--change to battery RAM memory domain
	mem_domain.saveram();
	giveItemFunctions[item]()
	
	mem_domain.systembus();
	
	local itemplayer = coop:GetUsernameForItem(item)

	--Chaos defeated
	if item == "EndGame" then
		local msgtxt = itemplayer .. " has defeated Chaos!";
		local msg = {TTL=600, message=msgtxt, color=0xFF00FF00};
		item_messages[item] = msg;
		gamestate["chaosDefeatedRemotely"] = true;
		return
	end
	
	--local msgtxt = "Received item:  " .. item;
	local msgtxt = itemplayer .. " obtained item: " .. item;
	local msg = {TTL=450, message=msgtxt, color=0xFFFF0000};
	item_messages[item] = msg;
end

local function compareAndUpdateItems(cur_k, resp_k)
	for k, v in pairs(cur_k) do
		if cur_k[k] == false and resp_k[k] == true then
			giveItem(k);
		end
	end
end

local function IsChaosDead()
	
	local btl_result = u8(0x6B86); --check for 0xFF
	local btl_battletype = u8(0x6C92); --check for 0x04
	local btlformation = u8(0x006A); --check for 0x7B
	
	if (btl_result == 0xFF) and (btl_battletype == 0x04) and (btlformation == 0x7B) then
		return true;
	end
	return false;
	
end

local function updateKeyItemsTable(k)
	
	--simple items
	if u8(0x6021) > 0 then k["Lute"] = 		true else k["Lute"] = 		false end;
	if u8(0x6022) > 0 then k["Crown"] = 	true else k["Crown"] = 		false end;
	if u8(0x6025) > 0 then k["Key"] = 		true else k["Key"] = 		false end;
	if u8(0x602A) > 0 then k["Rod"] = 		true else k["Rod"] = 		false end;
	if u8(0x602B) > 0 then k["Floater"] = 	true else k["Floater"] = 	false end;
	if u8(0x602C) > 0 then k["Chime"] = 	true else k["Chime"] = 		false end;
	if u8(0x602E) > 0 then k["Cube"] = 		true else k["Cube"] = 		false end;
	if u8(0x6030) > 0 then k["Oxyale"] = 	true else k["Oxyale"] = 	false end;
	if u8(0x6000) > 0 then k["Ship"] = 		true else k["Ship"] = 		false end;
	if u8(0x6012) > 0 then k["Canoe"] = 	true else k["Canoe"] = 		false end;
	if u8(0x6004) > 0 then k["Airship"] = 	true else k["Airship"] = 	false end;
	if u8(0x6008) > 0 then k["Bridge"] = 	true else k["Bridge"] = 	false end;
	if (u8(0x6032) > 0 and not shardhuntmode)    then k["FireOrb"] = 	true else k["FireOrb"]  = 	false end;
	if (u8(0x6033) > 0 and not shardhuntmode)    then k["WaterOrb"] = 	true else k["WaterOrb"] = 	false end;
	if (u8(0x6034) > 0 and not shardhuntmode)    then k["AirOrb"] = 	true else k["AirOrb"]   = 	false end;
	if (u8(0x6031) > 0 and not shardhuntmode)    then k["EarthOrb"] = 	true else k["EarthOrb"] = 	false end;

	--reverse items
	if u8(0x600C) == 0 then k["Canal"] = true  else k["Canal"] = false end;
	
	--complex items
	if (u8(0x6023) > 0 ) or (bit.band(u8(0x620A), 0x02) > 0) then k["Crystal"] = 	true else k["Crystal"] = 	false end;
	if (u8(0x6024) > 0 ) or (bit.band(u8(0x6205), 0x02) > 0) then k["Herb"] = 		true else k["Herb"] = 		false end;
	if (u8(0x6026) > 0 ) or (bit.band(u8(0x6208), 0x02) > 0) then k["TNT"] = 		true else k["TNT"] = 		false end;
	if (u8(0x6027) > 0 ) or (bit.band(u8(0x6209), 0x02) > 0) then k["Adamant"] = 	true else k["Adamant"] = 	false end;
	if (u8(0x6028) > 0 ) or (bit.band(u8(0x620B), 0x02) > 0) then k["Slab"] = 		true else k["Slab"] = 		false end;
	if (u8(0x6029) > 0 ) or (bit.band(u8(0x6214), 0x01) == 0) then k["Ruby"] = 		true else k["Ruby"] = 		false end;
	if (u8(0x602D) > 0 ) or (bit.band(u8(0x620E), 0x02) > 0) then k["Tail"] = 		true else k["Tail"] = 		false end;
	if (u8(0x602F) > 0 ) or (bit.band(u8(0x6213), 0x03) > 0) then k["Bottle"] = 	true else k["Bottle"] =		false end;
	
	if (bit.band(u8(0x620B), 0x02) > 0) then k["SlabTranslation"] = true else k["SlabTranslation"] = false end;
	if IsChaosDead() or gamestate["chaosDefeatedRemotely"] then 
		--gamestate.localChaosDefeated = true;
		k["EndGame"] = true;
	end
	
	
end

function table.empty (self)
    for _, _ in pairs(self) do
        return false
    end
    return true
end

local function BeginHttp(k)
	coop:SendDataTable(k)
end

local function ProcessNewDataTable(k)
	local kit = createKeyItemsTable();
	local resp = coop:GetResultTable(kit);
	compareAndUpdateItems(k, resp);
end

local function MessageDispatch()
	local messagetext = coop:GetMessage();
	local msg = {TTL=450, message=messagetext, color=0xFF00FF00};
	table.insert(item_messages, msg)
end
--local function GenericChecks()
--	if (not gamestate.localChaosDefeated) and (IsChaosDead()) then
--		gamestate.localChaosDefeated = true
--		--send generic message
--	end
--end

local function drawMessages()
	if table.empty(item_messages) then
		return
	end
	local y = 10;
	for k, v in pairs(item_messages) do
		if v["TTL"] > 0 then
			gui.pixelText(10, y, v["message"], v["color"], 0xB0000000, 0);
			y = y + 10;
			item_messages[k]["TTL"] = item_messages[k]["TTL"] - 1;
		end
	end
end

local keyitems = createKeyItemsTable();
gamestate = {};
local frame = 0;

local prevstate = ""
local curstate = ""
local checkedCurIter = 0;
local statusColor = ""
local doProcessing = false;

local function StateOKForMainLoop()
	--party has been created
	if u8(0x6102) == 0 then
		return false
	end
	
	--chaos shaking animation
	if u8(0x6B86) == 0xFF then
		return true
	end
	
	--not currently in battle
	if (u8(0x60FC) == 0x0B) or (u8(0x60FC) == 0x0C) then
		return false
	end
	
	return true
end

while true do
	gui.drawEllipse(248, 9, 6, 6, "Black", "Yellow");
	--check that game has been started
	--local lc = os.clock();
	curstate = coop:GetState();
	frame = frame + 1;
	drawMessages();
	if StateOKForMainLoop()  then
		if not (curstate == "Error") then
			gui.drawEllipse(248, 9, 6, 6, "Black", "Green");
		end
		if not (curstate == prevstate) then
			--console.log("Current state: "..curstate)
			prevstate = curstate
		end

		if (checkedCurIter == 0) and (frame % 60 == 0) then
			--console.log("updating local key items");
			gui.drawEllipse(248, 9, 6, 6, "Black", "Blue");
			keyitems = createKeyItemsTable();
			updateKeyItemsTable(keyitems);
			checkedCurIter = 1;
		end
		if doProcessing == true then
			doProcessing = false;
			if not curstate == "Receiving" then
				--console.log("Current state: "..curstate)
			end
			if (curstate == "Idle") or (curstate == "Error") then
				BeginHttp(keyitems);
			elseif curstate == "HasData" then
				gui.drawEllipse(248, 9, 6, 6, "Black", "Blue");
				ProcessNewDataTable(keyitems);
				frame = 0;
				checkedCurIter = 0;
			elseif curstate == "HasMessage" then
				MessageDispatch();
			end
		end
	end
	if (curstate == "Error") then
		gui.drawEllipse(248, 9, 6, 6, "Black", "Red");
	elseif (curstate == "Uninitialized") then
		gui.drawEllipse(248, 9, 6, 6, "Black", "White");
	end
	if (frame > 450) and (frame % 60 == 0) then doProcessing = true end
	--debug_log(string.format("whole loop executed in %.3f ms", os.clock() - lc))
	--GenericChecks();
	emu.frameadvance();
end