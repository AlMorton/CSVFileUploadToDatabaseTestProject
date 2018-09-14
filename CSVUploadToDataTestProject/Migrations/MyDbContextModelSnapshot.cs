﻿// <auto-generated />
using System;
using CSVUploadToDataTestProject.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CSVUploadToDataTestProject.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CSVUploadToDataTestProject.EntityFramework.DomainModel.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("CSVUploadToDataTestProject.EntityFramework.DomainModel.CSVData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("FooData");

                    b.Property<int>("SiteId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("SiteId");

                    b.ToTable("CSVData");
                });

            modelBuilder.Entity("CSVUploadToDataTestProject.EntityFramework.DomainModel.Site", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("CSVUploadToDataTestProject.EntityFramework.DomainModel.CSVData", b =>
                {
                    b.HasOne("CSVUploadToDataTestProject.EntityFramework.DomainModel.Client", "Client")
                        .WithMany("CSVData")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CSVUploadToDataTestProject.EntityFramework.DomainModel.Site", "Site")
                        .WithMany("CSVData")
                        .HasForeignKey("SiteId");
                });

            modelBuilder.Entity("CSVUploadToDataTestProject.EntityFramework.DomainModel.Site", b =>
                {
                    b.HasOne("CSVUploadToDataTestProject.EntityFramework.DomainModel.Client", "Client")
                        .WithMany("Sites")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
