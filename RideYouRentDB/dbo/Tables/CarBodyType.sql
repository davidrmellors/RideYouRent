CREATE TABLE [dbo].[CarBodyType] (
    [bodyTypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [bodyTypeName] VARCHAR (255) NOT NULL,
    CONSTRAINT [PK_bodyTypeID] PRIMARY KEY CLUSTERED ([bodyTypeID] ASC)
);

