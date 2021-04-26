/* Generated by Oracle SQL Developer Data Modeler 20.4.1.406.0906
--   at:        2021-04-10 12:05:30 CEST
--   site:      SQL Server 2012
--   type:      SQL Server 2012
--	 Creado por:	Borja Delgado Martin
*/

/*------------------------------------------------------
--  CREATE DATABASE
------------------------------------------------------*/
DROP DATABASE IF EXISTS tu_cartera_bd
GO
CREATE DATABASE tu_cartera_bd COLLATE Traditional_Spanish_CI_AS
GO


/*------------------------------------------------------
--  CREATE TABLES
------------------------------------------------------*/
CREATE TABLE [tu_cartera_bd].[dbo].[user] 
    (
     id INTEGER NOT NULL IDENTITY(1,1) , 
     name VARCHAR (50) NOT NULL , 
     email VARCHAR (200) NOT NULL UNIQUE, 
     pass BINARY(32) NOT NULL ,
     CONSTRAINT user_pk PRIMARY KEY (id)
    )
GO

CREATE TABLE [tu_cartera_bd].[dbo].[currency] 
    (
     id INTEGER NOT NULL IDENTITY(1,1) , 
     code VARCHAR (5) NOT NULL ,
     name VARCHAR (50) NOT NULL , 
     CONSTRAINT currency_pk PRIMARY KEY (id)
    )
GO

CREATE TABLE [tu_cartera_bd].[dbo].[portfolio] 
    (
     id INTEGER NOT NULL IDENTITY(1,1) , 
     name VARCHAR (200) NOT NULL ,
     is_global BIT NOT NULL,
     description TEXT , 
     user_id INTEGER NOT NULL,
     CONSTRAINT portfolio_pk PRIMARY KEY (id) ,
     CONSTRAINT portfolio_user_fk FOREIGN KEY (user_id) REFERENCES [tu_cartera_bd].[dbo].[user](id) 
        ON DELETE CASCADE ON UPDATE CASCADE
    )
GO

CREATE TABLE [tu_cartera_bd].[dbo].[ticker] 
    (
     id INTEGER NOT NULL IDENTITY(1,1) , 
     code VARCHAR (10) NOT NULL , 
     name VARCHAR (200) NOT NULL ,
     CONSTRAINT ticker_pk PRIMARY KEY (id)
    )
GO

CREATE TABLE [tu_cartera_bd].[dbo].[portfolio_tickers] 
    (
     portfolio_id INTEGER NOT NULL , 
     ticker_id INTEGER NOT NULL ,
     CONSTRAINT portfolio_tickers_pk PRIMARY KEY (portfolio_id,ticker_id) ,
     CONSTRAINT portfolio_tickers_portfolio_fk FOREIGN KEY (portfolio_id) REFERENCES [tu_cartera_bd].[dbo].[portfolio](id) 
        ON DELETE CASCADE ON UPDATE CASCADE ,
     CONSTRAINT portfolio_tickers_ticker_fk FOREIGN KEY (ticker_id) REFERENCES [tu_cartera_bd].[dbo].[ticker](id) 
        ON DELETE CASCADE ON UPDATE CASCADE
    )
GO

CREATE TABLE [tu_cartera_bd].[dbo].[transaction_type] 
    (
     id INTEGER NOT NULL IDENTITY(1,1) , 
     type VARCHAR (50) NOT NULL , 
     description TEXT ,
     CONSTRAINT transaction_type_pk PRIMARY KEY (id) ,
    )
GO

