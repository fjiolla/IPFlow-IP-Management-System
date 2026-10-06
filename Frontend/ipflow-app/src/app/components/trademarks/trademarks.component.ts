import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrademarkService } from '../../services/trademark.service';
import { Trademark } from '../../models/trademark.model';

@Component({
  selector: 'app-trademarks',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="container">
      <div class="header">
        <h1>Trademarks</h1>
        <button class="btn-primary">Add New Trademark</button>
      </div>
      
      <div class="table-container">
        <table>
          <thead>
            <tr>
              <th>Application Number</th>
              <th>Name</th>
              <th>Client</th>
              <th>Class</th>
              <th>Status</th>
              <th>Registration Date</th>
              <th>Renewal Date</th>
              <th>Lawyer</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr *ngFor="let trademark of trademarks">
              <td>{{ trademark.applicationNumber }}</td>
              <td>{{ trademark.name }}</td>
              <td>{{ trademark.clientName }}</td>
              <td>{{ trademark.classNumber }}</td>
              <td><span class="status">{{ trademark.status }}</span></td>
              <td>{{ trademark.registrationDate | date:'MMM dd, yyyy' }}</td>
              <td>{{ trademark.renewalDate ? (trademark.renewalDate | date:'MMM dd, yyyy') : '-' }}</td>
              <td>{{ trademark.lawyerName || 'Unassigned' }}</td>
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
    .status { padding: 0.25rem 0.75rem; background: #4caf50; color: white; border-radius: 12px; font-size: 0.75rem; font-weight: 500; }
    .btn-small { padding: 0.25rem 0.75rem; margin-right: 0.5rem; background: #3f51b5; color: white; border: none; border-radius: 4px; cursor: pointer; font-size: 0.875rem; }
    .btn-small:hover { background: #303f9f; }
    .btn-danger { background: #f44336; }
    .btn-danger:hover { background: #d32f2f; }
  `]
})
export class TrademarksComponent implements OnInit {
  private trademarkService = inject(TrademarkService);
  trademarks: Trademark[] = [];

  ngOnInit(): void {
    this.loadTrademarks();
  }

  loadTrademarks(): void {
    this.trademarkService.getAll().subscribe({
      next: (response) => {
        this.trademarks = response.trademarks;
      },
      error: (err) => console.error('Error loading trademarks:', err)
    });
  }
}
