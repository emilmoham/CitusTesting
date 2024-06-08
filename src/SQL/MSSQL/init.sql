CREATE table accounting.dbo.facilities (
	Id  int IDENTITY(1,1) PRIMARY KEY,
	Name varchar(50) NOT NULL
);

CREATE TABLE accounting.dbo.accounts (
	Id int IDENTITY(1,1) PRIMARY KEY,
	Name varchar(50) NOT NULL,
	Number int NOT NULL,
	Type bit NOT NULL,
	FacilityId int NOT NULL REFERENCES accounting.dbo.facilities(id)
);

CREATE TABLE accounting.dbo.transactions (
	Id bigint IDENTITY(1,1) PRIMARY KEY,
	FacilityId int NOT NULL REFERENCES accounting.dbo.facilities(id)
);

CREATE TABLE accounting.dbo.entries (
	Id bigint IDENTITY(1,1) PRIMARY KEY,
	FacilityId int NOT NULL REFERENCES accounting.dbo.facilities(id),
	TransactionId bigint NOT NULL REFERENCES accounting.dbo.transactions(id),
	AccountId int NOT NULL REFERENCES accounting.dbo.accounts(id),
	Credit int NOT NULL,
	Debit int NOT NULL
);
