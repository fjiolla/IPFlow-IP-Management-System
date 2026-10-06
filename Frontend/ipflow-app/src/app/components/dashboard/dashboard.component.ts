import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardService } from '../../services/dashboard.service';
import { Dashboard } from '../../models/dashboard.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="container">
      <h1>Dashboard</h1>
      
      <div class="stats-grid" *ngIf="dashboard">
        <div class="stat-card">
          <h3>{{ dashboard.totalPatents }}</h3>
          <p>Total Patents</p>
        </div>
        <div class="stat-card">
          <h3>{{ dashboard.activeTrademarks }}</h3>
          <p>Active Trademarks</p>
        </div>
        <div class="stat-card">
          <h3>{{ dashboard.pendingCases }}</h3>
          <p>Pending Cases</p>
        </div>
        <div class="stat-card">
          <h3>{{ dashboard.renewalsDue }}</h3>
          <p>Renewals Due</p>
        </div>
        <div class="stat-card">
          <h3>{{ dashboard.totalClients }}</h3>
          <p>Total Clients</p>
        </div>
      </div>

      <div class="content-grid">
        <div class="card" *ngIf="dashboard">
          <h2>Upcoming Deadlines</h2>
          <div class="deadline-list">
            <div class="deadline-item" *ngFor="let deadline of dashboard.upcomingDeadlines">
              <div class="deadline-type">{{ deadline.type }}</div>
              <div class="deadline-info">
                <strong>{{ deadline.title }}</strong>
                <small>{{ deadline.referenceNumber }}</small>
              </div>
              <div class="deadline-date">{{ deadline.date | date:'MMM dd, yyyy' }}</div>
            </div>
            <p *ngIf="dashboard.upcomingDeadlines.length === 0">No upcoming deadlines</p>
          </div>
        </div>

        <div class="card" *ngIf="dashboard">
          <h2>Recent Activities</h2>
          <div class="activity-list">
            <div class="activity-item" *ngFor="let activity of dashboard.recentActivities">
              <div class="activity-type">{{ activity.type }}</div>
              <div class="activity-info">
                <strong>{{ activity.title }}</strong>
                <small>{{ activity.createdAt | date:'MMM dd, yyyy' }}</small>
              </div>
            </div>
            <p *ngIf="dashboard.recentActivities.length === 0">No recent activities</p>
          </div>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .container {
      padding: 2rem;
    }
    h1 {
      color: #333;
      margin-bottom: 2rem;
    }
    .stats-grid {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
      gap: 1.5rem;
      margin-bottom: 2rem;
    }
    .stat-card {
      background: white;
      padding: 1.5rem;
      border-radius: 8px;
      box-shadow: 0 2px 4px rgba(0,0,0,0.1);
      text-align: center;
    }
    .stat-card h3 {
      margin: 0;
      font-size: 2.5rem;
      color: #3f51b5;
    }
    .stat-card p {
      margin: 0.5rem 0 0 0;
      color: #666;
    }
    .content-grid {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(400px, 1fr));
      gap: 2rem;
    }
    .card {
      background: white;
      padding: 1.5rem;
      border-radius: 8px;
      box-shadow: 0 2px 4px rgba(0,0,0,0.1);
    }
    .card h2 {
      margin: 0 0 1rem 0;
      color: #333;
      font-size: 1.25rem;
    }
    .deadline-list, .activity-list {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }
    .deadline-item, .activity-item {
      display: flex;
      align-items: center;
      gap: 1rem;
      padding: 1rem;
      background: #f5f5f5;
      border-radius: 4px;
    }
    .deadline-type, .activity-type {
      padding: 0.25rem 0.75rem;
      background: #3f51b5;
      color: white;
      border-radius: 12px;
      font-size: 0.75rem;
      font-weight: 500;
      white-space: nowrap;
    }
    .deadline-info, .activity-info {
      flex: 1;
      display: flex;
      flex-direction: column;
      gap: 0.25rem;
    }
    .deadline-info strong, .activity-info strong {
      color: #333;
    }
    .deadline-info small, .activity-info small {
      color: #666;
      font-size: 0.875rem;
    }
    .deadline-date {
      color: #f44336;
      font-weight: 500;
      white-space: nowrap;
    }
  `]
})
export class DashboardComponent implements OnInit {
  private dashboardService = inject(DashboardService);
  dashboard: Dashboard | null = null;

  ngOnInit(): void {
    this.loadDashboard();
  }

  loadDashboard(): void {
    this.dashboardService.getDashboardData().subscribe({
      next: (data) => {
        this.dashboard = data;
      },
      error: (err) => {
        console.error('Error loading dashboard:', err);
      }
    });
  }
}
