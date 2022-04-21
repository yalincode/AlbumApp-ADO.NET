use AlbumDB
go
SELECT TOP 10 *FROM Albums order BY AlbumAddDate DESC
go
SELECT TOP 10 * FROM Albums order BY Discount DESC
go
SELECT * FROM Albums WHERE CategoryId = @k
go
select od.AlbumId,AlbumName,Sum(Quantity) as ToplamAdet FROM OrderDetails as od
INNER JOIN Albums as a ON od.AlbumId=a.AlbumId
GROUP BY od.AlbumId,AlbumName
ORDER BY Sum(Quantity) DESC
go
select s.SingerId,SingerName,Sum(Quantity) as ToplamAdet FROM OrderDetails as od
INNER JOIN Albums as a ON od.AlbumId=a.AlbumId
INNER JOIN Singers AS s on s.SingerId=a.SingerId
GROUP BY s.SingerId,SingerName
ORDER BY Sum(Quantity) DESC

