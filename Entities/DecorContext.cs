using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace intern_prj.Entities;

public partial class DecorContext : IdentityDbContext<ApplicationUser>
{

    public DecorContext(DbContextOptions<DecorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RefreshTokenEntity> RefreshTokenEntities { get; set; }
    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<FeedBack> FeedBacks { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<ItemCart> ItemCarts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-TTSGQT3;Initial Catalog=Decor;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3213E83F6AAF5318");

            entity.ToTable("Cart");

            entity.HasIndex(e => e.UserId, "UQ__Cart__CB9A1CDEA0CA1EA7").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateDate).HasColumnName("createDate");
            entity.Property(e => e.ProductTypeQuan).HasColumnName("productTypeQuan");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.User).WithOne(p => p.Cart)
                .HasForeignKey<Cart>(d => d.UserId)
                .HasConstraintName("FK__Cart__userID__4BAC3F29");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3213E83F75780C5D");

            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<FeedBack>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__feedBack__3213E83FDEECB115");

            entity.ToTable("feedBack");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateDate).HasColumnName("createDate");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.roleName).HasColumnName("roleName");
            entity.Property(e => e.OrderId).HasColumnName("orderID");

            entity.HasOne(d => d.Order).WithMany(p => p.FeedBacks)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__feedBack__orderI__4CA06362");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__image__3213E83FB2657157");

            entity.ToTable("image");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImageUrl)
                .IsUnicode(false)
                .HasColumnName("imageUrl");
            entity.Property(e => e.ProductId).HasColumnName("productID");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__image__productID__4D94879B");
        });

        modelBuilder.Entity<ItemCart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ItemCart__3213E83FEED18C83");

            entity.ToTable("ItemCart");

            entity.HasIndex(e => e.ProductId, "UQ__ItemCart__2D10D14BDBBF2276").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CartId).HasColumnName("cartID");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("productID");

            entity.HasOne(d => d.Cart).WithMany(p => p.ItemCarts)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK__ItemCart__cartID__4E88ABD4")
                .OnDelete(DeleteBehavior.SetNull); // khi Cart bị xóa, CartId trong ItemCart sẽ bị set null

        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3213E83FA60FD569");

            entity.ToTable("Order");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderDate).HasColumnName("orderDate");
            entity.Property(e => e.OrderCode).HasColumnName("orderCode");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("paymentMethod");
            entity.Property(e => e.ShippingAddress)
                .HasMaxLength(255)
                .HasColumnName("shippingAddress");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.TotalAmount).HasColumnName("totalAmount");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Order_user");
            entity.HasMany(o => o.OrderDetails)
                    .WithOne(od => od.Order)
                    .HasForeignKey(od => od.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3213E83FD710BB0B");

            entity.ToTable("OrderDetail");

            entity.HasIndex(e => new { e.OrderId, e.ProductId }).IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderId).HasColumnName("orderID");
            entity.Property(e => e.UnitQuantity).HasColumnName("unitQuantity");

            entity.Property(e => e.UnitPrice)
                  .HasColumnType("decimal(18,2)");
            entity.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3213E83F3C8C6BC7");

            entity.ToTable("Product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("categoryID");
            entity.Property(e => e.CreateDate).HasColumnName("createDate");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Manufacturer).HasColumnName("Manufacturer");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.OriginalPrice)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("originalPrice");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Size)
                .HasMaxLength(50)
                .HasColumnName("size");
            entity.Property(e => e.SoldedCount)
                .HasDefaultValue(0)
                .HasColumnName("soldedCount");
            entity.Property(e => e.UpdateDate).HasColumnName("updateDate");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Product__categor__534D60F1");
            entity.HasMany(p => p.ItemCarts)
                  .WithOne(i => i.Product)
                  .HasForeignKey(i => i.ProductId)
                  .OnDelete(DeleteBehavior.Cascade); 
            entity.HasMany(p => p.OrderDetails)           
                .WithOne(od => od.Product)              
                .HasForeignKey(od => od.ProductId)      
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Cấu hình cho ApplicationUser
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasKey(e => e.Id); // Đảm bảo khóa chính được xác định
            entity.ToTable("AspNetUsers"); // Đảm bảo tên bảng khớp với tên trong cơ sở dữ liệu
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
