#pragma once

#define LUA_SIGNATURE   "\033Lua"

#define LUA_MULTRET     (-1)

#define LUA_REGISTRYINDEX       (-10000)
#define LUA_ENVIRONINDEX        (-10001)
#define LUA_GLOBALSINDEX        (-10002)
#define lua_upvalueindex(i)     (LUA_GLOBALSINDEX-(i))


#define LUA_YIELD       1
#define LUA_ERRRUN      2
#define LUA_ERRSYNTAX   3
#define LUA_ERRMEM      4
#define LUA_ERRERR      5

typedef struct lua_State lua_State;

typedef int(*lua_CFunction) (lua_State *L);

#define LUA_TNONE               (-1)

#define LUA_TNIL                0
#define LUA_TBOOLEAN            1
#define LUA_TLIGHTUSERDATA      2
#define LUA_TNUMBER             3
#define LUA_TSTRING             4
#define LUA_TTABLE              5
#define LUA_TFUNCTION           6
#define LUA_TUSERDATA           7
#define LUA_TTHREAD             8

#define LUA_MINSTACK    20

struct lua_State {

};

class CScriptEngine
{
public:
	virtual void __constructor() = 0;

	uint32			unk0;		//0004
	lua_State*		state;		//0008 (might be +4, i dunno)
	BYTE			unk1;		//000C (1 by default)
	BYTE			_pad[3];	//000D (alignment)
	uint32			unk2;		//0010
	uint32			unk3;		//0014
	uint32			unk4;		//0018
};

class CNetworking
{
public:
	uint32			vt;			//0000
	SOCKET			m_socket;	//0004
};

class CGame
{
public:
	CScriptEngine* GetScriptEngine() {
		/*
		if (*(DWORD*)((DWORD)this + 0x2988)) {
			return (CScriptEngine*)*(DWORD*)*(DWORD*)((DWORD)this + 0x2988);
		}

		return NULL;
		*/

		//return (CScriptEngine*) *(DWORD*)((DWORD)this + 0x2988);

		return (CScriptEngine*)((DWORD)this + 0x2988);
	}

	static CGame* Singleton();
};