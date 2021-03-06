﻿CREATE TABLE [dbo].[Payment]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [BankPaymentId] UNIQUEIDENTIFIER NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [CurrencyCode] NVARCHAR(3) NOT NULL, 
    [Amount] DECIMAL(18, 2) NOT NULL, 
    [RequestDate] DATETIME2 NOT NULL, 
    [Updated] DATETIME2 NOT NULL, 
    [RequestCompleted] DATETIME2 NULL, 
    [PaymentStatusId] INT NOT NULL, 
    [IsSuccessful] BIT NULL, 
    [Message] NVARCHAR(200) NULL, 
    CONSTRAINT [FK_Payment_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
    CONSTRAINT [FK_Payment_PaymentStatus] FOREIGN KEY ([PaymentStatusId]) REFERENCES [PaymentStatus]([Id])
)
