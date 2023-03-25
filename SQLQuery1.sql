USE [master]
GO
/****** Object:  Database [transacciones]    Script Date: 25/03/2023 13:32:50 ******/
CREATE DATABASE [transacciones]
 GO
USE [transacciones]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 25/03/2023 13:32:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] NOT NULL,
	[Name] [varchar](40) NULL,
	[Price] [int] NULL,
	[Quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductSales]    Script Date: 25/03/2023 13:32:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductSales](
	[ProductSalesId] [int] NOT NULL,
	[ProductId] [int] NULL,
	[QuantitySold] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductSalesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Product] ([ProductID], [Name], [Price], [Quantity]) VALUES (101, N'Laptop', 15000, 100)
INSERT [dbo].[Product] ([ProductID], [Name], [Price], [Quantity]) VALUES (102, N'Desktop', 20000, 150)
INSERT [dbo].[Product] ([ProductID], [Name], [Price], [Quantity]) VALUES (103, N'Mobile', 3000, 200)
INSERT [dbo].[Product] ([ProductID], [Name], [Price], [Quantity]) VALUES (104, N'Tablet', 4000, 250)
GO
INSERT [dbo].[ProductSales] ([ProductSalesId], [ProductId], [QuantitySold]) VALUES (1, 101, 10)
INSERT [dbo].[ProductSales] ([ProductSalesId], [ProductId], [QuantitySold]) VALUES (2, 102, 15)
INSERT [dbo].[ProductSales] ([ProductSalesId], [ProductId], [QuantitySold]) VALUES (3, 103, 30)
INSERT [dbo].[ProductSales] ([ProductSalesId], [ProductId], [QuantitySold]) VALUES (4, 104, 35)
INSERT [dbo].[ProductSales] ([ProductSalesId], [ProductId], [QuantitySold]) VALUES (5, 101, 10)
GO
/****** Object:  StoredProcedure [dbo].[spProduct]    Script Date: 25/03/2023 13:32:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create proc [dbo].[spProduct]
AS
Select * from Product
GO
/****** Object:  StoredProcedure [dbo].[spSellProduct]    Script Date: 25/03/2023 13:32:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spSellProduct]
@ProductID INT,
@QuantityToSell INT
AS
BEGIN
  -- First we need to check the stock available for the product we want to sell
  DECLARE @StockAvailable INT
  SELECT @StockAvailable = Quantity
  FROM Product 
  WHERE ProductId = @ProductId
  -- We need to throw an error to the calling application 
  -- if the stock is less than the quantity we want to sell
  IF(@StockAvailable< @QuantityToSell)
  BEGIN
    Raiserror('Enough Stock is not available',16,1)
  END
  -- If enough stock is available
  ELSE
  BEGIN
    BEGIN TRY
      -- We need to start the transaction
      BEGIN TRANSACTION
      -- First we need to reduce the quantity available
      UPDATE Product SET 
          Quantity = (Quantity - @QuantityToSell)
      WHERE ProductID = @ProductID
      -- Calculate MAX ProductSalesId
      DECLARE @MaxProductSalesId INT
      SELECT @MaxProductSalesId = CASE 
          WHEN MAX(ProductSalesId) IS NULL THEN 0 
          ELSE MAX(ProductSalesId) 
          END 
      FROM ProductSales
      -- Increment @MaxProductSalesId by 1, so we don't get a primary key violation
      Set @MaxProductSalesId = @MaxProductSalesId + 1
      -- We need to insert the quantity sold into the ProductSales table
      INSERT INTO ProductSales(ProductSalesId, ProductId, QuantitySold)
      VALUES(@MaxProductSalesId, @ProductId, @QuantityToSell)
      -- Finally Commit the transaction
      COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
      ROLLBACK TRANSACTION
    END CATCH
  End
END
GO

