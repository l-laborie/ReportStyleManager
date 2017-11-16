CREATE TABLE [dbo].[ReportStyleAssignation]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [StyleId] INT NOT NULL,
    [ReportName] NVARCHAR(255) NOT NULL,
    [ReportParameter] NVARCHAR(255) NULL,
    CONSTRAINT [UC_ReportStyleAssignation] UNIQUE ([ReportName], [ReportParameter])
)
