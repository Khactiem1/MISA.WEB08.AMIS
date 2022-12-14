using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MISA.WEB08.AMIS.API.Middleware;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.API
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            services.AddControllers().ConfigureApiBehaviorOptions(options => {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MISA.WEB08.AMIS.API", Version = "v1" });
            });
            //injiection
            services.AddScoped<IEmployeeBL, EmployeeBL>();
            services.AddScoped<IEmployeeDL, EmployeeDL>();
            services.AddScoped<IUnitBL, UnitBL>();
            services.AddScoped<IUnitDL, UnitDL>();
            services.AddScoped<IUnitCalculationBL, UnitCalculationBL>();
            services.AddScoped<IUnitCalculationDL, UnitCalculationDL>();
            services.AddScoped<IDepotBL, DepotBL>();
            services.AddScoped<IDepotDL, DepotDL>();
            services.AddScoped<ICommodityGroupBL, CommodityGroupBL>();
            services.AddScoped<ICommodityGroupDL, CommodityGroupDL>();
            services.AddScoped<IInventoryItemBL, InventoryItemBL>();
            services.AddScoped<IInventoryItemDL, InventoryItemDL>();
            services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
            services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
            DataContext.MySqlConnectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MISA.WEB08.AMIS.API v1"));
            }
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"Assets\Images")),
                RequestPath = new PathString("/Assets/Images")
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"Assets\Images")),
                RequestPath = new PathString("/Assets/Images")
            });
        }
    }
}
