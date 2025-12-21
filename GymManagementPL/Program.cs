using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.UnitOfWork;
using GymManagementBLL;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.Services.Classes;
using GymManagementDAL.Models;
using GymManagementDAL.Data.Data_seeding;
using GymManagementBLL.Services.Helper_Services;

namespace GymManagementPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //builder.Services.AddScoped(typeof(IPlanRepository), typeof(PlanRepository));
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository,SessionRepository>();
            builder.Services.AddScoped<ITrainerRepository,TrainerRepository>();
            builder.Services.AddScoped<IHomeAnalyticsService, HomeAnalyticsService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<IPlanService, PlanService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<ITrainerService, TrainerService>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddAutoMapper(X => X.AddProfile(new MappingProfiles()));

            /*
             * System.AggregateException: 'Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: GymManagementDAL.UnitOfWork.IUnitOfWork Lifetime: Scoped ImplementationType: GymManagementDAL.UnitOfWork.UnitOfWork': Unable to resolve service for type 'GymManagementDAL.Repositories.Interfaces.ISessionRepository' while attempting to activate 'GymManagementDAL.UnitOfWork.UnitOfWork'.) (Error while validating the service descriptor 'ServiceType: GymManagementBLL.Services.Interfaces.IHomeAnalyticsService Lifetime: Scoped ImplementationType: GymManagementBLL.Services.Classes.HomeAnalyticsService': Unable to resolve service for type 'GymManagementDAL.Repositories.Interfaces.ISessionRepository' while attempting to activate 'GymManagementDAL.UnitOfWork.UnitOfWork'.)'

             */
            var app = builder.Build();

            #region Seeding data

            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GymContext>();
            GymContextSeeding.IsSeeded(dbContext); 
            
            #endregion

            // Configure the HTTP request pipeline.
            #region Configure pipline
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
            #endregion

            app.Run();
        }
    }
}
