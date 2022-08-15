using Booking.Data;
using Booking.Data.Contexts;
using Booking.Data.Queries;
using Booking.Data.Repositories;
using Booking.Domain.Interfaces;
using Booking.Domain.Interfaces.Queries;
using Microsoft.AspNetCore.Identity;
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

            services.ApplyIdentityDatabase(configuration);

            services.AddDbContext<BookingContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IRoomQueries, RoomQueries>();
            services.AddScoped<IReservationQueries, ReservationQueries>();
        }

        public static void ApplyIdentityDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        public static void ApplyDefaultIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.ApplyIdentityDatabase(configuration);

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        public static void ApplyDatabaseMigrations(this IServiceProvider services)
        {
            var appContext = services.GetRequiredService<ApplicationDbContext>();
            if (appContext.Database.GetPendingMigrations().Any())
            {
                appContext.Database.Migrate();
            }

            var bookingContext = services.GetRequiredService<BookingContext>();
            if (bookingContext.Database.GetPendingMigrations().Any())
            {
                bookingContext.Database.Migrate();
            }
        }
    }
}
