﻿using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            };
        }
        private static void SeedData(AppDbContext context)
        {
            if (!context.platforms.Any())
            {
                Console.WriteLine("---> Seeding data...");

                context.platforms.AddRange(
                        new Platform { Name = "Dot net", Publisher = "Microsoft", Cost = "Free" }
                    );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("---> We have data already");
            }
        }
    }
}
