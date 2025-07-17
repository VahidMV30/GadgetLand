namespace GadgetLand.Infrastructure.Persistence;

public static class Scripts
{
    public const string DboProductsWithFilters = """
                                                 CREATE OR ALTER PROCEDURE dbo.ProductsWithFilters
                                                 	@CategorySlug NVARCHAR(256) = NULL,
                                                 	@BrandSlug NVARCHAR(256) = NULL,
                                                 	@OnlyDiscounted BIT = 0,
                                                 	@SortOrder NVARCHAR(50) = 'latest',
                                                 	@PageIndex INT = 1,
                                                 	@PageSize INT = 10
                                                 AS
                                                 BEGIN
                                                 	SET NOCOUNT ON;

                                                 	SELECT
                                                 		COUNT(*) AS TotalCount
                                                 		FROM Products AS P
                                                 		INNER JOIN Categories AS C
                                                 		ON P.CategoryId = C.Id
                                                 		INNER JOIN Brands AS B
                                                 		ON P.BrandId = B.Id
                                                 		WHERE (@CategorySlug IS NULL OR C.Slug = @CategorySlug)
                                                 			AND (@BrandSlug IS NULL OR B.Slug = @BrandSlug)
                                                 			AND (@OnlyDiscounted = 0 OR (@OnlyDiscounted = 1 AND DiscountPrice IS NOT NULL));

                                                 	WITH CTE_TopSellingProducts AS (
                                                 		SELECT
                                                 			OI.ProductId,
                                                 			SUM(COALESCE(QUANTITY, 0)) AS TopSales
                                                 		FROM Products AS P
                                                 		INNER JOIN OrderItems AS OI
                                                 		ON P.Id = OI.ProductId
                                                 		GROUP BY OI.ProductId
                                                 	)
                                                 	SELECT
                                                 		P.Name,
                                                 		P.Slug,
                                                 		P.Image,
                                                 		P.Price,
                                                 		P.DiscountPrice
                                                 	FROM Products AS P
                                                 	INNER JOIN Categories AS C
                                                 	ON P.CategoryId = C.Id
                                                 	INNER JOIN Brands AS B
                                                 	ON P.BrandId = B.Id
                                                 	LEFT JOIN CTE_TopSellingProducts AS CTE_TSP
                                                 	ON P.Id = CTE_TSP.ProductId
                                                 	WHERE (@CategorySlug IS NULL OR C.Slug = @CategorySlug)
                                                 		AND (@BrandSlug IS NULL OR B.Slug = @BrandSlug)
                                                 		AND (@OnlyDiscounted = 0 OR (@OnlyDiscounted = 1 AND DiscountPrice IS NOT NULL))
                                                 	ORDER BY
                                                 		CASE WHEN @SortOrder = 'latest' THEN P.Id END DESC,
                                                 		CASE WHEN @SortOrder = 'oldest' THEN P.Id END ASC,
                                                 		CASE WHEN @SortOrder = 'cheapest' THEN P.Price END ASC,
                                                 		CASE WHEN @SortOrder = 'expensive' THEN P.Price END DESC,
                                                 		CASE WHEN @SortOrder = 'topSales' THEN CTE_TSP.TopSales END DESC
                                                 	OFFSET (@PageIndex - 1) * @PageSize ROWS
                                                 	FETCH NEXT @PageSize ROWS ONLY
                                                 END
                                                 """;

    public const string DboAdminDashboardWidgets = """
                                                   CREATE OR ALTER PROCEDURE dbo.AdminDashboardWidgets
                                                     @StartOfCurrentMonth DATETIME,
                                                     @StartOfNextMonth DATETIME,
                                                     @StartOfToday DATETIME,
                                                     @StartOfTomorrow DATETIME
                                                   AS
                                                   BEGIN
                                                   	SELECT
                                                   		COUNT(*) AS ProductsCount
                                                   	FROM Products;

                                                   	SELECT
                                                   		COUNT(*) AS UsersCount
                                                   	FROM Users;

                                                   	SELECT
                                                   		COUNT(*) AS OrdersCount
                                                   	FROM Orders;

                                                   	SELECT
                                                   		SUM(TotalPayableAmount) AS TotalSales
                                                   	FROM Orders

                                                   	SELECT
                                                   		SUM(TotalPayableAmount) AS CurrentMonthSales
                                                   	FROM Orders
                                                   	WHERE OrderDate >= @StartOfCurrentMonth AND OrderDate < @StartOfNextMonth

                                                   	SELECT
                                                   		SUM(TotalPayableAmount) AS TodaySales
                                                   	FROM Orders
                                                   	WHERE OrderDate >= @StartOfToday AND OrderDate < @StartOfTomorrow;
                                                   END
                                                   """;

