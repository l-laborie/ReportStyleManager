CREATE PROCEDURE [dbo].[up_ReportStyle_by_StyleId]
    @ReportStyleId INT
AS
DECLARE @columns NVARCHAR(MAX);

SELECT @columns = STUFF(
(
    SELECT ', ' + QUOTENAME(ElementName, '[') AS [text()]
    FROM (
        SELECT
            e.[ElementName]
            , ISNULL(rse.ElementValue, e.ElementDefaultValue) AS Value
        FROM [Element] e WITH (NOLOCK)
            LEFT JOIN [ReportStyleElement] rse WITH (NOLOCK)
                ON rse.[ElementId] = e.[Id]
                AND rse.[ReportStyleId] = @ReportStyleId
        ) ReportStyleElements
        FOR XML PATH ('')
    )
    , 1
    , 1
    , ''
);

DECLARE @sql NVARCHAR(MAX);
SET @sql = 'SELECT ' + @columns + '
FROM (
SELECT e.[ElementName] , ISNULL(ElementValue,ElementDefaultValue) AS Value
FROM  [Element] e WITH (NOLOCK)
LEFT JOIN [ReportStyleElement] rse WITH (NOLOCK) ON
rse.[ElementId] = e.[Id] AND rse.[ReportStyleId] = ' + CAST(@ReportStyleId AS VARCHAR (9)) + '
) AS ReportStyleElements
PIVOT ( MIN(Value) FOR ElementName IN (' + @columns + ')) AS [Elements]';

EXEC sp_ExecuteSQL @sql
