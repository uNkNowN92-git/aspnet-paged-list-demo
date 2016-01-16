SELECT * FROM Persons
SELECT * FROM Authors
SELECT * FROM Books

SET IDENTITY_INSERT Books ON


INSERT INTO Books
([AuthorId], [Description])
VALUES(1, 'ASDFsdssda')
GO


SET IDENTITY_INSERT Authors OFF
INSERT INTO Authors
([PersonId])
VALUES(1)
GO

INSERT INTO Persons
([FirstName], [LastName])
VALUES('Test', 'ASDFsdssda')
GO

SELECT 
    [Project1].[C1] AS [C1], 
    [Project1].[BookId] AS [BookId], 
    [Project1].[Title] AS [Title], 
    [Project1].[Description] AS [Description], 
    [Project1].[C2] AS [C2], 
    [Project1].[PublishDate] AS [PublishDate]
    FROM ( SELECT 
        [Extent1].[BookId] AS [BookId], 
        [Extent1].[Title] AS [Title], 
        [Extent1].[Description] AS [Description], 
        [Extent1].[PublishDate] AS [PublishDate], 
        1 AS [C1], 
        CASE WHEN ([Extent3].[FirstName] IS NULL) THEN N'' ELSE [Extent3].[FirstName] END + N' ' + CASE WHEN ([Extent3].[LastName] IS NULL) THEN N'' ELSE [Extent3].[LastName] END AS [C2]
        FROM   [dbo].[Books] AS [Extent1]
        INNER JOIN [dbo].[Authors] AS [Extent2] ON [Extent1].[AuthorId] = [Extent2].[AuthorId]
        INNER JOIN [dbo].[Persons] AS [Extent3] ON [Extent2].[PersonId] = [Extent3].[PersonId]
    )  AS [Project1]
    ORDER BY [Project1].[BookId] ASC
    OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY 


SELECT 
    [Project1].[C1] AS [C1], 
    [Project1].[BookId] AS [BookId], 
    [Project1].[Title] AS [Title], 
    [Project1].[Description] AS [Description], 
    [Project1].[C2] AS [C2], 
    [Project1].[PublishDate] AS [PublishDate]
    FROM ( SELECT 
        [Extent1].[BookId] AS [BookId], 
        [Extent1].[Title] AS [Title], 
        [Extent1].[Description] AS [Description], 
        [Extent1].[PublishDate] AS [PublishDate], 
        1 AS [C1], 
        CASE WHEN ([Extent3].[FirstName] IS NULL) THEN N'' ELSE [Extent3].[FirstName] END + N' ' + CASE WHEN ([Extent3].[LastName] IS NULL) THEN N'' ELSE [Extent3].[LastName] END AS [C2]
        FROM   [dbo].[Books] AS [Extent1]
        INNER JOIN [dbo].[Authors] AS [Extent2] ON [Extent1].[AuthorId] = [Extent2].[AuthorId]
        INNER JOIN [dbo].[Persons] AS [Extent3] ON [Extent2].[PersonId] = [Extent3].[PersonId]
        WHERE [Extent1].[Title] LIKE '%a%'
    )  AS [Project1]
    ORDER BY [Project1].[BookId] ASC
    OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY

SELECT 
    [Project1].[C1] AS [C1], 
    [Project1].[BookId] AS [BookId], 
    [Project1].[Title] AS [Title], 
    [Project1].[Description] AS [Description], 
    [Project1].[C2] AS [C2], 
    [Project1].[PublishDate] AS [PublishDate]
    FROM ( SELECT 
        [Filter1].[BookId] AS [BookId], 
        [Filter1].[Title] AS [Title], 
        [Filter1].[Description] AS [Description], 
        [Filter1].[PublishDate] AS [PublishDate], 
        1 AS [C1], 
        CASE WHEN ([Extent3].[FirstName] IS NULL) THEN N'' ELSE [Extent3].[FirstName] END + N' ' + CASE WHEN ([Extent3].[LastName] IS NULL) THEN N'' ELSE [Extent3].[LastName] END AS [C2]
        FROM   (SELECT [Extent1].[BookId] AS [BookId], [Extent1].[Title] AS [Title], [Extent1].[Description] AS [Description], [Extent1].[PublishDate] AS [PublishDate], [Extent2].[PersonId] AS [PersonId]
            FROM  [dbo].[Books] AS [Extent1]
            INNER JOIN [dbo].[Authors] AS [Extent2] ON [Extent1].[AuthorId] = [Extent2].[AuthorId]
            WHERE ([Extent1].[Title] LIKE '%a%') AND ([Extent1].[Description] LIKE '%a%') ) AS [Filter1]
        INNER JOIN [dbo].[Persons] AS [Extent3] ON [Filter1].[PersonId] = [Extent3].[PersonId]
        WHERE ([Extent3].[LastName] LIKE '%s%') OR ([Extent3].[FirstName] LIKE '%s%')
    )  AS [Project1]
    ORDER BY [Project1].[BookId] ASC
    OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY 
