#include "stdafx.h"

//.text:00E0E430 luaL_loadbuffer proc near
//.text:00E0D510 lua_pcall       proc near  
//.text:00E0CB90 lua_tolstring   proc near 

typedef int(__cdecl* luaL_loadbuffer_t)(lua_State *L, const char *buff, size_t size, const char *name);
typedef int(__cdecl* lua_pcall_t)(lua_State* L, int nargs, int nresults, int errfunc);
typedef const char* (__cdecl* lua_tolstring_t)(lua_State *L, int idx, size_t *len);
typedef int(__thiscall* executeScriptString_t)(void* This, const char* buffer, int size, char* name, bool pcall);

//int __thiscall executeScriptBuffer(int this, int buff, int size, int name, char call)

luaL_loadbuffer_t pluaL_loadbuffer = NULL; // (luaL_loadbuffer_t)IDA_TO_PE(0x00E0E430);
lua_pcall_t plua_pcall = NULL; // (lua_pcall_t)IDA_TO_PE(0x00E0D510);
lua_tolstring_t plua_tolstring = NULL; // (lua_tolstring_t)IDA_TO_PE(0x00E0CB90);
executeScriptString_t pexecuteScriptString = NULL;

bool ResolveLuaAddresses() {
	static bool bResolved = false;

	if (bResolved) {
		return true;
	}

	DWORD dwLoadBufferAndToLStringPattern = Find::Pattern(gFFXIVModuleInfo,
		(BYTE*) "\x83\xC4\x14\x85\xFF\x74\x2C\x8B\x56\x08",
		(CHAR*) "xxxxxxxxxx");

	DWORD dwPCallPattern = Find::Pattern(gFFXIVModuleInfo,
		(BYTE*) "\x8B\x56\x08\x6A\x00\x51\x6A\x02\x52\xE8",
		(CHAR*) "xxxxxxxxxx");

	//if (!dwLoadBufferAndToLStringPattern)
		//Log::Fatal("Unable to locate lua signature #1!");

	//if (!dwPCallPattern)
		////Log::Fatal("Unable to locate lua signature #2!");

	////Log::Debug("dwLoadBufferAndToLStringPattern = 0x%X", dwLoadBufferAndToLStringPattern);
	////Log::Debug("dwPCallPattern = 0x%X", dwPCallPattern);

	DWORD dwLoadBuffer = Find::GetAddressOfCall(dwLoadBufferAndToLStringPattern - 0x7);
	DWORD dwToLString = Find::GetAddressOfCall(dwLoadBufferAndToLStringPattern + 0xF);
	DWORD dwPCall = Find::GetAddressOfCall(dwPCallPattern + 0x9);

	//Log::Debug("luaL_loadbuffer = 0x%X", dwLoadBuffer);
	//Log::Debug("lua_tolstring = 0x%X", dwToLString);
	//Log::Debug("lua_pcall = 0x%X", dwPCall);

	pluaL_loadbuffer = (luaL_loadbuffer_t) dwLoadBuffer;
	plua_tolstring = (lua_tolstring_t) dwToLString;
	plua_pcall = (lua_pcall_t) dwPCall;

	bResolved = true;

	return (bResolved);
}

bool Lua::Execute(std::string str, std::string* errorString) {
	if (!errorString)
		return false;

	if (!ResolveLuaAddresses()) {
		return false;
	}

	CGame* game = CGame::Singleton();

	if (!game) {
		//Log::Debug("[LUA] Error executing Lua: CGame is NULL");

		*errorString = "CGame class is NULL";

		return false;
	}

	CScriptEngine* script = game->GetScriptEngine();

	if (!script) {
		//Log::Debug("[LUA] Error executing Lua: CScriptEngine is NULL");

		*errorString = "CScriptEngine class is NULL";

		return false;
	}

	//Log::Debug("Calling loadbuffer...");

	int r1 = pluaL_loadbuffer(script->state, str.c_str(), str.length(), "");

	if (r1 != 0) {
		if (r1 == LUA_ERRSYNTAX) {
			*errorString = std::string("[Syntax Error]: ") + plua_tolstring(script->state, -1, NULL);

			////Log::Debug("Error loading Lua buffer %s", (*errorString).c_str());
		}
		else if (r1 == LUA_ERRMEM) {
			*errorString = std::string("[Memory Allocation Error]: ") + plua_tolstring(script->state, -1, NULL);

			////Log::Debug("Error loading Lua buffer %s", (*errorString).c_str());
		}
		else {
			////Log::Debug("Error loading Lua buffer (%i)", r1);
		}

		return false;
	}

	////Log::Debug("Calling pcall...");

	int r2 = plua_pcall(script->state, 0, LUA_MULTRET, 0);

	if (r2 != 0) {
		if (r2 == LUA_ERRRUN) {
			*errorString = std::string("[Runtime Error]: ") + plua_tolstring(script->state, -1, NULL);

			////Log::Debug("Error calling Lua code %s", (*errorString).c_str());
		}
		else if (r2 == LUA_ERRMEM) {
			*errorString = std::string("[Memory Allocation Error]: ") + plua_tolstring(script->state, -1, NULL);

			////Log::Debug("Error calling Lua code %s", (*errorString).c_str());
		}
		else if (r2 == LUA_ERRERR) {
			*errorString = std::string("[Error while running error handler]: ") + plua_tolstring(script->state, -1, NULL);

			////Log::Debug("Error calling Lua code %s", (*errorString).c_str());
		}
		else {
			////Log::Debug("Error calling Lua code (%i)", r2);
		}

		return false;
	}

	//Log::Debug("Executed Lua Fine!");

	return true;
}