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

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE TABLE [Category] (
        [id] int NOT NULL IDENTITY,
        [name] nvarchar(255) NOT NULL,
        [description] nvarchar(max) NULL,
        CONSTRAINT [PK__Category__3213E83F75780C5D] PRIMARY KEY ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE TABLE [Cart] (
        [id] int NOT NULL IDENTITY,
        [productTypeQuan] int NOT NULL,
        [createDate] date NULL,
        [userID] nvarchar(450) NULL,
        CONSTRAINT [PK__Cart__3213E83F6AAF5318] PRIMARY KEY ([id]),
        CONSTRAINT [FK__Cart__userID__4BAC3F29] FOREIGN KEY ([userID]) REFERENCES [AspNetUsers] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE TABLE [Order] (
        [id] int NOT NULL IDENTITY,
        [userID] nvarchar(450) NULL,
        [orderDate] date NULL,
        [totalAmount] int NULL,
        [status] nvarchar(50) NULL DEFAULT N'pending',
        [paymentMethod] nvarchar(50) NULL,
        [shippingAddress] nvarchar(255) NULL,
        CONSTRAINT [PK__Order__3213E83FA60FD569] PRIMARY KEY ([id]),
        CONSTRAINT [FK_Order_user] FOREIGN KEY ([userID]) REFERENCES [AspNetUsers] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE TABLE [RefreshTokenEntities] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [Token] nvarchar(max) NOT NULL,
        [JwtId] nvarchar(max) NOT NULL,
        [IssuedAt] datetime2 NOT NULL,
        [ExpiredAt] datetime2 NOT NULL,
        CONSTRAINT [PK_RefreshTokenEntities] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RefreshTokenEntities_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE TABLE [Product] (
        [id] int NOT NULL IDENTITY,
        [name] nvarchar(255) NOT NULL,
        [description] nvarchar(max) NOT NULL,
        [quantity] bigint NOT NULL,
        [originalPrice] decimal(18,0) NOT NULL,
        [price] decimal(18,0) NOT NULL,
        [createDate] datetime2 NULL,
        [updateDate] datetime2 NULL,
        [categoryID] int NULL,
        [size] nvarchar(50) NULL,
        [soledCount] int NULL DEFAULT 0,
        CONSTRAINT [PK__Product__3213E83F3C8C6BC7] PRIMARY KEY ([id]),
        CONSTRAINT [FK__Product__categor__534D60F1] FOREIGN KEY ([categoryID]) REFERENCES [Category] ([id]) ON DELETE SET NULL
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE TABLE [feedBack] (
        [id] int NOT NULL IDENTITY,
        [orderID] int NULL,
        [description] nvarchar(max) NULL,
        [createDate] date NULL,
        CONSTRAINT [PK__feedBack__3213E83FDEECB115] PRIMARY KEY ([id]),
        CONSTRAINT [FK__feedBack__orderI__4CA06362] FOREIGN KEY ([orderID]) REFERENCES [Order] ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE TABLE [OrderDetail] (
        [id] int NOT NULL IDENTITY,
        [orderID] int NOT NULL,
        [unitPrice] decimal(18,0) NULL,
        [unitQuantity] int NULL,
        CONSTRAINT [PK__OrderDet__3213E83FD710BB0B] PRIMARY KEY ([id]),
        CONSTRAINT [FK__OrderDeta__order__52593CB8] FOREIGN KEY ([orderID]) REFERENCES [Order] ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE TABLE [image] (
        [id] int NOT NULL IDENTITY,
        [productID] int NULL,
        [imageUrl] varchar(max) NULL,
        CONSTRAINT [PK__image__3213E83FB2657157] PRIMARY KEY ([id]),
        CONSTRAINT [FK__image__productID__4D94879B] FOREIGN KEY ([productID]) REFERENCES [Product] ([id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE TABLE [ItemCart] (
        [id] int NOT NULL IDENTITY,
        [productID] int NULL,
        [cartID] int NULL,
        [orderDetailID] int NULL,
        [Quantity] int NULL,
        [price] decimal(18,0) NOT NULL,
        CONSTRAINT [PK__ItemCart__3213E83FEED18C83] PRIMARY KEY ([id]),
        CONSTRAINT [FK_ItemCart_OrderDetail] FOREIGN KEY ([orderDetailID]) REFERENCES [OrderDetail] ([id]),
        CONSTRAINT [FK_ItemCart_Product_productID] FOREIGN KEY ([productID]) REFERENCES [Product] ([id]) ON DELETE CASCADE,
        CONSTRAINT [FK__ItemCart__cartID__4E88ABD4] FOREIGN KEY ([cartID]) REFERENCES [Cart] ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UQ__Cart__CB9A1CDEA0CA1EA7] ON [Cart] ([userID]) WHERE [userID] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE INDEX [IX_feedBack_orderID] ON [feedBack] ([orderID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE INDEX [IX_image_productID] ON [image] ([productID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE INDEX [IX_ItemCart_cartID] ON [ItemCart] ([cartID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE INDEX [IX_ItemCart_orderDetailID] ON [ItemCart] ([orderDetailID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UQ__ItemCart__2D10D14BDBBF2276] ON [ItemCart] ([productID]) WHERE [productID] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE INDEX [IX_Order_userID] ON [Order] ([userID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE UNIQUE INDEX [UQ__OrderDet__0809337CE7E3ABEC] ON [OrderDetail] ([orderID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE INDEX [IX_Product_categoryID] ON [Product] ([categoryID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    CREATE INDEX [IX_RefreshTokenEntities_UserId] ON [RefreshTokenEntities] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006044406_inint'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241006044406_inint', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006102633_setNull'
)
BEGIN
    ALTER TABLE [ItemCart] DROP CONSTRAINT [FK_ItemCart_OrderDetail];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006102633_setNull'
)
BEGIN
    ALTER TABLE [ItemCart] DROP CONSTRAINT [FK__ItemCart__cartID__4E88ABD4];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006102633_setNull'
)
BEGIN
    ALTER TABLE [ItemCart] ADD CONSTRAINT [FK_ItemCart_OrderDetail] FOREIGN KEY ([orderDetailID]) REFERENCES [OrderDetail] ([id]) ON DELETE SET NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006102633_setNull'
)
BEGIN
    ALTER TABLE [ItemCart] ADD CONSTRAINT [FK__ItemCart__cartID__4E88ABD4] FOREIGN KEY ([cartID]) REFERENCES [Cart] ([id]) ON DELETE SET NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006102633_setNull'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241006102633_setNull', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006104737_changepop'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cart]') AND [c].[name] = N'createDate');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Cart] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Cart] ALTER COLUMN [createDate] datetime2 NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006104737_changepop'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241006104737_changepop', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006152002_removepropv2'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrderDetail]') AND [c].[name] = N'unitPrice');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [OrderDetail] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [OrderDetail] DROP COLUMN [unitPrice];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006152002_removepropv2'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241006152002_removepropv2', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006152336_addProp'
)
BEGIN
    ALTER TABLE [Product] ADD [Manufacturer] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006152336_addProp'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241006152336_addProp', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006163635_changerr'
)
BEGIN
    ALTER TABLE [ItemCart] DROP CONSTRAINT [FK_ItemCart_OrderDetail];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006163635_changerr'
)
BEGIN
    ALTER TABLE [OrderDetail] DROP CONSTRAINT [FK__OrderDeta__order__52593CB8];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006163635_changerr'
)
BEGIN
    ALTER TABLE [OrderDetail] ADD [OrderId1] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006163635_changerr'
)
BEGIN
    ALTER TABLE [OrderDetail] ADD [ProductId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006163635_changerr'
)
BEGIN
    ALTER TABLE [OrderDetail] ADD [UnitPrice] decimal(18,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006163635_changerr'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Order]') AND [c].[name] = N'orderDate');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Order] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Order] ALTER COLUMN [orderDate] datetime2 NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006163635_changerr'
)
BEGIN
    ALTER TABLE [Order] ADD [orderCode] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006163635_changerr'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_OrderDetail_OrderId1] ON [OrderDetail] ([OrderId1]) WHERE [OrderId1] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006163635_changerr'
)
BEGIN
    CREATE INDEX [IX_OrderDetail_ProductId] ON [OrderDetail] ([ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006163635_changerr'
)
BEGIN
    ALTER TABLE [ItemCart] ADD CONSTRAINT [FK_ItemCart_OrderDetail_orderDetailID] FOREIGN KEY ([orderDetailID]) REFERENCES [OrderDetail] ([id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006163635_changerr'
)
BEGIN
    ALTER TABLE [OrderDetail] ADD CONSTRAINT [FK_OrderDetail_Order_OrderId1] FOREIGN KEY ([OrderId1]) REFERENCES [Order] ([id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006163635_changerr'
)
BEGIN
    ALTER TABLE [OrderDetail] ADD CONSTRAINT [FK_OrderDetail_Order_orderID] FOREIGN KEY ([orderID]) REFERENCES [Order] ([id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006163635_changerr'
)
BEGIN
    ALTER TABLE [OrderDetail] ADD CONSTRAINT [FK_OrderDetail_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006163635_changerr'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241006163635_changerr', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006164211_changepropv4'
)
BEGIN
    ALTER TABLE [ItemCart] DROP CONSTRAINT [FK_ItemCart_OrderDetail_orderDetailID];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006164211_changepropv4'
)
BEGIN
    ALTER TABLE [OrderDetail] DROP CONSTRAINT [FK_OrderDetail_Order_OrderId1];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006164211_changepropv4'
)
BEGIN
    DROP INDEX [IX_OrderDetail_OrderId1] ON [OrderDetail];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006164211_changepropv4'
)
BEGIN
    DROP INDEX [IX_ItemCart_orderDetailID] ON [ItemCart];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006164211_changepropv4'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrderDetail]') AND [c].[name] = N'OrderId1');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [OrderDetail] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [OrderDetail] DROP COLUMN [OrderId1];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006164211_changepropv4'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ItemCart]') AND [c].[name] = N'orderDetailID');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [ItemCart] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [ItemCart] DROP COLUMN [orderDetailID];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006164211_changepropv4'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241006164211_changepropv4', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006165143_changpop'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Order]') AND [c].[name] = N'totalAmount');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Order] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Order] ALTER COLUMN [totalAmount] decimal(18,2) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241006165143_changpop'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241006165143_changpop', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241007022105_changepropv5'
)
BEGIN
    DROP INDEX [UQ__OrderDet__0809337CE7E3ABEC] ON [OrderDetail];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241007022105_changepropv5'
)
BEGIN
    CREATE UNIQUE INDEX [IX_OrderDetail_orderID_ProductId] ON [OrderDetail] ([orderID], [ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241007022105_changepropv5'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241007022105_changepropv5', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241007032207_addpropv2'
)
BEGIN
    ALTER TABLE [feedBack] ADD [roleName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241007032207_addpropv2'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241007032207_addpropv2', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241007035219_changdate'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[feedBack]') AND [c].[name] = N'createDate');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [feedBack] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [feedBack] ALTER COLUMN [createDate] datetime2 NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241007035219_changdate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241007035219_changdate', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241007035852_addavatarUrl'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [avatarUrl] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241007035852_addavatarUrl'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241007035852_addavatarUrl', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241008013614_changepropv111'
)
BEGIN
    EXEC sp_rename N'[Product].[soledCount]', N'soldedCount', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241008013614_changepropv111'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241008013614_changepropv111', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241011082545_addpropv5'
)
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Order]') AND [c].[name] = N'status');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Order] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Order] DROP COLUMN [status];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241011082545_addpropv5'
)
BEGIN
    ALTER TABLE [Order] ADD [statusPayment] nvarchar(50) NULL DEFAULT N'Đang chờ';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241011082545_addpropv5'
)
BEGIN
    ALTER TABLE [Order] ADD [statusShipping] nvarchar(50) NULL DEFAULT N'Chưa thanh toán';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241011082545_addpropv5'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241011082545_addpropv5', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241012045130_addImageurlType'
)
BEGIN
    ALTER TABLE [Category] ADD [imageUrl] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241012045130_addImageurlType'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241012045130_addImageurlType', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241013114833_vvvv'
)
BEGIN
    DROP INDEX [UQ__ItemCart__2D10D14BDBBF2276] ON [ItemCart];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241013114833_vvvv'
)
BEGIN
    CREATE INDEX [UQ__ItemCart__2D10D14BDBBF2276] ON [ItemCart] ([productID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241013114833_vvvv'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241013114833_vvvv', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241014065842_addPropInOrder'
)
BEGIN
    ALTER TABLE [Order] ADD [RecipientAddress] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241014065842_addPropInOrder'
)
BEGIN
    ALTER TABLE [Order] ADD [RecipientEmail] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241014065842_addPropInOrder'
)
BEGIN
    ALTER TABLE [Order] ADD [RecipientName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241014065842_addPropInOrder'
)
BEGIN
    ALTER TABLE [Order] ADD [RecipientPhone] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241014065842_addPropInOrder'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241014065842_addPropInOrder', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241014100141_removeUNique'
)
BEGIN
    DROP INDEX [IX_OrderDetail_orderID_ProductId] ON [OrderDetail];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241014100141_removeUNique'
)
BEGIN
    CREATE INDEX [IX_OrderDetail_orderID_ProductId] ON [OrderDetail] ([orderID], [ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241014100141_removeUNique'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241014100141_removeUNique', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241015085402_addpropapplication'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Address] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241015085402_addpropapplication'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [PhoneNum] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241015085402_addpropapplication'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241015085402_addpropapplication', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241015085636_addpropapplicationv2'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241015085636_addpropapplicationv2', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241015093405_cacasder'
)
BEGIN
    ALTER TABLE [Cart] DROP CONSTRAINT [FK__Cart__userID__4BAC3F29];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241015093405_cacasder'
)
BEGIN
    ALTER TABLE [Cart] ADD CONSTRAINT [FK__Cart__userID__4BAC3F29] FOREIGN KEY ([userID]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241015093405_cacasder'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241015093405_cacasder', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241017104924_addStatusProduct'
)
BEGIN
    ALTER TABLE [Product] ADD [status] bit NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241017104924_addStatusProduct'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241017104924_addStatusProduct', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241017105543_renameStatusProduct'
)
BEGIN
    EXEC sp_rename N'[Product].[status]', N'OutOfStockstatus', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241017105543_renameStatusProduct'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241017105543_renameStatusProduct', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241017110446_updatePropProductv3'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241017110446_updatePropProductv3', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241021031333_removeOrderProperty'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Order]') AND [c].[name] = N'shippingAddress');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Order] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Order] DROP COLUMN [shippingAddress];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241021031333_removeOrderProperty'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241021031333_removeOrderProperty', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241023045750_updateRemoveUser'
)
BEGIN
    ALTER TABLE [Order] DROP CONSTRAINT [FK_Order_user];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241023045750_updateRemoveUser'
)
BEGIN
    ALTER TABLE [Order] ADD CONSTRAINT [FK_Order_user] FOREIGN KEY ([userID]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241023045750_updateRemoveUser'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241023045750_updateRemoveUser', N'8.0.8');
END;
GO

COMMIT;
GO

