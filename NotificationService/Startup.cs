using System;
using System.Reflection;
using Hangfire;
using Hangfire.PostgreSql;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationService.Application.NotificationMediator.Queries.GetNotif;
using NotificationService.Domain;

namespace NotificationService
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
            services.AddMediatR(typeof(GetNotifQueryHandler).GetTypeInfo().Assembly);

            services.AddDbContext<NotifContext>(opt =>
            opt.UseNpgsql(Configuration.GetConnectionString("NpgsqlConnection")));

            services.AddHangfire(opt =>
            opt.UsePostgreSqlStorage("Host = localhost; Username = postgres; Password = docker; Database = hangfire_db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireServer();

            app.UseHangfireDashboard();
            RecurringJob.AddOrUpdate<Listener>(x => x.Receive(), Cron.MinuteInterval(5));
        }
    }
}
