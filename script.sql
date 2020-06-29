CREATE TABLE [dbo].[Basket] (
    [BasketID]  INT   IDENTITY (1, 1) NOT NULL,
    [OrderID]   INT   NULL,
    [ProductID] INT   NULL,
    [Quantity]  INT   NULL,
    [Price]     MONEY NULL,
    [Sum]       MONEY NULL,
    PRIMARY KEY CLUSTERED ([BasketID] ASC),
    CONSTRAINT [FK_Basket_Orders] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Orders] ([OrderID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Basket_Products] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ProductID]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[Leaders] (
    [Nickname] NVARCHAR (50) NOT NULL,
    [FIO]      NVARCHAR (50) NULL,
    [Name]     NVARCHAR (50) NULL,
    [Phone]    NVARCHAR (50) NULL,
    [Password] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Nickname] ASC)
);

CREATE TABLE [dbo].[Orders] (
    [OrderID]          INT           NOT NULL,
    [Leader_nickname]  NVARCHAR (50) NOT NULL,
    [Pioneer_nickname] NVARCHAR (50) NULL,
    [OrderDate]        DATE          NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderID] ASC),
    CONSTRAINT [FK_Orders_Leaders] FOREIGN KEY ([Leader_nickname]) REFERENCES [dbo].[Leaders] ([Nickname]),
    CONSTRAINT [FK_Orders_Users] FOREIGN KEY ([Pioneer_nickname]) REFERENCES [dbo].[Users] ([Nickname])
);

CREATE TABLE [dbo].[Products] (
    [ProductID]       INT           IDENTITY (1, 1) NOT NULL,
    [ProductName]     NVARCHAR (50) NULL,
    [Price]           MONEY         NULL,
    [Leader_nickname] NVARCHAR (50) NULL,
    [Create_Date]     DATE          NULL,
    PRIMARY KEY CLUSTERED ([ProductID] ASC),
    CONSTRAINT [FK_Products_Leaders] FOREIGN KEY ([Leader_nickname]) REFERENCES [dbo].[Leaders] ([Nickname])
);

CREATE TABLE [dbo].[Users] (
    [Nickname] NVARCHAR (50) NOT NULL,
    [FIO]      NVARCHAR (50) NULL,
    [Phone]    NVARCHAR (50) NULL,
    [Email]    NVARCHAR (50) NULL,
    [Password] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Nickname] ASC)
);