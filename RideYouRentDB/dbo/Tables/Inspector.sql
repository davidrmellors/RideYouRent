CREATE TABLE [dbo].[Inspector] (
    [inspectorID]           VARCHAR (4)   NOT NULL,
    [inspectorName]         VARCHAR (255) NOT NULL,
    [inspectorEmail]        VARCHAR (255) NOT NULL,
    [inspectorMobileNumber] VARCHAR (10)  NOT NULL,
    CONSTRAINT [PK_inspectorID] PRIMARY KEY CLUSTERED ([inspectorID] ASC)
);

