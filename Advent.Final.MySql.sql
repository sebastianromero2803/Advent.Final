CREATE DATABASE AdventFinal;
USE AdventFinal;

CREATE TABLE Users(
	Id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	Name varchar(30) NOT NULL,
	LastName varchar(30) NOT NULL,
	Birthdate date NULL,
	Gender varchar(15) NULL,
	CellPhone varchar(20) NULL,
	Email varchar(50) NULL,
	UserName varchar(25) NOT NULL,
	Password varchar(100),
	CreatedAt date NOT NULL,
	LastLogIn date NOT NULL,
	LastLogOut date,
    Token varchar(100),
	Status varchar(20) NOT NULL
);

CREATE TABLE Bookings(
	Id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	PaymentId int NOT NULL,
	ContainerId varchar(20) NOT NULL,
	Fee int NOT NULL
);

CREATE TABLE Payments(
	Id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	UserId int NOT NULL,
	Date datetime NOT NULL,
	PaymentToken varchar(50) NOT NULL
);

CREATE TABLE PaymentMethods(
	Id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
	UserId int NOT NULL,
	Token varchar(50),
	LastDigits varchar(4) NOT NULL
);

-- DROP TABLE Users;
-- DROP TABLE Roles;
-- DROP TABLE UsersRoles;

INSERT INTO Users(Id, Name, LastName, Birthdate, Gender, CellPhone, Email, UserName, Password, CreatedAt, LastLogIn, LastLogOut, Status)
VALUES(1, 'Sebastian', 'Romero', '2002-03-28', 'M', '32897123','sebasromero@hotmail.com', 'sebasromero', '7e18b4476d66977486f9f5df1bde792d36411a13381645fd4c055515db424226', '2022-08-01', '2022-08-03', '2022-08-01', 'Valid');