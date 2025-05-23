﻿using System;
using System.Collections.Generic;
using System.Xml;
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

    public virtual DbSet<CalzonAddin> CalzonAddins { set; get; }

    public virtual DbSet<Calzone> Calzones { set; get; }

    public virtual DbSet<Category> Categories { set; get; }

    public virtual DbSet<CheeseBread> CheeseBreads { set; get; }

    public virtual DbSet<CustomerDTO> Customers { set; get; }

    public virtual DbSet<DAddinDTO> DAddins { set; get; }

    public virtual DbSet<Drink> Drinks { set; get; }

    public virtual DbSet<DrinkAddin> DrinkAddins { set; get; }

    public virtual DbSet<DrinkBaseDTO> DrinkBases { set; get; }

    public virtual DbSet<OrderItem> OrderItems { set; get; }

    public virtual DbSet<OrderPromotion> OrderPromotions { set; get; }

    public virtual DbSet<PAddinDTO> PAddins { set; get; }

    public virtual DbSet<Pizza> Pizzas { set; get; }

    public virtual DbSet<PizzaAddin> PizzaAddins { set; get; }

    public virtual DbSet<PizzaBaseDTO> PizzaBases { set; get; }

    public virtual DbSet<PizzaOrder> PizzaOrders { set; get; }

    public virtual DbSet<PizzaSize> PizzaSizes { set; get; }

    public virtual DbSet<Promotion> Promotions { set; get; }

    public virtual DbSet<Salad> Salads { set; get; }

    public virtual DbSet<SaladAddin> SaladAddins { set; get; }

    public virtual DbSet<Sauce> Sauces { set; get; }

    public virtual DbSet<StockItemDTO> StockItems { set; get; }

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
            entity.Ignore(e => e.IsChecked);
        });

        modelBuilder.Entity<Calzone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("calzone_pkey");

            entity.ToTable("calzone", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Price).HasColumnName("base_price");
            entity.Property(e => e.CookedAtHome)
                .HasDefaultValue(false)
                .HasColumnName("cooked_at_home");
            entity.Property(e => e.SauceId).HasColumnName("sauce_id");
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
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<CustomerDTO>(entity =>
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
            entity.Property(e => e.ApiKey)
                .HasMaxLength(50)
                .HasColumnName("api_key");
            entity.Property(e => e.HashedPass)
                .HasMaxLength(50)
                .HasColumnName("hashed_pass");
        });

        modelBuilder.Entity<DAddinDTO>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("d_addin_pkey");

            entity.ToTable("d_addin", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AddinName)
                .HasMaxLength(50)
                .HasColumnName("addin_name");
            entity.Property(d => d.Price)
                .HasColumnName("base_price");
            entity.Ignore(e => e.IsChecked);
        });

        modelBuilder.Entity<Drink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("drink_pkey");

            entity.ToTable("drink", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.BaseId).HasColumnName("base_id");

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
        });

        modelBuilder.Entity<DrinkBaseDTO>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("drink_base_pkey");

            entity.ToTable("drink_base", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Price).HasColumnName("base_price");
            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .HasColumnName("drink_name");
            entity.Ignore(e => e.ImagePath);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_item_pkey");

            entity.ToTable("order_item", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CalzoneId).HasColumnName("calzone_id");
            entity.Property(e => e.CheeseBreadId).HasColumnName("cheese_bread_id");
            entity.Property(e => e.DrinkId).HasColumnName("drink_id");
            entity.Property(e => e.PizzaId).HasColumnName("pizza_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.SaladId).HasColumnName("salad_id");
            entity.Property(e => e.StockItemId).HasColumnName("stock_item_id");

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
        });

        modelBuilder.Entity<PAddinDTO>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("p_addin_pkey");

            entity.ToTable("p_addin", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AddinName)
                .HasMaxLength(50)
                .HasColumnName("addin_name");
            entity.Property(e => e.Price).HasColumnName("base_price");
            entity.Ignore(e => e.IsChecked);

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
        });

        modelBuilder.Entity<PizzaBaseDTO>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pizza_base_pkey");

            entity.ToTable("pizza_base", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Price).HasColumnName("base_price");
            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("pizza_name");
            entity.Property(e => e.ImagePath)
                .HasColumnName("image_path")
                .HasDefaultValue("https://zasanddasstorage.blob.core.windows.net/menuitemimages/pizza.png")
                .HasMaxLength(256);

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

        });

        modelBuilder.Entity<PizzaSize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pizza_size_pkey");

            entity.ToTable("pizza_size", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.SizeName)
                .HasMaxLength(10)
                .HasColumnName("size_name");
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
            entity.Property(e => e.BasePrice).HasColumnName("base_price");
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

        modelBuilder.Entity<StockItemDTO>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("stock_item_pkey");

            entity.ToTable("stock_item", "zasanddas");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Price).HasColumnName("base_price");
            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .HasColumnName("description");
            entity.Property(e => e.ItemCategoryId).HasColumnName("item_category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("item_name");
            entity.Ignore(e => e.Quantity);
            entity.Ignore(e => e.ImagePath);

        });
        modelBuilder.HasSequence("jobid_seq", "cron");
        modelBuilder.HasSequence("runid_seq", "cron");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
