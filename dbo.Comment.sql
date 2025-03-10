CREATE TABLE [dbo].[Comment] (
    [Id]        INT NOT NULL,
    [Author]    NCHAR (100) NOT NULL,
    [Content]   NCHAR (500) NOT NULL,
    [CreatedAt] DATETIME    NOT NULL,
    [PortfolioPostId]    INT         NOT NULL,
    CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Comment_Comment] FOREIGN KEY ([Id]) REFERENCES [dbo].[Comment] ([Id])
);