CREATE TABLE [tu_cartera_bd].[dbo].[transaction] 
    (
     id INTEGER NOT NULL IDENTITY(1,1) , 
     number_of_shares NUMERIC (28) NOT NULL , 
     unit_price DECIMAL (16,8) NOT NULL , 
     date DATE NOT NULL , 
     comment TEXT , 
     user_id INTEGER NOT NULL , 
     transaction_type_id INTEGER NOT NULL , 
     currency_id INTEGER NOT NULL , 
     ticker_id INTEGER NOT NULL ,
     CONSTRAINT transaction_pk PRIMARY KEY (id) ,
     CONSTRAINT transaction_user_fk FOREIGN KEY (user_id) REFERENCES [tu_cartera_bd].[dbo].[user](id) 
        ON DELETE CASCADE ON UPDATE CASCADE ,
     CONSTRAINT transaction_transaction_type_fk FOREIGN KEY (transaction_type_id) REFERENCES [tu_cartera_bd].[dbo].[transaction_type](id) 
        ON DELETE CASCADE ON UPDATE CASCADE ,
     CONSTRAINT transaction_currency_fk FOREIGN KEY (currency_id) REFERENCES [tu_cartera_bd].[dbo].[currency](id) 
        ON DELETE CASCADE ON UPDATE CASCADE ,
     CONSTRAINT transaction_ticker_fk FOREIGN KEY (ticker_id) REFERENCES [tu_cartera_bd].[dbo].[ticker](id) 
        ON DELETE CASCADE ON UPDATE CASCADE
    )
GO


/*------------------------------------------------------
--  CREATE STORED PROCEDURES
------------------------------------------------------*/
USE [tu_cartera_bd]
GO

/*-- Users --*/
-- Description: Get user info
CREATE OR ALTER PROCEDURE [spUserGetLogin]
    @user_id INTEGER
AS
BEGIN
    SET NOCOUNT ON

    SELECT U.[id] as 'user_id', U.[name] as 'user_name', U.[email] as 'user_email'
    FROM [tu_cartera_bd].[dbo].[user] as U
    WHERE U.[id] = @user_id
END
GO

-- Description: Check user credentials
CREATE OR ALTER PROCEDURE [spUserPostLogin]
    @user_email NVARCHAR(200), 
    @user_pass NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @user_id INTEGER;
    DECLARE @passHash BINARY(32);
    SELECT @passHash = HASHBYTES('SHA2_256', @user_pass);
    
    SELECT @user_id = U.[id]
        FROM [dbo].[user] as U 
        WHERE U.[email] = @user_email AND U.[pass] = @passHash;

    IF(@user_id is null)
        SET @user_id = -1

    SELECT @user_id as [user_id]
END
GO

-- Description: Add a new user in the system
CREATE OR ALTER PROCEDURE [spUserRegister]
    @user_name NVARCHAR(50), 
    @user_email NVARCHAR(200), 
    @user_pass NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
        DECLARE @user_id INTEGER;
        DECLARE @passHash BINARY(32);
        SELECT @passHash = HASHBYTES('SHA2_256', @user_pass);

        INSERT INTO [dbo].[user] (name, email, pass)
          VALUES(@user_name, @user_email, @passHash)

        SELECT @user_id = U.[id]
            FROM [dbo].[user] as U 
            WHERE U.[email] = @user_email AND U.[pass] = @passHash;

        INSERT INTO [dbo].[portfolio] (name, is_global, description, user_id)
          VALUES('GLOBAL', 1, 'Datos acumulados de todas las transacciones', @user_id)

        SELECT @user_id as 'user_id', @user_name as 'user_name', @user_email as 'user_email'
    END TRY
    BEGIN CATCH
    END CATCH
END
GO


/*-- Transactions --*/
-- Description: Get transactions list of a user
CREATE OR ALTER PROCEDURE [spTransactionList]
    @user_id INTEGER
AS
BEGIN
    SET NOCOUNT ON

    SELECT TS.[id] as 'transaction_id', TS.[number_of_shares] as 'transaction_shares', 
           TS.[unit_price] as 'transaction_unit_price', TS.[date] as 'transaction_date', 
           TS.[comment] as 'transaction_comment', 
           TC.[id] as 'ticker_id', TC.[code] as 'ticker_code', TC.[name] as 'ticker_name', 
           C.[id] as 'currency_id', C.[code] as 'currency_code', 
           TT.[id] as 'transaction_type_id', TT.[type] as 'transaction_type_type'
    FROM [dbo].[transaction] as TS, [dbo].[ticker] as TC, 
		 [dbo].[currency] as C, [dbo].[transaction_type] as TT
    WHERE TS.[user_id] = @user_id and TS.[ticker_id] = TC.[id] and TS.[currency_id] = C.[id] and 
          TS.[transaction_type_id] = TT.[id]
    ORDER BY TS.[date] DESC
