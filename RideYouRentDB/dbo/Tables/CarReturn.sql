CREATE TABLE [dbo].[CarReturn] (
    [returnID]    INT          IDENTITY (1, 1) NOT NULL,
    [rentalID]    INT          NOT NULL,
    [inspectorID] VARCHAR (4)  NOT NULL,
    [driverID]    INT          NOT NULL,
    [returnDate]  DATE         NULL,
    [elapsedDate] INT          NOT NULL,
    [fine]        DECIMAL (18) NULL,
    CONSTRAINT [PK_returnID] PRIMARY KEY CLUSTERED ([returnID] ASC),
    CONSTRAINT [FK_rentalID] FOREIGN KEY ([rentalID]) REFERENCES [dbo].[CarRental] ([rentalID]),
    CONSTRAINT [FK_ReturnDriverID] FOREIGN KEY ([driverID]) REFERENCES [dbo].[Driver] ([driverID]),
    CONSTRAINT [FK_ReturnInspectorID] FOREIGN KEY ([inspectorID]) REFERENCES [dbo].[Inspector] ([inspectorID])
);

