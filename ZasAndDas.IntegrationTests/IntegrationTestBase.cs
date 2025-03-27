using ZasAndDasWeb.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Testcontainers.PostgreSql;
using Xunit.Abstractions;

namespace ZasAndDas.IntegrationTests
{
    public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
    {
        public PostgreSqlContainer _dbContainer;
        public ITestOutputHelper _outputHelper;
        public WebApplicationFactory<Program> _app;
        public IntegrationTestBase(WebApplicationFactory<Program> app, ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres")
            .WithPassword("Strong_password_123!")
            .Build();

            _app = app.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll<DbContextOptions>();
                    services.RemoveAll<PostgresContext>();
                    services.RemoveAll(typeof(DbContextOptions<PostgresContext>));
                    services.AddDbContext<PostgresContext>(options => options.UseNpgsql(_dbContainer.GetConnectionString()));//Expect this line to give you an error for now...
                });
                builder.ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddProvider(new XUnitLoggerProvider(_outputHelper));
                    logging.SetMinimumLevel(LogLevel.Information);
                });
            });
        }
        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            var InitScript = """"
            CREATE SCHEMA zasanddas;
            SET SEARCH_PATH TO zasanddas

            CREATE TABLE zasanddas.price_per_item (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                price decimal NOT NULL
            );

            INSERT INTO price_per_item (price) values (5.99);

            CREATE TABLE zasanddas.category (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                category_name VARCHAR(50) not null,
                description VARCHAR(256)
            );

            CREATE TABLE zasanddas.stock_item (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                item_name VARCHAR(50) not null,
                description VARCHAR(256),
                base_price_id INT REFERENCES price_per_item(id) NOT NULL,
                item_category_id INT REFERENCES category(id) NOT NULL
            );

            CREATE TABLE zasanddas.sauce (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                sauce_name VARCHAR(50) not null
            );

            CREATE TABLE zasanddas.pizza_size (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                size_name VARCHAR(10) not null, 
                base_price_id INT REFERENCES price_per_item(id) NOT NULL
            );

            CREATE TABLE zasanddas.pizza_base (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                pizza_name VARCHAR(50) not null,
                description VARCHAR(256),
                base_price_id INT REFERENCES price_per_item(id) NOT NULL
            );

            CREATE TABLE zasanddas.p_addin (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                addin_name VARCHAR(50) not null,
                base_price_id INT REFERENCES price_per_item(id) NOT NULL
            );

            CREATE TABLE zasanddas.pizza (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                size_id INT REFERENCES pizza_size(id) not null,
                base_id INT REFERENCES pizza_base(id) not null,
                cooked_at_home BOOLEAN NOT NULL DEFAULT FALSE
            );

            CREATE TABLE zasanddas.pizza_addin (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                pizza_id INT REFERENCES pizza(id) not null,
                addin_id INT REFERENCES p_addin(id) not null,
                addin_quantity INT default(1)
            );

            CREATE TABLE zasanddas.drink_base (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                drink_name VARCHAR(10) not null,
                description VARCHAR(256),
                base_price_id INT REFERENCES price_per_item(id) NOT NULL
            );

            CREATE TABLE zasanddas.d_addin (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                addin_name VARCHAR(50) not null,
                base_price_id INT REFERENCES price_per_item(id) NOT NULL
            );

            CREATE TABLE zasanddas.drink (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                base_id INT REFERENCES drink_base(id) not null
            );

            CREATE TABLE zasanddas.drink_addin (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                drink_id INT REFERENCES drink(id) not null,
                addin_id INT REFERENCES d_addin(id) not null
            );

            CREATE TABLE calzone (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                base_price_id INT REFERENCES price_per_item(id) NOT NULL,
                sauce_id INT REFERENCES sauce(id),
                cooked_at_home BOOLEAN default(False)
            );

            CREATE TABLE calzon_addin (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                calzone_id INT REFERENCES calzone(id) not null,
                addin_id INT REFERENCES p_addin(id) not null,
                quantity INT default(1)
            );

            CREATE TABLE salad (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                base_price_id INT REFERENCES price_per_item(id) not null
            );

            CREATE TABLE salad_addin (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                salad_id INT REFERENCES salad(id) not null,
                addin_id INT REFERENCES p_addin(id) not null,
                quantity INT default(1)
            );

            CREATE TABLE cheese_bread (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                size_id INT REFERENCES pizza_size(id) not null,
                cooked_at_home BOOLEAN default(False)
            );

            create table customer (
            	id int primary key generated always as identity, 
            	customer_name varchar(50) not null,
            	email varchar(50) not null,
            	phone varchar(50)
            );
            create table promotion (
            	id int primary key generated always as identity,
            	promotion_name varchar(50) not null
            );
            create table pizza_order (
            	id int primary key generated always as identity,
            	customer_id int references customer(id),
            	date_ordered timestamp,
            	gross_amount decimal,
            	discount_amount decimal default(0),
            	net_amount decimal,
            	sales_tax decimal
            );
            create table order_promotion (
            	id int primary key generated always as identity,
            	promotion_id int references promotion(id) not null,
            	order_id int references pizza_order(id) not null,
            	dollar_amount_off decimal
            );

            CREATE TABLE order_item (
                id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                order_id int references pizza_order(id) not null,
                quantity INT default(1),
                stock_item_id INT REFERENCES stock_item(id),
                pizza_id INT REFERENCES pizza(id),
                drink_id INT REFERENCES drink(id),
                calzone_id INT REFERENCES calzone(id),
                salad_id INT REFERENCES salad(id),
                cheese_bread_id INT REFERENCES cheese_bread(id),
                CHECK (
                ((stock_item_id IS NOT NULL)::int +
                 (pizza_id IS NOT NULL)::int +
                 (drink_id IS NOT NULL)::int +
                 (salad_id IS NOT NULL)::int +
                 (cheese_bread_id IS NOT NULL)::int +
                 (calzone_id is not null)::int = 1))
            );
            
            """";

            await _dbContainer.ExecScriptAsync(InitScript);
        }

        public async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}