    public const string DboGetPersianMonthName = """
                                                 CREATE OR ALTER FUNCTION dbo.GetPersianMonthName(@Date DATETIME)
                                                     RETURNS NVARCHAR(20)
                                                 AS
                                                 BEGIN
                                                     DECLARE @GregYear INT = YEAR(@Date)
                                                     DECLARE @GregMonth INT = MONTH(@Date)
                                                     DECLARE @GregDay INT = DAY(@Date)

                                                     DECLARE @DayOfYear INT = 
                                                         CASE @GregMonth
                                                             WHEN 1 THEN 0
                                                             WHEN 2 THEN 31
                                                             WHEN 3 THEN 59
                                                             WHEN 4 THEN 90
                                                             WHEN 5 THEN 120
                                                             WHEN 6 THEN 151
                                                             WHEN 7 THEN 181
                                                             WHEN 8 THEN 212
                                                             WHEN 9 THEN 243
                                                             WHEN 10 THEN 273
                                                             WHEN 11 THEN 304
                                                             WHEN 12 THEN 334
                                                         END + @GregDay

                                                     IF ((@GregYear % 4 = 0 AND @GregYear % 100 != 0) OR (@GregYear % 400 = 0)) 
                                                         AND @GregMonth > 2
                                                         SET @DayOfYear += 1

                                                     DECLARE @PersianMonth INT

                                                     IF @DayOfYear > 79
                                                     BEGIN
                                                         DECLARE @DaysAfterNowruz INT = @DayOfYear - 79
                                                         
                                                         IF @DaysAfterNowruz <= 186 
                                                             SET @PersianMonth = (@DaysAfterNowruz - 1) / 31 + 1
                                                         ELSE 
                                                             SET @PersianMonth = (@DaysAfterNowruz - 187) / 30 + 7
                                                     END
                                                     ELSE
                                                     BEGIN
                                                         DECLARE @DaysBeforeNowruz INT = @DayOfYear + 286
                                                         
                                                         IF @DaysBeforeNowruz <= 336 
                                                             SET @PersianMonth = (@DaysBeforeNowruz - 186) / 30 + 6
                                                         ELSE 
                                                             SET @PersianMonth = 12
                                                     END

                                                     RETURN CASE @PersianMonth
                                                         WHEN 1 THEN N'فروردین'
                                                         WHEN 2 THEN N'اردیبهشت'
                                                         WHEN 3 THEN N'خرداد'
                                                         WHEN 4 THEN N'تیر'
                                                         WHEN 5 THEN N'مرداد'
                                                         WHEN 6 THEN N'شهریور'
                                                         WHEN 7 THEN N'مهر'
                                                         WHEN 8 THEN N'آبان'
                                                         WHEN 9 THEN N'آذر'
                                                         WHEN 10 THEN N'دی'
                                                         WHEN 11 THEN N'بهمن'
                                                         WHEN 12 THEN N'اسفند'
                                                     END
                                                 END
                                                 """;

    public const string DboSalesByPersianMonthOfYear = """
                                                       CREATE OR ALTER PROCEDURE dbo.SalesByPersianMonthOfYear
                                                           @StartOfCurrentPersianYear DATETIME,
                                                           @StartOfNextPersianYear DATETIME
                                                       AS
                                                       BEGIN
                                                           WITH CTE_PersianMonths AS (
                                                               SELECT 1 AS MonthNumber, N'فروردین' AS PersianMonthName UNION ALL
                                                               SELECT 2, N'اردیبهشت' UNION ALL
                                                               SELECT 3, N'خرداد' UNION ALL
                                                               SELECT 4, N'تیر' UNION ALL
                                                               SELECT 5, N'مرداد' UNION ALL
                                                               SELECT 6, N'شهریور' UNION ALL
                                                               SELECT 7, N'مهر' UNION ALL
                                                               SELECT 8, N'آبان' UNION ALL
                                                               SELECT 9, N'آذر' UNION ALL
                                                               SELECT 10, N'دی' UNION ALL
                                                               SELECT 11, N'بهمن' UNION ALL
                                                               SELECT 12, N'اسفند'
                                                           ),
                                                           CTE_Sales AS (
                                                               SELECT
                                                                   dbo.GetPersianMonthName(OrderDate) AS PersianMonthName,
                                                                   SUM(TotalPayableAmount) AS Sales
                                                               FROM Orders
                                                               WHERE OrderDate >= @StartOfCurrentPersianYear AND OrderDate < @StartOfNextPersianYear
                                                               GROUP BY dbo.GetPersianMonthName(OrderDate)
                                                           )
                                                           SELECT
                                                               CTE_PM.PersianMonthName,
                                                               COALESCE(CTE_S.Sales, 0) AS Sales
                                                           FROM CTE_PersianMonths AS CTE_PM
                                                           LEFT JOIN CTE_Sales AS CTE_S
                                                           ON CTE_PM.PersianMonthName = CTE_S.PersianMonthName
                                                           ORDER BY CTE_PM.MonthNumber
                                                       END
                                                       """;

