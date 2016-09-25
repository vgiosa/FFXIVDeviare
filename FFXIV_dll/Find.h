#pragma once

struct FindStandard
{
	bool bDataCompare(const BYTE* pData, const BYTE* bMask, const char* szMask)
	{
		for (; *szMask; ++szMask, ++pData, ++bMask)
			if (*szMask == 'x' && *pData != *bMask)
				return false;

		return (*szMask) == NULL;
	}

	DWORD dwFindPattern(DWORD dwAddress, DWORD dwLen, BYTE *bMask, char * szMask)
	{
		for (DWORD i = 0; i < dwLen; i++)
		{
			if (bDataCompare((BYTE*)(dwAddress + i), bMask, szMask))
				return (DWORD)(dwAddress + i);
		}

		return 0;
	}
};

class Find
{
public:
	static DWORD Pattern(DWORD dwAddress, DWORD dwLen, BYTE* pbMask, char* pszMask) {
		FindStandard f;

		return f.dwFindPattern(dwAddress, dwLen, pbMask, pszMask);
	}

	static DWORD Pattern(MODULEINFO miModule, BYTE* pbMask, char* pszMask) {
		return Find::Pattern((DWORD)miModule.lpBaseOfDll, miModule.SizeOfImage, pbMask, pszMask);
	}

	static DWORD GetAddressOfCall(DWORD dwAddress) {
		return (*(DWORD *)(dwAddress + 1) + dwAddress + 5);
	}
};