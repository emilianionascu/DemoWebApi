using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddDbContextPool<WebApiDbContext>(options =>
            {
                options.UseSqlite(this.Configuration.GetConnectionString("SqliteConnection"), x => x.MigrationsAssembly(typeof(Startup).Assembly.GetName().Name));

                // A lot of warnings were shown in logging because of some queries. Some of them can be fixed and some others
                // are still an issue in EF Core. Hiding the warnings so that they don't fill up the logging table.
                options.ConfigureWarnings(builder => builder.Ignore(RelationalEventId.QueryClientEvaluationWarning));
            });

            services.AddMvc();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Web API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
                x.RoutePrefix = string.Empty;
            });
            app.UseMvc();
        }
    }
}
