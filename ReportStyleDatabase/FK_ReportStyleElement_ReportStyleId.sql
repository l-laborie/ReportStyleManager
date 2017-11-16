ALTER TABLE [dbo].[ReportStyleElement]
    ADD CONSTRAINT [FK_ReportStyleElement_ReportStyleId]
    FOREIGN KEY (ReportStyleId)
    REFERENCES [ReportStyle] (id)
