CREATE TABLE [dbo].[Expenses] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [Amount]      FLOAT            NOT NULL,
    CONSTRAINT [PK_Dashboard] PRIMARY KEY CLUSTERED ([Id] ASC)
);

