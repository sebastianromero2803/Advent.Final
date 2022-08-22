USE AdventFinal

CREATE TABLE Users(
	IdUser int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] varchar(30) NOT NULL,
	LastName varchar(30) NOT NULL,
	Birthdate date NULL,
	Gender varchar(15) NULL,
	CellPhone varchar(20) NULL,
	Email varchar(50) NULL,
	UserName varchar(25) NOT NULL,
	[Password] varchar(max),
	CreatedAt date NOT NULL,
	LastLogIn date NOT NULL,
	LastLogOut date,
	[Status] varchar(20) NOT NULL,
)

CREATE TABLE Roles(
	IdRole int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	RoleName varchar(20) NOT NULL,
	[Description] varchar(50) NULL,
)

CREATE TABLE UsersRoles(
	IdUser int NOT NULL,
	IdRole int NOT NULL,
	FOREIGN KEY (IdRole) REFERENCES Roles(IdRole),
	FOREIGN KEY (IdUser) REFERENCES Users(IdUser)
)

DROP TABLE Users
DROP TABLE Roles
DROP TABLE UsersRoles

INSERT INTO Users(Name, LastName, Birthdate, Gender, CellPhone, Email, UserName, [Password], CreatedAt, LastLogIn, LastLogOut, [Status])
VALUES('Sebastian', 'Romero', '2002-03-28', 'M', '32897123','sebasromero@hotmail.com', 'sebasromero', '7e18b4476d66977486f9f5df1bde792d36411a13381645fd4c055515db424226', '2022-08-01', '2022-08-03', '2022-08-01', 'Valid')