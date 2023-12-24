CREATE TABLE [dbo].[CarRental] (
    [rentalID]    INT         IDENTITY (1, 1) NOT NULL,
    [carID]       VARCHAR (6) NOT NULL,
    [inspectorID] VARCHAR (4) NOT NULL,
    [driverID]    INT         NOT NULL,
    [rentalFee]   INT         NOT NULL,
    [startDate]   DATE        NOT NULL,
    [endDate]     DATE        NULL,
    CONSTRAINT [PK_rentalID] PRIMARY KEY CLUSTERED ([rentalID] ASC),
    CONSTRAINT [FK_carID] FOREIGN KEY ([carID]) REFERENCES [dbo].[Car] ([carID]),
    CONSTRAINT [FK_driverID] FOREIGN KEY ([driverID]) REFERENCES [dbo].[Driver] ([driverID]),
    CONSTRAINT [FK_inspectorID] FOREIGN KEY ([inspectorID]) REFERENCES [dbo].[Inspector] ([inspectorID])
);

