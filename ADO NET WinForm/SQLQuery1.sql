USE [Stationery Firm]

CREATE TABLE TypeStationery(
[Id] int Primary Key IDENTITY(1,1),
[Type] nvarchar(100) NOT NULL UNIQUE,
[Quantity] int not null --Количество канцтоваров каждого типа
)

CREATE TABLE Stationery(
[Id] int Primary Key IDENTITY(1,1),
[Name] nvarchar(100) NOT NULL,
[TypeStationeryId] int NOT NULL FOREIGN KEY REFERENCES TypeStationery(Id),
[CostPrice] money not NULL,
)

CREATE TABLE Managers(
[Id] int Primary Key IDENTITY(1,1),
[Name] nvarchar(100) NOT NULL,
)

CREATE TABLE Customers(
[Id] int Primary Key IDENTITY(1,1),
[Name] nvarchar(100) NOT NULL
)

CREATE TABLE Sales(
[Id] int Primary Key IDENTITY(1,1),
[ManagerId] int NOT NULL FOREIGN KEY REFERENCES Managers(Id),
[CustomerId] int NOT NULL FOREIGN KEY REFERENCES Customers(Id),
[StationaryId] int NOT NULL FOREIGN KEY REFERENCES Stationery(Id),
[Price_of_one] money NOT NULL,
[Quantity] int NOT NULL,
[Date_of_sale] DATETIME NOT NULL Default(GETDATE())
)

--INSERT INTO TypeStationery(Type, Quantity)
--VALUES (N'Pen', 444)

--INSERT INTO Stationery(Name, TypeStationeryId, CostPrice)
--VALUES (N'Gucci', 1, 23.99)

--INSERT INTO Stationery(Name, TypeStationeryId, CostPrice)
--VALUES (N'Cyber', 1, 15)

--INSERT INTO Managers(Name)
--VALUES (N'Mamed')

--INSERT INTO Managers(Name)
--VALUES (N'Maxmud')

--INSERT INTO Customers(Name)
--VALUES (N'User Company official')

--INSERT INTO Customers(Name)
--VALUES (N'User Company')

--INSERT INTO Sales(ManagerId, CustomerId, StationaryId, Quantity, Price_of_one, Date_of_sale)
--VALUES (1, 1, 1, 30, 24, GETDATE())

--INSERT INTO Sales(ManagerId, CustomerId, StationaryId, Quantity, Price_of_one, Date_of_sale)
--VALUES (1, 1, 1, 45, 30, GETDATE())

--INSERT INTO Sales(ManagerId, CustomerId, StationaryId, Quantity, Price_of_one, Date_of_sale)
--VALUES (2, 1, 1, 145, 30, GETDATE())

--INSERT INTO Sales(ManagerId, CustomerId, StationaryId, Quantity, Price_of_one, Date_of_sale)
--VALUES (2, 2, 1, 40, 30, GETDATE())

--INSERT INTO Sales(ManagerId, CustomerId, StationaryId, Quantity, Price_of_one, Date_of_sale)
--VALUES (2, 2, 2, 40, 30, GETDATE())

--Task 1
SELECT Stationery.Name, TypeStationery.Type, Stationery.CostPrice FROM Stationery, TypeStationery
WHERE Stationery.TypeStationeryId = TypeStationery.Id

--Task 2
SELECT TypeStationery.Type[Type of Stationary] FROM TypeStationery

--Task 3
SELECT Managers.Name FROM Managers

--Task 4
SELECT Stationery.Name, TypeStationery.Type, Stationery.CostPrice FROM Stationery, TypeStationery
WHERE Stationery.TypeStationeryId = TypeStationery.Id
AND TypeStationery.Quantity = (SELECT MAX(Quantity) FROM TypeStationery)

--Task 5
SELECT Stationery.Name, TypeStationery.Type, Stationery.CostPrice FROM Stationery, TypeStationery
WHERE Stationery.TypeStationeryId = TypeStationery.Id
AND TypeStationery.Quantity = (SELECT MIN(Quantity) FROM TypeStationery)

