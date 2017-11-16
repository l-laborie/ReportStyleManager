/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/

INSERT INTO [Element] (ElementName, ElementDefaultValue)
VALUES
    ('HEADER_Font_Color', 'Black')
    , ('HEADER_Font_FontSize', '24pt')
    , ('HEADER_Font_FontFamily' , 'Segoe UI Light')
    , ('HEADER_Font_FontStyle', 'Normal')
    , ('HEADER_Font_FontWeight', 'Bold')
    , ('HEADER_Default_BackgroundColor', 'White')
    , ('SUBHEAD_Font_Color', 'Black')
    , ('SUBHEAD_Font_FontSize', '18pt')
    , ('SUBHEAD_Font_FontFamily' , 'Segoe UI Light')
    , ('SUBHEAD_Font_FontStyle', 'Normal')
    , ('SUBHEAD_Font_FontWeight', 'Bold')
    , ('SUBHEAD_Default_BackgroundColor', 'White')
    , ('TABLE_MAINTITLE_Font_Color', 'Black')
    , ('TABLE_MAINTITLE_Font_FontSize', '12pt')
    , ('TABLE_MAINTITLE_Font_FontFamily' , 'Normal')
    , ('TABLE_MAINTITLE_Font_FontStyle', 'Bold')
    , ('TABLE_MAINTITLE_Font_FontWeight', 'Normal')
    , ('TABLE_MAINTITLE_Default_BackgroundColor', 'White')
    , ('TABLE_SUBTITLE_Font_Color', 'Black')
    , ('TABLE_SUBTITLE_Font_FontSize', '11pt')
    , ('TABLE_SUBTITLE_Font_FontFamily' , 'Normal')
    , ('TABLE_SUBTITLE_Font_FontStyle', 'Bold')
    , ('TABLE_SUBTITLE_Font_FontWeight', 'Normal')
    , ('TABLE_SUBTITLE_Default_BackgroundColor', 'White')
    , ('TABLE_STANDARD_Font_Color', '#333333')
    , ('TABLE_STANDARD_Font_FontSize', '10pt')
    , ('TABLE_STANDARD_Font_FontFamily' , 'Normal')
    , ('TABLE_STANDARD_Font_FontStyle', 'Normal')
    , ('TABLE_STANDARD_Font_FontWeight', 'Normal')
    , ('TABLE_STANDARD_Default_BackgroundColor', 'White')
    , ('TABLE_TOTAL_Font_Color', '#333333')
    , ('TABLE_TOTAL_Font_FontSize', '10pt')
    , ('TABLE_TOTAL_Font_FontFamily' , 'Normal')
    , ('TABLE_TOTAL_Font_FontStyle', 'Bold')
    , ('TABLE_TOTAL_Font_FontWeight', 'Normal')
    , ('TABLE_TOTAL_Default_BackgroundColor', 'White')
