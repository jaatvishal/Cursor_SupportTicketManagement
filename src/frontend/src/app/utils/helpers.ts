import { TicketStatus } from '../models/ticket.models';

const VALID_TRANSITIONS: Record<TicketStatus, TicketStatus[]> = {
  'Open': ['In Progress', 'Cancelled'],
  'In Progress': ['Resolved', 'Cancelled'],
  'Resolved': ['Closed'],
  'Closed': [],
  'Cancelled': [],
};

export function getAllowedTransitions(status: TicketStatus): TicketStatus[] {
  return VALID_TRANSITIONS[status] || [];
}

export function getStatusColor(status: TicketStatus): string {
  const colors: Record<TicketStatus, string> = {
    'Open': '#3b82f6',
    'In Progress': '#f59e0b',
    'Resolved': '#10b981',
    'Closed': '#6b7280',
    'Cancelled': '#ef4444',
  };
  return colors[status];
}

export function getPriorityColor(priority: string): string {
  const colors: Record<string, string> = {
    Low: '#6b7280', Medium: '#3b82f6', High: '#f59e0b', Critical: '#ef4444',
  };
  return colors[priority] || '#6b7280';
}

export function formatDate(dateStr: string): string {
  return new Date(dateStr).toLocaleString();
}
