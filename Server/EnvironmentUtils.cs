using System;

namespace Server
{
    public static class EnvironmentUtils
    {
        public static string GetEnvironmentVariable(string variableName, string defaultValue)
        {
            return Environment.GetEnvironmentVariable(variableName) ?? defaultValue;
        }
    }
}