### Query With No SplitQuery with projection using Extension method
SELECT [t].[Id], [t].[Code], [t].[CodeUnique], [t].[ColorCode], [t].[MaterialTypeId], [t].[Name], [t].[RegionalMarketId], [t].[Unit], [t].[Varian], [t].[Weight], [t].[Width], [t0].[Id], [t0].[MaterialId], [t0].[MinQuantity], [t0].[Price], [t0].[Surcharge], [t0].[TransactionalPartnerId], [t0].[Id0], [t0].[Address_City], [t0].[Address_District], [t0].[Address_Street], [t0].[Address_Ward], [t0].[Address_Zipcode], [t0].[Name], [t0].[TaxNo], [t0].[TransactionalPartnerTypeId], [t0].[Website]
      FROM (
          SELECT TOP(1) [m].[Id], [m].[Code], [m].[CodeUnique], [m].[ColorCode], [m].[MaterialTypeId], [m].[Name], [m].[RegionalMarketId], [m].[Unit], [m].[Varian], [m].[Weight], [m].[Width]
          FROM [Material] AS [m]
          WHERE [m].[Id] = @__id_0
      ) AS [t]
      LEFT JOIN (
          SELECT [m0].[Id], [m0].[MaterialId], [m0].[MinQuantity], [m0].[Price], [m0].[Surcharge], [m0].[TransactionalPartnerId], [t1].[Id] AS [Id0], [t1].[Address_City], [t1].[Address_District], [t1].[Address_Street], [t1].[Address_Ward], [t1].[Address_Zipcode], [t1].[Name], [t1].[TaxNo], [t1].[TransactionalPartnerTypeId], [t1].[Website]
          FROM [MaterialCostManagement] AS [m0]
          INNER JOIN [TransactionalPartner] AS [t1] ON [m0].[TransactionalPartnerId] = [t1].[Id]
      ) AS [t0] ON [t].[Id] = [t0].[MaterialId]
      ORDER BY [t].[Id], [t0].[Id]


### Query With SplitQuery with projection using Extension method
SELECT TOP(1) [m].[Id], [m].[Code], [m].[CodeUnique], [m].[ColorCode], [m].[MaterialTypeId], [m].[Name], [m].[RegionalMarketId], [m].[Unit], [m].[Varian], [m].[Weight], [m].[Width]
      FROM [Material] AS [m]
      WHERE [m].[Id] = @__id_0
      ORDER BY [m].[Id]

SELECT [t0].[Id], [t0].[MaterialId], [t0].[MinQuantity], [t0].[Price], [t0].[Surcharge], [t0].[TransactionalPartnerId], [t0].[Id0], [t0].[Address_City], [t0].[Address_District], [t0].[Address_Street], [t0].[Address_Ward], [t0].[Address_Zipcode], [t0].[Name], [t0].[TaxNo], [t0].[TransactionalPartnerTypeId], [t0].[Website], [t].[Id]
FROM (
    SELECT TOP(1) [m].[Id]
    FROM [Material] AS [m]
    WHERE [m].[Id] = @__id_0
) AS [t]
INNER JOIN (
    SELECT [m0].[Id], [m0].[MaterialId], [m0].[MinQuantity], [m0].[Price], [m0].[Surcharge], [m0].[TransactionalPartnerId], [t1].[Id] AS [Id0], [t1].[Address_City], [t1].[Address_District], [t1].[Address_Street], [t1].[Address_Ward], [t1].[Address_Zipcode], [t1].[Name], [t1].[TaxNo], [t1].[TransactionalPartnerTypeId], [t1].[Website]
    FROM [MaterialCostManagement] AS [m0]
    INNER JOIN [TransactionalPartner] AS [t1] ON [m0].[TransactionalPartnerId] = [t1].[Id]
) AS [t0] ON [t].[Id] = [t0].[MaterialId]
ORDER BY [t].[Id]

### Query With SplitQuery with manual projection
SELECT TOP(1) [m].[Id], [m].[Code], [m].[Name], [m].[ColorCode], [m].[Unit], [m].[Varian], [m].[Weight], [m].[Width], [m].[MaterialTypeId], [m].[RegionalMarketId]
    FROM [Material] AS [m]
    WHERE [m].[Id] = @__id_0
    ORDER BY [m].[Id]

SELECT [t0].[MinQuantity], [t0].[Price], [t0].[Surcharge], [t0].[Id], [t0].[Name], [t].[Id]
FROM (
    SELECT TOP(1) [m].[Id]
    FROM [Material] AS [m]
    WHERE [m].[Id] = @__id_0
) AS [t]
INNER JOIN (
    SELECT [m0].[MinQuantity], [m0].[Price], [m0].[Surcharge], [t1].[Id], [t1].[Name], [m0].[MaterialId]
    FROM [MaterialCostManagement] AS [m0]
    INNER JOIN [TransactionalPartner] AS [t1] ON [m0].[TransactionalPartnerId] = [t1].[Id]
) AS [t0] ON [t].[Id] = [t0].[MaterialId]
ORDER BY [t].[Id]

As we can see, with manual projection, we can improve performance by just selecting columns for our purpose. With using projection through
extension method, they will get all columns and result in poor performance. So in some scenario's performance, we will move to manual 
projection