
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/20/2016 12:30:12
-- Generated from EDMX file: C:\TFS\Ygor\HostelMVC\HostelMVC\Camada.Dominio\Entidades\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HostelDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_"FK_Total_Transactions_Calc"]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Total_Transactions] DROP CONSTRAINT [FK_"FK_Total_Transactions_Calc"];
GO
IF OBJECT_ID(N'[dbo].[FK_Calc_Money_Count]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Calc] DROP CONSTRAINT [FK_Calc_Money_Count];
GO
IF OBJECT_ID(N'[dbo].[FK_Calc_Money_Count1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Calc] DROP CONSTRAINT [FK_Calc_Money_Count1];
GO
IF OBJECT_ID(N'[dbo].[FK_Calc_Money_Count2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Calc] DROP CONSTRAINT [FK_Calc_Money_Count2];
GO
IF OBJECT_ID(N'[dbo].[FK_Money_Count_Calc_Type]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Money_Count] DROP CONSTRAINT [FK_Money_Count_Calc_Type];
GO
IF OBJECT_ID(N'[dbo].[FK_Total_Transactions_Report_Type]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Total_Transactions] DROP CONSTRAINT [FK_Total_Transactions_Report_Type];
GO
IF OBJECT_ID(N'[dbo].[FK_Total_Transactions_Total_Transactions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Total_Transactions] DROP CONSTRAINT [FK_Total_Transactions_Total_Transactions];
GO
IF OBJECT_ID(N'[dbo].[FK_Transactions_Card_Type]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transactions] DROP CONSTRAINT [FK_Transactions_Card_Type];
GO
IF OBJECT_ID(N'[dbo].[FK_Transactions_Payment_Type]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transactions] DROP CONSTRAINT [FK_Transactions_Payment_Type];
GO
IF OBJECT_ID(N'[dbo].[FK_Transactions_TotalTransactions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transactions] DROP CONSTRAINT [FK_Transactions_TotalTransactions];
GO
IF OBJECT_ID(N'[dbo].[FK_Transactions_Transaction_Type]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transactions] DROP CONSTRAINT [FK_Transactions_Transaction_Type];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Calc]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Calc];
GO
IF OBJECT_ID(N'[dbo].[Calc_Type]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Calc_Type];
GO
IF OBJECT_ID(N'[dbo].[Card_Type]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Card_Type];
GO
IF OBJECT_ID(N'[dbo].[House_Keeping]', 'U') IS NOT NULL
    DROP TABLE [dbo].[House_Keeping];
GO
IF OBJECT_ID(N'[dbo].[Money_Count]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Money_Count];
GO
IF OBJECT_ID(N'[dbo].[Payment_Type]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Payment_Type];
GO
IF OBJECT_ID(N'[dbo].[Report_Type]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Report_Type];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Total_Transactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Total_Transactions];
GO
IF OBJECT_ID(N'[dbo].[Transaction_Type]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Transaction_Type];
GO
IF OBJECT_ID(N'[dbo].[Transactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Transactions];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Calcs'
CREATE TABLE [dbo].[Calcs] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DT_reg] datetime  NOT NULL,
    [LogLogin] varchar(50)  NULL,
    [Total] decimal(18,2)  NOT NULL,
    [ID_Money_Count] int  NULL,
    [ID_Money_Count2] int  NULL,
    [ID_Money_Count3] int  NULL
);
GO

-- Creating table 'Calc_Type'
CREATE TABLE [dbo].[Calc_Type] (
    [ID] int  NOT NULL,
    [Description] varchar(50)  NOT NULL,
    [DT_End] datetime  NULL
);
GO

-- Creating table 'Card_Type'
CREATE TABLE [dbo].[Card_Type] (
    [ID] int  NOT NULL,
    [Description] varchar(50)  NULL
);
GO

-- Creating table 'House_Keeping'
CREATE TABLE [dbo].[House_Keeping] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Room_Number] varchar(4)  NOT NULL,
    [Adults] int  NOT NULL,
    [DT_Registro] datetime  NOT NULL
);
GO

-- Creating table 'Money_Count'
CREATE TABLE [dbo].[Money_Count] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Qnt_1_Cent] int  NOT NULL,
    [Qnt_2_Cents] int  NOT NULL,
    [Qnt_5_Cents] int  NOT NULL,
    [Qnt_10_Cents] int  NOT NULL,
    [Qnt_20_Cents] int  NOT NULL,
    [Qnt_50_Cents] int  NOT NULL,
    [Qnt_1_Euro] int  NOT NULL,
    [Qnt_2_Euros] int  NOT NULL,
    [Qnt_5_Euros] int  NOT NULL,
    [Qnt_10_Euros] int  NOT NULL,
    [Qnt_20_Euros] int  NOT NULL,
    [Qnt_50_Euros] int  NOT NULL,
    [Qnt_100_Euros] int  NOT NULL,
    [Qnt_200_Euros] int  NOT NULL,
    [Qnt_500_Euros] int  NOT NULL,
    [Total] decimal(18,2)  NOT NULL,
    [DT_Reg] datetime  NOT NULL,
    [LogLogin] varchar(50)  NOT NULL,
    [ID_Calc_Type] int  NOT NULL
);
GO

-- Creating table 'Payment_Type'
CREATE TABLE [dbo].[Payment_Type] (
    [ID] int  NOT NULL,
    [Description] varchar(50)  NULL
);
GO

-- Creating table 'Report_Type'
CREATE TABLE [dbo].[Report_Type] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Total_Transactions'
CREATE TABLE [dbo].[Total_Transactions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TotalTransactions] decimal(18,2)  NULL,
    [TotalFinal] decimal(18,2)  NULL,
    [DifferenceFinalCalc] decimal(18,2)  NULL,
    [DT_Reg] datetime  NULL,
    [LogLogin] varchar(50)  NULL,
    [ID_Calc] int  NULL,
    [ID_Last_Transaction] int  NULL,
    [ID_Report_Type] int  NULL,
    [Last_Cashier_Total] decimal(18,2)  NULL
);
GO

-- Creating table 'Transaction_Type'
CREATE TABLE [dbo].[Transaction_Type] (
    [ID] int  NOT NULL,
    [Description] varchar(50)  NULL
);
GO

-- Creating table 'Transactions'
CREATE TABLE [dbo].[Transactions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DT_Reg] datetime  NOT NULL,
    [LogLogin] varchar(50)  NOT NULL,
    [Reservation_Number] varchar(100)  NOT NULL,
    [GuestName] varchar(50)  NOT NULL,
    [ID_Transaction_Type] int  NULL,
    [ID_Payment_Type] int  NULL,
    [ID_Card_Type] int  NULL,
    [Description] varchar(50)  NULL,
    [Total] decimal(18,2)  NULL,
    [ID_Total_Transactions] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Calcs'
ALTER TABLE [dbo].[Calcs]
ADD CONSTRAINT [PK_Calcs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Calc_Type'
ALTER TABLE [dbo].[Calc_Type]
ADD CONSTRAINT [PK_Calc_Type]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Card_Type'
ALTER TABLE [dbo].[Card_Type]
ADD CONSTRAINT [PK_Card_Type]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'House_Keeping'
ALTER TABLE [dbo].[House_Keeping]
ADD CONSTRAINT [PK_House_Keeping]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Money_Count'
ALTER TABLE [dbo].[Money_Count]
ADD CONSTRAINT [PK_Money_Count]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Payment_Type'
ALTER TABLE [dbo].[Payment_Type]
ADD CONSTRAINT [PK_Payment_Type]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Report_Type'
ALTER TABLE [dbo].[Report_Type]
ADD CONSTRAINT [PK_Report_Type]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [ID] in table 'Total_Transactions'
ALTER TABLE [dbo].[Total_Transactions]
ADD CONSTRAINT [PK_Total_Transactions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Transaction_Type'
ALTER TABLE [dbo].[Transaction_Type]
ADD CONSTRAINT [PK_Transaction_Type]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [PK_Transactions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ID_Calc] in table 'Total_Transactions'
ALTER TABLE [dbo].[Total_Transactions]
ADD CONSTRAINT [FK_C_FK_Total_Transactions_Calc_]
    FOREIGN KEY ([ID_Calc])
    REFERENCES [dbo].[Calcs]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_C_FK_Total_Transactions_Calc_'
CREATE INDEX [IX_FK_C_FK_Total_Transactions_Calc_]
ON [dbo].[Total_Transactions]
    ([ID_Calc]);
GO

-- Creating foreign key on [ID_Money_Count] in table 'Calcs'
ALTER TABLE [dbo].[Calcs]
ADD CONSTRAINT [FK_Calc_Money_Count]
    FOREIGN KEY ([ID_Money_Count])
    REFERENCES [dbo].[Money_Count]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Calc_Money_Count'
CREATE INDEX [IX_FK_Calc_Money_Count]
ON [dbo].[Calcs]
    ([ID_Money_Count]);
GO

-- Creating foreign key on [ID_Money_Count2] in table 'Calcs'
ALTER TABLE [dbo].[Calcs]
ADD CONSTRAINT [FK_Calc_Money_Count1]
    FOREIGN KEY ([ID_Money_Count2])
    REFERENCES [dbo].[Money_Count]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Calc_Money_Count1'
CREATE INDEX [IX_FK_Calc_Money_Count1]
ON [dbo].[Calcs]
    ([ID_Money_Count2]);
GO

-- Creating foreign key on [ID_Money_Count3] in table 'Calcs'
ALTER TABLE [dbo].[Calcs]
ADD CONSTRAINT [FK_Calc_Money_Count2]
    FOREIGN KEY ([ID_Money_Count3])
    REFERENCES [dbo].[Money_Count]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Calc_Money_Count2'
CREATE INDEX [IX_FK_Calc_Money_Count2]
ON [dbo].[Calcs]
    ([ID_Money_Count3]);
GO

-- Creating foreign key on [ID_Calc_Type] in table 'Money_Count'
ALTER TABLE [dbo].[Money_Count]
ADD CONSTRAINT [FK_Money_Count_Calc_Type]
    FOREIGN KEY ([ID_Calc_Type])
    REFERENCES [dbo].[Calc_Type]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Money_Count_Calc_Type'
CREATE INDEX [IX_FK_Money_Count_Calc_Type]
ON [dbo].[Money_Count]
    ([ID_Calc_Type]);
GO

-- Creating foreign key on [ID_Card_Type] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_Transactions_Card_Type]
    FOREIGN KEY ([ID_Card_Type])
    REFERENCES [dbo].[Card_Type]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Transactions_Card_Type'
CREATE INDEX [IX_FK_Transactions_Card_Type]
ON [dbo].[Transactions]
    ([ID_Card_Type]);
GO

-- Creating foreign key on [ID_Payment_Type] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_Transactions_Payment_Type]
    FOREIGN KEY ([ID_Payment_Type])
    REFERENCES [dbo].[Payment_Type]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Transactions_Payment_Type'
CREATE INDEX [IX_FK_Transactions_Payment_Type]
ON [dbo].[Transactions]
    ([ID_Payment_Type]);
GO

-- Creating foreign key on [ID_Report_Type] in table 'Total_Transactions'
ALTER TABLE [dbo].[Total_Transactions]
ADD CONSTRAINT [FK_Total_Transactions_Report_Type]
    FOREIGN KEY ([ID_Report_Type])
    REFERENCES [dbo].[Report_Type]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Total_Transactions_Report_Type'
CREATE INDEX [IX_FK_Total_Transactions_Report_Type]
ON [dbo].[Total_Transactions]
    ([ID_Report_Type]);
GO

-- Creating foreign key on [ID_Last_Transaction] in table 'Total_Transactions'
ALTER TABLE [dbo].[Total_Transactions]
ADD CONSTRAINT [FK_Total_Transactions_Total_Transactions]
    FOREIGN KEY ([ID_Last_Transaction])
    REFERENCES [dbo].[Total_Transactions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Total_Transactions_Total_Transactions'
CREATE INDEX [IX_FK_Total_Transactions_Total_Transactions]
ON [dbo].[Total_Transactions]
    ([ID_Last_Transaction]);
GO

-- Creating foreign key on [ID_Total_Transactions] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_Transactions_TotalTransactions]
    FOREIGN KEY ([ID_Total_Transactions])
    REFERENCES [dbo].[Total_Transactions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Transactions_TotalTransactions'
CREATE INDEX [IX_FK_Transactions_TotalTransactions]
ON [dbo].[Transactions]
    ([ID_Total_Transactions]);
GO

-- Creating foreign key on [ID_Transaction_Type] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_Transactions_Transaction_Type]
    FOREIGN KEY ([ID_Transaction_Type])
    REFERENCES [dbo].[Transaction_Type]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Transactions_Transaction_Type'
CREATE INDEX [IX_FK_Transactions_Transaction_Type]
ON [dbo].[Transactions]
    ([ID_Transaction_Type]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------