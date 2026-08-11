import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TicketService } from '../../services/ticket.service';
import { Ticket, TicketStatus } from '../../models/ticket.models';
import { getStatusColor, getPriorityColor } from '../../utils/helpers';

@Component({
  selector: 'app-ticket-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './ticket-list.component.html',
  styleUrl: './ticket-list.component.css',
})
export class TicketListComponent implements OnInit {
  tickets: Ticket[] = [];
  loading = true;
  error: string | null = null;
  search = '';
  statusFilter: TicketStatus | '' = '';

  readonly statuses: (TicketStatus | '')[] = ['', 'Open', 'In Progress', 'Resolved', 'Closed', 'Cancelled'];
  readonly getStatusColor = getStatusColor;
  readonly getPriorityColor = getPriorityColor;

  constructor(private ticketService: TicketService) {}

  ngOnInit(): void {
    this.loadTickets();
  }

  loadTickets(): void {
    this.loading = true;
    this.error = null;
    this.ticketService.getTickets(this.search || undefined, this.statusFilter || undefined).subscribe({
      next: (data) => { this.tickets = data; this.loading = false; },
      error: (err) => { this.error = err.message; this.loading = false; },
    });
  }

  onFilterChange(): void {
    this.loadTickets();
  }
}
