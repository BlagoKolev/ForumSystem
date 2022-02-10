using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Helper
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var db = services.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
        }
    }
}
