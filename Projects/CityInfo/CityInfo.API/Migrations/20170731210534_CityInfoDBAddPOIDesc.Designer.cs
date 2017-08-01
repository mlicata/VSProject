using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CityInfo.API.Entities;

namespace CityInfo.API.Migrations
{
    [DbContext(typeof(CityInfoContext))]
    [Migration("20170731210534_CityInfoDBAddPOIDesc")]
    partial class CityInfoDBAddPOIDesc
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CityInfo.API.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Desc")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("CityInfo.API.Entities.POI", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Desc")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("cityId");

                    b.HasKey("Id");

                    b.HasIndex("cityId");

                    b.ToTable("POIs");
                });

            modelBuilder.Entity("CityInfo.API.Entities.POI", b =>
                {
                    b.HasOne("CityInfo.API.Entities.City", "City")
                        .WithMany("PointsOfInterest")
                        .HasForeignKey("cityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
