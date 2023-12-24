CREATE TABLE [dbo].[Driver] (
    [driverID]           INT           IDENTITY (1, 1) NOT NULL,
    [driverName]         VARCHAR (255) NOT NULL,
    [driverAddress]      VARCHAR (255) NOT NULL,
    [driverEmail]        VARCHAR (255) NOT NULL,
    [driverMobileNumber] VARCHAR (10)  NOT NULL,
    CONSTRAINT [PK_driverID] PRIMARY KEY CLUSTERED ([driverID] ASC)
);

