// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

// Default
#include "targetver.h"

#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include <WinSock2.h>
#include <stdio.h>
#include <stdlib.h>
#include <intrin.h>
#include <vector>
#include <string>
#include <sstream>
#include <iomanip>
#include <cstdio>
#include <Psapi.h>

#pragma comment(lib, "Psapi.lib")

#define snprintf _snprintf

#ifndef _MSC_VER
#define NOEXCEPT noexcept
#else
#define NOEXCEPT
#endif

#pragma comment(lib, "Ws2_32.lib")

// Types
#include "Types.h"

// Memory stuff
#define PE_TO_IDA(x) (((DWORD) x - (DWORD) GetModuleHandle(NULL)) + 0x00400000)
#define IDA_TO_PE(x) (((DWORD) x - 0x00400000) + (DWORD) GetModuleHandle(NULL))
#include "CGame.h"
#include "Lua.h"

// App

#include "Find.h"

// Make this global
extern MODULEINFO gFFXIVModuleInfo;