-- ������������� �� ���� master ��� �������� ���� ������
USE master;
GO

-- ������� ���� ������ Shop
CREATE DATABASE Shop;
GO

-- ������������� �� ���� ������ Shop
USE Shop;
GO

-- ��������� ������� Customers
CREATE TABLE Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(20) NOT NULL,
    CONSTRAINT CK_Customer_Name_Length CHECK (LEN(Name) BETWEEN 2 AND 20),
    Email NVARCHAR(50) NOT NULL CONSTRAINT CK_Customer_Email_Format CHECK (Email LIKE '%@%.%')
);

-- ��������� ������� Products
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(20) NOT NULL,
    Price DECIMAL(18,2) NOT NULL CHECK (Price > 0.01 AND Price <= 100000),
    Description NVARCHAR(1024) NOT NULL CHECK (LEN(Description) BETWEEN 2 AND 1024)
);

-- ��������� ������� Orders
CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CreatedDate DATETIME NOT NULL,
    CustomerId INT NOT NULL,
    CONSTRAINT FK_Order_Customer FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);

-- ��������� ������� OrderProducts
CREATE TABLE OrderProducts (
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    CONSTRAINT PK_OrderProduct PRIMARY KEY (OrderId, ProductId),
    CONSTRAINT FK_OrderProduct_Order FOREIGN KEY (OrderId) REFERENCES Orders(Id),
    CONSTRAINT FK_OrderProduct_Product FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

-- ������� ������ � ������� Customers
INSERT INTO Customers (Name, Email) VALUES
('John Doe', 'johndoe@example.com'),
('Jane Smith', 'janesmith@example.com'),
('Alice Johnson', 'alicejohnson@domain.com'),
('Bob Brown', 'bobbrown@example.com'),
('Charlie Davis', 'charliedavis@domain.com');

-- ������� ������ � ������� Products
INSERT INTO Products (Name, Price, Description) VALUES
('Laptop', 1000.00, 'High-performance laptop for gaming and work.'),
('Smartphone', 500.00, 'Latest model smartphone with a high-quality camera.'),
('Headphones', 150.00, 'Noise-cancelling over-ear headphones for music lovers.'),
('Tablet', 300.00, 'Portable tablet for reading, web browsing, and entertainment.'),
('Smartwatch', 200.00, 'Wearable device to track fitness and notifications.');

-- ������� ������ � ������� Orders
INSERT INTO Orders (CreatedDate, CustomerId) VALUES
(GETDATE(), 1),  -- John Doe
(GETDATE(), 2),  -- Jane Smith
(GETDATE(), 3),  -- Alice Johnson
(GETDATE(), 4),  -- Bob Brown
(GETDATE(), 5);  -- Charlie Davis

-- ������� ������ � ������� OrderProducts
INSERT INTO OrderProducts (OrderId, ProductId, Quantity) VALUES
(1, 1, 1),  -- John Doe ������� 1 Laptop
(1, 2, 2),  -- John Doe ������� 2 Smartphones
(2, 3, 1),  -- Jane Smith �������� 1 Headphones
(2, 4, 1),  -- Jane Smith �������� 1 Tablet
(3, 5, 3),  -- Alice Johnson �������� 3 Smartwatches
(4, 2, 1),  -- Bob Brown ������� 1 Smartphone
(5, 1, 2),  -- Charlie Davis ������� 2 Laptops
(5, 3, 1);  -- Charlie Davis ������� 1 Headphones
GO

SELECT * FROM Customers;
GO
