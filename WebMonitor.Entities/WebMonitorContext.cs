using Microsoft.EntityFrameworkCore;
using System;

namespace WebMonitor.Entities
{
    
    public class WebMonitorContext : DbContext
    {

        public DbSet<Config> Configs { get; set; }
        public DbSet<CheckUrl> CheckUrls { get; set; }

        public WebMonitorContext() { }

        public WebMonitorContext(DbContextOptions<WebMonitorContext> options)
            : base(options)
        {

        }

#if DEBUG
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=localhost;Database=WebMonitor;Trusted_Connection=true");
        }
#endif

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Config>()
                .Property(q => q.Id)
                .UseSqlServerIdentityColumn();

            modelBuilder.Entity<Config>()
                .Property(q => q.Name)
                .IsRequired();
        }

    }

}
