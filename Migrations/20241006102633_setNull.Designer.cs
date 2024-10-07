﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using intern_prj.Entities;

#nullable disable

namespace intern_prj.Migrations
{
    [DbContext(typeof(DecorContext))]
    [Migration("20241006102633_setNull")]
    partial class setNull
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("intern_prj.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("intern_prj.Entities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("CreateDate")
                        .HasColumnType("date")
                        .HasColumnName("createDate");

                    b.Property<int>("ProductTypeQuan")
                        .HasColumnType("int")
                        .HasColumnName("productTypeQuan");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("userID");

                    b.HasKey("Id")
                        .HasName("PK__Cart__3213E83F6AAF5318");

                    b.HasIndex(new[] { "UserId" }, "UQ__Cart__CB9A1CDEA0CA1EA7")
                        .IsUnique()
                        .HasFilter("[userID] IS NOT NULL");

                    b.ToTable("Cart", (string)null);
                });

            modelBuilder.Entity("intern_prj.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("PK__Category__3213E83F75780C5D");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("intern_prj.Entities.FeedBack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("CreateDate")
                        .HasColumnType("date")
                        .HasColumnName("createDate");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("orderID");

                    b.HasKey("Id")
                        .HasName("PK__feedBack__3213E83FDEECB115");

                    b.HasIndex("OrderId");

                    b.ToTable("feedBack", (string)null);
                });

            modelBuilder.Entity("intern_prj.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("imageUrl");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("productID");

                    b.HasKey("Id")
                        .HasName("PK__image__3213E83FB2657157");

                    b.HasIndex("ProductId");

                    b.ToTable("image", (string)null);
                });

            modelBuilder.Entity("intern_prj.Entities.ItemCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CartId")
                        .HasColumnType("int")
                        .HasColumnName("cartID");

                    b.Property<int?>("OrderDetailId")
                        .HasColumnType("int")
                        .HasColumnName("orderDetailID");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 0)")
                        .HasColumnName("price");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("productID");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__ItemCart__3213E83FEED18C83");

                    b.HasIndex("CartId");

                    b.HasIndex("OrderDetailId");

                    b.HasIndex(new[] { "ProductId" }, "UQ__ItemCart__2D10D14BDBBF2276")
                        .IsUnique()
                        .HasFilter("[productID] IS NOT NULL");

                    b.ToTable("ItemCart", (string)null);
                });

            modelBuilder.Entity("intern_prj.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("OrderDate")
                        .HasColumnType("date")
                        .HasColumnName("orderDate");

                    b.Property<string>("PaymentMethod")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("paymentMethod");

                    b.Property<string>("ShippingAddress")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("shippingAddress");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("pending")
                        .HasColumnName("status");

                    b.Property<int?>("TotalAmount")
                        .HasColumnType("int")
                        .HasColumnName("totalAmount");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("userID");

                    b.HasKey("Id")
                        .HasName("PK__Order__3213E83FA60FD569");

                    b.HasIndex("UserId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("intern_prj.Entities.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("orderID");

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("decimal(18, 0)")
                        .HasColumnName("unitPrice");

                    b.Property<int?>("UnitQuantity")
                        .HasColumnType("int")
                        .HasColumnName("unitQuantity");

                    b.HasKey("Id")
                        .HasName("PK__OrderDet__3213E83FD710BB0B");

                    b.HasIndex(new[] { "OrderId" }, "UQ__OrderDet__0809337CE7E3ABEC")
                        .IsUnique();

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("intern_prj.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("categoryID");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("createDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<decimal>("OriginalPrice")
                        .HasColumnType("decimal(18, 0)")
                        .HasColumnName("originalPrice");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 0)")
                        .HasColumnName("price");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint")
                        .HasColumnName("quantity");

                    b.Property<string>("Size")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("size");

                    b.Property<int?>("SoledCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("soledCount");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updateDate");

                    b.HasKey("Id")
                        .HasName("PK__Product__3213E83F3C8C6BC7");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("intern_prj.Entities.RefreshTokenEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpiredAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("IssuedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("JwtId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokenEntities");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("intern_prj.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("intern_prj.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("intern_prj.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("intern_prj.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("intern_prj.Entities.Cart", b =>
                {
                    b.HasOne("intern_prj.Entities.ApplicationUser", "User")
                        .WithOne("Cart")
                        .HasForeignKey("intern_prj.Entities.Cart", "UserId")
                        .HasConstraintName("FK__Cart__userID__4BAC3F29");

                    b.Navigation("User");
                });

            modelBuilder.Entity("intern_prj.Entities.FeedBack", b =>
                {
                    b.HasOne("intern_prj.Entities.Order", "Order")
                        .WithMany("FeedBacks")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__feedBack__orderI__4CA06362");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("intern_prj.Entities.Image", b =>
                {
                    b.HasOne("intern_prj.Entities.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__image__productID__4D94879B");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("intern_prj.Entities.ItemCart", b =>
                {
                    b.HasOne("intern_prj.Entities.Cart", "Cart")
                        .WithMany("ItemCarts")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK__ItemCart__cartID__4E88ABD4");

                    b.HasOne("intern_prj.Entities.OrderDetail", "OrderDetail")
                        .WithMany("ItemCarts")
                        .HasForeignKey("OrderDetailId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_ItemCart_OrderDetail");

                    b.HasOne("intern_prj.Entities.Product", "Product")
                        .WithMany("ItemCarts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Cart");

                    b.Navigation("OrderDetail");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("intern_prj.Entities.Order", b =>
                {
                    b.HasOne("intern_prj.Entities.ApplicationUser", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Order_user");

                    b.Navigation("User");
                });

            modelBuilder.Entity("intern_prj.Entities.OrderDetail", b =>
                {
                    b.HasOne("intern_prj.Entities.Order", "Order")
                        .WithOne("OrderDetail")
                        .HasForeignKey("intern_prj.Entities.OrderDetail", "OrderId")
                        .IsRequired()
                        .HasConstraintName("FK__OrderDeta__order__52593CB8");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("intern_prj.Entities.Product", b =>
                {
                    b.HasOne("intern_prj.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK__Product__categor__534D60F1");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("intern_prj.Entities.RefreshTokenEntity", b =>
                {
                    b.HasOne("intern_prj.Entities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("intern_prj.Entities.ApplicationUser", b =>
                {
                    b.Navigation("Cart");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("intern_prj.Entities.Cart", b =>
                {
                    b.Navigation("ItemCarts");
                });

            modelBuilder.Entity("intern_prj.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("intern_prj.Entities.Order", b =>
                {
                    b.Navigation("FeedBacks");

                    b.Navigation("OrderDetail");
                });

            modelBuilder.Entity("intern_prj.Entities.OrderDetail", b =>
                {
                    b.Navigation("ItemCarts");
                });

            modelBuilder.Entity("intern_prj.Entities.Product", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("ItemCarts");
                });
#pragma warning restore 612, 618
        }
    }
}
