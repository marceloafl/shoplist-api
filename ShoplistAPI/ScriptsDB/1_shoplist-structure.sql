CREATE TABLE shoplist (
    id INTEGER IDENTITY PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description TEXT
)

CREATE TABLE product (
    id INTEGER IDENTITY PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    brand VARCHAR(255),
    description TEXT,
    number INTEGER NOT NULL,
    shoplistId INTEGER,
    FOREIGN KEY (shoplistId)
     REFERENCES shoplist (id)
)


