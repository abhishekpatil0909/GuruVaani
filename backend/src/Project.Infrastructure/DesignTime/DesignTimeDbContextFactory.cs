using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.DesignTime
{
    // Provides a DbContext at design time for EF Core tools (dotnet ef migrations ...)
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var basePath = Directory.GetCurrentDirectory();
            // If tools invoke from solution root, try to locate infrastructure project folder
            // Use current directory as base; users can set environment variables for connection string if needed.
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .AddEnvironmentVariables();

            var config = builder.Build();
            var conn = config.GetConnectionString("DefaultConnection")
                       ?? Environment.GetEnvironmentVariable("DefaultConnection")
                       ?? "Data Source=GuruVaniDb.db";

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite(conn);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
