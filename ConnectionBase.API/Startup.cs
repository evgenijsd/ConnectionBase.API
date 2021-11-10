using ConnectionBase.DataAccess.EFCore;
using ConnectionBase.Domain.Interface;
using ConnectionBase.Domain.Service;
using ConnectionBase.Domain.Service.Generic;
using ConnectionBase.Domain.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ConnectionBase.API
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
            services.AddDbContext<ConnectionBaseContext>(options =>
            options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ConnectionBaseContext).Assembly.FullName)));

            services.AddScoped<IUnitOfWorkAsync, UnitOfWorkAsync>();
            services.AddTransient(typeof(IGenericServiceAsync<,>), typeof(GenericServiceAsync<,>));
            services.AddTransient(typeof(ICrossServiceAsync<,>), typeof(CrossServiceAsync<,>));
            services.AddTransient(typeof(IDeviceServiceAsync<,>), typeof(DeviceServiceAsync<,>));
            services.AddTransient(typeof(IChainServiceAsync<,>), typeof(ChainServiceAsync<,>));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ConnectionBase.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConnectionBase.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
