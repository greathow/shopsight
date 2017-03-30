using System;
using Microsoft.EntityFrameworkCore;

internal static class StartupExtensions
{
    internal static void UseSqlLiteOrSqlServer(
        this DbContextOptionsBuilder contextOptionsBuilder, 
        string connectionString)
    {
        if (connectionString.StartsWith("Filename", StringComparison.OrdinalIgnoreCase))
        {
            contextOptionsBuilder.UseSqlite(connectionString);
        }
        else
        {
            contextOptionsBuilder.UseSqlServer(connectionString);
        }
    }
}