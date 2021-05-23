using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.EMMA;
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
using WebApi.Database;
using WebApi.Database.Repositories.Implementations;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Filters;
using WebApi.Middlewares;
using WebApi.Services.Hosted_Service;
using WebApi.Services.Hosted_Service.EmailSenderHostedService;
using WebApi.Services.Serives_Implementations;
using WebApi.Services.Services_Implementations;
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

            services.AddControllers(config =>
            {
                config.Filters.Add(new CustomAuthorizationFilter());
            });

            // configuring DatabaseContext:
            services.AddDbContext<DatabaseContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("SystemPromocjiDb")));

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<INewsletterService, NewsletterService>();

            // adding newsletter services
            services.AddSingleton<IBackgroundTaskQueueService<(List<ReceiverDTO>, string, string)>, BackgroundTaskQueueService<(List<ReceiverDTO>, string, string)>>();
            services.AddHostedService<EmailSender>();
            services.AddSingleton<ISendingMonitorService, SendingMonitorService>();

            // adding repository services
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<INewsletterRepository, NewsletterRepository>();

            services.AddSwaggerGen();

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

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V2");
            });

        }
    }
}
