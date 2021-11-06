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

