using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Meat_Store.sakila
{
    public partial class shopContext : DbContext
    {
        public shopContext()
        {
        }

        public shopContext(DbContextOptions<shopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Delivery> Deliveries { get; set; } = null!;
        public virtual DbSet<FavouritePosition> FavouritePositions { get; set; } = null!;
        public virtual DbSet<Meat> Meats { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<ShopCartItem> ShopCartItems { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=127.0.0.1;user=root;password=nbuh2013;database=shop", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("category_name");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.ToTable("delivery");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.DeliveryService)
                    .HasMaxLength(50)
                    .HasColumnName("delivery_service");

                entity.Property(e => e.DeliveryType)
                    .HasMaxLength(50)
                    .HasColumnName("delivery_type");
            });

            modelBuilder.Entity<FavouritePosition>(entity =>
            {
                entity.ToTable("favourite_positions");

                entity.HasIndex(e => e.MeatId, "meat_id_idx");

                entity.HasIndex(e => e.UserId, "user_id_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MeatId).HasColumnName("meat_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Meat)
                    .WithMany(p => p.FavouritePositions)
                    .HasForeignKey(d => d.MeatId)
                    .HasConstraintName("favourite_meat_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FavouritePositions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("user_id");
            });

            modelBuilder.Entity<Meat>(entity =>
            {
                entity.ToTable("meat");

                entity.HasIndex(e => e.CategoryId, "category_id_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Img)
                    .HasMaxLength(1000)
                    .HasColumnName("img");

                entity.Property(e => e.LongDesc)
                    .HasMaxLength(1000)
                    .HasColumnName("long_desc");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ShortDesc)
                    .HasMaxLength(100)
                    .HasColumnName("short_desc");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Meats)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("category_id");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.HasIndex(e => e.DeliveryId, "delivery_id_idx");

                entity.HasIndex(e => e.UserId, "user_id_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.OrderTime)
                    .HasColumnType("datetime(2)")
                    .HasColumnName("order_time");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .HasColumnName("surname");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Delivery)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DeliveryId)
                    .HasConstraintName("delivery_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("user_id_order");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("order_detail");

                entity.HasIndex(e => e.MeatId, "meat_id_idx");

                entity.HasIndex(e => e.OrderId, "order_id_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MeatId).HasColumnName("meat_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.Meat)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.MeatId)
                    .HasConstraintName("meat_order_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("order_id");
            });

            modelBuilder.Entity<ShopCartItem>(entity =>
            {
                entity.ToTable("shop_cart_items");

                entity.HasIndex(e => e.MeatId, "meat_id_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MeatId).HasColumnName("meat_id");

                entity.Property(e => e.ShopCartId)
                    .HasMaxLength(100)
                    .HasColumnName("shop_cart_id");

                entity.HasOne(d => d.Meat)
                    .WithMany(p => p.ShopCartItems)
                    .HasForeignKey(d => d.MeatId)
                    .HasConstraintName("meat_position_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.ShopCartId, "shop_cart_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .HasColumnName("phone_number");

                entity.Property(e => e.ShopCartId)
                    .HasMaxLength(100)
                    .HasColumnName("shop_cart_id");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .HasColumnName("surname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
