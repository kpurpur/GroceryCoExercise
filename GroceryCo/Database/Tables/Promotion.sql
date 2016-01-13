USE [GroceryCo]
GO

/****** Object:  Table [dbo].[Promotion]    Script Date: 1/11/2016 7:44:02 PM ******/
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


