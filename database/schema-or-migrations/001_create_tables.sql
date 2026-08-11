-- Support Ticket Management System - Schema
-- SQL Server

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        id          INT IDENTITY(1,1) PRIMARY KEY,
        name        NVARCHAR(100) NOT NULL,
        email       NVARCHAR(255) NOT NULL UNIQUE,
        role        NVARCHAR(50) NOT NULL CHECK (role IN ('Admin', 'Agent', 'User'))
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Tickets')
BEGIN
    CREATE TABLE Tickets (
        id          INT IDENTITY(1,1) PRIMARY KEY,
        title       NVARCHAR(200) NOT NULL,
        description NVARCHAR(MAX) NOT NULL,
        priority    NVARCHAR(20) NOT NULL CHECK (priority IN ('Low', 'Medium', 'High', 'Critical')),
        status      NVARCHAR(20) NOT NULL DEFAULT 'Open'
                    CHECK (status IN ('Open', 'In Progress', 'Resolved', 'Closed', 'Cancelled')),
        assignedTo  INT NULL REFERENCES Users(id),
        createdBy   INT NOT NULL REFERENCES Users(id),
        createdAt   DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        updatedAt   DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Comments')
BEGIN
    CREATE TABLE Comments (
        id          INT IDENTITY(1,1) PRIMARY KEY,
        ticketId    INT NOT NULL REFERENCES Tickets(id) ON DELETE CASCADE,
        message     NVARCHAR(MAX) NOT NULL,
        createdBy   INT NOT NULL REFERENCES Users(id),
        createdAt   DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
END
GO

CREATE INDEX IX_Tickets_Status ON Tickets(status);
CREATE INDEX IX_Tickets_AssignedTo ON Tickets(assignedTo);
CREATE INDEX IX_Tickets_CreatedBy ON Tickets(createdBy);
CREATE INDEX IX_Comments_TicketId ON Comments(ticketId);
GO
