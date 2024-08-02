IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Addresses] (
    [Id] int NOT NULL IDENTITY,
    [Street] varchar(100) NOT NULL,
    [Number] varchar(10) NOT NULL,
    [Complement] varchar(150) NULL,
    [Neighborhood] varchar(100) NOT NULL,
    [FederativeUnit] varchar(2) NOT NULL,
    [City] varchar(100) NOT NULL,
    [ZipCode] varchar(9) NOT NULL,
    CONSTRAINT [PK__Addresse__3214EC07CB7D83E4] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Vehicles] (
    [Id] int NOT NULL IDENTITY,
    [VehicleType] varchar(10) NULL,
    [Brand] varchar(50) NOT NULL,
    [Model] varchar(50) NOT NULL,
    [Color] varchar(50) NOT NULL,
    [VehicleYear] int NULL,
    [Notes] varchar(200) NULL,
    CONSTRAINT [PK__Vehicles__3214EC07D56A8AC5] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Customers] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(100) NOT NULL,
    [BirthDate] date NULL,
    [Cpf] varchar(11) NOT NULL,
    [Phone] varchar(15) NOT NULL,
    [Email] varchar(100) NOT NULL,
    [AddressId] int NULL,
    CONSTRAINT [PK__Customer__3214EC0794F4CDEF] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__Customers__Addre__3A81B327] FOREIGN KEY ([AddressId]) REFERENCES [Addresses] ([Id])
);
GO

CREATE TABLE [CustomerVehicles] (
    [Id] int NOT NULL IDENTITY,
    [CustomerId] int NULL,
    [VehicleId] int NULL,
    CONSTRAINT [PK__Customer__3214EC0773697324] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__CustomerV__Custo__412EB0B6] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]),
    CONSTRAINT [FK__CustomerV__Vehic__4222D4EF] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicles] ([Id])
);
GO

CREATE TABLE [Stays] (
    [Id] int NOT NULL IDENTITY,
    [CustomerVehicleId] int NULL,
    [LicensePlate] varchar(10) NOT NULL,
    [EntryDate] datetime NULL,
    [ExitDate] datetime NULL,
    [HourlyRate] decimal(18,2) NOT NULL,
    [TotalAmount] decimal(18,2) NULL,
    [StayStatus] varchar(20) NULL,
    CONSTRAINT [PK__Stays__3214EC078CDF02D7] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__Stays__CustomerV__45F365D3] FOREIGN KEY ([CustomerVehicleId]) REFERENCES [CustomerVehicles] ([Id])    
);
GO

CREATE INDEX [IX_Customers_AddressId] ON [Customers] ([AddressId]);
GO

CREATE UNIQUE INDEX [UQ__Customer__C1FF9309D04BA2C4] ON [Customers] ([Cpf]);
GO

CREATE INDEX [IX_CustomerVehicles_CustomerId] ON [CustomerVehicles] ([CustomerId]);
GO

CREATE INDEX [IX_CustomerVehicles_VehicleId] ON [CustomerVehicles] ([VehicleId]);
GO

CREATE INDEX [IX_Stays_CustomerVehicleId] ON [Stays] ([CustomerVehicleId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240706200459_InitialCreate', N'8.0.6');
GO

COMMIT;
GO