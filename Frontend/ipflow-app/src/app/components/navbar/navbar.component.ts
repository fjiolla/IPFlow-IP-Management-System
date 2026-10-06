import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <nav class="navbar">
      <div class="navbar-brand">
        <h1>IPFlow</h1>
      </div>
      <div class="navbar-links">
        <a routerLink="/dashboard" routerLinkActive="active">Dashboard</a>
        <a routerLink="/patents" routerLinkActive="active">Patents</a>
        <a routerLink="/trademarks" routerLinkActive="active">Trademarks</a>
        <a routerLink="/cases" routerLinkActive="active">Cases</a>
        <a routerLink="/clients" routerLinkActive="active">Clients</a>
      </div>
      <div class="navbar-user">
        <span *ngIf="authService.currentUser()">{{ authService.currentUser()?.name }}</span>
        <button (click)="logout()">Logout</button>
      </div>
    </nav>
  `,
  styles: [`
    .navbar {
      display: flex;
      align-items: center;
      padding: 1rem 2rem;
      background-color: #3f51b5;
      color: white;
      box-shadow: 0 2px 4px rgba(0,0,0,0.1);
    }
    .navbar-brand h1 {
      margin: 0;
      font-size: 1.5rem;
    }
    .navbar-links {
      display: flex;
      gap: 1rem;
      margin-left: 3rem;
      flex: 1;
    }
    .navbar-links a {
      color: white;
      text-decoration: none;
      padding: 0.5rem 1rem;
      border-radius: 4px;
      transition: background-color 0.3s;
    }
    .navbar-links a:hover {
      background-color: rgba(255,255,255,0.1);
    }
    .navbar-links a.active {
      background-color: rgba(255,255,255,0.2);
    }
    .navbar-user {
      display: flex;
      align-items: center;
      gap: 1rem;
    }
    .navbar-user button {
      padding: 0.5rem 1rem;
      background-color: white;
      color: #3f51b5;
      border: none;
      border-radius: 4px;
      cursor: pointer;
      font-weight: 500;
    }
    .navbar-user button:hover {
      background-color: #f0f0f0;
    }
  `]
})
export class NavbarComponent {
  authService = inject(AuthService);

  logout(): void {
    this.authService.logout();
  }
}
