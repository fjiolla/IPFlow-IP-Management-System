import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientService } from '../../services/client.service';
import { Client } from '../../models/client.model';

@Component({
  selector: 'app-clients',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="container">
      <div class="header">
        <h1>Clients</h1>
        <button class="btn-primary">Add New Client</button>
      </div>
      
      <div class="table-container">
        <table>
          <thead>
            <tr>
              <th>Name</th>
              <th>Email</th>
              <th>Phone</th>
              <th>Address</th>
              <th>Created At</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr *ngFor="let client of clients">
              <td>{{ client.name }}</td>
              <td>{{ client.email }}</td>
              <td>{{ client.phone }}</td>
              <td>{{ client.address }}</td>
              <td>{{ client.createdAt | date:'MMM dd, yyyy' }}</td>
              <td>
                <button class="btn-small">Edit</button>
                <button class="btn-small btn-danger">Delete</button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  `,
  styles: [`
    .container { padding: 2rem; }
    .header { display: flex; justify-content: space-between; align-items: center; margin-bottom: 2rem; }
    h1 { margin: 0; color: #333; }
    .btn-primary { padding: 0.75rem 1.5rem; background: #3f51b5; color: white; border: none; border-radius: 4px; cursor: pointer; font-weight: 500; }
    .btn-primary:hover { background: #303f9f; }
    .table-container { background: white; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); overflow-x: auto; }
    table { width: 100%; border-collapse: collapse; }
    th, td { padding: 1rem; text-align: left; border-bottom: 1px solid #eee; }
    th { background: #f5f5f5; font-weight: 600; color: #333; }
    .btn-small { padding: 0.25rem 0.75rem; margin-right: 0.5rem; background: #3f51b5; color: white; border: none; border-radius: 4px; cursor: pointer; font-size: 0.875rem; }
    .btn-small:hover { background: #303f9f; }
    .btn-danger { background: #f44336; }
    .btn-danger:hover { background: #d32f2f; }
  `]
})
export class ClientsComponent implements OnInit {
  private clientService = inject(ClientService);
  clients: Client[] = [];

  ngOnInit(): void {
    this.loadClients();
  }

  loadClients(): void {
    this.clientService.getAll().subscribe({
      next: (response) => {
        this.clients = response.clients;
      },
      error: (err) => console.error('Error loading clients:', err)
    });
  }
}
