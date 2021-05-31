USE StepAcademy

CREATE TABLE Genders
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Gender] nvarchar(30) NOT NULL CHECK(LEN(TRIM([Gender])) <> 0)
)

GO

CREATE TABLE Lecturers
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[FirstName] nvarchar(50) NOT NULL CHECK(LEN(TRIM([FirstName])) <> 0),
	[LastName] nvarchar(50) NOT NULL CHECK(LEN(TRIM([LastName])) <> 0),
	[BirthDate] date NOT NULL CHECK(LEN([BirthDate]) <> 0)
)

GO

CREATE TABLE Specialties
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[SpecialtyCode] smallint NOT NULL,
	[Specialty] nvarchar(50) NOT NULL CHECK(LEN(TRIM([Specialty])) <> 0)
)

GO

CREATE TABLE Groups
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL CHECK(LEN(TRIM([Name])) <> 0),
	[Class] tinyint NOT NULL,
	[LecturerId] int NULL FOREIGN KEY REFERENCES Lecturers(Id)
							 ON DELETE CASCADE
							 ON UPDATE CASCADE,
	[LeaderId] int NULL FOREIGN KEY REFERENCES Leaders(Id)
						 ON DELETE CASCADE
						 ON UPDATE CASCADE,
)

GO

CREATE TABLE Students
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    [GroupId] int NULL FOREIGN KEY REFERENCES Groups(Id)
						  ON DELETE CASCADE
						  ON UPDATE CASCADE,
	[FirstName] nvarchar(50) NOT NULL CHECK(LEN(TRIM([FirstName])) <> 0),
	[LastName] nvarchar(50) NOT NULL CHECK(LEN(TRIM([LastName])) <> 0),
	[GenderId] int NULL FOREIGN KEY REFERENCES Genders(Id)
						   ON DELETE CASCADE
						   ON UPDATE CASCADE,
	[BirthDate] date NOT NULL CHECK(LEN([BirthDate]) <> 0),
	[GradeBookNumber] nvarchar(15) NOT NULL CHECK(LEN(TRIM([GradeBookNumber])) <> 0) UNIQUE,
	[SpecialtyId] int NULL FOREIGN KEY REFERENCES Specialties(Id)
						   ON DELETE CASCADE
						   ON UPDATE CASCADE,
	[Note] nvarchar(500) NULL CHECK(LEN(TRIM([Note])) <> 0),
	[Phone] nvarchar(30) NOT NULL CHECK(LEN(TRIM([Phone])) <> 0) UNIQUE,
	[Email] nvarchar(50) NULL CHECK(LEN(TRIM([Email])) <> 0) UNIQUE,
	[AddressId] int NULL FOREIGN KEY REFERENCES Addresses(Id)
							ON DELETE NO ACTION
							ON UPDATE CASCADE,
	[AdmissionYear] smallint NOT NULL
) 

GO

CREATE TABLE Addresses
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[StudentId] int NULL FOREIGN KEY REFERENCES Students(Id)
						  ON DELETE CASCADE
						  ON UPDATE CASCADE,
	[District] nvarchar(30) NOT NULL CHECK(LEN(TRIM([District])) <> 0),
	[City] nvarchar(30) NOT NULL CHECK(LEN(TRIM([City])) <> 0),
	[Street] nvarchar(30) NOT NULL CHECK(LEN(TRIM([Street])) <> 0),
	[House] nvarchar(10) NOT NULL CHECK(LEN(TRIM([House])) <> 0),
	[Flat] nvarchar(10) NOT NULL CHECK(LEN(TRIM([Flat])) <> 0)
)

GO

CREATE TABLE Leaders
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[GroupId] int NULL FOREIGN KEY REFERENCES Groups(Id)
						  ON DELETE CASCADE
						  ON UPDATE CASCADE,
	[StudentId] int NULL FOREIGN KEY REFERENCES Students(Id)
						  ON DELETE CASCADE
						  ON UPDATE CASCADE,
)

GO

CREATE TABLE Grades
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Mark] nvarchar(10) NOT NULL CHECK(LEN(TRIM([Mark])) <> 0),
	[Grade] smallint NOT NULL
)

GO

CREATE TABLE Credits
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Grade] smallint NOT NULL,
	[Credit] nvarchar(50) NOT NULL CHECK(LEN(TRIM([Credit])) <> 0),
)

GO

CREATE TABLE [Students'Grades]
(
	[StudentId] int NULL FOREIGN KEY REFERENCES Students(Id)
						  ON DELETE CASCADE
						  ON UPDATE CASCADE,
	[GradeId] int NULL FOREIGN KEY REFERENCES Grades(Id)
						  ON DELETE NO ACTION
						  ON UPDATE CASCADE,
	[CreditId] int NULL FOREIGN KEY REFERENCES Credits(Id)
						  ON DELETE NO ACTION
						  ON UPDATE CASCADE,
)

GO

CREATE TABLE Subjects
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL CHECK(LEN(TRIM([Name])) <> 0),
	[Class] tinyint NOT NULL,
	[Hours] smallint NOT NULL CHECK([Hours] > 0)
)

GO

CREATE TABLE Academies
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[City] nvarchar(50) NOT NULL CHECK(LEN(TRIM([City])) <> 0),
	[Street] nvarchar(50) NOT NULL CHECK(LEN(TRIM([Street])) <> 0),
	[House] nvarchar(10) NOT NULL CHECK(LEN(TRIM([House])) <> 0)
) 

GO

CREATE TABLE [Academies' Phones]
(
	Id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Phone] nvarchar(30) NOT NULL CHECK(LEN(TRIM([Phone])) <> 0),
	[AcademyId] int NOT NULL FOREIGN KEY REFERENCES Academies(Id)
									ON DELETE CASCADE
									ON UPDATE CASCADE,
) 

GO


