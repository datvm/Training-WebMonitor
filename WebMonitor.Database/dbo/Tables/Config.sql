CREATE TABLE [dbo].[Config] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]  VARCHAR (20)   NOT NULL,
    [Value] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Config] PRIMARY KEY CLUSTERED ([Id] ASC)
);

