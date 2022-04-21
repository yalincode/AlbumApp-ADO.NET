CREATE DATABASE AlbumDB
GO
USE AlbumDB
GO
CREATE TABLE Users
(
	UserId INT PRIMARY KEY IDENTITY(1, 1),
	UserName NVARCHAR(100) NOT NULL,
	UserLastName NVARCHAR(100),
	UserPhone NVARCHAR(100),
	UserEmail NVARCHAR(100) NOT NULL,
	UserPassword NVARCHAR(100) NOT NULL,
)
GO
CREATE TABLE Albums
(
	AlbumId INT PRIMARY KEY IDENTITY(1, 1),
	AlbumName NVARCHAR(100) NOT NULL,
	CategoryId INT NOT NULL,
	SingerId INT NOT NULL,
	UnitPrice DECIMAL(6,2) NOT NULL,
	Discount DECIMAL(6,2),
	AlbumAddDate DATETIME NOT NULL,
)
GO
CREATE TABLE Singers
(
	SingerId INT PRIMARY KEY IDENTITY(1, 1),
	SingerName NVARCHAR(100) NOT NULL,
	SingerDescription NVARCHAR(200),
	
)
GO
CREATE TABLE Categories
(
	CategoryId INT PRIMARY KEY IDENTITY(1, 1),
	CategoryName NVARCHAR(100) NOT NULL,
	CategoryDescription NVARCHAR(200)
)
GO
CREATE TABLE Orders
(
	OrderId INT PRIMARY KEY IDENTITY(1, 1),
	OrderDate DATETIME NOT NULL,
	Country NVARCHAR(100),
	City NVARCHAR(100),
	County NVARCHAR(100),
	UserId INT,
)
GO
CREATE TABLE OrderDetails
(
	OrderDetailId INT PRIMARY KEY IDENTITY(1, 1),
	OrderId INT NOT NULL,
	AlbumId INT NOT NULL,
	Quantity INT NOT NULL,
	UnitPrice DECIMAL(6,2) not null,
	Discount DECIMAL(6,2) ,
)
GO
ALTER TABLE OrderDetails
ADD FOREIGN KEY(OrderId) REFERENCES Orders (OrderId)
GO
ALTER TABLE OrderDetails
ADD FOREIGN KEY(AlbumId) REFERENCES Albums(AlbumId)
GO
ALTER TABLE Albums
ADD FOREIGN KEY(SingerId) REFERENCES Singers(SingerId)
GO
ALTER TABLE Albums
ADD FOREIGN KEY(CategoryId) REFERENCES Categories(CategoryId)
GO
ALTER TABLE Orders
ADD FOREIGN KEY(UserId) REFERENCES Users (UserId)
go
