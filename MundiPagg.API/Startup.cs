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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
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

            services.AddControllers();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
