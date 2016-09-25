#include "stdafx.h"

DWORD g_dwGameSingleton = NULL;

CGame* CGame::Singleton() {
	if (g_dwGameSingleton == NULL) {
		DWORD dwGamePattern = Find::Pattern(gFFXIVModuleInfo,
			(BYTE*)"\x81\xFE\x01\x02\x00\x00\x0F\x85\xA3\x02\x00\x00\x39\x1D",
			(CHAR*)"xx??xxxxxxxxxx");

		if (dwGamePattern == NULL) {
			//Log::Fatal("Unable to find CGame pattern...");
		}

		//Log::Debug("CGame Signature Result: 0x%X", dwGamePattern);

		DWORD dwGameAddress = *(DWORD*)(dwGamePattern + 0xE);

		//Log::Debug("CGame Address: 0x%X", dwGameAddress);

		g_dwGameSingleton = dwGameAddress;
	}

	return (CGame*)*(DWORD*)g_dwGameSingleton;
}