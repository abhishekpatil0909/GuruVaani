using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Interfaces;
using Project.Domain.Enums;
using Project.Infrastructure;
using Project.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = builder.Configuration;

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(Project.Application.Mappings.MappingProfile));

// Infrastructure (DbContext, repositories, application services)
builder.Services.AddInfrastructure(configuration);

// JWT Authentication
var jwt = configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwt.GetValue<string>("Key"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt.GetValue<string>("Issuer"),
        ValidAudience = jwt.GetValue<string>("Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(key),
        // Map incoming JWT claim types to ASP.NET identity claims so role-based
        // [Authorize(Roles = "...")] works when the token contains a "role" claim.
        NameClaimType = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub,
        RoleClaimType = "role"
    };
});

builder.Services.AddAuthorization();

// FluentValidation automatic registration
// builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

// Apply EF Core migrations and seed initial data (admin user)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<AppDbContext>();
        db.Database.Migrate();

        var userRepo = services.GetRequiredService<IUserRepository>();
        var users = userRepo.ListAsync().GetAwaiter().GetResult();

        var adminEmail = configuration.GetValue<string>("Seed:AdminEmail") ?? "admin@local";
        var hasAdmin = users.Any(u => u.Email == adminEmail);
        if (!hasAdmin)
        {
            var auth = services.GetRequiredService<IAuthService>();
            var adminPassword = configuration.GetValue<string>("Seed:AdminPassword") ?? "ChangeMe123!";
            auth.RegisterAsync("Administrator", adminEmail, adminPassword).GetAwaiter().GetResult();
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Seeded initial admin user: {Email}", adminEmail);
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

// Global exception handling middleware
app.UseMiddleware<Project.Api.Middleware.ExceptionMiddleware>();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
