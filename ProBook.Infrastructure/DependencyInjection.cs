using Microsoft.Extensions.DependencyInjection;
using ProBook.Application.Interfaces;
using ProBook.Infrastructure.Services;

namespace ProBook.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IReportService, ReportService>();

            return services;
        }
    }
}