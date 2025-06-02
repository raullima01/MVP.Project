// src/app/layouts/main-layout/main-layout.component.ts
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../core/services/auth.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink],
  template: `
    <header *ngIf="isAuthenticated" class="app-header">
      <div class="container">
        <div class="logo">MVP Project</div>
        <nav class="nav-menu">
          <a routerLink="/customers" class="nav-item">Customers</a>
          <button (click)="logout()" class="btn-logout">Logout</button>
        </nav>
      </div>
    </header>
    
    <main class="app-content">
      <ng-content></ng-content>
    </main>
    
    <footer class="app-footer">
      <div class="container">
        <p>&copy; 2025 MVP Project. All rights reserved.</p>
      </div>
    </footer>
  `,
  styles: [`
    .app-header {
      background-color: #343a40;
      color: white;
      padding: 1rem 0;
    }
    
    .container {
      width: 100%;
      max-width: 1200px;
      margin: 0 auto;
      padding: 0 15px;
      display: flex;
      justify-content: space-between;
      align-items: center;
    }
    
    .logo {
      font-size: 1.5rem;
      font-weight: bold;
    }
    
    .nav-menu {
      display: flex;
      align-items: center;
    }
    
    .nav-item {
      color: rgba(255,255,255,0.8);
      text-decoration: none;
      margin-right: 1rem;
      padding: 0.5rem;
    }
    
    .nav-item:hover {
      color: white;
    }
    
    .btn-logout {
      background-color: transparent;
      border: 1px solid white;
      color: white;
      padding: 0.5rem 1rem;
      border-radius: 4px;
      cursor: pointer;
    }
    
    .btn-logout:hover {
      background-color: rgba(255,255,255,0.1);
    }
    
    .app-content {
      padding: 20px;
      max-width: 1200px;
      margin: 0 auto;
      min-height: calc(100vh - 120px); /* Altura da viewport menos header e footer */
    }
    
    .app-footer {
      background-color: #f8f9fa;
      padding: 1rem 0;
      border-top: 1px solid #e9ecef;
      text-align: center;
      color: #6c757d;
    }
  `]
})
export class MainLayoutComponent {
  isAuthenticated = false;
  
  constructor(private authService: AuthService) {}
  
  ngOnInit(): void {
    this.authService.isAuthenticated$.subscribe(
      isAuth => this.isAuthenticated = isAuth
    );
  }
  
  logout(): void {
    this.authService.logout();
  }
}