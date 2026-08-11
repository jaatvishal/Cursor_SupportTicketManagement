-- Support Ticket Management System - Schema
-- SQL Server (run against SupportTicketDB)

USE SupportTicketDB;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        Id          INT IDENTITY(1,1) PRIMARY KEY,
        Name        NVARCHAR(100) NOT NULL,
        Email       NVARCHAR(255) NOT NULL UNIQUE,
        Role        NVARCHAR(50) NOT NULL CHECK (Role IN ('Admin', 'Agent', 'User'))
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Tickets')
BEGIN
    CREATE TABLE Tickets (
        Id          INT IDENTITY(1,1) PRIMARY KEY,
        Title       NVARCHAR(200) NOT NULL,
        Description NVARCHAR(MAX) NOT NULL,
        Priority    NVARCHAR(20) NOT NULL CHECK (Priority IN ('Low', 'Medium', 'High', 'Critical')),
        Status      NVARCHAR(20) NOT NULL DEFAULT 'Open'
                    CHECK (Status IN ('Open', 'In Progress', 'Resolved', 'Closed', 'Cancelled')),
        AssignedTo  INT NULL REFERENCES Users(Id),
        CreatedBy   INT NOT NULL REFERENCES Users(Id),
        CreatedAt   DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt   DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Comments')
BEGIN
    CREATE TABLE Comments (
        Id          INT IDENTITY(1,1) PRIMARY KEY,
        TicketId    INT NOT NULL REFERENCES Tickets(Id) ON DELETE CASCADE,
        Message     NVARCHAR(MAX) NOT NULL,
        CreatedBy   INT NOT NULL REFERENCES Users(Id),
        CreatedAt   DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Tickets_Status')
    CREATE INDEX IX_Tickets_Status ON Tickets(Status);
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Tickets_AssignedTo')
    CREATE INDEX IX_Tickets_AssignedTo ON Tickets(AssignedTo);
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Tickets_CreatedBy')
    CREATE INDEX IX_Tickets_CreatedBy ON Tickets(CreatedBy);
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Comments_TicketId')
    CREATE INDEX IX_Comments_TicketId ON Comments(TicketId);
GO
