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
