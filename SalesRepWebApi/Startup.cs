using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SalesRepDAL;
using SalesRepDAL.Entities;
using SalesRepDAL.Repositories;
using SalesRepDAL.Repositories.Contracts;
using SalesRepDAL.Seeders;
using SalesRepServices.Services.Implementation;
using SalesRepServices.Services.Interfaces;
using SalesRepServices.Services_ForSalesRep;
using SalesRepServices.Services_Interfaces;
using System;
using System.Text;

namespace SalesRepWebApi
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
            services.AddControllers();
            services.AddScoped<EFContext>();
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            //we're using in memory database for a quick dev and testing
            services.AddDbContext<EFContext>(options =>
                        options.UseSqlServer(connectionString));
            services.AddIdentity<DbUser, DbRole>(options =>
             {
                 options.Stores.MaxLengthForKeys = 128;
                 options.User.RequireUniqueEmail = true;
                 options.SignIn.RequireConfirmedEmail = true;
             })
            .AddEntityFrameworkStores<EFContext>()
            .AddDefaultTokenProviders();

            //jwtToken and Bearer
            var singinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                                                    (Configuration.GetValue<string>("JwtKey")));
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = singinKey,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            //services
            services.AddScoped<ICustomerService,CustomerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISalesRepService, SalesRepService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<ITradeCompanyService, TradeCompanyService>();
            services.AddScoped<ITradeOrderService, TradeOrderService>();
            services.AddScoped<ILogsReport, LogsReport>();
            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISalesRepRepository, SalesRepRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ITradeCompanyRepository, TradeCompanyRepository>();
            services.AddScoped<ITradeOrderRepository, TradeOrderRepository>();
            services.AddSwaggerGen();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Oyster");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}"
                    );
            });

            //seeder temporary method
            //SeedDataToDB.SeedData(app.ApplicationServices);
        }
    }
}
