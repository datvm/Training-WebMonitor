﻿CREATE TABLE [dbo].[Url] (
    [Id]  INT             IDENTITY (1, 1) NOT NULL,
    [Url] NVARCHAR (3000) NOT NULL,
    CONSTRAINT [PK_Url] PRIMARY KEY CLUSTERED ([Id] ASC)
);

