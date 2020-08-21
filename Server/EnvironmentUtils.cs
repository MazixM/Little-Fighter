using System;

namespace Shared
{
    public static class EnvironmentUtils
    {
        public static string GetEnvironmentVariable(string variableName, string defaultValue)
        {
            string value = Environment.GetEnvironmentVariable(variableName);

            return value != null ? value : defaultValue;
        }
    }
}
