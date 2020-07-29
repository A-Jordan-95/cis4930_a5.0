CREATE TABLE [dbo].[Review]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[RatingOutOfFive] INT NOT NULL,
	[ReviewText] VARCHAR(100) NULL,
)
