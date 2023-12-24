CREATE TABLE [dbo].[Car] (
    [carID]               VARCHAR (6)   NOT NULL,
    [makeID]              INT           NOT NULL,
    [bodyTypeID]          INT           NOT NULL,
    [carModel]            VARCHAR (255) NOT NULL,
    [kilometersTravelled] INT           NOT NULL,
    [serviceKilometers]   INT           NOT NULL,
    [available]           BIT           NOT NULL,
    [serviceDue]          BIT           NOT NULL,
    CONSTRAINT [PK_carID] PRIMARY KEY CLUSTERED ([carID] ASC),
    CONSTRAINT [FK_bodyTypeID] FOREIGN KEY ([bodyTypeID]) REFERENCES [dbo].[CarBodyType] ([bodyTypeID]),
    CONSTRAINT [FK_makeID] FOREIGN KEY ([makeID]) REFERENCES [dbo].[CarMake] ([makeID])
);