END
GO

-- Description: Get transaction item by id
CREATE OR ALTER PROCEDURE [spTransactionItem]
    @transaction_id INTEGER
AS
BEGIN
    SET NOCOUNT ON

    SELECT TS.[id] as 'transaction_id', TS.[number_of_shares] as 'transaction_shares', 
           TS.[unit_price] as 'transaction_unit_price', TS.[date] as 'transaction_date', 
           TS.[comment] as 'transaction_comment', 
           TC.[id] as 'ticker_id', TC.[code] as 'ticker_code', TC.[name] as 'ticker_name', 
           C.[id] as 'currency_id', C.[code] as 'currency_code', 
           TT.[id] as 'transaction_type_id', TT.[type] as 'transaction_type_type'
    FROM [dbo].[transaction] as TS, [dbo].[ticker] as TC, 
		 [dbo].[currency] as C, [dbo].[transaction_type] as TT
    WHERE TS.[id] = @transaction_id and TS.[ticker_id] = TC.[id] and TS.[currency_id] = C.[id] and 
          TS.[transaction_type_id] = TT.[id]
    ORDER BY TS.[date] DESC
END
GO

-- Description: Add a new transaction
CREATE OR ALTER PROCEDURE [spTransactionAdd]
    @shares NUMERIC (28),
    @price DECIMAL (16,8),
    @date DATE,
    @comment TEXT = null,
    @user_id INTEGER,
    @transaction_type_id INTEGER,
    @currency_id INTEGER,
    @ticker_id INTEGER
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
        DECLARE @transaction TABLE([transaction_id] INTEGER);

        INSERT INTO [dbo].[transaction] (number_of_shares, unit_price, date, comment, user_id, transaction_type_id, currency_id, ticker_id)
          OUTPUT INSERTED.id as 'transaction_id' INTO @transaction
          VALUES(@shares, @price, @date, @comment, @user_id, @transaction_type_id, @currency_id, @ticker_id);
        
		SELECT [transaction_id] from @transaction
    END TRY
    BEGIN CATCH
        SELECT -1 as 'transaction_id'
    END CATCH
END
GO

-- Description: Update an existing transaction
CREATE OR ALTER PROCEDURE [spTransactionEdit]
    @transaction_id INTEGER,
    @shares NUMERIC (28),
    @price DECIMAL (16,8),
    @date DATE = null,
    @comment TEXT,
    @transaction_type_id INTEGER,
    @currency_id INTEGER,
    @ticker_id INTEGER
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
        DECLARE @transaction TABLE([transaction_id] INTEGER);

        UPDATE [dbo].[transaction]
          SET [number_of_shares] = @shares, [unit_price] = @price, [date] = @date, [comment] = @comment, 
              [transaction_type_id] = @transaction_type_id, [currency_id] = @currency_id, [ticker_id] = @ticker_id
          OUTPUT INSERTED.id as 'transaction_id' INTO @transaction
          WHERE [id] = @transaction_id;
        
		SELECT [transaction_id] from @transaction
    END TRY
    BEGIN CATCH
        SELECT -1 as 'transaction_id'
    END CATCH
END
GO

-- Description: Delete an existing transaction
CREATE OR ALTER PROCEDURE [spTransactionDelete]
    @transaction_id INTEGER
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
        DECLARE @transaction TABLE([transaction_id] INTEGER);

        DELETE [dbo].[transaction]
          OUTPUT DELETED.id as 'transaction_id' INTO @transaction
          WHERE [id] = @transaction_id;
        
		SELECT [transaction_id] from @transaction
    END TRY
    BEGIN CATCH
        SELECT -1 as 'transaction_id'
    END CATCH
END
GO

