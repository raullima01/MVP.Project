// src/app/features/auth/login/login.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { LoginUser } from '../../core/models';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  template: `
    <div class="login-container">
      <div class="login-card">
        <h2 class="login-title">Login</h2>
        
        <form [formGroup]="loginForm" (ngSubmit)="onSubmit()">
          <div class="form-group">
            <label for="email">Email</label>
            <input 
              type="email" 
              id="email" 
              formControlName="email" 
              class="form-control"
              [class.is-invalid]="submitted && f.email.errors"
            >
            <div *ngIf="submitted && f.email.errors" class="invalid-feedback">
              <div *ngIf="f.email.errors.required">Email is required</div>
              <div *ngIf="f.email.errors.email">Email must be a valid email address</div>
            </div>
          </div>
          
          <div class="form-group">
            <label for="password">Password</label>
            <input 
              type="password" 
              id="password" 
              formControlName="password" 
              class="form-control"
              [class.is-invalid]="submitted && f.password.errors"
            >
            <div *ngIf="submitted && f.password.errors" class="invalid-feedback">
              <div *ngIf="f.password.errors.required">Password is required</div>
            </div>
          </div>
          
          <div *ngIf="error" class="alert alert-danger">{{error}}</div>
          
          <div class="form-group">
            <button 
              type="submit" 
              class="btn btn-primary" 
              [disabled]="loading"
            >
              <span *ngIf="loading" class="spinner-border spinner-border-sm mr-1"></span>
              Login
            </button>
            <div class="register-link">
              Don't have an account? <a routerLink="/register">Register</a>
            </div>
          </div>
        </form>
      </div>
    </div>
  `,
  styles: [`
    .login-container {
      display: flex;
      justify-content: center;
      align-items: center;
      min-height: 100vh;
      background-color: #f5f5f5;
    }
    
    .login-card {
      width: 100%;
      max-width: 400px;
      padding: 20px;
      background: white;
      border-radius: 8px;
      box-shadow: 0 4px 6px rgba(0,0,0,0.1);
    }
    
    .login-title {
      text-align: center;
      margin-bottom: 20px;
      color: #333;
    }
    
    .form-group {
      margin-bottom: 15px;
    }
    
    label {
      display: block;
      margin-bottom: 5px;
      font-weight: 500;
    }
    
    .form-control {
      width: 100%;
      padding: 10px;
      border: 1px solid #ddd;
      border-radius: 4px;
      font-size: 16px;
    }
    
    .is-invalid {
      border-color: #dc3545;
    }
    
    .invalid-feedback {
      color: #dc3545;
      font-size: 14px;
      margin-top: 5px;
    }
    
    .btn {
      display: inline-block;
      font-weight: 400;
      text-align: center;
      white-space: nowrap;
      vertical-align: middle;
      user-select: none;
      border: 1px solid transparent;
      padding: 0.375rem 0.75rem;
      font-size: 1rem;
      line-height: 1.5;
      border-radius: 0.25rem;
      transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
      cursor: pointer;
      width: 100%;
    }
    
    .btn-primary {
      color: #fff;
      background-color: #007bff;
      border-color: #007bff;
    }
    
    .btn-primary:disabled {
      opacity: 0.65;
      cursor: not-allowed;
    }
    
    .alert {
      position: relative;
      padding: 0.75rem 1.25rem;
      margin-bottom: 1rem;
      border: 1px solid transparent;
      border-radius: 0.25rem;
    }
    
    .alert-danger {
      color: #721c24;
      background-color: #f8d7da;
      border-color: #f5c6cb;
    }
    
    .register-link {
      text-align: center;
      margin-top: 15px;
    }
    
    .spinner-border {
      display: inline-block;
      width: 1rem;
      height: 1rem;
      vertical-align: text-bottom;
      border: 0.2em solid currentColor;
      border-right-color: transparent;
      border-radius: 50%;
      animation: spinner-border .75s linear infinite;
    }
    
    @keyframes spinner-border {
      to { transform: rotate(360deg); }
    }
    
    .mr-1 {
      margin-right: 0.25rem;
    }
  `]
})
export class LoginComponent {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  error = '';
  returnUrl: string = '/customers';
  
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {
    // Redirecionar para a página inicial se já estiver logado
    if (this.authService.getToken()) {
      this.router.navigate(['/customers']);
    }
    
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
    
    // Obter a URL de retorno dos parâmetros da rota ou usar o padrão
    this.route.queryParams.subscribe(params => {
      this.returnUrl = params['returnUrl'] || '/customers';
    });
  }
  
  // Getter para facilitar o acesso aos campos do formulário
  get f() { return this.loginForm.controls; }
  
  onSubmit() {
    this.submitted = true;
    
    // Para se o formulário for inválido
    if (this.loginForm.invalid) {
      return;
    }
    
    this.loading = true;
    this.error = '';
    
    const loginData: LoginUser = {
      email: this.f.email.value,
      password: this.f.password.value
    };
    
    this.authService.login(loginData).subscribe({
      next: () => {
        this.router.navigate([this.returnUrl]);
      },
      error: error => {
        this.error = error.message || 'Login failed';
        this.loading = false;
      }
    });
  }
}