    public const string DboTopFiveCitiesBySalesOfYear = """
                                                        CREATE OR ALTER PROCEDURE dbo.TopFiveCitiesBySalesOfYear
                                                            @StartOfCurrentPersianYear DATETIME,
                                                            @StartOfNextPersianYear DATETIME
                                                        AS
                                                        BEGIN
                                                            SELECT TOP 5
                                                                C.Name AS City,
                                                                SUM(O.TotalPayableAmount) AS Sales
                                                            FROM Orders AS O
                                                            INNER JOIN Users AS U
                                                            ON O.UserId = U.Id
                                                            INNER JOIN Cities AS C
                                                            ON U.CityId = C.Id
                                                            WHERE OrderDate >= @StartOfCurrentPersianYear AND OrderDate < @StartOfNextPersianYear
                                                            GROUP BY C.Name
                                                            ORDER BY Sales DESC
                                                        END
                                                        """;

    public const string DboTopFiveProvincesBySalesOfYear = """
                                                           CREATE OR ALTER PROCEDURE dbo.TopFiveProvincesBySalesOfYear
                                                           	@StartOfCurrentPersianYear DATETIME,
                                                           	@StartOfNextPersianYear DATETIME
                                                           AS
                                                           BEGIN
                                                           	SELECT TOP 5
                                                           		P.Name AS Province,
                                                           		SUM(O.TotalPayableAmount) AS Sales
                                                           	FROM Orders AS O
                                                           	INNER JOIN Users AS U
                                                           	ON O.UserId = U.Id
                                                           	INNER JOIN Cities AS C
                                                           	ON U.CityId = C.Id
                                                           	INNER JOIN Provinces AS P
                                                           	ON C.ProvinceId = P.Id
                                                           	WHERE O.OrderDate >= @StartOfCurrentPersianYear AND O.OrderDate < @StartOfNextPersianYear
                                                           	GROUP BY P.Name
                                                           	ORDER BY Sales DESC
                                                           END
                                                           """;

    public const string DboUserDashboardWidgets = """
                                                  CREATE OR ALTER PROCEDURE dbo.UserDashboardWidgets
                                                  	@UserId INT,
                                                  	@StartOfCurrentMonth DATETIME,
                                                  	@StartOfNextMonth DATETIME
                                                  AS
                                                  BEGIN

                                                  	SELECT
                                                  		COUNT(*) AS TotalOrders
                                                  	FROM Orders
                                                  	WHERE UserId = @UserId

                                                  	SELECT
                                                  		SUM(TotalPayableAmount) AS TotalPurchase
                                                  	FROM Orders
                                                  	WHERE UserId = @UserId

                                                  	SELECT
                                                  		COALESCE(SUM(TotalPayableAmount), 0) AS CurrentMonthPurchase
                                                  	FROM Orders
                                                  	WHERE UserId = @UserId AND OrderDate >= @StartOfCurrentMonth AND OrderDate < @StartOfNextMonth

                                                  	SELECT
                                                  		COUNT(*) AS PendingOrders
                                                  	FROM Orders
                                                  	WHERE UserId = @UserId AND OrderStatus = 'Pending'

                                                  	SELECT
                                                  		COUNT(*) AS ProcessingOrders
                                                  	FROM Orders
                                                  	WHERE UserId = @UserId AND OrderStatus = 'Processing'

                                                  	SELECT
                                                  		COUNT(*) AS ShippedOrders
                                                  	FROM Orders
                                                  	WHERE UserId = @UserId AND OrderStatus = 'Shipped'
                                                  END
                                                  """;
}
