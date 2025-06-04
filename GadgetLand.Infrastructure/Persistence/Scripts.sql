CREATE PROCEDURE GetProductsForAdminTable
AS
BEGIN
	SELECT
		P.Id,
		P.Name,
		P.Image,
		P.Price,
		P.DiscountPrice,
		P.QuantityInStock,

		C.Id,
		C.Name,

		B.Id,
		B.Name
	FROM Products AS P
	INNER JOIN Categories AS C
	ON P.CategoryId = C.Id
	INNER JOIN Brands AS B
	ON P.BrandId = B.Id
END

EXEC GetProductsForAdminTable

--
--
--

ALTER PROCEDURE GetProductsWithFilters
	@CategorySlug NVARCHAR(256),
	@BrandSlug NVARCHAR(256),
	@OnlyDiscounted BIT,
	@SortOrder NVARCHAR(50),
	@PageIndex INT,
	@PageSize INT
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
	WHERE (@CategorySlug IS NULL OR C.Slug = @CategorySlug)
		AND (@BrandSlug IS NULL OR B.Slug = @BrandSlug)
		AND (@OnlyDiscounted = 0 OR (@OnlyDiscounted = 1 AND DiscountPrice IS NOT NULL))
	ORDER BY
		CASE WHEN @SortOrder = 'latest' THEN P.Id END DESC,
		CASE WHEN @SortOrder = 'oldest' THEN P.Id END ASC,
		CASE WHEN @SortOrder = 'cheapest' THEN P.Price END ASC,
		CASE WHEN @SortOrder = 'expensive' THEN P.Price END DESC
	OFFSET (@PageIndex - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY
END

--
--
--
