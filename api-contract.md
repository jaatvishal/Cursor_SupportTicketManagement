# API Contract

Base URL: `http://localhost:5000/api`

Implementation: ASP.NET Core 9 controllers in
`src/backend/SupportTicket.Api/Controllers/TicketsController.cs`.

## Health Check

**Method:** GET  
**Path:** `/health`  
**Purpose:** Verify API is running

### Response
```json
{ "status": "ok", "timestamp": "2026-08-11T12:00:00.000Z" }
```

---

## List Tickets

**Method:** GET  
**Path:** `/tickets`  
**Purpose:** List all tickets with optional search and status filter

### Query Parameters
| Param | Type | Required | Description |
|-------|------|----------|-------------|
| search | string | No | Keyword search on title and description |
| status | string | No | Filter by status (Open, In Progress, Resolved, Closed, Cancelled) |

### Response
```json
[
  {
    "id": 1,
    "title": "Cannot login to portal",
    "description": "User reports 500 error...",
    "priority": "High",
    "status": "Open",
    "assignedTo": 2,
    "createdBy": 4,
    "createdAt": "2026-08-06T10:00:00.000Z",
    "updatedAt": "2026-08-06T10:00:00.000Z",
    "assigneeName": "Bob Agent",
    "creatorName": "Dave User"
  }
]
```

---

## Get Ticket Detail

**Method:** GET  
**Path:** `/tickets/:id`  
**Purpose:** Get ticket with comments

### Response
```json
{
  "id": 1,
  "title": "Cannot login to portal",
  "description": "...",
  "priority": "High",
  "status": "Open",
  "assignedTo": 2,
  "createdBy": 4,
  "createdAt": "...",
  "updatedAt": "...",
  "assigneeName": "Bob Agent",
  "creatorName": "Dave User",
  "comments": [
    {
      "id": 1,
      "ticketId": 1,
      "message": "Investigating SSO configuration.",
      "createdBy": 2,
      "createdAt": "...",
      "authorName": "Bob Agent"
    }
  ]
}
```

### Error Responses
- `404`: Ticket not found

---

## Create Ticket

**Method:** POST  
**Path:** `/tickets`  
**Purpose:** Create a new ticket

### Request
```json
{
  "title": "New issue",
  "description": "Detailed description",
  "priority": "Medium",
  "assignedTo": 2,
  "createdBy": 4
}
```

### Validation Rules
- `title`: required, 1-200 characters
- `description`: required, non-empty
- `priority`: required, one of Low/Medium/High/Critical
- `assignedTo`: optional, positive integer (must exist in Users)
- `createdBy`: required, positive integer (must exist in Users)

### Response: `201 Created`
Returns the created ticket object.

### Error Responses
- `400`: Validation failed with details array

---

## Update Ticket

**Method:** PUT  
**Path:** `/tickets/:id`  
**Purpose:** Update ticket fields (title, description, priority, assignee, status)

### Request
```json
{
  "title": "Updated title",
  "description": "Updated description",
  "priority": "High",
  "assignedTo": 3,
  "status": "In Progress"
}
```

All fields optional. If `status` is included, state machine rules apply.

### Error Responses
- `400`: Validation failed
- `404`: Ticket not found
- `422`: Invalid status transition

---

## Update Ticket Status

**Method:** PATCH  
**Path:** `/tickets/:id/status`  
**Purpose:** Change ticket status through state machine

### Request
```json
{ "status": "In Progress" }
```

### Valid Transitions
| From | To |
|------|-----|
| Open | In Progress, Cancelled |
| In Progress | Resolved, Cancelled |
| Resolved | Closed |

### Error Responses
- `400`: Invalid status value
- `404`: Ticket not found
- `422`: Invalid status transition with descriptive message

---

## Add Comment

**Method:** POST  
**Path:** `/tickets/:id/comments`  
**Purpose:** Add a comment to a ticket

### Request
```json
{
  "message": "Comment text",
  "createdBy": 4
}
```

### Validation Rules
- `message`: required, non-empty
- `createdBy`: required, positive integer

### Response: `201 Created`
Returns the created comment object.

---

## List Users

**Method:** GET  
**Path:** `/tickets/users`  
**Purpose:** Get all seeded users (for assignee/creator dropdowns)

### Response
```json
[
  { "id": 1, "name": "Alice Admin", "email": "alice@company.com", "role": "Admin" }
]
```
