using Microsoft.EntityFrameworkCore;

namespace Meat_Store.Models
{
    public partial class ShopContext : DbContext
    {
        public ShopContext()
        {
        }

        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Delivery> Deliveries { get; set; } = null!;
        public virtual DbSet<FavoutirePosition> FavoutirePositions { get; set; } = null!;
        public virtual DbSet<Meat> Meats { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<ShopCartItem> ShopCartItems { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Receive> Receives { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server = DESKTOP-1B5F523; Database=Shop; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .HasColumnName("category_name");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");
                entity.Property(e => e.Img)
                    .HasColumnName("img");
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.ToTable("delivery");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .HasColumnName("city");

                entity.Property(e => e.DeliveryType)
                    .HasColumnName("delivery_type_id");

                entity.Property(e => e.OrderTime).HasColumnName("order_time");
            });

            modelBuilder.Entity<FavoutirePosition>(entity =>
            {
                entity.ToTable("favoutire_position");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MeatId).HasColumnName("meat_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Meat)
                    .WithMany(p => p.FavoutirePositions)
                    .HasForeignKey(d => d.MeatId)
                    .HasConstraintName("FK_favourite_position_meat");
            });

            modelBuilder.Entity<Meat>(entity =>
            {
                entity.ToTable("meat");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Img).HasColumnName("img");

                entity.Property(e => e.LongDesc)
                    .HasMaxLength(200)
                    .HasColumnName("long_desc");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Portion).HasColumnName("portion");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ShortDesc)
                    .HasMaxLength(100)
                    .HasColumnName("short_desc");

                entity.Property(e => e.Error_msg)
                    .HasMaxLength(30)
                    .HasColumnName("error_msg");

                entity.Property(e => e.SizeOfPortion).HasColumnName("size_of_portion");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Meats)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_meat_category");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("ordering");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.OrderTime).HasColumnName("order_time");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .HasColumnName("surname");

                entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("user_id");

                entity.HasOne(d => d.Delivery)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DeliveryId)
                    .HasConstraintName("FK_ordering_delivery");

            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("order_detail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MeatId).HasColumnName("meat_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.Meat)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.MeatId)
                    .HasConstraintName("FK_order_detail_meat");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_ordering_detail_order");
            });

            modelBuilder.Entity<ShopCartItem>(entity =>
            {
                entity.ToTable("shop_cart_item");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MeatId).HasColumnName("meat_id");

                entity.Property(e => e.ShopCartId).HasColumnName("shop_cart_id");
                
                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("buyer");

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
            modelBuilder.Entity<Receive>(entity =>
            {
                entity.ToTable("delivery_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Delivery_servise)
                    .HasMaxLength(100)
                    .HasColumnName("delivery_servise");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