--Task 6
SELECT Stationery.Name, Stationery.CostPrice FROM Stationery
WHERE Stationery.CostPrice = (SELECT MIN(CostPrice) FROM Stationery)

--Task 7
SELECT Stationery.Name, Stationery.CostPrice FROM Stationery
WHERE Stationery.CostPrice = (SELECT MAX(CostPrice) FROM Stationery)

--Task 8
SELECT Stationery.Name, TypeStationery.Type, Stationery.CostPrice FROM Stationery, TypeStationery
WHERE TypeStationery.Type = 'Pen' AND Stationery.TypeStationeryId = TypeStationery.Id

--Task 9
SELECT Managers.Name, Stationery.Name, TypeStationery.Type, Sales.Price_of_one, Sales.Quantity, Sales.Date_of_sale FROM Managers, Sales, Stationery, TypeStationery
WHERE Managers.Name = 'Mamed' 
AND Sales.ManagerId = Managers.Id AND Sales.StationaryId = Stationery.Id AND Stationery.TypeStationeryId = TypeStationery.Id

--Task 10
SELECT Customers.Name, Stationery.Name, TypeStationery.Type, Sales.Price_of_one, Sales.Quantity, Sales.Date_of_sale FROM Customers, Sales, Stationery, TypeStationery
WHERE Customers.Name = 'User Company official' 
AND Sales.CustomerId = Customers.Id AND Sales.StationaryId = Stationery.Id AND Stationery.TypeStationeryId = TypeStationery.Id

--Task 11
SELECT TOP 1 Managers.Name, Customers.Name, Sales.Price_of_one, Sales.Quantity, Stationery.Name, TypeStationery.Type, Sales.Date_of_sale FROM Sales, Managers, Customers, Stationery, TypeStationery
WHERE Sales.CustomerId = Customers.Id AND Sales.ManagerId = Managers.Id AND Sales.StationaryId = Stationery.Id AND Stationery.TypeStationeryId = TypeStationery.Id
ORDER BY Sales.Date_of_sale DESC

--Task 12
SELECT TypeStationery.Type, (AVG(TypeStationery.Quantity) / COUNT(TypeStationery.Type))[Average_amount] FROM TypeStationery, Stationery
WHERE Stationery.TypeStationeryId = TypeStationery.Id
GROUP BY TypeStationery.Type

SELECT Customers.Name FROM Customers

--CREATE PROCEDURE Task2_1
--@nameOfStationary nvarchar(100),
--@costPrice money,
--@type nvarchar(100),
--@quantity int
--AS
--BEGIN
--	BEGIN TRY
--		IF((SELECT COUNT(Id) FROM TypeStationery WHERE TypeStationery.Type = @type) > 0)
--		BEGIN
--			BEGIN TRAN
--			UPDATE TypeStationery
--			SET Quantity = (@quantity + (SELECT TOP 1 Quantity FROM TypeStationery as t WHERE t.Type = @type))
--			WHERE TypeStationery.Type = @type

--			INSERT INTO Stationery(Name, TypeStationeryId, CostPrice)
--			VALUES (@nameOfStationary, (SELECT TOP 1 Id FROM TypeStationery WHERE TypeStationery.Type = @type), @costPrice)
--			COMMIT
--		END
--	END TRY
--	BEGIN CATCH
--		IF (@@TRANCOUNT > 0) ROLLBACK
--	END CATCH
--END

--CREATE PROCEDURE Task2_2
--@type nvarchar(100)
--AS
--BEGIN
--	IF((SELECT COUNT(Id) FROM TypeStationery WHERE TypeStationery.Type = @type) = 0)
--	BEGIN
--		INSERT INTO TypeStationery(Type, Quantity)
--		VALUES (@type, 0)
--	END
--END

--CREATE PROCEDURE Task2_3
--@name nvarchar(100)
--AS
--BEGIN
--	IF ((SELECT COUNT(Id) FROM Managers WHERE Managers.Name = @name) = 0)
--	BEGIN
--		INSERT INTO Managers(Name)
--		VALUES (@name)
--	END
--END

