USE Sales
GO

CREATE TABLE Schedule
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[FirstName] nvarchar(50) NOT NULL CHECK(LEN(TRIM([FirstName])) <> 0),
	[LastName] nvarchar(50) NOT NULL CHECK(LEN(TRIM([LastName])) <> 0),
	[StartTime] datetime2(7) NOT NULL CHECK(LEN([StartTime]) <> 0),
	[EndTime] datetime2(7) NOT NULL CHECK(LEN([EndTime]) <> 0)
)

CREATE TABLE WorkDays
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[DayNumber] int NOT NULL,
	[ScheduleId] int NULL FOREIGN KEY REFERENCES [Schedule](Id)
							 ON DELETE NO ACTION
							 ON UPDATE CASCADE
)