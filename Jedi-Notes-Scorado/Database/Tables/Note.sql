CREATE TABLE [Note]
(
	ID INT NOT NULL PRIMARY KEY IDENTITY(1,1), --use identity speculation to increment the id's
	Title VARCHAR(256) NOT NULL,
	Body VARCHAR(MAX) NULL,
	Created DATETIME NOT NULL,
	Updated DATETIME NULL,
	[Owner] VARCHAR(1024) NULL, --Some alien members of jedi council will probably have very long names
	[Rank] INT NOT NULL --Will take the numerical value from the eNum c# equivalent here as a type identifier
)