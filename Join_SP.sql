USE [InGameDb]
GO
/****** Object:  StoredProcedure [dbo].[SpProductList]    Script Date: 19/03/2019 06:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SpProducts]
AS
BEGIN
    SELECT 
         p.Id,p.Name,p.Price,p.Description,p.PictureUri,sc.CategoryName,sc.CategoryId
	    FROM Products p join Categories sc on p.CategoryId=sc.CategoryId  and p.IsActive=1   
END;
