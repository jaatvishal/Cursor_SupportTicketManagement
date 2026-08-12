# UI Flow

## Navigation Structure

```
App Header
├── "All Tickets" → /           (TicketListComponent)
├── "Create Ticket" → /create   (TicketCreateComponent)
└── Ticket link → /tickets/:id  (TicketDetailComponent)
```

## Page Flows

### 1. Ticket List (`/`)

```
User lands on ticket list
  │
  ├── Sees table of all tickets (ID, title, priority, status, assignee, updated)
  │
  ├── Types in search box → API call with ?search=keyword on each change
  │
  ├── Selects status filter → API call with ?status=Open
  │
  ├── Clicks ticket title → navigates to /tickets/:id
  │
  └── Clicks "+ New Ticket" → navigates to /create
```

### 2. Create Ticket (`/create`)

```
User fills form:
  ├── Title (required)
  ├── Description (required)
  ├── Priority (dropdown: Low/Medium/High/Critical)
  ├── Assign To (dropdown: agents/admins or Unassigned)
  └── Created By (dropdown: all users)
  │
  ├── Client validation → show field errors
  │
  ├── Submit → POST /api/tickets
  │     ├── Success → redirect to /tickets/:id
  │     └── Error → show alert with backend message
  │
  └── Cancel → navigate back to /
```

### 3. Ticket Detail (`/tickets/:id`)

```
User views ticket detail (2-column layout)
  │
  ├── Left column:
  │     ├── Ticket info (title, description, badges)
  │     ├── Edit mode (inline form for title, description, priority, assignee)
  │     └── Comments section (list + add comment form)
  │
  ├── Right column:
  │     ├── Details panel (creator, assignee, dates)
  │     └── Status actions (buttons for allowed transitions only)
  │
  ├── Click "Edit" → toggle edit mode
  │     ├── Save → PUT /api/tickets/:id
  │     └── Cancel → revert changes
  │
  ├── Click status button → PATCH /api/tickets/:id/status
  │     ├── Success → update status badge
  │     └── Error (422) → show "Invalid status transition" alert
  │
  └── Add comment → POST /api/tickets/:id/comments
        ├── Success → append to comment list
        └── Error → show alert
```

## Error States

| Scenario | UI Behavior |
|----------|------------|
| API unreachable | Red alert: "Failed to load tickets" |
| Validation error (400) | Red alert with field details |
| Invalid transition (422) | Inline alert on status panel |
| Ticket not found (404) | "Ticket not found" empty state |
| Empty search results | "No tickets found" with create link |

## Status Action Buttons

Only valid transitions are shown as buttons:
- **Open ticket**: "→ In Progress", "→ Cancelled"
- **In Progress**: "→ Resolved", "→ Cancelled"
- **Resolved**: "→ Closed"
- **Closed/Cancelled**: "This ticket is in a terminal state."

Cancelled button uses danger styling (red).
