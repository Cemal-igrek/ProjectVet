﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectVet.EfCore;

#nullable disable

namespace ProjectVet.Persistance.Migrations
{
    [DbContext(typeof(KlinikContext))]
    partial class KlinikContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Kullanici", b =>
                {
                    b.Property<Guid>("KullaniciId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("KullaniciName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parola")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TelefonNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KullaniciId");

                    b.ToTable("Kullanici");
                });

            modelBuilder.Entity("Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cins")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("KullaniciId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Tur")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Yas")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KullaniciId");

                    b.ToTable("Pet");
                });

            modelBuilder.Entity("ProjectVet.Domain.Entities.Models.RandevuKisit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("OgledenOnceMi")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("RandevuKisit");
                });

            modelBuilder.Entity("ProjectVet.Models.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("ProjectVet.Models.Randevu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Aciklama")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("AsiMiMuayeneMi")
                        .HasColumnType("bit");

                    b.Property<Guid>("HayvanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("KullaniciId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("OnaylandiMi")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RandevuSaat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RandevuTarih")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HayvanId");

                    b.HasIndex("KullaniciId");

                    b.ToTable("Randevu");
                });

            modelBuilder.Entity("Pet", b =>
                {
                    b.HasOne("Kullanici", "Kullanici")
                        .WithMany("Petler")
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("ProjectVet.Models.Randevu", b =>
                {
                    b.HasOne("Pet", "Pet")
                        .WithMany("Randevular")
                        .HasForeignKey("HayvanId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Kullanici", "Kullanici")
                        .WithMany("Randevular")
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Kullanici");

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("Kullanici", b =>
                {
                    b.Navigation("Petler");

                    b.Navigation("Randevular");
                });

            modelBuilder.Entity("Pet", b =>
                {
                    b.Navigation("Randevular");
                });
#pragma warning restore 612, 618
        }
    }
}
