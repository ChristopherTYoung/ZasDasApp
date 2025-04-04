﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ZasUndDas.Shared.Data;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CalzonAddin> CalzonAddins { get; set; }

    public virtual DbSet<Calzone> Calzones { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CheeseBread> CheeseBreads { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DAddin> DAddins { get; set; }

    public virtual DbSet<Drink> Drinks { get; set; }

    public virtual DbSet<DrinkAddin> DrinkAddins { get; set; }

    public virtual DbSet<DrinkBase> DrinkBases { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderPromotion> OrderPromotions { get; set; }

    public virtual DbSet<PAddin> PAddins { get; set; }

    public virtual DbSet<Pizza> Pizzas { get; set; }

    public virtual DbSet<PizzaAddin> PizzaAddins { get; set; }

    public virtual DbSet<PizzaBase> PizzaBases { get; set; }

    public virtual DbSet<PizzaOrder> PizzaOrders { get; set; }

    public virtual DbSet<PizzaSize> PizzaSizes { get; set; }

    public virtual DbSet<PricePerItem> PricePerItems { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Salad> Salads { get; set; }

    public virtual DbSet<SaladAddin> SaladAddins { get; set; }

    public virtual DbSet<Sauce> Sauces { get; set; }

    public virtual DbSet<StockItem> StockItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("pg_catalog", "azure")
            .HasPostgresExtension("pg_catalog", "pg_cron")
            .HasPostgresExtension("pg_catalog", "pgaadauth");

        modelBuilder.Entity<CalzonAddin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("calzon_addin_pkey");

            entity.ToTable("calzon_addin", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AddinId).HasColumnName("addin_id");
            entity.Property(e => e.CalzoneId).HasColumnName("calzone_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Addin).WithMany(p => p.CalzonAddins)
                .HasForeignKey(d => d.AddinId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("calzon_addin_addin_id_fkey");

            entity.HasOne(d => d.Calzone).WithMany(p => p.CalzonAddins)
                .HasForeignKey(d => d.CalzoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("calzon_addin_calzone_id_fkey");
        });

        modelBuilder.Entity<Calzone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("calzone_pkey");

            entity.ToTable("calzone", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.BasePriceId).HasColumnName("base_price_id");
            entity.Property(e => e.CookedAtHome)
                .HasDefaultValue(false)
                .HasColumnName("cooked_at_home");
            entity.Property(e => e.SauceId).HasColumnName("sauce_id");

            entity.HasOne(d => d.BasePrice).WithMany(p => p.Calzones)
                .HasForeignKey(d => d.BasePriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("calzone_base_price_id_fkey");

            entity.HasOne(d => d.Sauce).WithMany(p => p.Calzones)
                .HasForeignKey(d => d.SauceId)
                .HasConstraintName("calzone_sauce_id_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pkey");

            entity.ToTable("category", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("category_name");
            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .HasColumnName("description");
        });

        modelBuilder.Entity<CheeseBread>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cheese_bread_pkey");

            entity.ToTable("cheese_bread", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CookedAtHome)
                .HasDefaultValue(false)
                .HasColumnName("cooked_at_home");
            entity.Property(e => e.SizeId).HasColumnName("size_id");

            entity.HasOne(d => d.Size).WithMany(p => p.CheeseBreads)
                .HasForeignKey(d => d.SizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cheese_bread_size_id_fkey");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customer_pkey");

            entity.ToTable("customer", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .HasColumnName("customer_name");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<DAddin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("d_addin_pkey");

            entity.ToTable("d_addin", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AddinName)
                .HasMaxLength(50)
                .HasColumnName("addin_name");
            entity.Property(e => e.BasePriceId).HasColumnName("base_price_id");

            entity.HasOne(d => d.BasePrice).WithMany(p => p.DAddins)
                .HasForeignKey(d => d.BasePriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("d_addin_base_price_id_fkey");
        });

        modelBuilder.Entity<Drink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("drink_pkey");

            entity.ToTable("drink", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");

            entity.HasOne(d => d.Base).WithMany(p => p.Drinks)
                .HasForeignKey(d => d.BaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("drink_base_id_fkey");
        });

        modelBuilder.Entity<DrinkAddin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("drink_addin_pkey");

            entity.ToTable("drink_addin", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AddinId).HasColumnName("addin_id");
            entity.Property(e => e.DrinkId).HasColumnName("drink_id");

            entity.HasOne(d => d.Addin).WithMany(p => p.DrinkAddins)
                .HasForeignKey(d => d.AddinId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("drink_addin_addin_id_fkey");

            entity.HasOne(d => d.Drink).WithMany(p => p.DrinkAddins)
                .HasForeignKey(d => d.DrinkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("drink_addin_drink_id_fkey");
        });

        modelBuilder.Entity<DrinkBase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("drink_base_pkey");

            entity.ToTable("drink_base", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.BasePriceId).HasColumnName("base_price_id");
            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .HasColumnName("description");
            entity.Property(e => e.DrinkName)
                .HasMaxLength(10)
                .HasColumnName("drink_name");

            entity.HasOne(d => d.BasePrice).WithMany(p => p.DrinkBases)
                .HasForeignKey(d => d.BasePriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("drink_base_base_price_id_fkey");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_item_pkey");

            entity.ToTable("order_item", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CalzoneId).HasColumnName("calzone_id");
            entity.Property(e => e.CheeseBreadId).HasColumnName("cheese_bread_id");
            entity.Property(e => e.DrinkId).HasColumnName("drink_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PizzaId).HasColumnName("pizza_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.SaladId).HasColumnName("salad_id");
            entity.Property(e => e.StockItemId).HasColumnName("stock_item_id");

            entity.HasOne(d => d.Calzone).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.CalzoneId)
                .HasConstraintName("order_item_calzone_id_fkey");

            entity.HasOne(d => d.CheeseBread).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.CheeseBreadId)
                .HasConstraintName("order_item_cheese_bread_id_fkey");

            entity.HasOne(d => d.Drink).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.DrinkId)
                .HasConstraintName("order_item_drink_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_item_order_id_fkey");

            entity.HasOne(d => d.Pizza).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.PizzaId)
                .HasConstraintName("order_item_pizza_id_fkey");

            entity.HasOne(d => d.Salad).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.SaladId)
                .HasConstraintName("order_item_salad_id_fkey");

            entity.HasOne(d => d.StockItem).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.StockItemId)
                .HasConstraintName("order_item_stock_item_id_fkey");
        });

        modelBuilder.Entity<OrderPromotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_promotion_pkey");

            entity.ToTable("order_promotion", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.DollarAmountOff).HasColumnName("dollar_amount_off");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PromotionId).HasColumnName("promotion_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderPromotions)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_promotion_order_id_fkey");

            entity.HasOne(d => d.Promotion).WithMany(p => p.OrderPromotions)
                .HasForeignKey(d => d.PromotionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_promotion_promotion_id_fkey");
        });

        modelBuilder.Entity<PAddin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("p_addin_pkey");

            entity.ToTable("p_addin", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AddinName)
                .HasMaxLength(50)
                .HasColumnName("addin_name");
            entity.Property(e => e.BasePriceId).HasColumnName("base_price_id");

            entity.HasOne(d => d.BasePrice).WithMany(p => p.PAddins)
                .HasForeignKey(d => d.BasePriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("p_addin_base_price_id_fkey");
        });

        modelBuilder.Entity<Pizza>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pizza_pkey");

            entity.ToTable("pizza", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");
            entity.Property(e => e.CookedAtHome)
                .HasDefaultValue(false)
                .HasColumnName("cooked_at_home");
            entity.Property(e => e.SizeId).HasColumnName("size_id");

            entity.HasOne(d => d.Base).WithMany(p => p.Pizzas)
                .HasForeignKey(d => d.BaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pizza_base_id_fkey");

            entity.HasOne(d => d.Size).WithMany(p => p.Pizzas)
                .HasForeignKey(d => d.SizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pizza_size_id_fkey");
        });

        modelBuilder.Entity<PizzaAddin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pizza_addin_pkey");

            entity.ToTable("pizza_addin", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AddinId).HasColumnName("addin_id");
            entity.Property(e => e.AddinQuantity)
                .HasDefaultValue(1)
                .HasColumnName("addin_quantity");
            entity.Property(e => e.PizzaId).HasColumnName("pizza_id");

            entity.HasOne(d => d.Addin).WithMany(p => p.PizzaAddins)
                .HasForeignKey(d => d.AddinId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pizza_addin_addin_id_fkey");

            entity.HasOne(d => d._Pizza).WithMany(p => p.PizzaAddins)
                .HasForeignKey(d => d.PizzaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pizza_addin_pizza_id_fkey");
        });

        modelBuilder.Entity<PizzaBase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pizza_base_pkey");

            entity.ToTable("pizza_base", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.BasePriceId).HasColumnName("base_price_id");
            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .HasColumnName("description");
            entity.Property(e => e.PizzaName)
                .HasMaxLength(50)
                .HasColumnName("pizza_name");
            entity.Property(e => e.ImagePath)
                .HasColumnName("image_path")
                .HasMaxLength(256);

            entity.HasOne(d => d.BasePrice).WithMany(p => p.PizzaBases)
                .HasForeignKey(d => d.BasePriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pizza_base_base_price_id_fkey");
        });

        modelBuilder.Entity<PizzaOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pizza_order_pkey");

            entity.ToTable("pizza_order", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DateOrdered)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_ordered");
            entity.Property(e => e.DiscountAmount)
                .HasDefaultValueSql("0")
                .HasColumnName("discount_amount");
            entity.Property(e => e.GrossAmount).HasColumnName("gross_amount");
            entity.Property(e => e.NetAmount).HasColumnName("net_amount");
            entity.Property(e => e.SalesTax).HasColumnName("sales_tax");

            entity.HasOne(d => d.Customer).WithMany(p => p.PizzaOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("pizza_order_customer_id_fkey");
        });

        modelBuilder.Entity<PizzaSize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pizza_size_pkey");

            entity.ToTable("pizza_size", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.BasePriceId).HasColumnName("base_price_id");
            entity.Property(e => e.SizeName)
                .HasMaxLength(10)
                .HasColumnName("size_name");

            entity.HasOne(d => d.BasePrice).WithMany(p => p.PizzaSizes)
                .HasForeignKey(d => d.BasePriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pizza_size_base_price_id_fkey");
        });

        modelBuilder.Entity<PricePerItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("price_per_item_pkey");

            entity.ToTable("price_per_item", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("promotion_pkey");

            entity.ToTable("promotion", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.PromotionName)
                .HasMaxLength(50)
                .HasColumnName("promotion_name");
        });

        modelBuilder.Entity<Salad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("salad_pkey");

            entity.ToTable("salad", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.BasePriceId).HasColumnName("base_price_id");

            entity.HasOne(d => d.BasePrice).WithMany(p => p.Salads)
                .HasForeignKey(d => d.BasePriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("salad_base_price_id_fkey");
        });

        modelBuilder.Entity<SaladAddin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("salad_addin_pkey");

            entity.ToTable("salad_addin", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AddinId).HasColumnName("addin_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.SaladId).HasColumnName("salad_id");

            entity.HasOne(d => d.Addin).WithMany(p => p.SaladAddins)
                .HasForeignKey(d => d.AddinId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("salad_addin_addin_id_fkey");

            entity.HasOne(d => d.Salad).WithMany(p => p.SaladAddins)
                .HasForeignKey(d => d.SaladId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("salad_addin_salad_id_fkey");
        });

        modelBuilder.Entity<Sauce>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sauce_pkey");

            entity.ToTable("sauce", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.SauceName)
                .HasMaxLength(50)
                .HasColumnName("sauce_name");
        });

        modelBuilder.Entity<StockItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("stock_item_pkey");

            entity.ToTable("stock_item", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.BasePriceId).HasColumnName("base_price_id");
            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .HasColumnName("description");
            entity.Property(e => e.ItemCategoryId).HasColumnName("item_category_id");
            entity.Property(e => e.ItemName)
                .HasMaxLength(50)
                .HasColumnName("item_name");

            entity.HasOne(d => d.BasePrice).WithMany(p => p.StockItems)
                .HasForeignKey(d => d.BasePriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stock_item_base_price_id_fkey");

            entity.HasOne(d => d.ItemCategory).WithMany(p => p.StockItems)
                .HasForeignKey(d => d.ItemCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stock_item_item_category_id_fkey");
        });
        modelBuilder.HasSequence("jobid_seq", "cron");
        modelBuilder.HasSequence("runid_seq", "cron");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
