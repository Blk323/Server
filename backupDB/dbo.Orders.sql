CREATE TABLE [dbo].[Orders] (
    [Id]        INT           NOT NULL,
    [ProductId] INT           NOT NULL,
    [Count]     INT           NOT NULL,
    [Name]      VARCHAR (30)  NOT NULL,
    [Date]      DATETIME2 (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

