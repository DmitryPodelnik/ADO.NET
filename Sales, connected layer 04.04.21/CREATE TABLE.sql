USE Sales
GO

CREATE TABLE [Custom]
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[First Name] nvarchar(50) NOT NULL CHECK(LEN(TRIM([First Name])) <> 0),
	[Last Name] nvarchar(50) NOT NULL CHECK(LEN(TRIM([Last Name])) <> 0)
)

GO

CREATE TABLE [Salespeople]
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[First Name] nvarchar(50) NOT NULL CHECK(LEN(TRIM([First Name])) <> 0),
	[Last Name] nvarchar(50) NOT NULL CHECK(LEN(TRIM([Last Name])) <> 0)
)

GO

CREATE TABLE [Sales]
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[CustomId] int NULL FOREIGN KEY REFERENCES [Custom](Id)
						ON DELETE NO ACTION
						ON UPDATE CASCADE,
	[SalespersonId] int NULL FOREIGN KEY REFERENCES [Salespeople](Id)
							 ON DELETE NO ACTION
							 ON UPDATE CASCADE
)