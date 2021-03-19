using BLL.dto;
using BLL.impl;
using BLL.Intarfaces;
using ComputerNet.DAL.Interfaces;
using ComputerNet.DAL.Repositories;
using DAL;
using DAL.Entity;
using DAL.impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.Dal;

namespace WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<MyDbContext>();
            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<MyDbContext>().AddDefaultTokenProviders();

            services.AddScoped<IDeliveryService, DeliveryService>();
            services.AddScoped<ILocalityService, LocalityService>();
            services.AddScoped<IWayRepository, WayRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            services.AddScoped<ILocalityRepository, LocalityRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IBillRepository, BillRepository>();

//            services.AddScoped<IGenericService<StreetDto, Street>, GenericService<Street, StreetDto>>();
//            services.AddScoped<IGenericService<HouseDto, House>, GenericService<House, HouseDto>>();
//            services.AddScoped<IGenericService<ApartmentDto, Apartment>, GenericService<Apartment, ApartmentDto>>();
//            services.AddScoped<IGenericService<UserDto, User>, GenericService<User, UserDto>>();
//            services.AddScoped<IGenericService<UserAppartmenDto, UserApartment>, GenericService<UserApartment, UserAppartmenDto >>();
//            services.AddScoped<IEmailService, EmailService>();


            services.AddDbContext<MyDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

//            var mapperAPIConfig = new MapperConfiguration(opt => opt.AddProfile<ModelDtoMaper>());
//            IMapper mapperAPI = new Mapper(mapperAPIConfig);
//            services.AddSingleton(mapperAPI);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
                    pattern: "{controller=Home}/{action=Home}");
            });
        }
    }
}