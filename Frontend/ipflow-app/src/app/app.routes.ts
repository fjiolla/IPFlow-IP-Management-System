import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { 
    path: 'login', 
    loadComponent: () => import('./components/login/login.component').then(m => m.LoginComponent)
  },
  { 
    path: 'dashboard', 
    loadComponent: () => import('./components/dashboard/dashboard.component').then(m => m.DashboardComponent),
    canActivate: [authGuard]
  },
  { 
    path: 'patents', 
    loadComponent: () => import('./components/patents/patents.component').then(m => m.PatentsComponent),
    canActivate: [authGuard]
  },
  { 
    path: 'trademarks', 
    loadComponent: () => import('./components/trademarks/trademarks.component').then(m => m.TrademarksComponent),
    canActivate: [authGuard]
  },
  { 
    path: 'cases', 
    loadComponent: () => import('./components/cases/cases.component').then(m => m.CasesComponent),
    canActivate: [authGuard]
  },
  { 
    path: 'clients', 
    loadComponent: () => import('./components/clients/clients.component').then(m => m.ClientsComponent),
    canActivate: [authGuard]
  },
  { path: '**', redirectTo: '/dashboard' }
];
