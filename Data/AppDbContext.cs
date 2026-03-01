using Gladiators.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gladiators.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Fighter> Gladiators => Set<Fighter>();
        public DbSet<MarketSlave> MarketSlaves => Set<MarketSlave>();
        public DbSet<PlayersSlave> PlayerSlaves => Set<PlayersSlave>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User → PlayersSlave (каскад)
            modelBuilder.Entity<PlayersSlave>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.PlayerSlaves)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // User → MarketSlave (каскад)
            modelBuilder.Entity<MarketSlave>()
                .HasOne(m => m.Player)
                .WithMany(u => u.MarketSlaves)
                .HasForeignKey(m => m.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
