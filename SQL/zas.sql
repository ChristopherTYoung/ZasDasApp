CREATE TABLE category (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    category_name VARCHAR(50) not null,
    description VARCHAR(256)
);

INSERT INTO zasanddas.category(category_name)
VALUES ('2L Drink');

CREATE TABLE stock_item (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    item_name VARCHAR(50) not null,
    description VARCHAR(256),
    base_price decimal NOT NULL,
    item_category_id INT REFERENCES category(id) NOT NULL
);

INSERT INTO zasanddas.stock_item(item_name, base_price_id, item_category_id)
VALUES ('Coke', 2, 1);

CREATE TABLE sauce (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    sauce_name VARCHAR(50) not null
);

CREATE TABLE pizza_size (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    size_name VARCHAR(10) not null, 
    price decimal NOT NULL
);

CREATE TABLE pizza_base (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    pizza_name VARCHAR(50) not null,
    description VARCHAR(256),
    base_price decimal NOT null,
    image_path varchar(256)
);

INSERT INTO zasanddas.pizza_base (pizza_name, base_price_id) values ('Test', 4);

CREATE TABLE p_addin (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    addin_name VARCHAR(50) not null,
    base_price decimal NOT null
);

CREATE TABLE pizza (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    size_id INT REFERENCES pizza_size(id) not null,
    base_id INT REFERENCES pizza_base(id) not null,
    cooked_at_home BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE pizza_addin (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    pizza_id INT REFERENCES pizza(id) not null,
    addin_id INT REFERENCES p_addin(id) not null,
    addin_quantity INT default(1)
);

CREATE TABLE drink_base (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    drink_name VARCHAR(10) not null,
    description VARCHAR(256),
    base_price decimal NOT NULL
);

CREATE TABLE d_addin (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    addin_name VARCHAR(50) not null,
    base_price decimal NOT NULL
);

CREATE TABLE drink (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    base_id INT REFERENCES drink_base(id) not null
);

CREATE TABLE drink_addin (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    drink_id INT REFERENCES drink(id) not null,
    addin_id INT REFERENCES d_addin(id) not null
);

CREATE TABLE calzone (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    base_price decimal NOT NULL,
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
    base_price_id decimal not null
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
	phone varchar(50),
	api_key varchar(50) NOT NULL
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

INSERT INTO zasanddas.pizza_base (pizza_name, base_price)
VALUES 
    ('Chicken Pesto', 14.99),
    ('Chicken Bacon Ranch', 13.99),
    ('Combo', 15.99),
    ('Hawaiian', 13.99),
    ('BBQ Chicken', 14.99),
    ('Buffalo Chicken', 14.99),
    ('Dragonscale Pepperoni', 14.99),
    ('Biscuits and Gravy', 13.99),
    ('Cheese', 9.99),
    ('Pepperoni', 11.99),
    ('Chicken Bacon Artichoke', 14.99),
    ('Sweet Chili Hawaiian', 14.99),
    ('Mediterranean', 14.99),
    ('Fun Guy', 14.99),
    ('Hog Heaven', 14.99),
    ('All Meat', 15.99),
    ('Honey Badger', 14.99),
    ('BLT', 12.99),
    ('Margarita', 12.99),
    ('Vegetarian', 14.99),
    ('Choose your own', 10.99),
    ('The Za', 16.99),
    ('Ninja Turtle', 12.99);

