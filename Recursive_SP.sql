CREATE PROCEDURE [dbo].[sp_GetCategories]
	@CategoryId int = null
AS
BEGIN

	WITH parents AS 
	(
	  SELECT 
		Id
		, CategoryId
		, CategoryName
		, Case
		WHEN ParentCategoryId IS NULL THEN ''
		ELSE Cast(ParentCategoryId as varchar(200)) 
		END as ParentIds
		, 1 AS relative_depth
	  FROM Categories C

	  UNION ALL

	  SELECT 
		SC.Id	
		, SC.CategoryId
		, SC.CategoryName
		, CASE 
		WHEN SC.ParentCategoryId IS NOT NULL THEN CAST(CONCAT( CAST(SC.ParentCategoryId As varchar(50) ), ',', ParentIds ) AS varchar(200))
		ELSE ParentIds
		END AS ParentIds
		, p.relative_depth + 1 AS relative_depth
	  FROM Categories SC, parents P
	  WHERE SC.ParentCategoryId = P.CategoryId AND p.ParentIds != ''
	)
	SELECT 
		p.* 
	FROM parents p
	INNER JOIN (
		select 
			CategoryId, MAX(relative_depth) As [lastRecord] 
		from parents p2
		WHERE (@CategoryId IS NULL) OR (p2.CategoryId = @CategoryId)
		Group By CategoryId 
	) Last on Last.CategoryId = p.CategoryId AND Last.lastRecord = p.relative_depth 
	order by 1;

END