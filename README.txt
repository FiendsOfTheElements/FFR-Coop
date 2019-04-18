FFR Coop Mode v0.08

Setup consists of two files:
    LibFFRNetwork.dll
	ffr_team.lua
	
Place the LibFFRNetwork.dll file into your BizHawk's dll folder
ex. D:\games\emulators\BizHawk-2.3\dll

The ffr_team.lua file can be placed anywhere but for convenience
it is recommended to put into BizHawk's lua folder
ex. D:\games\emulators\BizHawk-2.3\Lua

Load an FFR ROM before loading the lua script.  When you load the
lua script before the game has begun, you should see a white dot
in the upper-right corner of your emulator window.  This means that
the DLL and script have been initialized properly.  If you do not
see this, make sure you have at least version 4.71 of .net framework
installed. (note:  this is different from .net core).

Unimportant stuff:
The little dot in the upper-right corner is a status indicator.
White = Uninitialized.
Green = Initialized and working ok.
Blue = Processing.
Red = Error.
Yellow = Yielding. This means the script is backing off to prevent lag.

--==<[CHANGE LOG]>==--
0.08:
	- Fixed uncaught exception.
	- 10% friendlier.
0.07:
	- Lots more error handling.  Co-op can now handle being disconnected
	and reconnected to a network.
	- Simple version check at startup.
0.06:
	- Added minimal error handling.
0.05:
	- Starting a game now asks you to enter a name.  This name will
		be displayed to others when they receive an item that you
		obtained.
	- Fixed bugs.
	- Improved performance.
	- Manipulated widgets.
0.04:
	- The main lua loop now only runs when bank D is swapped in.
		This means that you MUST NOT BE IN A MENU for network
		access and processing to happen.  This prevents any code
		from being run in battle or while menuing so that any
		potential lag cannot result in an input error.
		tl;dr: If you aren't walking around a map, no co-op will happen.
	- Moved serialization/deserialization code to DLL for further
		performance improvements.
	