using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Interfaces;
using Project.Application.Services;
using Project.Domain.Entities;
using Project.Infrastructure.Data;
using Project.Infrastructure.Repositories;

namespace Project.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlite(config.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // register specialized repositories
            services.AddScoped<Project.Application.Interfaces.IUserRepository, Project.Infrastructure.Repositories.UserRepository>();
            services.AddScoped<Project.Application.Interfaces.IBookingRepository, Project.Infrastructure.Repositories.BookingRepository>();
            services.AddScoped<Project.Application.Interfaces.IAvailabilityRepository, Project.Infrastructure.Repositories.AvailabilityRepository>();
            services.AddScoped<Project.Application.Interfaces.IPaymentRepository, Project.Infrastructure.Repositories.PaymentRepository>();

            // application services wired here
            services.AddScoped<Project.Application.Interfaces.IAuthService, Project.Application.Services.AuthService>();
            services.AddScoped<Project.Application.Interfaces.IGuruService, Project.Application.Services.GuruService>();
            services.AddScoped<Project.Application.Interfaces.IAvailabilityService, Project.Application.Services.AvailabilityService>();
            services.AddScoped<Project.Application.Interfaces.IBookingService, Project.Application.Services.BookingService>();
            services.AddScoped<Project.Application.Interfaces.IPaymentService, Project.Application.Services.PaymentService>();
            // Unit of Work for transactional operations
            services.AddScoped<Project.Application.Interfaces.IUnitOfWork, Project.Infrastructure.UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}
