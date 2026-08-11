import { Routes } from '@angular/router';
import { TicketListComponent } from './pages/ticket-list/ticket-list.component';
import { TicketCreateComponent } from './pages/ticket-create/ticket-create.component';
import { TicketDetailComponent } from './pages/ticket-detail/ticket-detail.component';

export const routes: Routes = [
  { path: '', component: TicketListComponent },
  { path: 'create', component: TicketCreateComponent },
  { path: 'tickets/:id', component: TicketDetailComponent },
];
