CREATE PROCEDURE GetProductsWithDetails
AS
BEGIN
	SELECT
		P.Id,
		P.CategoryId,
		P.BrandId,
		P.Name,
		P.Slug,
		P.Image,
		P.Price,
		P.DiscountPrice,
		P.QuantityInStock,
		P.Description,

		C.Id,
		C.Name,
		C.Slug,
		C.Image,

		B.Id,
		B.Name,
		B.Slug,
		B.Image,

		PI.Id,
		PI.Image
	FROM Products AS P
	INNER JOIN Categories AS C
	ON P.CategoryId = C.Id
	INNER JOIN Brands AS B
	ON P.BrandId = B.Id
	LEFT JOIN ProductImages AS PI
	ON P.Id = PI.ProductId
END
