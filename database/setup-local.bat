@echo off
REM Setup SupportTicketDB on local SQL Server Express (Windows Authentication)
REM Run from repository root: database\setup-local.bat

echo Creating database...
sqlcmd -S localhost\SQLEXPRESS -E -i database\schema-or-migrations\000_create_database.sql
if %ERRORLEVEL% NEQ 0 goto :error

echo Creating tables...
sqlcmd -S localhost\SQLEXPRESS -E -d SupportTicketDB -i database\schema-or-migrations\001_create_tables.sql
if %ERRORLEVEL% NEQ 0 goto :error

echo Seeding data...
sqlcmd -S localhost\SQLEXPRESS -E -d SupportTicketDB -i database\seed-data\seed.sql
if %ERRORLEVEL% NEQ 0 goto :error

echo.
echo Database setup complete!
echo   Server:   localhost\SQLEXPRESS
echo   Database: SupportTicketDB
echo   Users:    5 | Tickets: 5 | Comments: 7
goto :end

:error
echo.
echo Setup failed. Ensure SQL Server Express is running and sqlcmd is available.
exit /b 1

:end
