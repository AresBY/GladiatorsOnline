using Gladiators.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gladiators.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Gladiator> Gladiators => Set<Gladiator>();
        public DbSet<MarketSlave> MarketSlaves => Set<MarketSlave>();
        public DbSet<PlayersSlave> PlayerSlaves => Set<PlayersSlave>();
    }
}
