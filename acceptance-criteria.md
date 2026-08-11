# Acceptance Criteria

## Core

- [x] A user can create a ticket via the UI
- [x] A user can view all tickets from the database
- [x] A user can open a ticket detail view
- [x] A user can update ticket fields and reassign
- [x] A user can add comments
- [x] Status changes only through valid transitions; invalid ones are rejected
- [x] Keyword search and status filter work
- [x] Data remains available after restart
- [x] Backend validation prevents invalid records
- [x] No secrets committed to the repo
- [x] State-machine integration tests pass

## Validation

- [x] Required fields (title, description, createdBy) enforced on create
- [x] Priority must be Low/Medium/High/Critical
- [x] Status must be a valid enum value
- [x] Invalid status transitions return HTTP 422 with descriptive message
- [x] Frontend displays validation errors from backend

## Error Handling

- [x] 400 for validation failures with field-level details
- [x] 404 for non-existent tickets/users
- [x] 422 for invalid status transitions
- [x] 500 for unexpected server errors (no stack traces exposed)
- [x] Frontend shows error alerts for API failures

## Testing

- [x] Unit tests for state machine (all valid/invalid transitions)
- [x] Integration tests for API status transitions (with DB)
- [x] Tests for validation rejection (empty fields, invalid status)

## Documentation

- [x] README with setup instructions
- [x] Database setup notes with schema and seed scripts
- [x] API contract documented
- [x] Environment variable example (.env.example)
- [x] Full lifecycle artifacts in repository
- [x] AI prompt history organized by activity
