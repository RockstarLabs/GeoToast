using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GeoToast.Data;
using GeoToast.Data.Models;

namespace GeoToast.Migrations
{
    [DbContext(typeof(GeoToastDbContext))]
    [Migration("20170201095647_AddNotification")]
    partial class AddNotification
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("GeoToast.Data.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<float>("Latitude");

                    b.Property<float>("Longitude");

                    b.Property<int>("PropertyId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("GeoToast.Data.Models.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Kind");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Url");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("GeoToast.Data.Models.Notification", b =>
                {
                    b.HasOne("GeoToast.Data.Models.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
