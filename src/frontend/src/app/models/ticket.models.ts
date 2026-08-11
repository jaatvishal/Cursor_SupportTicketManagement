export type TicketStatus = 'Open' | 'In Progress' | 'Resolved' | 'Closed' | 'Cancelled';
export type TicketPriority = 'Low' | 'Medium' | 'High' | 'Critical';

export interface User {
  id: number;
  name: string;
  email: string;
  role: string;
}

export interface Ticket {
  id: number;
  title: string;
  description: string;
  priority: TicketPriority;
  status: TicketStatus;
  assignedTo: number | null;
  createdBy: number;
  createdAt: string;
  updatedAt: string;
  assigneeName?: string;
  creatorName?: string;
}

export interface Comment {
  id: number;
  ticketId: number;
  message: string;
  createdBy: number;
  createdAt: string;
  authorName?: string;
}

export interface TicketDetail extends Ticket {
  comments: Comment[];
}

export interface ApiError {
  error: string;
  details?: string[];
}

export interface CreateTicketRequest {
  title: string;
  description: string;
  priority: TicketPriority;
  assignedTo: number | null;
  createdBy: number;
}

export interface UpdateTicketRequest {
  title?: string;
  description?: string;
  priority?: TicketPriority;
  assignedTo?: number | null;
  status?: TicketStatus;
}

export interface CreateCommentRequest {
  message: string;
  createdBy: number;
}
