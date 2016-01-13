USE [GroceryCo]
GO

/****** Object:  Table [dbo].[Products]    Script Date: 1/11/2016 8:08:52 PM ******/
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

ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductType] FOREIGN KEY([ProductTypeCode])
REFERENCES [dbo].[ProductType] ([Code])
GO

ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductType]
GO


