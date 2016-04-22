CREATE TABLE [Sales] (
  [salesid] int IDENTITY (1,1) NOT NULL
, [productid] int NOT NULL
, [datetime] datetime NOT NULL
, [customerid] int NOT NULL
);
GO
ALTER TABLE [Sales] ADD CONSTRAINT [PK_Sales] PRIMARY KEY ([salesid]);
GO

SET IDENTITY_INSERT [Sales] ON;
GO
INSERT INTO [Sales] ([salesid],[productid],[datetime],[customerid]) VALUES (3,20,{ts '2016-01-01 00:00:00.000'},400);
GO
INSERT INTO [Sales] ([salesid],[productid],[datetime],[customerid]) VALUES (4,30,{ts '2016-01-02 00:00:00.000'},400);
GO
INSERT INTO [Sales] ([salesid],[productid],[datetime],[customerid]) VALUES (5,20,{ts '2016-01-03 00:00:00.000'},500);
GO
INSERT INTO [Sales] ([salesid],[productid],[datetime],[customerid]) VALUES (7,30,{ts '2016-01-04 00:00:00.000'},600);
GO
INSERT INTO [Sales] ([salesid],[productid],[datetime],[customerid]) VALUES (8,40,{ts '2016-01-03 00:00:00.000'},400);
GO
INSERT INTO [Sales] ([salesid],[productid],[datetime],[customerid]) VALUES (9,50,{ts '2016-01-04 00:00:00.000'},600);
GO
SET IDENTITY_INSERT [Sales] OFF;
GO

SELECT productid, COUNT(FirstPurchaseTimes.customerid) AS cnt
  FROM Sales
  LEFT OUTER JOIN 
  	(SELECT customerid, MIN(datetime) AS firstPurchaseTime
  		FROM Sales
		GROUP BY customerid) AS FirstPurchaseTimes
  ON FirstPurchaseTimes.customerid = Sales.customerid AND FirstPurchaseTimes.firstPurchaseTime = Sales.datetime
  GROUP BY productid
GO