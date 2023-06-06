-----------------Shopping Cart SQL Server------------------------

CREATE TABLE Products (
  id INT PRIMARY KEY IDENTITY(1,1),
  name NVARCHAR(255) NOT NULL,
  slug NVARCHAR(255) NOT NULL,
  description NVARCHAR(MAX),
  metaDescription NVARCHAR(255) NOT NULL,
  metaKeywords NVARCHAR(255),
  sku NVARCHAR(255) NOT NULL,
  model NVARCHAR(255) NOT NULL,
  price DECIMAL(18,2) NOT NULL,
  oldPrice DECIMAL(18,2) NOT NULL,
  imageUrl NVARCHAR(MAX) NOT NULL,
  isBestseller BIT DEFAULT 0,
  isFeatured BIT DEFAULT 0,
  quantity INT NOT NULL,
  productStatus NVARCHAR(10) DEFAULT 'active' CHECK (productStatus IN ('active', 'inactive')),
  isDeleted BIT DEFAULT 0,
  createdAt DATETIME2 NOT NULL DEFAULT GETDATE(),
  updatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

----------------------------------------------------------

CREATE TABLE Categories (
id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
name VARCHAR(255) NOT NULL,
slug VARCHAR(255) NOT NULL,
description VARCHAR(MAX) NOT NULL,
metaDescription VARCHAR(MAX),
metaKeywords VARCHAR(MAX),
categoryStatus VARCHAR(20) DEFAULT 'active' CHECK(categoryStatus IN ('active', 'inactive')),
isDeleted BIT DEFAULT 0,
createdAt DATETIME NOT NULL,
updatedAt DATETIME NOT NULL
);

CREATE TABLE ProductCategories (
  id INT PRIMARY KEY IDENTITY(1,1),
  productId INT FOREIGN KEY REFERENCES Products(id) ON DELETE CASCADE ON UPDATE CASCADE,
  categoryId INT FOREIGN KEY REFERENCES Categories(id) ON DELETE CASCADE ON UPDATE CASCADE,
  createdAt DATETIME2 NOT NULL DEFAULT GETDATE(),
  updatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

-------------------------------------------------------------------

CREATE TABLE Addresses (
  id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  name NVARCHAR(255) NOT NULL,
  addressLine1 NVARCHAR(255) NOT NULL,
  addressLine2 NVARCHAR(255),
  city NVARCHAR(255) NOT NULL,
  state NVARCHAR(255) NOT NULL,
  country NVARCHAR(255) NOT NULL,
  zipCode NVARCHAR(20) NOT NULL,
  addressType NVARCHAR(20) NOT NULL CHECK (addressType IN ('Delivery', 'Billing', 'Unknown')) DEFAULT 'Unknown',
  isDeleted BIT NOT NULL DEFAULT 0,
  createdAt DATETIME2(0) NOT NULL DEFAULT GETUTCDATE(),
  updatedAt DATETIME2(0) NOT NULL DEFAULT GETUTCDATE()
);


-----------------------------------------------------------------------------

CREATE TABLE Brands (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  name NVARCHAR(255) NOT NULL,
  slug NVARCHAR(255) NOT NULL,
  description NVARCHAR(MAX),
  metaDescription NVARCHAR(MAX),
  metaKeywords NVARCHAR(MAX),
  brandStatus NVARCHAR(10) NOT NULL CHECK (brandStatus IN ('active', 'inactive')) DEFAULT 'active',
  isDeleted BIT NOT NULL DEFAULT 0,
  createdAt DATETIME2(0) NOT NULL DEFAULT GETUTCDATE(),
  updatedAt DATETIME2(0) NOT NULL DEFAULT GETUTCDATE()
);

CREATE TABLE ProductBrands (
  id INT PRIMARY KEY IDENTITY(1,1),
  productId INT FOREIGN KEY REFERENCES Products(id) ON DELETE CASCADE ON UPDATE CASCADE,
  brandId INT FOREIGN KEY REFERENCES Brands(id) ON DELETE CASCADE ON UPDATE CASCADE,
  createdAt DATETIME2 NOT NULL DEFAULT GETDATE(),
  updatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

-----------------------------------------------------------------------------

CREATE TABLE People (
  id INT PRIMARY KEY IDENTITY(1,1),
  firstName NVARCHAR(255) NOT NULL,
  middleName NVARCHAR(255),
  lastName NVARCHAR(255) NOT NULL,
  emailAddress NVARCHAR(255) NOT NULL,
  phoneNumber NVARCHAR(255),
  gender NVARCHAR(255),
  dateOfBirth DATE,
  isDeleted BIT NOT NULL DEFAULT 0,  
  createdAt DATETIME2 NOT NULL DEFAULT GETDATE(),
  updatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Customers (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  personId INT FOREIGN KEY REFERENCES People(id) ON DELETE CASCADE ON UPDATE CASCADE,
  isDeleted BIT NOT NULL DEFAULT 0, 
  createdAt DATETIME2(0) NOT NULL DEFAULT GETUTCDATE(),
  updatedAt DATETIME2(0) NOT NULL DEFAULT GETUTCDATE()
);

CREATE TABLE CustomerAddresses (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  customerId INT FOREIGN KEY REFERENCES Customers(id) ON DELETE CASCADE ON UPDATE CASCADE,
  addressId INT FOREIGN KEY REFERENCES Addresses(id) ON DELETE CASCADE ON UPDATE CASCADE,
  createdAt DATETIME2(0) NOT NULL DEFAULT GETUTCDATE(),
  updatedAt DATETIME2(0) NOT NULL DEFAULT GETUTCDATE()
);

--------------------------------------------------------------------

CREATE TABLE Orders (
  id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  orderTotal DECIMAL(10,2),
  orderItemTotal DECIMAL(10,2),
  shippingCharge DECIMAL(10,2),
  deliveryAddressId INT NOT NULL REFERENCES Addresses(id) ON DELETE CASCADE ON UPDATE CASCADE,
  customerId INT NOT NULL REFERENCES Customers(id) ON DELETE CASCADE ON UPDATE CASCADE,
  orderStatus NVARCHAR(20) NOT NULL CHECK (orderStatus IN ('Canceled', 'Submitted', 'Completed', 'Processing')) DEFAULT 'Submitted',
  isDeleted BIT NOT NULL DEFAULT 0,
  createdAt DATETIME2(0) NOT NULL DEFAULT GETUTCDATE(),
  updatedAt DATETIME2(0) NOT NULL DEFAULT GETUTCDATE()
);

CREATE TABLE OrderItems (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  quantity INT,
  price DECIMAL(10, 0),
  orderId INT FOREIGN KEY REFERENCES Orders(id) ON DELETE CASCADE ON UPDATE CASCADE,
  productId INT FOREIGN KEY REFERENCES Products(id) ON DELETE CASCADE ON UPDATE CASCADE,
  createdAt DATETIME2(0) NOT NULL DEFAULT GETUTCDATE(),
  updatedAt DATETIME2(0) NOT NULL DEFAULT GETUTCDATE()
);

------------------------------------------------------------------------------------

CREATE TABLE Carts (
  id INT PRIMARY KEY IDENTITY(1,1),
  uniqueCartId NVARCHAR(255) NOT NULL,
  cartStatus NVARCHAR(50) CHECK (cartStatus IN ('Open', 'CheckedOut')) DEFAULT 'Open',
  createdAt DATETIME NOT NULL DEFAULT GETDATE(),
  updatedAt DATETIME NOT NULL DEFAULT GETDATE()
)

CREATE TABLE CartItems (
  id INT PRIMARY KEY IDENTITY(1,1),
  quantity INT NOT NULL,
  cartId INT FOREIGN KEY REFERENCES Carts(id) ON DELETE CASCADE ON UPDATE CASCADE,
  productId INT FOREIGN KEY REFERENCES Products(id) ON DELETE CASCADE ON UPDATE CASCADE,
  createdAt DATETIME NOT NULL DEFAULT GETDATE(),
  updatedAt DATETIME NOT NULL DEFAULT GETDATE()
)









