# Setup SupportTicketDB on local SQL Server Express (Windows Authentication)
# Run from repository root: .\database\setup-local.ps1

$ErrorActionPreference = "Stop"
$Server = "localhost\SQLEXPRESS"

Write-Host "Creating database..." -ForegroundColor Cyan
sqlcmd -S $Server -E -i "database\schema-or-migrations\000_create_database.sql"

Write-Host "Creating tables..." -ForegroundColor Cyan
sqlcmd -S $Server -E -d SupportTicketDB -i "database\schema-or-migrations\001_create_tables.sql"

Write-Host "Seeding data..." -ForegroundColor Cyan
sqlcmd -S $Server -E -d SupportTicketDB -i "database\seed-data\seed.sql"

Write-Host ""
Write-Host "Database setup complete!" -ForegroundColor Green
Write-Host "  Server:   $Server"
Write-Host "  Database: SupportTicketDB"
Write-Host "  Users: 5 | Tickets: 5 | Comments: 7"
