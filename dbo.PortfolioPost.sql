CREATE TABLE [dbo].[PortfolioPost] (
    [PortfolioPostID] INT         NOT NULL,
    [Title]           NCHAR (120) NOT NULL,
    [Description]     NCHAR (255) NOT NULL,
    [DateCreated]     DATE        NULL,
    [DateUpdated]     DATE        NULL, 
    CONSTRAINT [PK_PortfolioPost] PRIMARY KEY ([PortfolioPostID])
);

