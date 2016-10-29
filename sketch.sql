
DECLARE @offset INT = 0 
DECLARE @amount INT = 1

DECLARE @test INT = IIF(@offset < 0, 0, @offSet * @amount)
	SELECT [Id],
        [LastUpdate],
        [Active],
        [OtherFlag],
        [FKFirstNotNull],
        [FKSecondNull],
        [FKThirdDefaultValue],
        [Labels] FROM [dbo].[Foo] WITH (NOLOCK)
		ORDER BY ID DESC
		OFFSET @test ROWS FETCH NEXT @amount ROWS ONLY 

		sp_executesql @sql


		EXEC Foo_List 0, 20

		DECLARE @OrderBy NVARCHAR(100) = 'ID'
		DECLARE @SortDirection NVARCHAR(100) = 'DESC'
		DECLARE @sql NVARCHAR(MAX) = 
		N'SELECT [Id],
        [LastUpdate],
        [Active],
        [OtherFlag],
        [FKFirstNotNull],
        [FKSecondNull],
        [FKThirdDefaultValue],
        [Labels] 
		FROM [dbo].[Foo] WITH (NOLOCK)
		ORDER BY ' + @OrderBy + N' ' + @SortDirection + N'
		OFFSET @offSet ROWS
		FETCH NEXT @PageQty ROWS ONLY'

		DECLARE @paramArg NVARCHAR(MAX) = N'@offSet INT, @PageQty INT'
		EXEC sp_executesql @sql, @paramArg, 0, 1