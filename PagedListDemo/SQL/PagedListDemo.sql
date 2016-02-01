-- SqlLocalDb info PagedListDemo
-- sqlcmd -S (localDB)\PagedListDemo

-- Create datebase MileageTracker
IF DB_ID('PagedListDemo') IS NOT NULL
    DROP DATABASE PagedListDemo
GO

-- CREATE DATABASE MileageTracker ON PRIMARY (
--     NAME = 'MileageTracker'
--     ,FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\MileageTracker.mdf'
--     ,FILEGROWTH = 1024 KB
--     ) LOG ON (
--     NAME = 'MileageTracker_log'
--     ,FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\MileageTracker_log.ldf'
--     ,FILEGROWTH = 10 %
--     );
-- GO
CREATE DATABASE PagedListDemo
GO

-- Create table in PagedListDemo database
USE PagedListDemo
GO

IF OBJECT_ID('Books', 'U') IS NOT NULL
    TRUNCATE TABLE Books
    DROP TABLE Books
GO

--CREATE TABLE Books (
--     BookId         BIGINT IDENTITY(1, 1)   NOT NULL CONSTRAINT PK_BookId PRIMARY KEY
--    ,Title          VARCHAR(50) NULL
--    ,Description    VARCHAR(50) NULL
--    ,Author         VARCHAR(50) NULL
--    ,AcceptAndAgree BIT DEFAULT 0
--    )
--GO

CREATE TABLE [dbo].[Books] (
    [BookId]      BIGINT       IDENTITY (1, 1) NOT NULL CONSTRAINT PK_BookId PRIMARY KEY,
    [Title]       VARCHAR (50) NULL,
    [Description] VARCHAR (50) NULL,
    [AuthorId]    BIGINT       NOT NULL,
    [PublishDate] DATETIME     NULL,
    CONSTRAINT [PK_BookId] PRIMARY KEY CLUSTERED ([BookId] ASC),
    CONSTRAINT [FK_Books_Author] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Authors] ([AuthorId])
);
GO

IF OBJECT_ID('Authors', 'U') IS NOT NULL
    TRUNCATE TABLE Authors
    DROP TABLE Authors
GO

CREATE TABLE [dbo].[Authors] (
    [AuthorId]  BIGINT       IDENTITY (1, 1) NOT NULL,
    [FirstName] VARCHAR (50) NULL,
    [LastName]  VARCHAR (50) NULL,
    [BirthDate] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([AuthorId] ASC)
);
GO
