// dllmain.cpp : Defines the entry point for the DLL application.

#include "stdafx.h"
#include "winsock.h"

HMODULE g_hOriginalModule = NULL;

MODULEINFO gFFXIVModuleInfo;

BOOL APIENTRY DllMain(HMODULE /* hModule */, DWORD ul_reason_for_call, LPVOID /* lpReserved */)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

extern "C" __declspec(dllexport) HRESULT WINAPI fnCustom();
__declspec(dllexport) HRESULT WINAPI fnCustom()
{
	MessageBox(NULL, "test", "in", NULL); return 99;
}

HRESULT WINAPI StartModuleInfo() {
	// We find the patterns we need
	if (GetModuleInformation(GetCurrentProcess(), GetModuleHandle(NULL), &gFFXIVModuleInfo, sizeof(gFFXIVModuleInfo)) == FALSE) {

		return true;
	}
	return false;
}


typedef int(__cdecl* luaL_loadbuffer_t)(lua_State *L, const char *buff, size_t size, const char *name);
typedef int(__cdecl* lua_pcall_t)(lua_State* L, int nargs, int nresults, int errfunc);
typedef const char* (__cdecl* lua_tolstring_t)(lua_State *L, int idx, size_t *len);
typedef int(__thiscall* executeScriptString_t)(void* This, const char* buffer, int size, char* name, bool pcall);

//int __thiscall executeScriptBuffer(int this, int buff, int size, int name, char call)


extern "C" __declspec(dllexport) HRESULT WINAPI  GetLuaAddresses(DWORD &loadBuffer, DWORD &toLString, DWORD &pCall) {


	DWORD dwLoadBufferAndToLStringPattern = Find::Pattern(gFFXIVModuleInfo,
		(BYTE*) "\x83\xC4\x14\x85\xFF\x74\x2C\x8B\x56\x08",
		(CHAR*) "xxxxxxxxxx");

	DWORD dwPCallPattern = Find::Pattern(gFFXIVModuleInfo,
		(BYTE*) "\x8B\x56\x08\x6A\x00\x51\x6A\x02\x52\xE8",
		(CHAR*) "xxxxxxxxxx");

	if (!dwLoadBufferAndToLStringPattern) return -1;
	//Log::Fatal("Unable to locate lua signature #1!");

	if (!dwPCallPattern) return -2;
	////Log::Fatal("Unable to locate lua signature #2!");

	////Log::Debug("dwLoadBufferAndToLStringPattern = 0x%X", dwLoadBufferAndToLStringPattern);
	////Log::Debug("dwPCallPattern = 0x%X", dwPCallPattern);

	DWORD dwLoadBuffer = Find::GetAddressOfCall(dwLoadBufferAndToLStringPattern - 0x7);
	DWORD dwToLString = Find::GetAddressOfCall(dwLoadBufferAndToLStringPattern + 0xF);
	DWORD dwPCall = Find::GetAddressOfCall(dwPCallPattern + 0x9);


	loadBuffer = dwLoadBuffer;
	toLString = dwToLString;
	pCall = dwPCall;
	

	return 1;
}



extern "C" __declspec(dllexport) HRESULT WINAPI GetSocketInfo(SOCKET s, char* p) {
	

	int len;
	struct sockaddr_storage addr;
	char ipstr[8];
	int port;

	len = sizeof addr;
	getpeername(s, (struct sockaddr*)&addr, &len);

	// deal with both IPv4 and IPv6:
	if (addr.ss_family == AF_INET) {
		struct sockaddr_in *s = (struct sockaddr_in *)&addr;
		port = ntohs(s->sin_port);
	}

	printf("Peer IP address: %s\n", ipstr);
	printf("Peer port      : %d\n", port);
	p = ipstr;
	return port;
}


extern "C" __declspec(dllexport) HRESULT WINAPI GenerateUniqueTimestamp(unsigned __int64 &timestamp) {
	_FILETIME v1, FileTime;
	_SYSTEMTIME SystemTime;
	SYSTEMTIME v4;

	GetSystemTime(&SystemTime);
	SystemTimeToFileTime(&SystemTime, &FileTime);
	v4.wMilliseconds = 0;
	*(DWORD *)&v4.wYear = 67506;
	*(DWORD *)&v4.wDay = 1;
	*(DWORD *)&v4.wMinute = 0;
	v4.wDayOfWeek = 0;
	SystemTimeToFileTime(&v4, &v1);
	timestamp = (unsigned __int64)(*(unsigned __int64 *)&FileTime - *(unsigned __int64 *)&v1) / 1000 / 10;
	return true;
}

extern "C" __declspec(dllexport) HRESULT WINAPI SocketSend(SOCKET s, char *buf, int len, int flags) {
	
	int r = send(s, buf, len, flags);
	return len;//r;
}
