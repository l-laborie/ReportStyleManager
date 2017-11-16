ALTER TABLE [dbo].[ReportStyleAssignation]
    ADD CONSTRAINT [FK_ReportStyleAssignation_StyleId]
    FOREIGN KEY (StyleId)
    REFERENCES [ReportStyle] (Id)
