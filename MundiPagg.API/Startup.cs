using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MundiPagg.API.Configurations;
using MundiPagg.API.DatabaseContext;
using MundiPagg.API.DatabaseContext.Repository.Produto;
using MundiPagg.API.Helpers;
using MundiPagg.API.Services;

namespace MundiPagg.API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc(option => option.EnableEndpointRouting = false);
            //services.AddCors();

            /*
            services.AddCors(options =>
            {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              builder =>
                              {
                                  builder.WithOrigins("http://localhost*",
                                                      "http://localhost*",
                                                      "httpS://localhost*",
                                                      "https://localhost*"
                                                      )
                                                      .SetIsOriginAllowedToAllowWildcardSubdomains()
                                                      .AllowAnyHeader()
                                                      .AllowAnyMethod()
                                                      .AllowAnyOrigin();
                              });
            });
            */
            services.AddControllers();

            services.AddCors(
                options => options.AddDefaultPolicy(
                    builder => builder.AllowAnyOrigin())
            );  
             

            //MongoDB
            services.Configure<ProdutosDBSettings>(
                Configuration.GetSection(nameof(ProdutosDBSettings))
            );

            services.AddSingleton<IProdutosDBSettings>(sp =>
            sp.GetRequiredService<IOptions<ProdutosDBSettings>>().Value);

            services.AddScoped<ProdutoService>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IAppDatabase, AppDatabase>();

            services.AddAutoMapper();
            //services.AddMvc();

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseCors();
            app.UseStaticFiles();
            //app.UseCors(options => options.AllowAnyOrigin());
            //app.UseMvc();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
            });
           
            
        }
    }
}
