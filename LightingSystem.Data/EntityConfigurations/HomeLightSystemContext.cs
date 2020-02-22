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
                .HasKey(mb => mb.LocalLightingSystemId);
            modelBuilder.Entity<HomeLightSystem>().Property<Guid>("LocalLightingSystemId")
                .HasColumnName("locallightingsystemid");

            modelBuilder.Entity<LightPoint>().Property<Guid>("HomeLightSystemLocalLightingSystemId")
              .HasColumnName("homelightsystemlocallightingsystemid");

            modelBuilder.Entity<LightPoint>().Property<Guid>("LightPointId")
            .HasColumnName("lightpointid");

            modelBuilder.Entity<Bulb>().Property<Guid>("Id")
           .HasColumnName("id");

            modelBuilder.Entity<Bulb>().Property<Guid>("LightPointId")
          .HasColumnName("lightpointid");
            //modelBuilder.Entity<HomeLightSystem>().Ignore(
            //    hls => hls.LightPoints);
            //.Property<IEnumerable<LightPoint>>(hls=>hls.LightPoints)
            //.UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<HomeLightSystem>().ToTable("homelightsystem");
            modelBuilder.Entity<LightPoint>().ToTable("lightpoint");
            modelBuilder.Entity<Bulb>().ToTable("bulb");


            modelBuilder.Entity<LightPoint>().HasKey(mb => mb.LightPointId);
            modelBuilder.Entity<Bulb>().HasKey(mb => mb.Id);

            //modelBuilder.Entity<HomeLightSystem>()
            //    .OwnsMany<LightPoint>(hls => {
            //       hls.WithOwner().HasForeignKey("CustomerId");
            //    })
            //    .WithOne();
            var navigation = modelBuilder.Entity<LightPoint>().Metadata
                .FindNavigation(nameof(LightingSystem.Domain.HomeLightSystem.LightPoint.LightBulbs));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.ApplyConfiguration(new HomeLightSystemEntityTypeConfiguration());

        }

        public DbSet<Bulb> Bulb { get; set; }
        public DbSet<LightPoint> LightPoint { get; set; }
        public DbSet<HomeLightSystem> HomeLightSystem { get; set; }
    }

    internal class HomeLightSystemEntityTypeConfiguration : IEntityTypeConfiguration<HomeLightSystem>
    {
        public void Configure(EntityTypeBuilder<HomeLightSystem> builder)
        {
            builder.HasKey(b => b.LocalLightingSystemId);
            builder
                .Metadata
                .FindNavigation(nameof(HomeLightSystem.LightPoints))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            //builder
            //    .Metadata
            //    .FindNavigation(nameof(LightPoint.LightBulbs))
            //    .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsMany<LightPoint>("lightPoints", lp =>
            {
                 lp.Property<string>("MqttId");
                 lp.Property<string>("CustomName");
                 lp.Property<bool>("IsAvailable");
                
                lp.OwnsMany<Bulb>("lightBulbs", b =>
                 {
                    b.Property<int>("Number");
                    b.Property<bool>("Status");
                 });     
            });
        }
    }
}