--CREATE PROCEDURE Task2_4
--@nameCompany nvarchar(100)
--AS
--BEGIN
--	IF ((SELECT COUNT(Id) FROM Customers WHERE Customers.Name = @nameCompany) = 0)
--	BEGIN
--		INSERT INTO Customers(Name)
--		VALUES (@nameCompany)
--	END
--END

--CREATE PROCEDURE Task2_8
--@nameStationary nvarchar(100)
--AS
--BEGIN
--	DELETE FROM Stationery
--	WHERE Stationery.Name = @nameStationary
--END

--CREATE PROCEDURE Task2_9
--@nameManager nvarchar(100)
--AS
--BEGIN
--	DELETE FROM Managers
--	WHERE Managers.Name = @nameManager
--END

--CREATE PROCEDURE Task2_10
--@nameType nvarchar(100)
--AS
--BEGIN
--	DELETE FROM TypeStationery
--	WHERE TypeStationery.Type = @nameType
--END

--CREATE PROCEDURE Task2_11
--@nameCustomer nvarchar(100)
--AS
--BEGIN
--	DELETE FROM Customers
--	WHERE Customers.Name = @nameCustomer
--END

--Task 2.12
SELECT TOP 1 Managers.Name, SUM(Quantity)[CountOfSales] FROM Managers, Sales
WHERE Sales.ManagerId = Managers.Id
GROUP BY Managers.Name
ORDER BY SUM(Quantity) DESC

--Task 13
SELECT TOP 1 Managers.Name, SUM(Sales.Price_of_one * Sales.Quantity)[SalaryOfSales] FROM Managers, Sales
WHERE Sales.ManagerId = Managers.Id
GROUP BY Managers.Name
ORDER BY SUM(Sales.Price_of_one * Sales.Quantity) DESC

--Task 14
SELECT TOP 1 Managers.Name, SUM(Sales.Price_of_one * Sales.Quantity)[SalaryOfSales] FROM Managers, Sales
WHERE Sales.ManagerId = Managers.Id AND Sales.Date_of_sale > '2021-11-01' AND Sales.Date_of_sale < '2021-11-03'
GROUP BY Managers.Name
ORDER BY SUM(Sales.Price_of_one * Sales.Quantity) DESC

--Task 15
SELECT TOP 1 Customers.Name, SUM(Sales.Price_of_one * Sales.Quantity)[SalaryOfSales] FROM Customers, Sales
WHERE Sales.CustomerId = Customers.Id
GROUP BY Customers.Name
ORDER BY SUM(Sales.Price_of_one * Sales.Quantity) DESC

--Task 16
SELECT TOP 1 TypeStationery.Type, SUM(TypeStationery.Id)[CountOfSales] FROM TypeStationery, Stationery, Sales
WHERE Stationery.TypeStationeryId = TypeStationery.Id AND Sales.StationaryId = Stationery.Id
GROUP BY TypeStationery.Type
ORDER BY SUM(TypeStationery.Id) DESC

--Task 17
SELECT TOP 1 TypeStationery.Type, SUM(Sales.Price_of_one * Sales.Quantity)[SalaryOfSales] FROM TypeStationery, Stationery, Sales
WHERE Stationery.TypeStationeryId = TypeStationery.Id AND Sales.StationaryId = Stationery.Id
GROUP BY TypeStationery.Type
ORDER BY SUM(Sales.Price_of_one * Sales.Quantity) DESC

--Task 18
SELECT TypeStationery.Type, SUM(TypeStationery.Id)[CountOfSales] FROM TypeStationery, Stationery, Sales
WHERE Stationery.TypeStationeryId = TypeStationery.Id AND Sales.StationaryId = Stationery.Id
GROUP BY TypeStationery.Type
ORDER BY SUM(TypeStationery.Id) DESC

--Task 19
SELECT Stationery.Name, MAX(Sales.Date_of_sale) FROM Stationery, Sales
WHERE Sales.StationaryId = Stationery.Id
GROUP BY Stationery.Name
HAVING DATEDIFF(day, MAX(Sales.Date_of_sale), GETDATE()) = 5

SELECT * FROM SALES



SELECT * FROM Sales

