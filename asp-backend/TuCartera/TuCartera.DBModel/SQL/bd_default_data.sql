/* Generated by Oracle SQL Developer Data Modeler 20.4.1.406.0906
--   at:        2021-04-10 12:05:30 CEST
--   site:      SQL Server 2012
--   type:      SQL Server 2012
--	 Creado por:	Borja Delgado Martin
*/


/*------------------------------------------------------
--  INSERT INITIAL VALUES
------------------------------------------------------*/
USE [tu_cartera_bd]
GO

/*-- User --*/
-- Register users
EXEC	[dbo].[spUserRegister]
 			@user_name = N'Admin', @user_email = N'admin@mail.com', @user_pass = N'12345678'
GO

EXEC	[dbo].[spUserRegister]
 			@user_name = N'Usuario 1', @user_email = N'user1@mail.com', @user_pass = N'12345678'
GO


/*-- Currency --*/
INSERT INTO [dbo].[currency] (name, code) VALUES('Dolar estadounidense', 'USD');
INSERT INTO [dbo].[currency] (name, code) VALUES ('Euro', 'EUR');


/*-- Portfolio --*/
INSERT INTO [dbo].[portfolio] (name, is_global, description, user_id) VALUES ('Tecnológicas', 0, 'Empresas tecnológicas', '1');
INSERT INTO [dbo].[portfolio] (name, is_global, description, user_id) VALUES ('Turísticas', 0, 'Empresas turísticas', '1');


/*-- Ticker --*/
INSERT INTO [dbo].[ticker] (code, name) VALUES ('AAPL', 'Apple');
INSERT INTO [dbo].[ticker] (code, name) VALUES ('INTC', 'Intel Corp');
INSERT INTO [dbo].[ticker] (code, name) VALUES ('MCD',  'McDonalds Corp');
INSERT INTO [dbo].[ticker] (code, name) VALUES ('MSFT', 'Microsoft Corp');


/*-- Portfolio tickers --*/
INSERT INTO [dbo].[portfolio_tickers] (portfolio_id, ticker_id) VALUES ('3', '1');
INSERT INTO [dbo].[portfolio_tickers] (portfolio_id, ticker_id) VALUES ('3', '2');
INSERT INTO [dbo].[portfolio_tickers] (portfolio_id, ticker_id) VALUES ('4', '3');
INSERT INTO [dbo].[portfolio_tickers] (portfolio_id, ticker_id) VALUES ('3', '4');


/*-- Transaction type --*/
INSERT INTO [dbo].[transaction_type] (type, description) VALUES ('Compra', 'Adquisición de acciones de una empresa');
INSERT INTO [dbo].[transaction_type] (type, description) VALUES ('Venta', 'Venta de acciones de una empresa');
INSERT INTO [dbo].[transaction_type] (type, description) VALUES ('Dividendo', 'Reparto de beneficios de la empresa');
INSERT INTO [dbo].[transaction_type] (type, description) VALUES ('Comision', 'Gastos derivados de operar o mantener las acciones');


/*-- Transaction --*/
INSERT INTO [dbo].[transaction] (number_of_shares, unit_price, date, comment, user_id, transaction_type_id, currency_id, ticker_id) VALUES ('5', '133.19', '2021-02-17', NULL, '1', '1', '1', '1');
INSERT INTO [dbo].[transaction] (number_of_shares, unit_price, date, comment, user_id, transaction_type_id, currency_id, ticker_id) VALUES ('10', '60.78', '2021-02-26', NULL, '1', '1', '1', '2');
INSERT INTO [dbo].[transaction] (number_of_shares, unit_price, date, comment, user_id, transaction_type_id, currency_id, ticker_id) VALUES ('3', '206.14', '2021-02-26', NULL, '1', '1', '1', '3');
INSERT INTO [dbo].[transaction] (number_of_shares, unit_price, date, comment, user_id, transaction_type_id, currency_id, ticker_id) VALUES ('5', '122.06', '2021-03-03', NULL, '1', '2', '1', '1');
INSERT INTO [dbo].[transaction] (number_of_shares, unit_price, date, comment, user_id, transaction_type_id, currency_id, ticker_id) VALUES ('5', '227.56', '2021-03-03', NULL, '1', '1', '1', '4');
