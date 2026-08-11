-- Create SupportTicketDB database on local SQL Server Express
-- Run against master database

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'SupportTicketDB')
BEGIN
    CREATE DATABASE SupportTicketDB;
END
GO

USE SupportTicketDB;
GO
