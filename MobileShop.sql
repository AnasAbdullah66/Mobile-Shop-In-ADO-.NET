CREATE DATABASE MobileShopDB
GO

USE MobileShopDB
GO

CREATE TABLE [dbo].[BrandName] 
(
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Brand] NVARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [dbo].[Mobile] 
(
    [mobile_ID]      INT             IDENTITY (1, 1) NOT NULL,
    [brand_ID]       INT             NOT NULL,
    [model_Name]     VARCHAR (50)    NOT NULL,
    [mobile_Price]   MONEY           NOT NULL,
    [mobile_Picture] VARBINARY (MAX) NULL,
    [camera]         INT             NULL,
    [ram]            INT             NULL,
    [rom]            INT             NULL,
    PRIMARY KEY CLUSTERED ([mobile_ID] ASC),
    FOREIGN KEY ([brand_ID]) REFERENCES [dbo].[BrandName] ([Id])
)
GO 

CREATE TABLE Post
(
	Post_Id INT PRIMARY KEY IDENTITY,
	Post_Name NVARCHAR(30)
)
GO
INSERT INTO Post VALUES
('Manager'),
('Sels Consaltent'),
('Brand Promoter')
GO

CREATE TABLE Employee
(
	Employee_ID INT IDENTITY (1, 1) PRIMARY KEY NOT NULL,
	Employee_Name NVARCHAR(50) NOT NULL,
	Join_Date DATE NOT NULL,
	Employee_Post INT REFERENCES Post(Post_Id) NOT NULL,
	Basic_Salary MONEY NOT NULL,
	Employee_Picture VARBINARY (MAX) NULL,
)
GO

CREATE TABLE Bill
(
	Bill_Id INT IDENTITY (1, 1) NOT NULL,
	Product_Name INT REFERENCES Mobile(mobile_ID),
	Customer_Name NVARCHAR(50) NOT NULL,
	Customer_Contact NVARCHAR(30) NOT NULL,
	Product_Price MONEY NOT NULL,
	Sold_Date DATE NOT NULL,
	Quantity INT NOT NULL,
	Sold_By INT REFERENCES Employee(Employee_ID)
)
GO