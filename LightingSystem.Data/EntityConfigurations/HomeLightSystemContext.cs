using LightingSystem.Domain.HomeLightSystem;
using LightingSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LightingSystem.Data.EntityConfigurations
{
    public class HomeLightSystemContext : DbContext
    {

        public HomeLightSystemContext(DbContextOptions<HomeLightSystemContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HomeLightSystem>().HasKey(mb => mb.Id);
            modelBuilder.Entity<LightPoint>().HasKey(mb => mb.Id);
            modelBuilder.Entity<LightBulb>().HasKey(mb => mb.Id);
            modelBuilder.Entity<LightsGroup>().HasKey(mb => mb.Id);

            modelBuilder.Entity<HomeLightSystem>().Property<Guid>("Id")
                .HasColumnName("id");

            modelBuilder.Entity<LightPoint>().Property<Guid>("HomeLightSystemId")
                .HasColumnName("homelightsystemid");

            modelBuilder.Entity<LightPoint>().Property<Guid>("Id")
                .HasColumnName("id");

            modelBuilder.Entity<LightPoint>().Property<Guid>("LightsGroupId")
                .HasColumnName("lightsgroupid");

            modelBuilder.Entity<LightBulb>().Property<Guid>("Id")
                .HasColumnName("id");

            modelBuilder.Entity<LightBulb>().Property<Guid>("LightPointId")
                .HasColumnName("lightpointid");

            modelBuilder.Entity<LightsGroup>().Property<Guid>("Id")
               .HasColumnName("id");

            //modelBuilder.Entity<LightsGroup>().Property<Guid>("LightPointId")
            //  .HasColumnName("lightpointid");

            modelBuilder.Entity<HomeLightSystem>().ToTable("homelightsystem");
            modelBuilder.Entity<LightPoint>().ToTable("lightpoint");
            modelBuilder.Entity<LightBulb>().ToTable("lightbulb");
            modelBuilder.Entity<LightsGroup>().ToTable("lightsgroup");

            var navigation = modelBuilder.Entity<LightPoint>().Metadata
                .FindNavigation(nameof(LightingSystem.Domain.HomeLightSystem.LightPoint.LightBulbs));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.ApplyConfiguration(new HomeLightSystemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LightPointsEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LightsGroupEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LightBulbEntityTypeConfiguration());
        }

        public DbSet<LightBulb> LightBulb { get; set; }
        public DbSet<LightPoint> LightPoint { get; set; }
        public DbSet<HomeLightSystem> HomeLightSystem { get; set; }
        public DbSet<LightsGroup> LightsGroup { get; set; }
    }

    //internal class HomeLightSystemEntityTypeConfiguration : IEntityTypeConfiguration<HomeLightSystem>
    //{
    //    public void Configure(EntityTypeBuilder<HomeLightSystem> builder)
    //    {
    //        builder.HasKey(b => b.Id);
    //        builder
    //            .Metadata
    //            .FindNavigation(nameof(HomeLightSystem.LightPoints))
    //            .SetPropertyAccessMode(PropertyAccessMode.Field);

    //        builder.OwnsMany<LightPoint>("lightPoints", lp =>
    //        {
    //            lp.Property<string>("CustomName");
    //            lp.Property<bool>("IsAvailable");


    //            lp.OwnsOne<LightsGroup>("lightsGroup", lg =>
    //            {
    //                lg.Property<string>("LightGroupName");
    //            });

    //            lp.OwnsMany<LightBulb>("lightBulbs", b =>
    //             {
    //                 b.Property<bool>("Status");
    //             });
    //        });


    //    }
    //}

    internal class HomeLightSystemEntityTypeConfiguration : IEntityTypeConfiguration<HomeLightSystem>
    {
        public void Configure(EntityTypeBuilder<HomeLightSystem> builder)
        {
            builder.HasKey(b => b.Id);
            builder
                .Metadata
                .FindNavigation(nameof(HomeLightSystem.LightPoints))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder
                .HasMany(ha => ha.LightPoints);
        }
    }

    internal class LightPointsEntityTypeConfiguration : IEntityTypeConfiguration<LightPoint>
    {
        public void Configure(EntityTypeBuilder<LightPoint> builder)
        {
            builder.HasKey(b => b.Id);
            builder
                .Metadata
                .FindNavigation(nameof(LightPoint.LightBulbs))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder
                .HasMany(ha => ha.LightBulbs);

            builder.Property<string>("CustomName");
            builder.Property<bool>("IsAvailable");
        }
    }

    internal class LightsGroupEntityTypeConfiguration : IEntityTypeConfiguration<LightsGroup>
    {
        public void Configure(EntityTypeBuilder<LightsGroup> builder)
        {
            builder.HasKey(b => b.Id);
            builder
                .Metadata
                .FindNavigation(nameof(LightsGroup.LightPoints))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder
                .HasMany(ha => ha.LightPoints)
                .WithOne(lp=>lp.LightsGroup);

            builder.Property<string>("LightGroupName");
        }
    }

    internal class LightBulbEntityTypeConfiguration : IEntityTypeConfiguration<LightBulb>
    {
        public void Configure(EntityTypeBuilder<LightBulb> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property<bool>("Status");
        }
    }
}
