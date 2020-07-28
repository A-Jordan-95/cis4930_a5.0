CREATE TABLE [dbo].[Class]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CourseId] INT NULL, 
    [SemesterId] INT NULL, 
    [instructor] NVARCHAR(50) NULL, 
    [syllabus] VARBINARY(MAX) NULL, 
    [canvasLink] NVARCHAR(255) NULL, 
    [enrollment] INT NULL
)
