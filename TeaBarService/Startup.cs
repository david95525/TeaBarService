using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using TeaBarService.Data;
using System;

namespace TeaBarService
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
            services.AddHttpClient();
            //自訂類別注入進來 (物件獨一無二)Singleton Pattern
           
                                                          //加入DbContext變成服務
            services.AddDbContext<ApplicationDbContext>(
                //lamdba
                (builder) =>
                {
                    builder.UseSqlServer
                    (Configuration.GetConnectionString("DefaultConnection"));
                }
                );
            //不支援畫面服務 (包含靜態頁面)
            services.AddControllers();
            //跨資源存取服務設定
            services.AddCors(
                (options) =>
                {
                    options.AddPolicy("all",
                        (policy) =>
                        {
                            policy.WithOrigins("https://teabar.azurewebsites.net");
                            policy.AllowAnyMethod();
                            policy.AllowAnyHeader();
                        });
                }
                );
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TeaBarService", Version = "v1" });
            });
     
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TeaBarService v1"));
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TeaBarService v1"));
            }
     
            app.UseHttpsRedirection();
            app.UseCors("all");
            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
