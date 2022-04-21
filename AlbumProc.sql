use AlbumDB
go
create proc Sp_User_InsertUpdate
(
	@UserId INT=0,
	@UserName NVARCHAR(100),
	@UserLastName NVARCHAR(100),
	@UserPhone NVARCHAR(100),
	@UserEmail NVARCHAR(100),
	@UserPassword NVARCHAR(100)
)
as
begin
	IF(@UserId=0)
	BEGIN
		INSERT Users(UserName,UserLastName,UserPhone,UserEmail,UserPassword) VALUES (@UserName,@UserLastName,@UserPhone,@UserEmail,@UserPassword)
		SELECT @@IDENTITY AS ID
	END
	ELSE
	BEGIN
		UPDATE Users SET UserName=@UserName,UserLastName=@UserLastName,UserPhone=@UserPhone,UserEmail=@UserEmail,UserPassword=@UserPassword WHERE UserId=@UserId
		select @UserId as ID
	END
end
go
Create PROC Sp_User_Getir
(
	@UserId INT=NULL
)
AS
BEGIN
	SELECT * FROM Users where (@UserId=UserId or @UserId IS NULL)
END
go
create proc Sp_User_Delete
(
	@UserId INT
)
AS
BEGIN
	DELETE Users where UserId=@UserId
END
go
---------------------------------------------------------------SÝNGER
create proc Sp_Singer_InsertUpdate
(
	@SingerId INT=0,
	@SingerName NVARCHAR(100),
	@SingerDescription NVARCHAR(100)

)
as
begin
	IF(@SingerId=0)
	BEGIN
		INSERT Singers(SingerName,SingerDescription) VALUES (@SingerName,@SingerDescription)
		SELECT @@IDENTITY AS ID
	END
	ELSE
	BEGIN
		UPDATE Singers SET SingerName=@SingerName,SingerDescription=@SingerDescription WHERE SingerId=@SingerId
		select @SingerId as ID
	END
end
go
Create PROC Sp_Singer_Getir
(
	@SingerId INT=NULL
)
AS
BEGIN
	SELECT * FROM Singers where (@SingerId=SingerId or @SingerId IS NULL)
END
go
create proc Sp_Singer_Delete
(
	@SingerId INT
)
AS
BEGIN
	DELETE Singers where SingerId=@SingerId
END
go
-----------------------------------------------------------------------------KATEGORÝ
create proc Sp_Category_InsertUpdate
(
	@CategoryId INT=0,
	@CategoryName NVARCHAR(100),
	@CategoryDescription NVARCHAR(100)

)
as
begin
	IF(@CategoryId=0)
	BEGIN
		INSERT Categories(CategoryName,CategoryDescription) VALUES (@CategoryName,@CategoryDescription)
		SELECT @@IDENTITY AS ID
	END
	ELSE
	BEGIN
		UPDATE Categories SET CategoryName=@CategoryName,CategoryDescription=@CategoryDescription WHERE CategoryId=@CategoryId
		select @CategoryId as ID
	END
end
go
Create PROC Sp_Category_Getir
(
	@CategoryId INT=NULL
)
AS
BEGIN
	SELECT * FROM Categories where (@CategoryId=CategoryId or @CategoryId IS NULL)
END
go
create proc Sp_Category_Delete
(
	@CategoryId INT
)
AS
BEGIN
	DELETE Categories where CategoryId=@CategoryId
END
go
----------------------------------------------------------ALBÜMLER
create proc Sp_Album_InsertUpdate
(
	@AlbumId INT =0,
	@AlbumName NVARCHAR(100),
	@CategoryId INT,
	@SingerId INT,
	@UnitPrice DECIMAL(6,2),
	@Discount DECIMAL(6,2),
	@AlbumAddDate DATETIME
)
as
begin
	IF(@AlbumId=0)
	BEGIN
		INSERT Albums(AlbumName,CategoryId,SingerId,UnitPrice,Discount,AlbumAddDate) VALUES (@AlbumName,@CategoryId,@SingerId,@UnitPrice,@Discount,@AlbumAddDate)
		SELECT @@IDENTITY AS ID
	END
	ELSE
	BEGIN
		UPDATE Albums SET AlbumName=@AlbumName,CategoryId=@CategoryId,SingerId=@SingerId,UnitPrice=@UnitPrice,Discount=@Discount,AlbumAddDate=@AlbumAddDate WHERE AlbumId=@AlbumId
		select @AlbumId as ID
	END
end
go
Create PROC Sp_Album_Getir
(
	@AlbumId INT=NULL
)
AS
BEGIN
	SELECT * FROM Albums where (@AlbumId=AlbumId or @AlbumId IS NULL)
END
go
create proc Sp_Album_Delete
(
	@AlbumId INT
)
AS
BEGIN
	DELETE Albums where AlbumId=@AlbumId
END
go
------------------------------------------------------------------- ORDER
create proc Sp_Order_InsertUpdate
(
	@OrderId INT=0,
	@OrderDate DATETIME,
	@Country NVARCHAR(100),
	@City NVARCHAR(100),
	@County NVARCHAR(100),
	@UserId INT
)
as
begin
	IF(@OrderId=0)
	BEGIN
		INSERT Orders(OrderDate,Country,City,County,UserId) VALUES (@OrderDate,@Country,@City,@County,@UserId)
		SELECT @@IDENTITY AS ID
	END
	ELSE
	BEGIN
		UPDATE Orders SET OrderDate=@OrderDate,Country=@Country,City=@City,County=@County,UserId=@UserId WHERE OrderId=@OrderId
		select @OrderId as ID
	END
end
go
Create PROC Sp_Order_Getir
(
	@OrderId INT=NULL
)
AS
BEGIN
	SELECT * FROM Orders where (@OrderId=OrderId or @OrderId IS NULL)
END
go
create proc Sp_Order_Delete
(
	@OrderId INT
)
AS
BEGIN
	DELETE Orders where OrderId=@OrderId
END
go
--------------------------------------------------------------ORDERDETAIL
create proc Sp_OrderDetail_InsertUpdate
(
	@OrderDetailId INT=0,
	@OrderId INT,
	@AlbumId INT,
	@Quantity INT,
	@UnitPrice DECIMAL(6,2),
	@Discount DECIMAL(6,2)
)
as
begin
	IF(@OrderDetailId=0)
	BEGIN
		INSERT OrderDetails (OrderId,AlbumId,Quantity,UnitPrice,Discount) VALUES (@OrderId,@AlbumId,@Quantity,@UnitPrice,@Discount)
		SELECT @@IDENTITY AS ID
	END
	ELSE
	BEGIN
		UPDATE OrderDetails SET OrderId=@OrderId,AlbumId=@AlbumId,Quantity=@Quantity,UnitPrice=@UnitPrice,Discount=@Discount WHERE OrderDetailId=@OrderDetailId
		select @OrderDetailId as ID
	END
end
go
Create PROC Sp_OrderDetail_Getir
(
	@OrderId INT,
	@AlbumId INT=NULL
)
AS
BEGIN
	SELECT * FROM OrderDetails where @OrderId=OrderId and (@AlbumId=AlbumId or @AlbumId IS NULL)
END
go
create proc Sp_OrderDetail_Delete
(
	@OrderDetailId INT
)
AS
BEGIN
	DELETE OrderDetails where OrderDetailId=@OrderDetailId
END
go
-----------------------------------------------------------