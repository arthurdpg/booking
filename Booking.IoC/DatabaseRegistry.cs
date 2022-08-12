using Booking.Data;
using Booking.Data.Contexts;
using Booking.Data.Queries;
using Booking.Data.Repositories;
using Booking.Domain.Interfaces;
using Booking.Domain.Interfaces.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.IoC
{
    internal static class DatabaseRegistry
    {
        public static void RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BookingContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IRoomQueries, RoomQueries>();
            services.AddScoped<IReservationQueries, ReservationQueries>();
        }

        public static void ApplyDatabaseMigrations(this IServiceProvider services)
        {
            var context = services.GetRequiredService<BookingContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
