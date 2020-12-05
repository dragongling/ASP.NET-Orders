CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Number] NVARCHAR(MAX) NOT NULL, 
    [Date] DATETIME2 NOT NULL, 
    [ProviderId] INT NOT NULL, 
    CONSTRAINT [FK_Order_ToProvider] FOREIGN KEY ([ProviderId]) REFERENCES [Provider]([Id])
)
