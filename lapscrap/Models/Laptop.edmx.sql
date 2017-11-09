
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/09/2017 13:39:03
-- Generated from EDMX file: d:\data\downloads\documents\visual studio 2015\Projects\lapscrap\lapscrap\Models\Laptop.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [devices];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_LaptopEbayItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EbayItemSet] DROP CONSTRAINT [FK_LaptopEbayItem];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[LaptopSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LaptopSet];
GO
IF OBJECT_ID(N'[dbo].[EbayItemSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EbayItemSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'LaptopSet'
CREATE TABLE [dbo].[LaptopSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Brand] nvarchar(max)  NOT NULL,
    [Cpu] nvarchar(max)  NOT NULL,
    [Ram] int  NOT NULL,
    [Size] real  NOT NULL,
    [Resolution] nvarchar(max)  NOT NULL,
    [Gpu] nvarchar(max)  NOT NULL,
    [Hd] int  NOT NULL,
    [Ssd] int  NOT NULL,
    [Width] real  NOT NULL,
    [Length] real  NOT NULL,
    [Height] real  NOT NULL,
    [Weight] real  NOT NULL,
    [Battery] int  NOT NULL,
    [Runtime] nvarchar(max)  NOT NULL,
    [Price] real  NOT NULL,
    [Review_url] nvarchar(max)  NOT NULL,
    [Shop_url] nvarchar(max)  NOT NULL,
    [Ebay_url] nvarchar(max)  NOT NULL,
    [Video_Url] nvarchar(max)  NOT NULL,
    [Components] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EbayItemSet'
CREATE TABLE [dbo].[EbayItemSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ItemId] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Subtitle] nvarchar(max)  NOT NULL,
    [GalleryURL] nvarchar(max)  NOT NULL,
    [ViewItemURL] nvarchar(max)  NOT NULL,
    [postalCode] nvarchar(max)  NOT NULL,
    [Location] nvarchar(max)  NOT NULL,
    [Country] nvarchar(max)  NOT NULL,
    [ShippingServiceCost] nvarchar(max)  NOT NULL,
    [CurrentPrice] nvarchar(max)  NOT NULL,
    [TimeLeft] nvarchar(max)  NOT NULL,
    [ConditionId] nvarchar(max)  NOT NULL,
    [ConditionDisplayName] nvarchar(max)  NOT NULL,
    [LaptopId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'LaptopSet'
ALTER TABLE [dbo].[LaptopSet]
ADD CONSTRAINT [PK_LaptopSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EbayItemSet'
ALTER TABLE [dbo].[EbayItemSet]
ADD CONSTRAINT [PK_EbayItemSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LaptopId] in table 'EbayItemSet'
ALTER TABLE [dbo].[EbayItemSet]
ADD CONSTRAINT [FK_LaptopEbayItem]
    FOREIGN KEY ([LaptopId])
    REFERENCES [dbo].[LaptopSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LaptopEbayItem'
CREATE INDEX [IX_FK_LaptopEbayItem]
ON [dbo].[EbayItemSet]
    ([LaptopId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------