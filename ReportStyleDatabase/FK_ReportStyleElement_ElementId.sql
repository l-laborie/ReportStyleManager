ALTER TABLE [dbo].[ReportStyleElement]
    ADD CONSTRAINT [FK_ReportStyleElement_ElementId]
    FOREIGN KEY (ElementId)
    REFERENCES [Element] (id)
