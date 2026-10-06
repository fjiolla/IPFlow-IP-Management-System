import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CaseService } from '../../services/case.service';
import { Case } from '../../models/case.model';

@Component({
  selector: 'app-cases',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="container">
      <div class="header">
        <h1>Litigation Cases</h1>
        <button class="btn-primary">Add New Case</button>
      </div>
      
      <div class="table-container">
        <table>
          <thead>
            <tr>
              <th>Case Number</th>
              <th>Title</th>
              <th>Client</th>
              <th>Type</th>
              <th>Status</th>
              <th>Open Date</th>
              <th>Next Hearing</th>
              <th>Lawyer</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr *ngFor="let case of cases">
              <td>{{ case.caseNumber }}</td>
              <td>{{ case.title }}</td>
              <td>{{ case.clientName }}</td>
              <td>{{ case.caseType }}</td>
              <td><span class="status">{{ case.status }}</span></td>
              <td>{{ case.openDate | date:'MMM dd, yyyy' }}</td>
              <td>{{ case.nextHearingDate ? (case.nextHearingDate | date:'MMM dd, yyyy') : '-' }}</td>
              <td>{{ case.lawyerName || 'Unassigned' }}</td>
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
export class CasesComponent implements OnInit {
  private caseService = inject(CaseService);
  cases: Case[] = [];

  ngOnInit(): void {
    this.loadCases();
  }

  loadCases(): void {
    this.caseService.getAll().subscribe({
      next: (response) => {
        this.cases = response.cases;
      },
      error: (err) => console.error('Error loading cases:', err)
    });
  }
}
