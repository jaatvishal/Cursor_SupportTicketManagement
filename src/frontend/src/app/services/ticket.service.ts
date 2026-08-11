import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {
  Ticket, TicketDetail, User, Comment,
  CreateTicketRequest, UpdateTicketRequest, CreateCommentRequest,
  TicketStatus, ApiError
} from '../models/ticket.models';

@Injectable({ providedIn: 'root' })
export class TicketService {
  private readonly apiUrl = 'http://localhost:5000/api/tickets';

  constructor(private http: HttpClient) {}

  getTickets(search?: string, status?: TicketStatus): Observable<Ticket[]> {
    let params = new HttpParams();
    if (search?.trim()) params = params.set('search', search.trim());
    if (status) params = params.set('status', status);
    return this.http.get<Ticket[]>(this.apiUrl, { params }).pipe(catchError(this.handleError));
  }

  getTicket(id: number): Observable<TicketDetail> {
    return this.http.get<TicketDetail>(`${this.apiUrl}/${id}`).pipe(catchError(this.handleError));
  }

  createTicket(data: CreateTicketRequest): Observable<Ticket> {
    return this.http.post<Ticket>(this.apiUrl, data).pipe(catchError(this.handleError));
  }

  updateTicket(id: number, data: UpdateTicketRequest): Observable<Ticket> {
    return this.http.put<Ticket>(`${this.apiUrl}/${id}`, data).pipe(catchError(this.handleError));
  }

  updateStatus(id: number, status: TicketStatus): Observable<Ticket> {
    return this.http.patch<Ticket>(`${this.apiUrl}/${id}/status`, { status }).pipe(catchError(this.handleError));
  }

  addComment(ticketId: number, data: CreateCommentRequest): Observable<Comment> {
    return this.http.post<Comment>(`${this.apiUrl}/${ticketId}/comments`, data).pipe(catchError(this.handleError));
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/users`).pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    const body = error.error as ApiError;
    let message = body?.error || `HTTP ${error.status}: ${error.statusText}`;
    if (body?.details?.length) {
      message += ': ' + body.details.join(', ');
    }
    return throwError(() => new Error(message));
  }
}
