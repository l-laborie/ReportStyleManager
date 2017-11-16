CREATE TABLE [dbo].[ReportStyleElement]
(
    [ReportStyleId] INT NOT NULL,
    [ElementId] INT NOT NULL,
    [ElementValue] VARCHAR(50) NULL,
    CONSTRAINT [PK_ReportStyleElement] PRIMARY KEY ([ReportStyleId], [ElementId]),
    CONSTRAINT [UC_ReportStyleElement] UNIQUE ([ReportStyleId], [ElementId])
)
