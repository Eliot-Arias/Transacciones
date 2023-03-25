SET IMPLICIT_TRANSACTIONS ON
 begin tran
 insert into [transacciones].[dbo].[ProductSales] values (16, 101, 5)
SELECT TOP (1000) [ProductSalesid]
	,[ProductId]
	,[QuantitySold]
	FROM [transacciones].[dbo].[ProductSales]
print @@trancount
rollback