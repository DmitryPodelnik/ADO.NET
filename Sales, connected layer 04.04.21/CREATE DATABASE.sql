USE Users
GO

CREATE TABLE [Users]
(
	[Id] int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Username] nvarchar(50) NOT NULL CHECK(LEN(TRIM([Username])) > 6),
	[Password] nvarchar(50) NOT NULL CHECK(LEN(TRIM([Password])) > 8),
	[Email] nvarchar(320) NOT NULL CHECK(LEN(TRIM([Email])) > 8)
)