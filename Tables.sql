CREATE TABLE [dbo].[Access] (
    [UserId]      INT     IDENTITY (1, 1) NOT NULL,
    [UserName]    VARCHAR (50) NULL,
    [Password]    VARCHAR (50) NULL
);

CREATE TABLE [dbo].[Clients] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50) NULL,
    [SurName]    NVARCHAR (50) NULL,
    [Patronymic] NVARCHAR (50) NULL,
    [Phone]      NVARCHAR (50) NULL,
    [E_Mail]     NVARCHAR (50) NULL
);

CREATE TABLE [dbo].[Purchases] (
    [Id]            INT   IDENTITY (1, 1) NOT NULL,
    [E_Mail]        NVARCHAR (50) NULL,
    [ProductCode]   INT           NULL,
    [ProductName]   NVARCHAR (50) NULL
);

CREATE TABLE [dbo].[ElectronicsProducts] (
    [Id]            INT   IDENTITY (1, 1) NOT NULL,
    [ProductCode]   INT           NULL,
    [ProductName]   NVARCHAR (50) NULL
);

CREATE TABLE [dbo].[SpaceProducts] (
    [Id]            INT   IDENTITY (1, 1) NOT NULL,
    [ProductCode]   INT           NULL,
    [ProductName]   NVARCHAR (50) NULL
);

CREATE TABLE [dbo].[FoodsProducts] (
    [Id]            INT   IDENTITY (1, 1) NOT NULL,
    [ProductCode]   INT           NULL,
    [ProductName]   NVARCHAR (50) NULL
);

SET IDENTITY_INSERT [dbo].[Access] ON
INSERT INTO [dbo].[Access] ([UserId], [UserName], [Password]) VALUES (1, N'R2D2', N'R2D2')
SET IDENTITY_INSERT [dbo].[Access] OFF

SET IDENTITY_INSERT [dbo].[Clients] ON
INSERT INTO [dbo].[Clients] ([id], [Name], [SurName], [Patronymic], [Phone], [E_Mail]) 
                           VALUES (1, N'Иван', N'Иванов', N'Иванович', '', 'ivanov@mail.ru')
SET IDENTITY_INSERT [dbo].[Clients] OFF

SELECT * FROM Clients

SET IDENTITY_INSERT [dbo].[Purchases] ON
INSERT INTO [dbo].[Purchases] ([Id], [E_Mail], [ProductCode], [ProductName]) VALUES (1, 'ivanov@mail.ru', 34, N'Iphone телефон')
INSERT INTO [dbo].[Purchases] ([Id], [E_Mail], [ProductCode], [ProductName]) VALUES (2, 'ivanov@mail.ru', 32, N'Samsung телевизор')
INSERT INTO [dbo].[Purchases] ([Id], [E_Mail], [ProductCode], [ProductName]) VALUES (3, 'ivanov@mail.ru', 23, N'Вентилятор')
SET IDENTITY_INSERT [dbo].[Purchases] OFF

SELECT * FROM Purchases

SET IDENTITY_INSERT [dbo].[ElectronicsProducts] ON
INSERT INTO [dbo].[ElectronicsProducts] ([Id], [ProductCode], [ProductName]) VALUES (1, 12, N'PS 5 игровая приставка')
INSERT INTO [dbo].[ElectronicsProducts] ([Id], [ProductCode], [ProductName]) VALUES (2, 21, N'Samsung 8к смарт телевизор')
INSERT INTO [dbo].[ElectronicsProducts] ([Id], [ProductCode], [ProductName]) VALUES (3, 37, N'Super Rishat Навигатор по Млечному пути')
SET IDENTITY_INSERT [dbo].[ElectronicsProducts] OFF

SELECT * FROM [ElectronicsProducts]

SET IDENTITY_INSERT [dbo].[SpaceProducts] ON
INSERT INTO [dbo].[SpaceProducts] ([Id], [ProductCode], [ProductName]) VALUES (1, 777, N'R2-D2 звездалёт')
INSERT INTO [dbo].[SpaceProducts] ([Id], [ProductCode], [ProductName]) VALUES (2, 33, N'Полёт до Альфа центавра В')
INSERT INTO [dbo].[SpaceProducts] ([Id], [ProductCode], [ProductName]) VALUES (3, 44, N'Полёт до Галлактики Андромеда')
SET IDENTITY_INSERT [dbo].[SpaceProducts] OFF

SELECT * FROM [SpaceProducts]

SET IDENTITY_INSERT [dbo].[FoodsProducts] ON
INSERT INTO [dbo].[FoodsProducts] ([Id], [ProductCode], [ProductName]) VALUES (1, 111, N'Скатерть Самобранка для длительных полётов')
INSERT INTO [dbo].[FoodsProducts] ([Id], [ProductCode], [ProductName]) VALUES (2, 47, N'Бургер')
INSERT INTO [dbo].[FoodsProducts] ([Id], [ProductCode], [ProductName]) VALUES (3, 55, N'Пельмени')
SET IDENTITY_INSERT [dbo].[FoodsProducts] OFF

SELECT * FROM [FoodsProducts]

SELECT 
Clients.Id as 'Id',
Purchases.Id  as 'Id',
ElectronicsProducts.Id as 'Id',
SpaceProducts.Id as 'Id',
FoodsProducts.Id as 'Id'
FROM  Clients, Purchases, ElectronicsProducts, SpaceProducts, FoodsProducts
WHERE Clients.Id = Clients.Id and Purchases.Id = Purchases.Id and 
      ElectronicsProducts.Id = ElectronicsProducts.Id and 
      SpaceProducts.Id = SpaceProducts.Id and FoodsProducts.Id = FoodsProducts.Id
