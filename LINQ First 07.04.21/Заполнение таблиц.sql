USE Sales
GO

INSERT INTO [Schedule]
VALUES('Test1', 'Test12', CAST(GETDATE() as datetime2(7)), CAST(GETDATE() as datetime2(7)))

INSERT INTO [Schedule]
VALUES('Test1', 'Test12', '2017-12-21', '2018-12-21')

INSERT INTO [Schedule]
VALUES('Test1', 'Test12', '2016-04-21', '2020-09-21')

SELECT *
FROM Schedule

INSERT INTO WorkDays
VALUES(1, 1)

INSERT INTO WorkDays
VALUES(2, 2)

INSERT INTO WorkDays
VALUES(4, 3)

INSERT INTO WorkDays
VALUES(5, 4)