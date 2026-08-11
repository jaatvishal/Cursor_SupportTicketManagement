# Data Model

## Entity Relationship Diagram

```
┌──────────────┐       ┌──────────────┐       ┌──────────────┐
│    Users     │       │   Tickets    │       │   Comments   │
├──────────────┤       ├──────────────┤       ├──────────────┤
│ id (PK)      │◄──┐   │ id (PK)      │◄──────│ id (PK)      │
│ name         │   ├───│ assignedTo   │       │ ticketId (FK)│
│ email        │   │   │ createdBy    │───┐   │ message      │
│ role         │   │   │ title        │   │   │ createdBy(FK)│──┐
└──────────────┘   │   │ description  │   │   │ createdAt    │  │
                   │   │ priority     │   │   └──────────────┘  │
                   │   │ status       │   │                     │
                   │   │ createdAt    │   └─────────────────────┘
                   │   │ updatedAt    │
                   └───────────────────────────────────────────┘
```

## Users

Seeded only — no user management UI.

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| id | INT | PK, IDENTITY | Auto-increment ID |
| name | NVARCHAR(100) | NOT NULL | Display name |
| email | NVARCHAR(255) | NOT NULL, UNIQUE | Email address |
| role | NVARCHAR(50) | NOT NULL, CHECK | Admin, Agent, or User |

**Seed data:** 5 users (1 Admin, 2 Agents, 2 Users)

## Tickets

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| id | INT | PK, IDENTITY | Auto-increment ID |
| title | NVARCHAR(200) | NOT NULL | Ticket summary |
| description | NVARCHAR(MAX) | NOT NULL | Full description |
| priority | NVARCHAR(20) | NOT NULL, CHECK | Low, Medium, High, Critical |
| status | NVARCHAR(20) | NOT NULL, DEFAULT 'Open', CHECK | Open, In Progress, Resolved, Closed, Cancelled |
| assignedTo | INT | FK → Users(id), NULL | Assigned agent |
| createdBy | INT | FK → Users(id), NOT NULL | Ticket creator |
| createdAt | DATETIME2 | NOT NULL, DEFAULT GETUTCDATE() | Creation timestamp |
| updatedAt | DATETIME2 | NOT NULL, DEFAULT GETUTCDATE() | Last update timestamp |

**Seed data:** 5 tickets across all statuses

## Comments

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| id | INT | PK, IDENTITY | Auto-increment ID |
| ticketId | INT | FK → Tickets(id), ON DELETE CASCADE | Parent ticket |
| message | NVARCHAR(MAX) | NOT NULL | Comment text |
| createdBy | INT | FK → Users(id), NOT NULL | Comment author |
| createdAt | DATETIME2 | NOT NULL, DEFAULT GETUTCDATE() | Creation timestamp |

**Seed data:** 7 comments across 4 tickets

## Status State Machine

```
        ┌──────────────────────────────────────┐
        │                                      │
        ▼                                      │
    ┌───────┐    ┌─────────────┐    ┌──────────┐    ┌────────┐
    │ Open  │───►│ In Progress │───►│ Resolved │───►│ Closed │
    └───┬───┘    └──────┬──────┘    └──────────┘    └────────┘
        │               │
        │               │
        ▼               ▼
    ┌───────────────────────┐
    │      Cancelled        │
    └───────────────────────┘
```

Terminal states: Closed, Cancelled (no further transitions allowed)
