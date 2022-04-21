CREATE DATABASE ServisDb
go
use ServisDb
go
create table UserAccount
(
	UserAccountId INT PRIMARY KEY IDENTITY(1,1),
	FullName NVARCHAR(255),
	Email NVARCHAR(255),
	Password NVARCHAR(100),
	RecordDate DATETIME,
	UpdateDate DATETIME,
	IsDeleted BIT NOT NULL DEFAULT(0)
)
GO
INSERT INTO UserAccount(FullName,Email,Password,RecordDate ,UpdateDate ,IsDeleted) VALUES ('Yalýn Sonat Yüksel','yukselyal@gmail.com','1',GETDATE(),GETDATE(),0)
GO
CREATE PROC Sp_UserAccount_Add
(
	@UserAccountId INT OUTPUT,
	@FullName NVARCHAR(255),
	@Email NVARCHAR(255),
	@Password NVARCHAR(100),
	@RecordDate DATETIME,
	@UpdateDate DATETIME,
	@IsDeleted BIT
)
AS
INSERT INTO UserAccount(FullName,Email,Password,RecordDate,UpdateDate,IsDeleted)
values(@FullName,@Email,@Password,@RecordDate,@UpdateDate,@IsDeleted)
--Ýnsert iþleminden sonra id deðerini alýyoruz ve output parametre þeklinde istediðimiz deðere atýyoruz.
SELECT @UserAccountId=SCOPE_IDENTITY()
go
CREATE PROC Sp_UserAccount_Update
(
	@UserAccountId INT ,
	@FullName NVARCHAR(255),
	@Email NVARCHAR(255),
	@Password NVARCHAR(100),
	@RecordDate DATETIME,
	@UpdateDate DATETIME,
	@IsDeleted BIT
)
AS
UPDATE UserAccount SEt FullName=@FullName,Email=@Email,Password=@Password,RecordDate=@RecordDate,UpdateDate=@UpdateDate,IsDeleted=@IsDeleted
where UserAccountId=@UserAccountId
--Ýnsert iþleminden sonra id deðerini alýyoruz ve output parametre þeklinde istediðimiz deðere atýyoruz.

go
CREATE TABLE Customer
(
	CustomerId INT PRIMARY KEY IDENTITY(1,1),
	CustomerName NVARCHAR(50),
	Phone NVARCHAR(50),
	Country NVARCHAR(50),
	City NVARCHAR(50),
	County NVARCHAR(50),
	RecordDate DATETIME,
	UpdateDate DATETIME,
	IsDeleted BIT NOT NULL DEFAULT(0)
)
go
CREATE PROC Sp_Customer_Add
(
	@CustomerId INT OUTPUT,
	@CustomerName NVARCHAR(50),
	@Phone NVARCHAR(50),
	@Country NVARCHAR(50),
	@City NVARCHAR(50),
	@County NVARCHAR(50),
	@RecordDate DATETIME,
	@UpdateDate DATETIME,
	@IsDeleted BIT
)
AS
INSERT INTO Customer(CustomerName,Phone,Country,City,County,RecordDate,UpdateDate,IsDeleted)
values(@CustomerName,@Phone,@Country,@City,@County,@RecordDate,@UpdateDate,@IsDeleted)
--Ýnsert iþleminden sonra id deðerini alýyoruz ve output parametre þeklinde istediðimiz deðere atýyoruz.
SELECT @CustomerId=SCOPE_IDENTITY()
go
ALTER PROC Sp_Customer_Update
(
	@CustomerId INT,
	@CustomerName NVARCHAR(100),
	@Phone NVARCHAR(50),
	@Country NVARCHAR(50),
	@City NVARCHAR(50),
	@County NVARCHAR(50),
	@RecordDate DATETIME,
	@UpdateDate DATETIME,
	@IsDeleted BIT
)
AS
UPDATE Customer SET CustomerName=@CustomerName,Phone=@Phone,Country=@Country,City=@City,County=@County,RecordDate=@RecordDate,UpdateDate=@UpdateDate,IsDeleted=@IsDeleted
where CustomerId=@CustomerId
go
create table Category
(
	CategoryId INT PRIMARY KEY IDENTITY(1,1),
	CategoryName NVARCHAR(50),
	RecordDate DATETIME,
	UpdateDate DATETIME,
	IsDeleted BIT NOT NULL DEFAULT(0)
)
go
create table Product
(
	ProductId INT PRIMARY KEY IDENTITY(1,1),
	ProductName NVARCHAR(50),
	CategoryId INT,
	UnitPrice money,
	UnitInStock int,
	RecordDate DATETIME,
	UpdateDate DATETIME,
	IsDeleted BIT NOT NULL DEFAULT(0)
	FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
)
go
go
CREATE PROC Sp_Product_Add
(
	@ProductId INT OUTPUT,
	@ProductName NVARCHAR(50),
	@CategoryId INT,
	@UnitPrice money,
	@UnitInStock int,
	@RecordDate DATETIME,
	@UpdateDate DATETIME,
	@IsDeleted BIT
)
AS
INSERT INTO Product (ProductName,CategoryId,UnitPrice,UnitInStock,RecordDate,UpdateDate,IsDeleted) VALUES (@ProductName,@CategoryId,@UnitPrice,@UnitInStock,@RecordDate,@UpdateDate,@IsDeleted) 
SELECT ProductId=SCOPE_IDENTITY()
go
CREATE PROC Sp_Product_Update
(
	@ProductId INT ,
	@ProductName NVARCHAR(50),
	@CategoryId INT,
	@UnitPrice money,
	@UnitInStock int,
	@RecordDate DATETIME,
	@UpdateDate DATETIME,
	@IsDeleted BIT
)
AS
update Product set ProductName = @ProductName,CategoryId=@CategoryId,UnitPrice=@UnitPrice,UnitInStock=@UnitInStock,RecordDate=@RecordDate,UpdateDate=@UpdateDate,IsDeleted=@IsDeleted where ProductId=@ProductId
go
CREATE PROC Sp_Category_Add
(
	@CategoryId INT OUTPUT,
	@CategoryName NVARCHAR(50),
	@RecordDate DATETIME,
	@UpdateDate DATETIME,
	@IsDeleted BIT
)
AS
INSERT INTO Category (CategoryName,RecordDate,UpdateDate,IsDeleted) values(@CategoryName,@RecordDate,@UpdateDate,@IsDeleted)
select CategoryId=SCOPE_IDENTITY()
go
CREATE









