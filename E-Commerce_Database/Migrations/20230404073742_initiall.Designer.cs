﻿// <auto-generated />
using System;
using E_Commerce_Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace E_Commerce_Database.Migrations
{
    [DbContext(typeof(dbContext))]
    [Migration("20230404073742_initiall")]
    partial class initiall
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("E_Commerce_Database.Seller_Model.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("category");
                });

            modelBuilder.Entity("E_Commerce_Database.Seller.Products", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DiscountPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductSKU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SellerId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("E_Commerce_Database.Seller.SellerAddress", b =>
                {
                    b.Property<int>("SellerAddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SellerAddressId"), 1L, 1);

                    b.Property<string>("CustomerReturnAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.Property<string>("WareHouseAddress_PickUp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("sellerDetailSellerId")
                        .HasColumnType("int");

                    b.HasKey("SellerAddressId");

                    b.HasIndex("sellerDetailSellerId");

                    b.ToTable("sellerAddress");
                });

            modelBuilder.Entity("E_Commerce_Database.Seller.SellerDetail", b =>
                {
                    b.Property<int>("SellerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SellerId"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastLoginTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("LoginTry")
                        .HasColumnType("int");

                    b.Property<string>("SellerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellerPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellerPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellerStoreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SellerId");

                    b.ToTable("sellerDetail");
                });

            modelBuilder.Entity("E_Commerce_Database.Seller.Products", b =>
                {
                    b.HasOne("E_Commerce_Database.Seller_Model.Category", "Category")
                        .WithMany("products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_Commerce_Database.Seller.SellerDetail", "Seller")
                        .WithMany("products")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("E_Commerce_Database.Seller.SellerAddress", b =>
                {
                    b.HasOne("E_Commerce_Database.Seller.SellerDetail", "sellerDetail")
                        .WithMany("sellerAddress")
                        .HasForeignKey("sellerDetailSellerId");

                    b.Navigation("sellerDetail");
                });

            modelBuilder.Entity("E_Commerce_Database.Seller_Model.Category", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("E_Commerce_Database.Seller.SellerDetail", b =>
                {
                    b.Navigation("products");

                    b.Navigation("sellerAddress");
                });
#pragma warning restore 612, 618
        }
    }
}
