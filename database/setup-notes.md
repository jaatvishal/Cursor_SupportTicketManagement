# Database Setup Notes

## Database Choice

**Microsoft SQL Server** — chosen for enterprise alignment and strong relational data integrity for the ticket lifecycle state machine.

## Prerequisites

- SQL Server 2019+ (or SQL Server Express)
- SQL Server Management Studio (SSMS) or Azure Data Studio (optional)
- Docker (alternative — see below)

## Option A: Docker (Recommended for Local Development)

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrong!Passw0rd" \
  -p 1433:1433 --name support-ticket-sql \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

Wait ~30 seconds for SQL Server to start, then run the schema and seed scripts.

## Option B: Local SQL Server Instance

Use your existing SQL Server instance and create a database:

```sql
CREATE DATABASE SupportTicketDB;
GO
USE SupportTicketDB;
GO
```

## Running Migrations

Execute scripts in order:

1. `database/schema-or-migrations/001_create_tables.sql`
2. `database/seed-data/seed.sql`

### Using sqlcmd

```bash
sqlcmd -S localhost,1433 -U sa -P "YourStrong!Passw0rd" -i database/schema-or-migrations/001_create_tables.sql
sqlcmd -S localhost,1433 -U sa -P "YourStrong!Passw0rd" -i database/seed-data/seed.sql
```

### Using the .NET API

After running SQL scripts, start the API — EF Core will connect to the existing schema:

```bash
cd src/backend
dotnet run --project SupportTicket.Api
```

The API reads the connection string from `appsettings.Development.json`.

## Environment Variables

Copy `.env.example` to `.env` in `src/backend/` and configure:

| Variable | Description | Example |
|----------|-------------|---------|
| `DB_SERVER` | SQL Server host | `localhost` |
| `DB_PORT` | SQL Server port | `1433` |
| `DB_NAME` | Database name | `SupportTicketDB` |
| `DB_USER` | SQL login | `sa` |
| `DB_PASSWORD` | SQL password | `YourStrong!Passw0rd` |
| `DB_ENCRYPT` | Use encryption | `false` (local dev) |
| `DB_TRUST_SERVER_CERTIFICATE` | Trust self-signed cert | `true` (local dev) |

## Verification

After setup, verify with:

```sql
SELECT COUNT(*) AS UserCount FROM Users;      -- Expected: 5
SELECT COUNT(*) AS TicketCount FROM Tickets;  -- Expected: 5
SELECT COUNT(*) AS CommentCount FROM Comments; -- Expected: 7
```

## Troubleshooting

| Issue | Solution |
|-------|----------|
| Connection refused | Ensure SQL Server is running and port 1433 is open |
| Login failed | Verify SA password matches `.env` |
| Certificate error | Set `DB_TRUST_SERVER_CERTIFICATE=true` for local dev |
| Database not found | Create `SupportTicketDB` before running migrations |
