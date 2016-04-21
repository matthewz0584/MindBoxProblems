CREATE TABLE [Sales] (
  [salesid] int IDENTITY (1,1) NOT NULL
, [productid] int NOT NULL
, [datetime] datetime NOT NULL
, [customerid] int NOT NULL
);
GO
ALTER TABLE [Sales] ADD CONSTRAINT [PK_Sales] PRIMARY KEY ([salesid]);
GO

SELECT productid, COUNT(firstPurchaseTime)
  FROM Sales
  LEFT JOIN 
  	(SELECT customerid, MIN(datetime) firstPurchaseTime
  		FROM Sales
		GROUP BY customerid) FirstPurchaseTimes
  ON FirstPurchaseTimes.customerid = Sales.customerid AND FirstPurchaseTimes.firstPurchaseTime = Sales.datetime
  GROUP BY productid
GO