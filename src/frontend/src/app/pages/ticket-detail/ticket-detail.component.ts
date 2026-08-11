import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { TicketService } from '../../services/ticket.service';
import { TicketDetail, User, TicketStatus, TicketPriority } from '../../models/ticket.models';
import { getAllowedTransitions, getStatusColor, getPriorityColor, formatDate } from '../../utils/helpers';

@Component({
  selector: 'app-ticket-detail',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './ticket-detail.component.html',
  styleUrl: './ticket-detail.component.css',
})
export class TicketDetailComponent implements OnInit {
  ticket: TicketDetail | null = null;
  users: User[] = [];
  loading = true;
  error: string | null = null;
  statusError: string | null = null;
  commentError: string | null = null;
  editMode = false;
  editTitle = '';
  editDescription = '';
  editPriority: TicketPriority = 'Medium';
  editAssignee: number | null = null;
  commentText = '';
  commentAuthor = 4;
  saving = false;

  readonly priorities: TicketPriority[] = ['Low', 'Medium', 'High', 'Critical'];
  readonly getStatusColor = getStatusColor;
  readonly getPriorityColor = getPriorityColor;
  readonly formatDate = formatDate;

  constructor(
    private route: ActivatedRoute,
    private ticketService: TicketService,
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.ticketService.getUsers().subscribe({ next: (u) => this.users = u });
    this.loadTicket(id);
  }

  get agents(): User[] {
    return this.users.filter(u => u.role === 'Agent' || u.role === 'Admin');
  }

  get allowedTransitions(): TicketStatus[] {
    return this.ticket ? getAllowedTransitions(this.ticket.status) : [];
  }

  loadTicket(id: number): void {
    this.loading = true;
    this.ticketService.getTicket(id).subscribe({
      next: (data) => {
        this.ticket = data;
        this.editTitle = data.title;
        this.editDescription = data.description;
        this.editPriority = data.priority;
        this.editAssignee = data.assignedTo;
        this.loading = false;
      },
      error: (err) => { this.error = err.message; this.loading = false; },
    });
  }

  save(): void {
    if (!this.ticket) return;
    this.saving = true;
    this.error = null;
    this.ticketService.updateTicket(this.ticket.id, {
      title: this.editTitle,
      description: this.editDescription,
      priority: this.editPriority,
      assignedTo: this.editAssignee,
    }).subscribe({
      next: (updated) => {
        this.ticket = { ...this.ticket!, ...updated, comments: this.ticket!.comments };
        this.editMode = false;
        this.saving = false;
      },
      error: (err) => { this.error = err.message; this.saving = false; },
    });
  }

  changeStatus(newStatus: TicketStatus): void {
    if (!this.ticket) return;
    this.statusError = null;
    this.ticketService.updateStatus(this.ticket.id, newStatus).subscribe({
      next: (updated) => {
        this.ticket = { ...this.ticket!, ...updated };
      },
      error: (err) => { this.statusError = err.message; },
    });
  }

  addComment(): void {
    if (!this.ticket || !this.commentText.trim()) return;
    this.commentError = null;
    this.ticketService.addComment(this.ticket.id, {
      message: this.commentText.trim(),
      createdBy: this.commentAuthor,
    }).subscribe({
      next: (comment) => {
        this.ticket!.comments = [...this.ticket!.comments, comment];
        this.commentText = '';
      },
      error: (err) => { this.commentError = err.message; },
    });
  }
}
