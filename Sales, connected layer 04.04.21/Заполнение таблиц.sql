USE Sales
GO

INSERT INTO [Custom]
VALUES('John', 'Smith')

INSERT INTO [Custom]
VALUES('Mark', 'Walhberg')

INSERT INTO [Custom]
VALUES('Tom', 'Cruise')

SELECT *
FROM [Custom]


INSERT INTO [Salespeople]
VALUES('Bruce', 'Willis')

INSERT INTO [Salespeople]
VALUES('Daniel', 'Craig')

INSERT INTO [Salespeople]
VALUES('Kevin', 'Costner')

SELECT *
FROM [Salespeople]


INSERT INTO Sales
VALUES(1, 1)

INSERT INTO Sales
VALUES(2, 2)

INSERT INTO Sales
VALUES(3, 3)

INSERT INTO Sales
VALUES(3, 1)

INSERT INTO Sales
VALUES(3, 2)

SELECT * 
FROM [Sales]