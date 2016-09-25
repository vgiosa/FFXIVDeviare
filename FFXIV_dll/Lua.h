#pragma once

class Lua
{
public:
	static bool Execute(std::string str, std::string* errorString);
	static bool ErrorToString(int error);
};