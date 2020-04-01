using LightingSystem.Domain.HomeLightSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightingSystem.Data.EntityConfigurations
{
    public class HomeLightSystemContext : DbContext
    {
        public HomeLightSystemContext(DbContextOptions<HomeLightSystemContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HomeLightSystem>()
                .HasKey(mb => mb.Id);
            modelBuilder.Entity<HomeLightSystem>().Property<Guid>("Id")
                .HasColumnName("id");

            modelBuilder.Entity<LightPoint>().Property<Guid>("HomeLightSystemId")
                .HasColumnName("homelightsystemid");

            modelBuilder.Entity<LightPoint>().Property<Guid>("Id")
                .HasColumnName("id");

            modelBuilder.Entity<LightBulb>().Property<Guid>("Id")
                .HasColumnName("id");

            modelBuilder.Entity<LightBulb>().Property<Guid>("LightPointId")
                .HasColumnName("lightpointid");
   
            modelBuilder.Entity<HomeLightSystem>().ToTable("homelightsystem");
            modelBuilder.Entity<LightPoint>().ToTable("lightpoint");
            modelBuilder.Entity<LightBulb>().ToTable("lightbulb");

            modelBuilder.Entity<LightPoint>().HasKey(mb => mb.Id);
            modelBuilder.Entity<LightBulb>().HasKey(mb => mb.Id);

            var navigation = modelBuilder.Entity<LightPoint>().Metadata
                .FindNavigation(nameof(LightingSystem.Domain.HomeLightSystem.LightPoint.LightBulbs));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.ApplyConfiguration(new HomeLightSystemEntityTypeConfiguration());

        }

        public DbSet<LightBulb> LightBulb { get; set; }
        public DbSet<LightPoint> LightPoint { get; set; }
        public DbSet<HomeLightSystem> HomeLightSystem { get; set; }
    }

    internal class HomeLightSystemEntityTypeConfiguration : IEntityTypeConfiguration<HomeLightSystem>
    {
        public void Configure(EntityTypeBuilder<HomeLightSystem> builder)
        {
            builder.HasKey(b => b.Id);
            builder
                .Metadata
                .FindNavigation(nameof(HomeLightSystem.LightPoints))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsMany<LightPoint>("lightPoints", lp =>
            {
                 lp.Property<string>("CustomName");
                 lp.Property<bool>("IsAvailable");
                
                lp.OwnsMany<LightBulb>("lightBulbs", b =>
                 {
                    b.Property<bool>("Status");
                 });     
            });
        }
    }
}
