namespace GadgetLand.Infrastructure.Persistence;

public static class Scripts
{
    public const string SpGetProductsWithFilters = """
                                                   CREATE OR ALTER PROCEDURE SP_GetProductsWithFilters
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
}
