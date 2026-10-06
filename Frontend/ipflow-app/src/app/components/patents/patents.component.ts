import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatentService } from '../../services/patent.service';
import { Patent } from '../../models/patent.model';

@Component({
  selector: 'app-patents',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="container">
      <div class="header">
        <h1>Patents</h1>
        <button class="btn-primary">Add New Patent</button>
      </div>
      
      <div class="table-container">
        <table>
          <thead>
            <tr>
              <th>Application Number</th>
              <th>Title</th>
              <th>Client</th>
              <th>Status</th>
              <th>Filing Date</th>
              <th>Expiry Date</th>
              <th>Lawyer</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr *ngFor="let patent of patents">
              <td>{{ patent.applicationNumber }}</td>
              <td>{{ patent.title }}</td>
              <td>{{ patent.clientName }}</td>
              <td><span class="status">{{ patent.status }}</span></td>
              <td>{{ patent.filingDate | date:'MMM dd, yyyy' }}</td>
              <td>{{ patent.expiryDate ? (patent.expiryDate | date:'MMM dd, yyyy') : '-' }}</td>
              <td>{{ patent.lawyerName || 'Unassigned' }}</td>
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
export class PatentsComponent implements OnInit {
  private patentService = inject(PatentService);
  patents: Patent[] = [];

  ngOnInit(): void {
    this.loadPatents();
  }

  loadPatents(): void {
    this.patentService.getAll().subscribe({
      next: (response) => {
        this.patents = response.patents;
      },
      error: (err) => console.error('Error loading patents:', err)
    });
  }
}
