SET IDENTITY_INSERT Comment ON;

-- Example of inserting data with an explicit identity value
INSERT INTO Comment (Id, PortfolioPostId, Content, Author)
VALUES (1, 123, 'This is a comment', 'Jane Doe');
