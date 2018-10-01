using Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Server
{
    public static class ProcessDbCommands
    {
        public static void Process(string[] args, IWebHost host)
        {
            var services = host.Services.GetService<IServiceScopeFactory>();

            using (var scope = services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<WebApiDbContext>();
                db.Database.Migrate();
            }
        }
    }
}
