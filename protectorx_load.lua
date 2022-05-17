--_G.pirofessioanlprotectionofpiro = "tuaxautilsissopiroburrruosdkdjsdhjfhdhhfhdfdhfdhdfhdhdfhdfdfhfhdf"
local loadedfuncssss = 0
_G.ProtectorXFreeApiOmgggOMMMGMGMGMGMGMGMGGMMG = "Unkown"
function protectorx_notifiy(text)
    wait(1.2)
game.StarterGui:SetCore("SendNotification", {
Title = "Tuaxa Utils"; -- the title (ofc)
Text = text; -- what the text says (ofc)
Duration = 5; -- how long the notification should in secounds
})
end

	function saveinstance()
		return"Coding."
		end
		function getsynasset()
		return"Tuaxa Utils Ver 1.234.ES"
		end
		--saveinstance()
		
		function protectorx_getplayername()
		local username = game.Players.LocalPlayer.Name
		return username;
		end
		
		--print(getplayername())
		
		function protectorx_getuserid()
		local userid = game.Players.LocalPlayer.UserId
		return userid;
		end
		
		--print(getuserid())
		
		function websocketsenderpiro()
		return "Coding.";
		end
		
		function identifyexecutor()
		return "Protector X";
		end
		function getexecutorname()
		return "Protector X";
		end
		
		
		function http_request(options)
		return"Tuaxa Utils Error."
		end
		
		function spoof(yas,yas,yas)
			return"Tuaxa Utils Script Secure Systems"
		end
		
		
		function KRNL_LOADED()
		return true
		end
		
		
		function setsimulationradius(value)
				sethiddenproperty(game.Players.LocalPlayer, "SimulationRadius", value)
		end
		
		function protector_executescript(message)
loadstring(message)()
	end

function protectorx_getapi()
return _G.ProtectorXFreeApiOmgggOMMMGMGMGMGMGMGMGGMMG
end

function protectorx_optimizegame()
	local decalsyeeted = true -- Leaving this on makes games look shitty but the fps goes up by at least 20.
	local g = game
	local w = g.Workspace
	local l = g.Lighting
	local t = w.Terrain
	t.WaterWaveSize = 0
	t.WaterWaveSpeed = 0
	t.WaterReflectance = 0
	t.WaterTransparency = 0
	l.GlobalShadows = false
	l.FogEnd = 9e9
	l.Brightness = 0
	settings().Rendering.QualityLevel = "Level01"
	for i, v in pairs(g:GetDescendants()) do
		if v:IsA("Part") or v:IsA("Union") or v:IsA("CornerWedgePart") or v:IsA("TrussPart") then
			v.Material = "Plastic"
			v.Reflectance = 0
		elseif v:IsA("Decal") or v:IsA("Texture") and decalsyeeted then
			v.Transparency = 1
		elseif v:IsA("ParticleEmitter") or v:IsA("Trail") then
			v.Lifetime = NumberRange.new(0)
		elseif v:IsA("Explosion") then
			v.BlastPressure = 1
			v.BlastRadius = 1
		elseif v:IsA("Fire") or v:IsA("SpotLight") or v:IsA("Smoke") or v:IsA("Sparkles") then
			v.Enabled = false
		elseif v:IsA("MeshPart") then
			v.Material = "Plastic"
			v.Reflectance = 0
			v.TextureID = 10385902758728957
		end
	end
	for i, e in pairs(l:GetChildren()) do
		if e:IsA("BlurEffect") or e:IsA("SunRaysEffect") or e:IsA("ColorCorrectionEffect") or e:IsA("BloomEffect") or e:IsA("DepthOfFieldEffect") then
			e.Enabled = false
		end
	end
	return"Optimized :)"
end

