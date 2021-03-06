USE [master]
GO
/****** Object:  Database [GroceryCo]    Script Date: 1/13/2016 8:49:12 AM ******/
CREATE DATABASE [GroceryCo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GroceryCo', FILENAME = N'C:\Projects\Blatant\GroceryCo\Database\GroceryCo.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GroceryCo_log', FILENAME = N'C:\Projects\Blatant\GroceryCo\Database\GroceryCo_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [GroceryCo] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GroceryCo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GroceryCo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GroceryCo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GroceryCo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GroceryCo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GroceryCo] SET ARITHABORT OFF 
GO
ALTER DATABASE [GroceryCo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GroceryCo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GroceryCo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GroceryCo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GroceryCo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GroceryCo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GroceryCo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GroceryCo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GroceryCo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GroceryCo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GroceryCo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GroceryCo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GroceryCo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GroceryCo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GroceryCo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GroceryCo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GroceryCo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GroceryCo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GroceryCo] SET  MULTI_USER 
GO
ALTER DATABASE [GroceryCo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GroceryCo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GroceryCo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GroceryCo] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [GroceryCo]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 1/13/2016 8:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](5) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 1/13/2016 8:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[ProductCode] [nvarchar](20) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[PromotionApplied] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 1/13/2016 8:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[Code] [nvarchar](5) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 1/13/2016 8:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Code] [nvarchar](20) NOT NULL,
	[ProductTypeCode] [nvarchar](5) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_ProductCode] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 1/13/2016 8:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductType](
	[Code] [nvarchar](5) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 1/13/2016 8:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductCode] [nvarchar](20) NOT NULL,
	[PromotionCode] [nvarchar](3) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[Quantity] [int] NOT NULL,
	[ApplyTo] [int] NULL,
	[Amount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PromotionType]    Script Date: 1/13/2016 8:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PromotionType](
	[Code] [nvarchar](3) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PromotionType] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_OrderStatus] FOREIGN KEY([Status])
REFERENCES [dbo].[OrderStatus] ([Code])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_OrderStatus]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Order]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductType] FOREIGN KEY([ProductTypeCode])
REFERENCES [dbo].[ProductType] ([Code])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductType]
GO
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [FK_Promotion_Products] FOREIGN KEY([ProductCode])
REFERENCES [dbo].[Products] ([Code])
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [FK_Promotion_Products]
GO
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [FK_Promotion_PromotionType] FOREIGN KEY([PromotionCode])
REFERENCES [dbo].[PromotionType] ([Code])
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [FK_Promotion_PromotionType]
GO
USE [master]
GO
ALTER DATABASE [GroceryCo] SET  READ_WRITE 
GO
USE [GroceryCo]

insert into OrderStatus values ('pend', 'Pending');

insert into OrderStatus values ('comp', 'Complete');

insert into ProductType values ('fruit','Fruit')

insert into ProductType values ('veg','Vegetable')

insert into ProductType values ('coff','Coffee')

insert into ProductType values ('dess','Dessert')

insert into Products values ('apple', 'fruit', 'Apple', 0.75);

insert into Products values ('banan', 'fruit', 'Bananas', 1);

insert into Products values ('orang', 'fruit', 'Orange', 1.2);

insert into Products values ('kickc', 'coff', 'Kicking Horse Coffee', 8);

insert into Products values ('nuttl', 'dess', 'Nutella', 5.5);

insert into PromotionType values ('d', 'Discount');

insert into PromotionType values ('p', 'Price');

SET IDENTITY_INSERT Promotion OFF;

-- buy 3 apples for $2
insert into Promotion values ('apple', 'p', '2016-01-01', null, 3, null, 2);

-- buy 1 Nutella, get 1 free
insert into Promotion values ('nuttl', 'd', '2016-01-01', null, 1, 1, 0);

-- buy 1 orang, get 1 for 50% off
insert into Promotion values ('orang', 'd', '2016-01-01', null, 1, 1, 0.5);
