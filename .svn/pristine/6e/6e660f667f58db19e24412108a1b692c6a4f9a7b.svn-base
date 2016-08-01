
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/17/2015 14:17:45
-- Generated from EDMX file: C:\dev\+54Portfolio\Plus54PortfolioRedesign2014.Entities\Plus54PortfolioRedesign2014Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Plus54PortfolioRedesign2014];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Project_Thumbnail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Project] DROP CONSTRAINT [FK_Project_Thumbnail];
GO
IF OBJECT_ID(N'[dbo].[FK_Project_SliderImages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PortfolioImage] DROP CONSTRAINT [FK_Project_SliderImages];
GO
IF OBJECT_ID(N'[dbo].[FK_Project_Client]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Project] DROP CONSTRAINT [FK_Project_Client];
GO
IF OBJECT_ID(N'[dbo].[FK_Project_Partner]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Project] DROP CONSTRAINT [FK_Project_Partner];
GO
IF OBJECT_ID(N'[dbo].[FK_Technology_Thumbnail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Technology] DROP CONSTRAINT [FK_Technology_Thumbnail];
GO
IF OBJECT_ID(N'[dbo].[FK_SocialMedia_Thumbnail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SocialMedia] DROP CONSTRAINT [FK_SocialMedia_Thumbnail];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectCategory_Thumbnail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectCategory] DROP CONSTRAINT [FK_ProjectCategory_Thumbnail];
GO
IF OBJECT_ID(N'[dbo].[FK_FK_Project_Technologies_Project]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FK_Project_Technologies] DROP CONSTRAINT [FK_FK_Project_Technologies_Project];
GO
IF OBJECT_ID(N'[dbo].[FK_FK_Project_Technologies_Technology]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FK_Project_Technologies] DROP CONSTRAINT [FK_FK_Project_Technologies_Technology];
GO
IF OBJECT_ID(N'[dbo].[FK_FK_Project_SocialMedia_Project]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FK_Project_SocialMedia] DROP CONSTRAINT [FK_FK_Project_SocialMedia_Project];
GO
IF OBJECT_ID(N'[dbo].[FK_FK_Project_SocialMedia_SocialMedia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FK_Project_SocialMedia] DROP CONSTRAINT [FK_FK_Project_SocialMedia_SocialMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_FK_Project_ProjectTypeTag_Project]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FK_Project_ProjectTypeTag] DROP CONSTRAINT [FK_FK_Project_ProjectTypeTag_Project];
GO
IF OBJECT_ID(N'[dbo].[FK_FK_Project_ProjectTypeTag_ProjectTypeTag]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FK_Project_ProjectTypeTag] DROP CONSTRAINT [FK_FK_Project_ProjectTypeTag_ProjectTypeTag];
GO
IF OBJECT_ID(N'[dbo].[FK_Project_SiteLogo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Project] DROP CONSTRAINT [FK_Project_SiteLogo];
GO
IF OBJECT_ID(N'[dbo].[FK_FK_Project_ProjectCategory_Project]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FK_Project_ProjectCategory] DROP CONSTRAINT [FK_FK_Project_ProjectCategory_Project];
GO
IF OBJECT_ID(N'[dbo].[FK_FK_Project_ProjectCategory_ProjectCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FK_Project_ProjectCategory] DROP CONSTRAINT [FK_FK_Project_ProjectCategory_ProjectCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ScreenSupport_Thumbnail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScreenSupport] DROP CONSTRAINT [FK_ScreenSupport_Thumbnail];
GO
IF OBJECT_ID(N'[dbo].[FK_FK_Project_ScreenSupport_Project]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FK_Project_ScreenSupport] DROP CONSTRAINT [FK_FK_Project_ScreenSupport_Project];
GO
IF OBJECT_ID(N'[dbo].[FK_FK_Project_ScreenSupport_ScreenSupport]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FK_Project_ScreenSupport] DROP CONSTRAINT [FK_FK_Project_ScreenSupport_ScreenSupport];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PortfolioImage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PortfolioImage];
GO
IF OBJECT_ID(N'[dbo].[Project]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Project];
GO
IF OBJECT_ID(N'[dbo].[Client]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Client];
GO
IF OBJECT_ID(N'[dbo].[Partner]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Partner];
GO
IF OBJECT_ID(N'[dbo].[Technology]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Technology];
GO
IF OBJECT_ID(N'[dbo].[SocialMedia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SocialMedia];
GO
IF OBJECT_ID(N'[dbo].[ProjectCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectCategory];
GO
IF OBJECT_ID(N'[dbo].[ProjectTypeTag]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectTypeTag];
GO
IF OBJECT_ID(N'[dbo].[ScreenSupport]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ScreenSupport];
GO
IF OBJECT_ID(N'[dbo].[FK_Project_Technologies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FK_Project_Technologies];
GO
IF OBJECT_ID(N'[dbo].[FK_Project_SocialMedia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FK_Project_SocialMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_Project_ProjectTypeTag]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FK_Project_ProjectTypeTag];
GO
IF OBJECT_ID(N'[dbo].[FK_Project_ProjectCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FK_Project_ProjectCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_Project_ScreenSupport]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FK_Project_ScreenSupport];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PortfolioImage'
CREATE TABLE [dbo].[PortfolioImage] (
    [IdImage] int IDENTITY(1,1) NOT NULL,
    [Path] nvarchar(255)  NOT NULL,
    [Type] nvarchar(20)  NOT NULL,
    [Height] int  NOT NULL,
    [Width] int  NOT NULL,
    [FK_Project_SliderImages_PortfolioImage_IdProject] int  NULL
);
GO

-- Creating table 'Project'
CREATE TABLE [dbo].[Project] (
    [IdProject] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Overview] nvarchar(255)  NOT NULL,
    [Url] nvarchar(255)  NULL,
    [Year] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [Thumbnail_IdImage] int  NOT NULL,
    [Client_IdClient] int  NOT NULL,
    [Partner_IdPartner] int  NOT NULL,
    [SiteLogo_IdImage] int  NOT NULL
);
GO

-- Creating table 'Client'
CREATE TABLE [dbo].[Client] (
    [IdClient] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'Partner'
CREATE TABLE [dbo].[Partner] (
    [IdPartner] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'Technology'
CREATE TABLE [dbo].[Technology] (
    [IdTechnology] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Thumbnail_IdImage] int  NOT NULL
);
GO

-- Creating table 'SocialMedia'
CREATE TABLE [dbo].[SocialMedia] (
    [IdSocialMedia] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Thumbnail_IdImage] int  NOT NULL
);
GO

-- Creating table 'ProjectCategory'
CREATE TABLE [dbo].[ProjectCategory] (
    [IdProjectCategory] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Thumbnail_IdImage] int  NOT NULL
);
GO

-- Creating table 'ProjectTypeTag'
CREATE TABLE [dbo].[ProjectTypeTag] (
    [IdProjectTypeTag] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'ScreenSupport'
CREATE TABLE [dbo].[ScreenSupport] (
    [IdScreenSupport] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Thumbnail_IdImage] int  NOT NULL
);
GO

-- Creating table 'FK_Project_Technologies'
CREATE TABLE [dbo].[FK_Project_Technologies] (
    [Projects_IdProject] int  NOT NULL,
    [Technologies_IdTechnology] int  NOT NULL
);
GO

-- Creating table 'FK_Project_SocialMedia'
CREATE TABLE [dbo].[FK_Project_SocialMedia] (
    [Projects_IdProject] int  NOT NULL,
    [SocialMedia_IdSocialMedia] int  NOT NULL
);
GO

-- Creating table 'FK_Project_ProjectTypeTag'
CREATE TABLE [dbo].[FK_Project_ProjectTypeTag] (
    [Projects_IdProject] int  NOT NULL,
    [ProjectTypeTags_IdProjectTypeTag] int  NOT NULL
);
GO

-- Creating table 'FK_Project_ProjectCategory'
CREATE TABLE [dbo].[FK_Project_ProjectCategory] (
    [Projects_IdProject] int  NOT NULL,
    [ProjectCategory_IdProjectCategory] int  NOT NULL
);
GO

-- Creating table 'FK_Project_ScreenSupport'
CREATE TABLE [dbo].[FK_Project_ScreenSupport] (
    [Projects_IdProject] int  NOT NULL,
    [ScreenSupport_IdScreenSupport] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdImage] in table 'PortfolioImage'
ALTER TABLE [dbo].[PortfolioImage]
ADD CONSTRAINT [PK_PortfolioImage]
    PRIMARY KEY CLUSTERED ([IdImage] ASC);
GO

-- Creating primary key on [IdProject] in table 'Project'
ALTER TABLE [dbo].[Project]
ADD CONSTRAINT [PK_Project]
    PRIMARY KEY CLUSTERED ([IdProject] ASC);
GO

-- Creating primary key on [IdClient] in table 'Client'
ALTER TABLE [dbo].[Client]
ADD CONSTRAINT [PK_Client]
    PRIMARY KEY CLUSTERED ([IdClient] ASC);
GO

-- Creating primary key on [IdPartner] in table 'Partner'
ALTER TABLE [dbo].[Partner]
ADD CONSTRAINT [PK_Partner]
    PRIMARY KEY CLUSTERED ([IdPartner] ASC);
GO

-- Creating primary key on [IdTechnology] in table 'Technology'
ALTER TABLE [dbo].[Technology]
ADD CONSTRAINT [PK_Technology]
    PRIMARY KEY CLUSTERED ([IdTechnology] ASC);
GO

-- Creating primary key on [IdSocialMedia] in table 'SocialMedia'
ALTER TABLE [dbo].[SocialMedia]
ADD CONSTRAINT [PK_SocialMedia]
    PRIMARY KEY CLUSTERED ([IdSocialMedia] ASC);
GO

-- Creating primary key on [IdProjectCategory] in table 'ProjectCategory'
ALTER TABLE [dbo].[ProjectCategory]
ADD CONSTRAINT [PK_ProjectCategory]
    PRIMARY KEY CLUSTERED ([IdProjectCategory] ASC);
GO

-- Creating primary key on [IdProjectTypeTag] in table 'ProjectTypeTag'
ALTER TABLE [dbo].[ProjectTypeTag]
ADD CONSTRAINT [PK_ProjectTypeTag]
    PRIMARY KEY CLUSTERED ([IdProjectTypeTag] ASC);
GO

-- Creating primary key on [IdScreenSupport] in table 'ScreenSupport'
ALTER TABLE [dbo].[ScreenSupport]
ADD CONSTRAINT [PK_ScreenSupport]
    PRIMARY KEY CLUSTERED ([IdScreenSupport] ASC);
GO

-- Creating primary key on [Projects_IdProject], [Technologies_IdTechnology] in table 'FK_Project_Technologies'
ALTER TABLE [dbo].[FK_Project_Technologies]
ADD CONSTRAINT [PK_FK_Project_Technologies]
    PRIMARY KEY CLUSTERED ([Projects_IdProject], [Technologies_IdTechnology] ASC);
GO

-- Creating primary key on [Projects_IdProject], [SocialMedia_IdSocialMedia] in table 'FK_Project_SocialMedia'
ALTER TABLE [dbo].[FK_Project_SocialMedia]
ADD CONSTRAINT [PK_FK_Project_SocialMedia]
    PRIMARY KEY CLUSTERED ([Projects_IdProject], [SocialMedia_IdSocialMedia] ASC);
GO

-- Creating primary key on [Projects_IdProject], [ProjectTypeTags_IdProjectTypeTag] in table 'FK_Project_ProjectTypeTag'
ALTER TABLE [dbo].[FK_Project_ProjectTypeTag]
ADD CONSTRAINT [PK_FK_Project_ProjectTypeTag]
    PRIMARY KEY CLUSTERED ([Projects_IdProject], [ProjectTypeTags_IdProjectTypeTag] ASC);
GO

-- Creating primary key on [Projects_IdProject], [ProjectCategory_IdProjectCategory] in table 'FK_Project_ProjectCategory'
ALTER TABLE [dbo].[FK_Project_ProjectCategory]
ADD CONSTRAINT [PK_FK_Project_ProjectCategory]
    PRIMARY KEY CLUSTERED ([Projects_IdProject], [ProjectCategory_IdProjectCategory] ASC);
GO

-- Creating primary key on [Projects_IdProject], [ScreenSupport_IdScreenSupport] in table 'FK_Project_ScreenSupport'
ALTER TABLE [dbo].[FK_Project_ScreenSupport]
ADD CONSTRAINT [PK_FK_Project_ScreenSupport]
    PRIMARY KEY CLUSTERED ([Projects_IdProject], [ScreenSupport_IdScreenSupport] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Thumbnail_IdImage] in table 'Project'
ALTER TABLE [dbo].[Project]
ADD CONSTRAINT [FK_Project_Thumbnail]
    FOREIGN KEY ([Thumbnail_IdImage])
    REFERENCES [dbo].[PortfolioImage]
        ([IdImage])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Project_Thumbnail'
CREATE INDEX [IX_FK_Project_Thumbnail]
ON [dbo].[Project]
    ([Thumbnail_IdImage]);
GO

-- Creating foreign key on [FK_Project_SliderImages_PortfolioImage_IdProject] in table 'PortfolioImage'
ALTER TABLE [dbo].[PortfolioImage]
ADD CONSTRAINT [FK_Project_SliderImages]
    FOREIGN KEY ([FK_Project_SliderImages_PortfolioImage_IdProject])
    REFERENCES [dbo].[Project]
        ([IdProject])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Project_SliderImages'
CREATE INDEX [IX_FK_Project_SliderImages]
ON [dbo].[PortfolioImage]
    ([FK_Project_SliderImages_PortfolioImage_IdProject]);
GO

-- Creating foreign key on [Client_IdClient] in table 'Project'
ALTER TABLE [dbo].[Project]
ADD CONSTRAINT [FK_Project_Client]
    FOREIGN KEY ([Client_IdClient])
    REFERENCES [dbo].[Client]
        ([IdClient])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Project_Client'
CREATE INDEX [IX_FK_Project_Client]
ON [dbo].[Project]
    ([Client_IdClient]);
GO

-- Creating foreign key on [Partner_IdPartner] in table 'Project'
ALTER TABLE [dbo].[Project]
ADD CONSTRAINT [FK_Project_Partner]
    FOREIGN KEY ([Partner_IdPartner])
    REFERENCES [dbo].[Partner]
        ([IdPartner])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Project_Partner'
CREATE INDEX [IX_FK_Project_Partner]
ON [dbo].[Project]
    ([Partner_IdPartner]);
GO

-- Creating foreign key on [Thumbnail_IdImage] in table 'Technology'
ALTER TABLE [dbo].[Technology]
ADD CONSTRAINT [FK_Technology_Thumbnail]
    FOREIGN KEY ([Thumbnail_IdImage])
    REFERENCES [dbo].[PortfolioImage]
        ([IdImage])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Technology_Thumbnail'
CREATE INDEX [IX_FK_Technology_Thumbnail]
ON [dbo].[Technology]
    ([Thumbnail_IdImage]);
GO

-- Creating foreign key on [Thumbnail_IdImage] in table 'SocialMedia'
ALTER TABLE [dbo].[SocialMedia]
ADD CONSTRAINT [FK_SocialMedia_Thumbnail]
    FOREIGN KEY ([Thumbnail_IdImage])
    REFERENCES [dbo].[PortfolioImage]
        ([IdImage])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SocialMedia_Thumbnail'
CREATE INDEX [IX_FK_SocialMedia_Thumbnail]
ON [dbo].[SocialMedia]
    ([Thumbnail_IdImage]);
GO

-- Creating foreign key on [Thumbnail_IdImage] in table 'ProjectCategory'
ALTER TABLE [dbo].[ProjectCategory]
ADD CONSTRAINT [FK_ProjectCategory_Thumbnail]
    FOREIGN KEY ([Thumbnail_IdImage])
    REFERENCES [dbo].[PortfolioImage]
        ([IdImage])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectCategory_Thumbnail'
CREATE INDEX [IX_FK_ProjectCategory_Thumbnail]
ON [dbo].[ProjectCategory]
    ([Thumbnail_IdImage]);
GO

-- Creating foreign key on [Projects_IdProject] in table 'FK_Project_Technologies'
ALTER TABLE [dbo].[FK_Project_Technologies]
ADD CONSTRAINT [FK_FK_Project_Technologies_Project]
    FOREIGN KEY ([Projects_IdProject])
    REFERENCES [dbo].[Project]
        ([IdProject])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Technologies_IdTechnology] in table 'FK_Project_Technologies'
ALTER TABLE [dbo].[FK_Project_Technologies]
ADD CONSTRAINT [FK_FK_Project_Technologies_Technology]
    FOREIGN KEY ([Technologies_IdTechnology])
    REFERENCES [dbo].[Technology]
        ([IdTechnology])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FK_Project_Technologies_Technology'
CREATE INDEX [IX_FK_FK_Project_Technologies_Technology]
ON [dbo].[FK_Project_Technologies]
    ([Technologies_IdTechnology]);
GO

-- Creating foreign key on [Projects_IdProject] in table 'FK_Project_SocialMedia'
ALTER TABLE [dbo].[FK_Project_SocialMedia]
ADD CONSTRAINT [FK_FK_Project_SocialMedia_Project]
    FOREIGN KEY ([Projects_IdProject])
    REFERENCES [dbo].[Project]
        ([IdProject])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SocialMedia_IdSocialMedia] in table 'FK_Project_SocialMedia'
ALTER TABLE [dbo].[FK_Project_SocialMedia]
ADD CONSTRAINT [FK_FK_Project_SocialMedia_SocialMedia]
    FOREIGN KEY ([SocialMedia_IdSocialMedia])
    REFERENCES [dbo].[SocialMedia]
        ([IdSocialMedia])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FK_Project_SocialMedia_SocialMedia'
CREATE INDEX [IX_FK_FK_Project_SocialMedia_SocialMedia]
ON [dbo].[FK_Project_SocialMedia]
    ([SocialMedia_IdSocialMedia]);
GO

-- Creating foreign key on [Projects_IdProject] in table 'FK_Project_ProjectTypeTag'
ALTER TABLE [dbo].[FK_Project_ProjectTypeTag]
ADD CONSTRAINT [FK_FK_Project_ProjectTypeTag_Project]
    FOREIGN KEY ([Projects_IdProject])
    REFERENCES [dbo].[Project]
        ([IdProject])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProjectTypeTags_IdProjectTypeTag] in table 'FK_Project_ProjectTypeTag'
ALTER TABLE [dbo].[FK_Project_ProjectTypeTag]
ADD CONSTRAINT [FK_FK_Project_ProjectTypeTag_ProjectTypeTag]
    FOREIGN KEY ([ProjectTypeTags_IdProjectTypeTag])
    REFERENCES [dbo].[ProjectTypeTag]
        ([IdProjectTypeTag])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FK_Project_ProjectTypeTag_ProjectTypeTag'
CREATE INDEX [IX_FK_FK_Project_ProjectTypeTag_ProjectTypeTag]
ON [dbo].[FK_Project_ProjectTypeTag]
    ([ProjectTypeTags_IdProjectTypeTag]);
GO

-- Creating foreign key on [SiteLogo_IdImage] in table 'Project'
ALTER TABLE [dbo].[Project]
ADD CONSTRAINT [FK_Project_SiteLogo]
    FOREIGN KEY ([SiteLogo_IdImage])
    REFERENCES [dbo].[PortfolioImage]
        ([IdImage])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Project_SiteLogo'
CREATE INDEX [IX_FK_Project_SiteLogo]
ON [dbo].[Project]
    ([SiteLogo_IdImage]);
GO

-- Creating foreign key on [Projects_IdProject] in table 'FK_Project_ProjectCategory'
ALTER TABLE [dbo].[FK_Project_ProjectCategory]
ADD CONSTRAINT [FK_FK_Project_ProjectCategory_Project]
    FOREIGN KEY ([Projects_IdProject])
    REFERENCES [dbo].[Project]
        ([IdProject])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProjectCategory_IdProjectCategory] in table 'FK_Project_ProjectCategory'
ALTER TABLE [dbo].[FK_Project_ProjectCategory]
ADD CONSTRAINT [FK_FK_Project_ProjectCategory_ProjectCategory]
    FOREIGN KEY ([ProjectCategory_IdProjectCategory])
    REFERENCES [dbo].[ProjectCategory]
        ([IdProjectCategory])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FK_Project_ProjectCategory_ProjectCategory'
CREATE INDEX [IX_FK_FK_Project_ProjectCategory_ProjectCategory]
ON [dbo].[FK_Project_ProjectCategory]
    ([ProjectCategory_IdProjectCategory]);
GO

-- Creating foreign key on [Thumbnail_IdImage] in table 'ScreenSupport'
ALTER TABLE [dbo].[ScreenSupport]
ADD CONSTRAINT [FK_ScreenSupport_Thumbnail]
    FOREIGN KEY ([Thumbnail_IdImage])
    REFERENCES [dbo].[PortfolioImage]
        ([IdImage])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ScreenSupport_Thumbnail'
CREATE INDEX [IX_FK_ScreenSupport_Thumbnail]
ON [dbo].[ScreenSupport]
    ([Thumbnail_IdImage]);
GO

-- Creating foreign key on [Projects_IdProject] in table 'FK_Project_ScreenSupport'
ALTER TABLE [dbo].[FK_Project_ScreenSupport]
ADD CONSTRAINT [FK_FK_Project_ScreenSupport_Project]
    FOREIGN KEY ([Projects_IdProject])
    REFERENCES [dbo].[Project]
        ([IdProject])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ScreenSupport_IdScreenSupport] in table 'FK_Project_ScreenSupport'
ALTER TABLE [dbo].[FK_Project_ScreenSupport]
ADD CONSTRAINT [FK_FK_Project_ScreenSupport_ScreenSupport]
    FOREIGN KEY ([ScreenSupport_IdScreenSupport])
    REFERENCES [dbo].[ScreenSupport]
        ([IdScreenSupport])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FK_Project_ScreenSupport_ScreenSupport'
CREATE INDEX [IX_FK_FK_Project_ScreenSupport_ScreenSupport]
ON [dbo].[FK_Project_ScreenSupport]
    ([ScreenSupport_IdScreenSupport]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------