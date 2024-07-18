CREATE DATABASE DoAn_BunDau
use DoAn_BunDau
CREATE TABLE [dbo].[AdminUser](
	[ID] INT NOT NULL,
	[NameUser] NVARCHAR (MAX) NULL,
	[RoleUser] NVARCHAR(MAX) NULL,
	[PasswordUser] NCHAR(50) NULL,
	PRIMARY KEY CLUSTERED ([ID] ASC)
);
ALTER TABLE [dbo].[AdminUser]
ADD [PhoneAd] VARCHAR (11) NOT NULL,
	[MailAd] Nvarchar(max) not null;

 CREATE TABLE [dbo].[Category](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[IDCate] NCHAR(20) NOT NULL,
	[NameCate] NVARCHAR(MAX) NULL,
	PRIMARY KEY CLUSTERED ([IDCate] ASC),
 );
 ALTER TABLE [dbo].[Category]
ADD [Alias] VARCHAR (max) NULL;

 CREATE TABLE [dbo].[Customer](
	[IDCus] INT IDENTITY(1,1) NOT NULL,
	[NameCus] NVARCHAR (MAX) NULL,
	[PhoneCus] NVARCHAR(15) NULL,
	[EmailCus] NVARCHAR (MAX) NULL,
	PRIMARY KEY CLUSTERED ([IDCus] ASC),
 );
  ALTER TABLE [dbo].[Customer]
ADD [Password] VARCHAR (max) NULL;

 CREATE TABLE [dbo].[Product](
	[ProductID] INT IDENTITY(1,1) NOT NULL,
	[NamePro] NVARCHAR(MAX) NULL,
	[DecriptionPro] NVARCHAR(MAX) NULL,
	[Category] NCHAR(20)NULL,
	[Price] DECIMAL(18,2) NULL,
	[ImagePro] NVARCHAR(MAX) NULL,
	PRIMARY KEY CLUSTERED ([ProductID] ASC),
	CONSTRAINT [FK_Pro_Category] FOREIGN KEY ([Category]) REFERENCES [dbo].[Category]([IDCate])
 );
 ALTER TABLE [dbo].[Product]
ADD [IsActive] Bit ;
ALTER TABLE [dbo].[Product]
ADD [SeoTitle] nvarchar(max),
	[SeoKeyWords] nvarchar(max),
	[SeoDescription] nvarchar(max);
 Create table [dbo].[ProductCategory](
	[ID] INT IDENTITY(1,1) NOT NULL,
	[Title] NVARCHAR(MAX) NULL,
	[Description] Nvarchar(max) null,
	[Icon] varchar(max) null,
	[SeoTitle] nvarchar(max) null,
	[SeoDescription] nvarchar(max) null,
	[SeoKeyWords] nvarchar(max) null,
	[Product] Int null,
	[Alias] varchar(max) null,
	Primary Key clustered ([ID] Asc),
	Constraint [FK_Pro_ProCate] Foreign key([Product]) references [dbo].[Product]([ProductID])
 );
 ALTER TABLE [dbo].[Product]
ADD [SoLuong] VARCHAR (max) NULL;
	ALTER TABLE [dbo].[Product]
ADD [Alias] VARCHAR (max) NULL;

 CREATE TABLE [dbo].[OrderPro](
	[ID] INT IDENTITY (1,1) NOT NULL,
	[DateOrder] DATE NULL,
	[IDCus] INT NULL,
	[AddressDelivery] NVARCHAR(MAX) NULL,
	PRIMARY KEY CLUSTERED ([ID] ASC),
	FOREIGN KEY ([IDCus]) REFERENCES [dbo].[Customer] ([IDCus]),
 );

 CREATE TABLE[dbo].[OrderDetail](
	[ID] INT IDENTITY(1,1) NOT NULL,
	[IDProduct] INT NULL,
	[IDOrder] INT NULL,
	[Quantity] INT NULL,
	[UnitPrice] FLOAT(53) NULL,
	PRIMARY KEY CLUSTERED ([ID] ASC),
	FOREIGN KEY([IDProduct]) REFERENCES [dbo].[Product] ([ProductID]),
	FOREIGN KEY ([IDOrder]) REFERENCES [dbo].[OrderPro] ([ID]),
 );
