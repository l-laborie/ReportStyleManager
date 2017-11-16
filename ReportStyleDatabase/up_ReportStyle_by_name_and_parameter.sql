CREATE PROCEDURE [dbo].[up_ReportStyle_by_name_and_parameter]
	@report_name varchar(255),
	@report_parameter varchar(255) = NULL
AS

DECLARE @style_id int

SELECT
    @style_id = StyleId
FROM ReportStyleAssignation
WHERE ReportName = @report_name
    AND (
        (@report_parameter IS NULL AND ReportParameter IS NULL)
        OR ReportParameter = @report_parameter
    )

SELECT @style_id = ISNULL(@style_id, 0)

EXEC up_ReportStyle_by_StyleId @style_id
