-- Seed data for Support Ticket Management System

USE SupportTicketDB;
GO

-- Clear existing data (for re-seeding)
DELETE FROM Comments;
DELETE FROM Tickets;
DELETE FROM Users;
GO

-- Seed Users
SET IDENTITY_INSERT Users ON;
INSERT INTO Users (Id, Name, Email, Role) VALUES
    (1, 'Alice Admin',   'alice@company.com',  'Admin'),
    (2, 'Bob Agent',     'bob@company.com',    'Agent'),
    (3, 'Carol Agent',   'carol@company.com',  'Agent'),
    (4, 'Dave User',     'dave@company.com',   'User'),
    (5, 'Eve User',      'eve@company.com',    'User');
SET IDENTITY_INSERT Users OFF;
GO

-- Seed Tickets
SET IDENTITY_INSERT Tickets ON;
INSERT INTO Tickets (Id, Title, Description, Priority, Status, AssignedTo, CreatedBy, CreatedAt, UpdatedAt) VALUES
    (1, 'Cannot login to portal',       'User reports 500 error when attempting to login via SSO.',           'High',     'Open',         2, 4, DATEADD(day, -5, GETUTCDATE()), DATEADD(day, -5, GETUTCDATE())),
    (2, 'Printer not working',          'Office printer on 3rd floor shows offline status.',                  'Medium',   'In Progress',  3, 5, DATEADD(day, -3, GETUTCDATE()), DATEADD(day, -1, GETUTCDATE())),
    (3, 'Request new software license', 'Need Adobe Creative Suite license for design team.',               'Low',      'Resolved',     2, 4, DATEADD(day, -10, GETUTCDATE()), DATEADD(day, -2, GETUTCDATE())),
    (4, 'VPN connection drops',         'VPN disconnects every 15 minutes on macOS Sonoma.',                'Critical', 'Open',         NULL, 5, DATEADD(day, -1, GETUTCDATE()), DATEADD(day, -1, GETUTCDATE())),
    (5, 'Email sync issue',             'Outlook not syncing sent items folder.',                             'Medium',   'Cancelled',    3, 4, DATEADD(day, -7, GETUTCDATE()), DATEADD(day, -4, GETUTCDATE()));
SET IDENTITY_INSERT Tickets OFF;
GO

-- Seed Comments
SET IDENTITY_INSERT Comments ON;
INSERT INTO Comments (Id, TicketId, Message, CreatedBy, CreatedAt) VALUES
    (1, 1, 'Investigating SSO configuration. Will update shortly.',          2, DATEADD(day, -4, GETUTCDATE())),
    (2, 1, 'Found misconfigured SAML endpoint. Applying fix.',                 2, DATEADD(day, -3, GETUTCDATE())),
    (3, 2, 'Checked printer network cable. Replaced faulty cable.',          3, DATEADD(day, -2, GETUTCDATE())),
    (4, 2, 'Printer back online. Monitoring for 24 hours.',                  3, DATEADD(day, -1, GETUTCDATE())),
    (5, 3, 'License request submitted to procurement.',                      2, DATEADD(day, -8, GETUTCDATE())),
    (6, 3, 'License approved and installed. Closing ticket.',                2, DATEADD(day, -2, GETUTCDATE())),
    (7, 5, 'User resolved issue independently. Cancelling ticket.',          4, DATEADD(day, -4, GETUTCDATE()));
SET IDENTITY_INSERT Comments OFF;
GO

PRINT 'Seed data applied successfully.';
PRINT 'Users: 5 | Tickets: 5 | Comments: 7';
GO
