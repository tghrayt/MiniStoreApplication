using System;

namespace MiniStore.Helper;

public static class EnvirenmentVariablesHelper
{
    public static string GetEnvironmentVariable(string variableName)
    {
        return Environment.GetEnvironmentVariable(variableName);
    }
}