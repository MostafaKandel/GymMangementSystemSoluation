using GymMangmentDAL.Data.Context;
using GymMangmentDAL.Data.DataSeed;
using GymMangmentDAL.Entities;
using GymMangmentDAL.Repositories.Classes;
using GymMangmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymMangementPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(IGenericRepository<>));
            //builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            #region Seed Data - Migrate Database

            using var Scoped = app.Services.CreateScope();
            var dbContext= Scoped.ServiceProvider.GetRequiredService<GymDbContext>();

            var PendingMigrations= dbContext.Database.GetPendingMigrations();
            if(PendingMigrations?.Any() ?? false)
                dbContext.Database.Migrate();
            GymDbContextDataSeeding.SeedData(dbContext);

            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
