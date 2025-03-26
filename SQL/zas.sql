CREATE TABLE price_per_item (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    price decimal NOT NULL
);

CREATE TABLE category (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    category_name VARCHAR(50),
    description VARCHAR(256)
);

CREATE TABLE stock_item (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    item_name VARCHAR(50),
    description VARCHAR(256),
    base_price_id INT REFERENCES price_per_item(id) NOT NULL,
    item_category_id INT REFERENCES category(id) NOT NULL
);

CREATE TABLE sauce (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    sauce_name VARCHAR(50)
);

CREATE TABLE pizza_size (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    size_name VARCHAR(10), 
    base_price_id INT REFERENCES price_per_item(id) NOT NULL
);

CREATE TABLE pizza_base (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    pizza_name VARCHAR(50),
    description VARCHAR(256),
    base_price_id INT REFERENCES price_per_item(id) NOT NULL
);

CREATE TABLE p_addin (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    addin_name VARCHAR(50),
    base_price_id INT REFERENCES price_per_item(id) NOT NULL
);

CREATE TABLE pizza (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    size_id INT REFERENCES pizza_size(id),
    base_id INT REFERENCES pizza_base(id),
    cooked_at_home BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE pizza_addin (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    pizza_id INT REFERENCES pizza(id),
    addin_id INT REFERENCES p_addin(id),
    addin_quantity INT
);

CREATE TABLE drink_base (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    drink_name VARCHAR(10),
    description VARCHAR(256),
    base_price_id INT REFERENCES price_per_item(id) NOT NULL
);

CREATE TABLE d_addin (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    addin_name VARCHAR(50),
    base_price_id INT REFERENCES price_per_item(id) NOT NULL
);

CREATE TABLE drink (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    base_id INT REFERENCES drink_base(id)
);

CREATE TABLE drink_addin (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    drink_id INT REFERENCES drink(id),
    addin_id INT REFERENCES d_addin(id)
);

CREATE TABLE calzone (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    base_price_id INT REFERENCES price_per_item(id) NOT NULL,
    sauce_id INT REFERENCES sauce(id),
    cooked_at_home BOOLEAN
);

CREATE TABLE calzon_addin (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    calzone_id INT REFERENCES calzone(id),
    addin_id INT REFERENCES p_addin(id),
    quantity INT
);

CREATE TABLE salad (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    base_price_id INT REFERENCES price_per_item(id)
);

CREATE TABLE salad_addin (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    salad_id INT REFERENCES salad(id),
    addin_id INT REFERENCES p_addin(id),
    quantity INT
);

CREATE TABLE cheese_bread (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    size_id INT REFERENCES pizza_size(id),
    cooked_at_home BOOLEAN
);

create table customer (
	id int primary key generated always as identity, 
	customer_name varchar(50),
	email varchar(50),
	phone varchar(50)
);
create table promotion (
	id int primary key generated always as identity,
	promotion_name varchar(50)
);
create table pizza_order (
	id int primary key generated always as identity,
	customer_id int references customer(id),
	date_ordered timestamp,
	gross_amount decimal,
	discount_amount decimal,
	net_amount decimal,
	sales_tax decimal
);
create table order_promotion (
	id int primary key generated always as identity,
	promotion_id int references promotion(id),
	order_id int references pizza_order(id),
	dollar_amount_off decimal
);

CREATE TABLE order_item (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    order_id int references pizza_order(id),
    quantity INT NOT NULL,
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
     (calzone_id is not null)::int = 1)
);

