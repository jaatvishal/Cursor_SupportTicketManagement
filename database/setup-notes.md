# Database Setup Notes

## Database Choice

**Microsoft SQL Server Express** — local instance with Windows Authentication.

## Connection String

```
Data Source=localhost\SQLEXPRESS;Initial Catalog=SupportTicketDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Command Timeout=0
```

This is configured in `src/backend/SupportTicket.Api/appsettings.Development.json`.

## Prerequisites

- SQL Server Express installed and running (`localhost\SQLEXPRESS`)
- Windows Authentication enabled
- `sqlcmd` utility (included with SQL Server tools)

## Quick Setup (Windows)

Run one of these from the **repository root**:

### Option A: Batch script
```bat
database\setup-local.bat
```

### Option B: PowerShell
```powershell
.\database\setup-local.ps1
```

### Option C: Manual sqlcmd
```bat
sqlcmd -S localhost\SQLEXPRESS -E -i database\schema-or-migrations\000_create_database.sql
sqlcmd -S localhost\SQLEXPRESS -E -d SupportTicketDB -i database\schema-or-migrations\001_create_tables.sql
sqlcmd -S localhost\SQLEXPRESS -E -d SupportTicketDB -i database\seed-data\seed.sql
```

### Option D: SQL Server Management Studio (SSMS)

1. Connect to `localhost\SQLEXPRESS` with Windows Authentication
2. Open and execute each script in order:
   - `database/schema-or-migrations/000_create_database.sql`
   - `database/schema-or-migrations/001_create_tables.sql`
   - `database/seed-data/seed.sql`

## Scripts (run in order)

| Script | Purpose |
|--------|---------|
| `000_create_database.sql` | Creates `SupportTicketDB` |
| `001_create_tables.sql` | Creates Users, Tickets, Comments tables |
| `seed.sql` | Inserts 5 users, 5 tickets, 7 comments |

## Verification

```sql
USE SupportTicketDB;
SELECT COUNT(*) AS UserCount FROM Users;      -- Expected: 5
SELECT COUNT(*) AS TicketCount FROM Tickets;  -- Expected: 5
SELECT COUNT(*) AS CommentCount FROM Comments; -- Expected: 7
```

## Start the API

```bash
cd src/backend
dotnet run --project SupportTicket.Api
```

The API connects to `SupportTicketDB` on `localhost\SQLEXPRESS` using your Windows credentials.

## Troubleshooting

| Issue | Solution |
|-------|----------|
| Cannot connect to SQLEXPRESS | Open **Services** and ensure **SQL Server (SQLEXPRESS)** is running |
| Login failed | Use Windows Authentication (`-E` flag in sqlcmd); run SSMS/cmd as your Windows user |
| Database not found | Run `000_create_database.sql` first |
| sqlcmd not found | Install [SQL Server Command Line Utilities](https://learn.microsoft.com/en-us/sql/tools/sqlcmd/sqlcmd-utility) or use SSMS |
| Certificate error | `TrustServerCertificate=True` is already set in the connection string |
