using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.Repository;
using Server.Data.interfaces;
using Server.Data.Validate;

namespace Server
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt =>
               opt.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddTransient<IOrder, OrderRepository>();
            services.AddTransient<IProduct, ProductRepository>();
            services.AddTransient<IOrderValidator, OrderValidate>();
            services.AddTransient<IProductValidator, ProductValidator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            using (var scope = app.ApplicationServices.CreateScope())
            {
                DataContext content = scope.ServiceProvider.GetRequiredService<DataContext>();
                DbObjectInitializer.Init(content);
            }
           
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
