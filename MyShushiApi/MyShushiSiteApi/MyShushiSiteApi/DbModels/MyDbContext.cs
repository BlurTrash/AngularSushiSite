using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyShushiSiteApi.DbModels
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderLine> OrderLines { get; set; } = null!;
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=dev_my_shushi_site;Username=postgres;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.HasComment("Таблица разделов меню");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("'2023-05-06 16:31:07.647713'::timestamp without time zone");

                entity.Property(e => e.FullUrl)
                    .HasMaxLength(1000)
                    .HasColumnName("full_url");

                entity.Property(e => e.IconUrl)
                    .HasMaxLength(1000)
                    .HasColumnName("icon_url");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(1000)
                    .HasColumnName("image_url");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Url)
                    .HasMaxLength(1000)
                    .HasColumnName("url");
            });

            modelBuilder.Entity<DeliveryMethod>(entity =>
            {
                entity.ToTable("delivery_method");

                entity.HasComment("Способ доставки");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreateDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("items");

                entity.HasComment("Таблица товаров");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Desription)
                    .HasMaxLength(2000)
                    .HasColumnName("desription");

                entity.Property(e => e.FullUrl)
                    .HasMaxLength(1000)
                    .HasColumnName("full_url");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(1000)
                    .HasColumnName("image_url");

                entity.Property(e => e.Ingredients)
                    .HasMaxLength(1000)
                    .HasColumnName("ingredients");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.UrlName)
                    .HasMaxLength(100)
                    .HasColumnName("url_name");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("items_fk");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.HasComment("Таблица заказов");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Address)
                    .HasColumnType("character varying")
                    .HasColumnName("address");

                entity.Property(e => e.Comment)
                    .HasMaxLength(2000)
                    .HasColumnName("comment");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DeliveryMethodId).HasColumnName("delivery_method_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .HasColumnName("email");

                entity.Property(e => e.FullPrice).HasColumnName("full_price");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserName)
                    .HasMaxLength(200)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(100)
                    .HasColumnName("user_phone");

                entity.HasOne(d => d.DeliveryMethod)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DeliveryMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orders_fk1");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orders_fk");
            });

            modelBuilder.Entity<OrderLine>(entity =>
            {
                entity.ToTable("order_line");

                entity.HasComment("Позиции заказов");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreateDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UnitPrice).HasColumnName("unit_price");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OrderLines)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_line_fk1");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderLines)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_line_fk");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("order_status");

                entity.HasComment("Статус заказа");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreateDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payment");

                entity.HasComment("Сбособы оплаты");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreateDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
