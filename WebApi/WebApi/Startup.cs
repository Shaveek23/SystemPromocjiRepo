using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi.Database;
using WebApi.Database.Repositories.Implementations;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Middlewares;
using WebApi.Services.Serives_Implementations;
using WebApi.Services.Services_Interfaces;

namespace WebApi
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

            // configuring DatabaseContext:
            services.AddDbContext<DatabaseContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("SystemPromocjiDb")));

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ICommentService, CommentService>();

            // adding repository services
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorWrappingMiddleware>();

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
