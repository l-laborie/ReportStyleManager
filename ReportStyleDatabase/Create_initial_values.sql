/*
Post-Deployment Script
--------------------------------------------------------------------------------------
 This file contains SQL statements that will insert in the new Database
 Some initial values.
--------------------------------------------------------------------------------------
*/

DECLARE @current_element_count INT
SELECT @current_element_count = COUNT(*)
FROM [Element]

IF @current_element_count = 0
BEGIN
    INSERT INTO [Element] (ElementName, ElementDefaultValue)
    VALUES
        ('HEADER_Font_Color', 'Black')
        , ('HEADER_Font_FontSize', '24pt')
        , ('HEADER_Font_FontFamily' , 'Segoe UI Light')
        , ('HEADER_Font_FontStyle', 'Normal')
        , ('HEADER_Font_FontWeight', 'Bold')
        , ('HEADER_Default_Background_Color', 'White')
        , ('SUBHEAD_Font_Color', 'Black')
        , ('SUBHEAD_Font_FontSize', '18pt')
        , ('SUBHEAD_Font_FontFamily' , 'Segoe UI Light')
        , ('SUBHEAD_Font_FontStyle', 'Normal')
        , ('SUBHEAD_Font_FontWeight', 'Bold')
        , ('SUBHEAD_Default_Background_Color', 'White')
        , ('TABLE_MAINTITLE_Font_Color', 'Black')
        , ('TABLE_MAINTITLE_Font_FontSize', '12pt')
        , ('TABLE_MAINTITLE_Font_FontFamily' , 'Normal')
        , ('TABLE_MAINTITLE_Font_FontStyle', 'Bold')
        , ('TABLE_MAINTITLE_Font_FontWeight', 'Normal')
        , ('TABLE_MAINTITLE_Default_Background_Color', 'White')
        , ('TABLE_SUBTITLE_Font_Color', 'Black')
        , ('TABLE_SUBTITLE_Font_FontSize', '11pt')
        , ('TABLE_SUBTITLE_Font_FontFamily' , 'Normal')
        , ('TABLE_SUBTITLE_Font_FontStyle', 'Bold')
        , ('TABLE_SUBTITLE_Font_FontWeight', 'Normal')
        , ('TABLE_SUBTITLE_Default_Background_Color', 'White')
        , ('TABLE_STANDARD_Font_Color', '#333333')
        , ('TABLE_STANDARD_Font_FontSize', '10pt')
        , ('TABLE_STANDARD_Font_FontFamily' , 'Normal')
        , ('TABLE_STANDARD_Font_FontStyle', 'Normal')
        , ('TABLE_STANDARD_Font_FontWeight', 'Normal')
        , ('TABLE_STANDARD_Default_Background_Color', 'White')
        , ('TABLE_TOTAL_Font_Color', '#333333')
        , ('TABLE_TOTAL_Font_FontSize', '10pt')
        , ('TABLE_TOTAL_Font_FontFamily' , 'Normal')
        , ('TABLE_TOTAL_Font_FontStyle', 'Bold')
        , ('TABLE_TOTAL_Font_FontWeight', 'Normal')
        , ('TABLE_TOTAL_Default_Background_Color', 'White')
END