function protectorx_protectgame()
	--game.Loaded:Wait()
	local Dir = game:GetService("CoreGui"):FindFirstChild("RobloxPromptGui"):FindFirstChild("promptOverlay")
	Dir.DescendantAdded:Connect(function(Err)
		if Err.Name == "ErrorTitle" then
			Err:GetPropertyChangedSignal("Text"):Connect(function()
				if Err.Text:sub(0, 12) == "Disconnected" then
					if #game.Players:GetPlayers() <= 1 then
						game.Players.LocalPlayer:Kick("\n Tuaxa Account Protect: \n ACCOUNT KİCKED BY GAME ! \n\n Rejoinig...")
						wait()
						game:GetService("TeleportService"):Teleport(game.PlaceId, game.Players.LocalPlayer)
					else
						game:GetService("TeleportService"):TeleportToPlaceInstance(game.PlaceId, game.JobId, game.Players.LocalPlayer)
					end
				end
			end)
		end
	end)

	local mt = getrawmetatable(game)
	local old = mt.__namecall
	local protect = newcclosure or protect_function
	
	if not protect then
		protect = function(f) 
			return f 
		end
	end
	
	setreadonly(mt, false)
	mt.__namecall = protect(function(self, ...)
	local method = getnamecallmethod()
	
	if method == "Kick" then
	protectorx_notifiy("Kick Call Blocked CRYYYYY Powered By Protector X")    
	wait(9e9)
			return
		end
		return old(self, ...)
	end)
	
	hookfunction(game:GetService("Players").LocalPlayer.Kick,protect(function() 
		wait(9e9) 
	end))

	--game.Loaded:Wait()
	local mt = getrawmetatable(game)
	local old = mt.__namecall
	local protect = newcclosure or protect_function
	
	if not protect then
		protect = function(f) 
			return f 
		end
	end
	
	setreadonly(mt, false)
	mt.__namecall = protect(function(self, ...)
	local method = getnamecallmethod()
	
	if method == "GetTotalMemoryUsageMb" then
	protectorx_notifiy("piro Call Blocked CRYYYYY Powered By Protector X")    
	wait(9e9)
			return
		end
		return old(self, ...)
	end)
	
	hookfunction(game:GetService("Stats").GetTotalMemoryUsageMb,protect(function() 
		return nil;
	end))
end
_G.protected = protected or {}

function protectorx_protectui(obj)
	assert(typeof(obj) == "Instance","bad argument #1 to 'protect_gui' (Instance expected, got "..typeof(obj)..")")
	assert(table.find(protected,obj) == nil,tostring(obj.Name).." is already protected")
	table.insert(protected,obj)
	for i,v in next, obj:GetDescendants() do
		table.insert(protected,v)
	end
	local c
	c = obj.DescendantAdded:Connect(function(d)
		if table.find(protected,obj) then
			table.insert(protected,d)
		else
			c:Disconnect()
		end
	end)
end

function protectorx_securecallbacks(func,env,...)
	assert(typeof(func) == "function","bad argument to #1 to 'secure_call' (function expected, got "..typeof(func)..")")
		assert(env.ClassName == ("LocalScript" or "ModuleScript"),"bad argument to #2 to 'secure_call' (LocalScript or ModuleScript expected, got "..env.ClassName..")")
		local args = {...}
		return coroutine.wrap(function()
			setfenv(0,getsenv(env))
			setfenv(1,getsenv(env))
			return func(unpack(args))
		end)()
end

function protectorx_unprotectui(obj)
	assert(typeof(obj) == "Instance","bad argument #1 to 'unprotect_gui' (Instance expected, got "..typeof(obj)..")")
	assert(table.find(protected,obj) ~= nil,obj.Name.." is not protected")
	table.remove(protected,table.find(protected,obj))
	for _,v in next, obj:GetDescendants() do
		if table.find(protected,v) then
			table.remove(protected,table.find(protected,v))
		end
	end
end

function protectorx_loadedcustomfuncs()
return "Loaded."
end

--game:GetService("Stats").GetTotalMemoryUsageMb()

		--print(syn.protect_gui)
		--loadstring(game:HttpGetAsync("https://gist.githubusercontent.com/M6HqVBcddw2qaN4s/dabc2500988785fbec1ce7c7aaee105d/raw/hVQJXfF4sR6yqSfJ"),true)()
		--KRNL_LOADED()
		--loadstring(game:HttpGet("https://raw.githubusercontent.com/stangithuboffical/selexity/main/Main", true))()
		--loadstring(game:HttpGet("https://pastebinp.com/raw/M4mGByep", true))()
		--loadstring(game:HttpGet('https://solarishub.dev/script.lua',true))() X
		--loadstring(game:HttpGet(('https://shlex.dev/release/domainx/latest.lua'),true))()
		--loadstring(game:HttpGet(('https://raw.githubusercontent.com/EdgeIY/infiniteyield/master/source'),true))()
		--queue_on_teleport("loadstring(game:HttpGet(('https://raw.githubusercontent.com/EdgeIY/infiniteyield/master/source'),true))()")
		--loadstring(game:HttpGetAsync("https://raw.githubusercontent.com/LionelStreaming/GumaHub/main/LX-FE-Hub"))()


		--protectorx_protectgame()

--game.Players.LocalPlayer:Kick("wddffs")


--print(identifyexecutor())



-- Gui to Lua
-- Version: 3.2

-- Instances:

