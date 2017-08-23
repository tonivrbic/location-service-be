using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LocationServicesApi;

namespace LocationServicesApi.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20170611134631_soem")]
    partial class soem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LocationServicesApi.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Icon")
                        .IsRequired();

                    b.Property<float?>("Latitude");

                    b.Property<float?>("Longitude");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("LocationServicesApi.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("Date");

                    b.Property<int>("DeviceId");

                    b.Property<float>("Latitude");

                    b.Property<float>("Longitude");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("LocationServicesApi.Models.Location", b =>
                {
                    b.HasOne("LocationServicesApi.Models.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
