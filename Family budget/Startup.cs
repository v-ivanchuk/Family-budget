using AutoMapper;
using Family_budget.BusinessLayer.Interfaces;
using Family_budget.BusinessLayer.Mapping;
using Family_budget.BusinessLayer.Services;
using Family_budget.DataAccessLayer;
using Family_budget.DataAccessLayer.Entities;
using Family_budget.DataAccessLayer.Interfaces;
using Family_budget.DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace Family_budget
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BudgetContext>(options =>
                options.UseSqlServer(connection));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IUserService, UserService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            
            services.AddSingleton(mapper);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, UserRole.Admin.ToString());
                });

                options.AddPolicy("HeadOfFamily", builder =>
                {
                    builder.RequireRole(UserRole.FamilyHead.ToString());
                });
            });

            services.AddControllersWithViews();

            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Clear();
                o.ViewLocationFormats.Add
                    ("/PresentationLayer/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add
                    ("/PresentationLayer/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
