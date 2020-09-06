
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/06/2020 17:52:58
-- Generated from EDMX file: D:\Test\EURISTest\EURIS.Entities\LocalDbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LocalDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProductCatalog_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductCatalog] DROP CONSTRAINT [FK_ProductCatalog_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductCatalog_Catalog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductCatalog] DROP CONSTRAINT [FK_ProductCatalog_Catalog];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[Catalog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Catalog];
GO
IF OBJECT_ID(N'[dbo].[ProductCatalog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductCatalog];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Product'
CREATE TABLE [dbo].[Product] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(10)  NOT NULL,
    [Description] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Catalog'
CREATE TABLE [dbo].[Catalog] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(10)  NOT NULL,
    [Description] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ProductCatalog'
CREATE TABLE [dbo].[ProductCatalog] (
    [Product_Id] int  NOT NULL,
    [ProductCatalog_Product_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Product'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [PK_Product]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Catalog'
ALTER TABLE [dbo].[Catalog]
ADD CONSTRAINT [PK_Catalog]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Product_Id], [ProductCatalog_Product_Id] in table 'ProductCatalog'
ALTER TABLE [dbo].[ProductCatalog]
ADD CONSTRAINT [PK_ProductCatalog]
    PRIMARY KEY CLUSTERED ([Product_Id], [ProductCatalog_Product_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Product_Id] in table 'ProductCatalog'
ALTER TABLE [dbo].[ProductCatalog]
ADD CONSTRAINT [FK_ProductCatalog_Product]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[Product]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductCatalog_Product_Id] in table 'ProductCatalog'
ALTER TABLE [dbo].[ProductCatalog]
ADD CONSTRAINT [FK_ProductCatalog_Catalog]
    FOREIGN KEY ([ProductCatalog_Product_Id])
    REFERENCES [dbo].[Catalog]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCatalog_Catalog'
CREATE INDEX [IX_FK_ProductCatalog_Catalog]
ON [dbo].[ProductCatalog]
    ([ProductCatalog_Product_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------