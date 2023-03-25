/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [ProductID]
      ,[Name]
      ,[Price]
      ,[Quantity]
  FROM [transacciones].[dbo].[Product]