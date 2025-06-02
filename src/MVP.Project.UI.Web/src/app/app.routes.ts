import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'customers',
    pathMatch: 'full'
  },
  {
    path: 'login',
    loadComponent: () => import('./features/auth/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'register',
    loadComponent: () => import('./features/auth/register.component').then(m => m.RegisterComponent)
  },
  {
    path: 'customers',
    canActivate: [authGuard],
    loadChildren: () => import('./features/customers/customer.routes').then(r => r.CUSTOMER_ROUTES)
  },
  {
    path: '**',
    redirectTo: 'customers'
  }
];