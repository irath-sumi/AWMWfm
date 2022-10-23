using AWMWfm.GraphQL;
using AWMWfm.Interface;
using AWMWfm.Model;
using AWMWfm.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWMWfm
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            });
            services.AddControllers();
            services.AddGraphQLServer().AddQueryType<Query>().AddFiltering().AddSorting();
            services.AddTransient<IAWMService,AWMService>();
            services.AddPooledDbContextFactory<AWMDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AWMDBConnectionString"))); 
          //  services.AddDbContext<AWMDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AWMDBConnectionString")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI V1");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // create db if not present already
            //using (var scope = app.ApplicationServices.CreateScope())
            //{
            //    var dbContext = scope.ServiceProvider.GetRequiredService<AWMDBContext>();
            //    dbContext.Database.EnsureCreated();
            //}

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
               //  endpoints.MapControllers();
                endpoints.MapGraphQL();
            });
        }
    }
}
