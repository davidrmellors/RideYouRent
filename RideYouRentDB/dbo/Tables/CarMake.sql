CREATE TABLE [dbo].[CarMake] (
    [makeID]   INT           IDENTITY (1, 1) NOT NULL,
    [makeName] VARCHAR (255) NOT NULL,
    CONSTRAINT [PK_makeID] PRIMARY KEY CLUSTERED ([makeID] ASC)
);

