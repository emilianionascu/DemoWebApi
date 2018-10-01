using Entities.EntityModels;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options)
        {
        }
        public DbSet<Request> Requests { get; set; }
    }
}
