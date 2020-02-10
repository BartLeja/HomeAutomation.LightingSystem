using LightingSystem.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace LightingSystem.Infrastructure.Database
{
    public class HomeLightSystemContext: DbContext
    {
        public HomeLightSystemContext(DbContextOptions<HomeLightSystemContext> options) : base(options)
        {

        }

        public DbSet<Bulb> Bulb { get; set; }
        public DbSet<LightPoint> LightPoint { get; set; }
        public DbSet<HomeLightSystem> HomeLightSystem { get; set; }
    }
}
