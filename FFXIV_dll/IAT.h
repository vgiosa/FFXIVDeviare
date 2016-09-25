#pragma once

extern HRESULT PatchIat(__in HMODULE Module, __in PSTR ImportedModuleName, __in PSTR ImportedProcName, __in PVOID AlternateProc, __out_opt PVOID *OldProc);
extern HRESULT PatchIat(__in HMODULE Module, __in PSTR ImportedModuleName, __in DWORD Ordinal, __in PVOID AlternateProc, __out_opt PVOID *OldProc);