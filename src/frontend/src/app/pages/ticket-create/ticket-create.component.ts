import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { TicketService } from '../../services/ticket.service';
import { User, TicketPriority } from '../../models/ticket.models';

@Component({
  selector: 'app-ticket-create',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './ticket-create.component.html',
  styleUrl: './ticket-create.component.css',
})
export class TicketCreateComponent implements OnInit {
  users: User[] = [];
  title = '';
  description = '';
  priority: TicketPriority = 'Medium';
  assignedTo: number | null = null;
  createdBy = 4;
  errors: Record<string, string> = {};
  submitError: string | null = null;
  submitting = false;

  readonly priorities: TicketPriority[] = ['Low', 'Medium', 'High', 'Critical'];

  constructor(private ticketService: TicketService, private router: Router) {}

  ngOnInit(): void {
    this.ticketService.getUsers().subscribe({
      next: (users) => this.users = users,
      error: () => this.submitError = 'Failed to load users',
    });
  }

  get agents(): User[] {
    return this.users.filter(u => u.role === 'Agent' || u.role === 'Admin');
  }

  validate(): boolean {
    this.errors = {};
    if (!this.title.trim()) this.errors['title'] = 'Title is required';
    if (!this.description.trim()) this.errors['description'] = 'Description is required';
    return Object.keys(this.errors).length === 0;
  }

  onSubmit(): void {
    this.submitError = null;
    if (!this.validate()) return;

    this.submitting = true;
    this.ticketService.createTicket({
      title: this.title.trim(),
      description: this.description.trim(),
      priority: this.priority,
      assignedTo: this.assignedTo,
      createdBy: this.createdBy,
    }).subscribe({
      next: (ticket) => this.router.navigate(['/tickets', ticket.id]),
      error: (err) => { this.submitError = err.message; this.submitting = false; },
    });
  }

  cancel(): void {
    this.router.navigate(['/']);
  }
}
